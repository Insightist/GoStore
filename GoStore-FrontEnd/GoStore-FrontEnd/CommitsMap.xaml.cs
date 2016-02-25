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

        // Methods:

        public CommitsMap()
        {
            InitializeComponent();

            canvas.Width = 0;
            canvas.Height = 0;

            _topIndex = 0;

            _nodes = new List<TimeNode>();
            _startNodeIndecies = new List<uint>();
        }
        
        public void AddNode(TimeNode timeNode)
        {
            timeNode.index = _topIndex++;
            timeNode.drawn = false;

            _nodes.Add(timeNode);
        }

        public bool RemoveNode(uint index)
        {
            return true;
        }

        public void ClearNodes()
        {
            _nodes.Clear();
            _startNodeIndecies.Clear();
            _topIndex = 0;
        }

        public void ReDraw(uint maxDepth)
        {
            canvas.Children.Clear();
            canvas.Width = 0;
            canvas.Height = 0;

            foreach(TimeNode eachnode in _nodes)
            {
                eachnode.drawn = false;
            }

            uint depth = 0;

            Stack<TimeNode> undrawNodeStack = new Stack<TimeNode>();
            Stack<uint> levels = new Stack<uint>();
            TimeNode currNode;
            RadioButton radioBtn;
            Thickness margin = new Thickness();
            foreach (int startNodeIdx in _startNodeIndecies)
            {
                // Point to the start node, and get ready to start drawing loop
                currNode = _nodes[startNodeIdx];
                depth = 0;

                // Drawing loop. The tree depth can't exceed maxDepth.
                while(depth <= maxDepth)
                {
                    // Draw current node.

                    margin.Top = (depth) * 50;

                    if(canvas.Height < (depth+1) * 50)
                    {
                        canvas.Height = (depth+1) * 50;
                    }


                    radioBtn = new RadioButton();
                    radioBtn.Content = currNode.name;
                    radioBtn.Margin = margin;

                    canvas.Children.Add(radioBtn);


                    // Judge the loop boundary.
                    if(currNode.parentNodes.Count == 0 || currNode.drawn == true)
                    {
                        if (undrawNodeStack.Count == 0)  // Nothing to draw. Finished.
                        {
                            // Finished drawing.
                            currNode.drawn = true;

                            break;
                        }
                        else
                        {
                            // Finished drawing.
                            currNode.drawn = true;

                            // Draw remaining forks.
                            currNode = undrawNodeStack.Pop();
                            depth = levels.Pop();
                            margin.Left += 50;
                            
                            if(canvas.Width < margin.Left)
                            {
                                canvas.Width = margin.Left;
                            }
                            continue;
                        }
                    }
                    // Save the forks.
                    else if(currNode.parentNodes.Count > 1)
                    {
                        for (int i = 1; i < currNode.parentNodes.Count; ++i)
                        {
                            if (currNode.parentNodes[i].drawn == false)
                            {
                                undrawNodeStack.Push(currNode.parentNodes[i]);
                                levels.Push(depth + 1);
                            }
                        }
                    }

                    // Point to the left parent node, continue the loop.
                    currNode.drawn = true;
                    currNode = currNode.parentNodes[0];
                    ++depth;
                }

                margin.Left += 50;
                if (canvas.Width < margin.Left)
                {
                    canvas.Width = margin.Left;
                }
            }
        }

        public void AddStartNodeIndex(uint index)
        {
            _startNodeIndecies.Add(index);
        }
        
        // Properties:
        List<uint>      _startNodeIndecies;
        List<TimeNode>  _nodes;
        uint            _topIndex;
    }
}
