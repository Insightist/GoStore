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

        public class TimeNode
        {
            public TimeNode()
            {
                parentNodes = new List<TimeNode>();
                drawn = false;
            }
            
            public uint             index;
            public string           name;
            public string           text;
            public DateTime         time;
            public bool             drawn;
            public List<TimeNode>   parentNodes;
        };

        public class Track
        {
            public Track()
            {

            }



            List<TimeNode> _nodes;
        };

        // Methods:

        public CommitsMap()
        {
            InitializeComponent();

            canvas.Width = 0;
            canvas.Height = 0;

            _topIndex = 0;

        }
        
        public void AddNode(TimeNode timeNode)
        {
            timeNode.index = _topIndex++;
            timeNode.drawn = false;

        }

        public bool RemoveNode(uint index)
        {
            return true;
        }

        public void ClearNodes()
        {
            _topIndex = 0;
        }

        public void ReDraw(uint maxDepth)
        {
            canvas.Children.Clear();
            canvas.Width = 0;
            canvas.Height = 0;

        }
        
        // Properties:
        List<Track>     _tracks;
        uint            _topIndex;
    }
}
