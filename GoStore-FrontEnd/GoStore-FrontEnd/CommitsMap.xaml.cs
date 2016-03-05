using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoStore_FrontEnd
{
    /// <summary>
    /// CommitsMap.xaml 的交互逻辑
    /// </summary>
    public partial class CommitsMap : UserControl
    {
        // Structures:

        public class TimeNode : IComparable
        {
            public TimeNode()
            {
                parentNodes = new List<TimeNode>();

                _track = 0xffffffff;
            }

            public int CompareTo(object obj)
            {
                TimeNode node = obj as TimeNode;

                return -time.CompareTo(node.time);
            }
            
            public string           name;
            public string           text;
            public DateTime         time;
            public List<TimeNode>   parentNodes;

            public bool             _drawn;
            public uint             _track;
        };


        // Methods:

        // Constructor
        public CommitsMap()
        {
            InitializeComponent();

            canvas.Width = 0;
            canvas.Height = 0;

            _tracks = 0;

            _nodes = new List<TimeNode>();
        }
        
        public void AddNode(TimeNode timeNode)
        {
            _nodes.Add(timeNode);
            timeNode._track = _tracks++;

        }

        public void AddNode(TimeNode timeNode, TimeNode upperNode)
        {
            upperNode.parentNodes.Add(timeNode);
            timeNode._track = upperNode._track;
        }

        public void AddNode(TimeNode timeNode, List<TimeNode>upperNodes)
        {
            int countNodes = upperNodes.Count;
            timeNode._track = 0;

            for(int i = 0; i < countNodes; ++i)
            {
                upperNodes[i].parentNodes.Add(timeNode);

                if (timeNode._track < upperNodes[i]._track)
                    timeNode._track = upperNodes[i]._track;
            }
        }

        public bool RemoveNode(uint index)
        {
            return true;
        }

        public void ClearNodes()
        {
            _nodes.Clear();
        }

        public void Draw(int maxDepth)
        {            
            _nodes.Sort();          // DESC sort.

            //Clean up temp resources.
            canvas.Children.Clear();
            canvas.Width = 0;
            canvas.Height = 0;

            // Correct the max depth.
            if (_nodes.Count < maxDepth)
                maxDepth = (int)_nodes.Count;

            // Rendering loop.
            RadioButton rbtn;
            Thickness margin = new Thickness();

            for(int i = 0; i < maxDepth; i++)
            {
                rbtn = new RadioButton();

                margin.Left = _nodes[i]._track * 50;
                margin.Top = i * 80;

                rbtn.Margin = margin;

                canvas.Children.Add(rbtn);
            }

            _depth = maxDepth;
        }

        public void DrawContinue(uint maxDepth)
        {
            
        }
        
        // Properties:
        List<TimeNode>      _nodes;
        uint                _tracks;
        int                 _depth;
    }
}
