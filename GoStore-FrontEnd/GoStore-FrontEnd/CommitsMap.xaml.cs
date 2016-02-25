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
            }
            
            public string           name;
            public string           text;
            public DateTime         time;
            public List<TimeNode>   parentNodes;
            public bool             _drawn;
        };


        // Methods:

        public CommitsMap()
        {
            InitializeComponent();

            canvas.Width = 0;
            canvas.Height = 0;


        }
        
        public void AddNode(TimeNode timeNode)
        {
        }

        public bool RemoveNode(uint index)
        {
            return true;
        }

        public void ClearNodes()
        {
        }

        public void Draw(uint maxDepth)
        {
            canvas.Children.Clear();
            canvas.Width = 0;
            canvas.Height = 0;

        }
        
        // Properties:
        List<TimeNode>      _nodes;
    }
}
