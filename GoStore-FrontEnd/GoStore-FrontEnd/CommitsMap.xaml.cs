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

        public struct TimeNode
        {
            public string           text;
            public string           name;
            public List<TimeNode>   parentNodes;
        };

        // Methods:

        public CommitsMap()
        {
            InitializeComponent();

            canvas.Width = 0;
            canvas.Height = 0;

            startNodes = new List<TimeNode>();
        }

        public void AddNode(TimeNode timeNode, TimeNode curNode)
        {
            curNode.parentNodes.Add(timeNode);
            startNodes.Add(timeNode);
        }

        public void AddNode(TimeNode timeNode)
        {
            startNodes.Add(timeNode);
        }

        public void ReDraw(uint maxDepth)
        {
            canvas.Children.Clear();
            canvas.Width = 0;
            canvas.Height = 0;

            uint depth = 0;

            
            foreach(TimeNode begNodeOfOneLine in startNodes)
            {
                while(depth <= maxDepth)
                {

                }
            }
        }
        
        // Properties:
        List<TimeNode> startNodes;
    }
}
