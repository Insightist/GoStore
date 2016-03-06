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
            timeNode._track = _tracks++;
            _nodes.Add(timeNode);
        }

        public void AddNode(TimeNode timeNode, TimeNode upperNode)
        {
            timeNode._track = upperNode._track + (uint)upperNode.parentNodes.Count;
            upperNode.parentNodes.Add(timeNode);
            _nodes.Add(timeNode);

            if (timeNode._track >= _tracks)
                _tracks = timeNode._track + 1;
        }

        public void AddNode(TimeNode timeNode, List<TimeNode>upperNodes)
        {
            int countNodes = upperNodes.Count;
            timeNode._track = upperNodes[0]._track;

            for(int i = 0; i < countNodes; ++i)
            {
                upperNodes[i].parentNodes.Add(timeNode);

                if (timeNode._track > upperNodes[i]._track)
                    timeNode._track = upperNodes[i]._track;
            }

            _nodes.Add(timeNode);

            _tracks -= (uint)upperNodes.Count - 1;
        }

        public bool RemoveNode(uint index)
        {
            return true;
        }

        public void ClearNodes()
        {
            _tracks = 0;
            _nodes.Clear();
        }

        public void Draw(int maxDepth)
        {            
            _nodes.Sort();          // DESC sort.

            //Clean up temp resources.
            canvas.Children.Clear();
            canvas.Width = 80;
            canvas.Height = 0;

            // Correct the max depth.
            if (_nodes.Count < maxDepth)
                maxDepth = _nodes.Count;

            // Rendering loop.
            RadioButton         rbtn;
            Thickness           margin = new Thickness();
            LineGeometry        line;
            Path                path;
            uint                dist;

            for(int i = 0; i < maxDepth; i++)
            {
                // Draw connecting line.
                if (i != maxDepth - 1)      // Make sure that current node is not the last one.
                {
                    foreach(TimeNode parNode in _nodes[i].parentNodes)
                    {
                        dist = 0;
                        foreach(TimeNode destNode in _nodes)
                        {
                            if (destNode == parNode)
                                break;
                            ++dist;
                        }
                        dist -= (uint)i;

                        line = new LineGeometry();
                        line.StartPoint = new Point(_nodes[i]._track * _SINGLE_HORIZONAL_OFFSET + _LINE_POINT_X_OFFSET,
                            i * _SINGLE_VERTICAL_OFFSET + _LINE_POINT_Y_OFFSET);
                        line.EndPoint = new Point(parNode._track * _SINGLE_HORIZONAL_OFFSET + _LINE_POINT_X_OFFSET,
                            i * _SINGLE_VERTICAL_OFFSET + dist * _SINGLE_VERTICAL_OFFSET + _LINE_POINT_Y_OFFSET);

                        path = new Path();
                        path.Data = line;
                        path.StrokeThickness = _LINE_THICKNESS;

                        // Decide color of the line
                        if(parNode._track > _nodes[i]._track)
                            path.Stroke = IndexedColor((int)parNode._track);
                        else
                            path.Stroke = IndexedColor((int)_nodes[i]._track);


                        canvas.Children.Add(path);
                    }
                }
                 

                // Draw time node.
                rbtn = new RadioButton();

                margin.Left = _nodes[i]._track * _SINGLE_HORIZONAL_OFFSET;
                margin.Top = i * _SINGLE_VERTICAL_OFFSET;

                rbtn.Margin = margin;
                rbtn.Content = _nodes[i].name;
                rbtn.Foreground = Brushes.LightGray;

                canvas.Children.Add(rbtn);
                canvas.Height += _SINGLE_VERTICAL_OFFSET;
                if (margin.Left >= canvas.Width + _SINGLE_HORIZONAL_OFFSET)
                    canvas.Width = margin.Left + _SINGLE_HORIZONAL_OFFSET;
            }

            _depth = maxDepth;
        }

        public void DrawContinue(uint maxDepth)
        {
            
        }
        
        public SolidColorBrush IndexedColor(int index)
        {
            Color clr = new Color();
            clr.A = 255;

            switch(index%8)
            {
                case 1:
                    // Light Blue
                    clr.R = 62;
                    clr.G = 192;
                    clr.B = 255;
                    break;

                case 2:
                    // Yellow
                    clr.R = 255;
                    clr.G = 224;
                    clr.B = 9;
                    break;

                case 3:
                    // Orange
                    clr.R = 254;
                    clr.G = 139;
                    clr.B = 63;
                    break;

                case 4: 
                    // Red
                    clr.R = 255;
                    clr.G = 62;
                    clr.B = 62;
                    break;

                case 5:
                    // Purple
                    clr.R = 197;
                    clr.G = 62;
                    clr.B = 255;
                    break;

                case 6:
                    // Pink
                    clr.R = 252;
                    clr.G = 65;
                    clr.B = 215;
                    break;

                case 7:
                    // Deep Blue
                    clr.R = 9;
                    clr.G = 83;
                    clr.B = 255;
                    break;

                default:
                    // Light Green
                    clr.R = 69;
                    clr.G = 248;
                    clr.B = 87;
                    break;
            }

            return new SolidColorBrush(clr);
        }


        // Properties:
        List<TimeNode>      _nodes;
        uint                _tracks;
        int                 _depth;

        const double _SINGLE_HORIZONAL_OFFSET = 50.0f;
        const double _SINGLE_VERTICAL_OFFSET = 30.0f;
        const double _LINE_POINT_X_OFFSET = 6.0f;
        const double _LINE_POINT_Y_OFFSET = 8.0f;
        const double _LINE_THICKNESS = 5.0f;
    }
}
