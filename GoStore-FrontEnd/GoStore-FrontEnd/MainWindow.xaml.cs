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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_test_cm_Click(object sender, RoutedEventArgs e)
        {
            CommitsMap.TimeNode[] nodes = new CommitsMap.TimeNode[10];

            for (int i = 0; i < nodes.Length; ++i )
            {
                nodes[i] = new CommitsMap.TimeNode();
                nodes[i].name = "#" + i;
            }

            nodes[0].time = new DateTime(2016, 3, 1);
            nodes[1].time = new DateTime(2016, 2, 1);
            nodes[2].time = new DateTime(2016, 5, 1);
            nodes[3].time = new DateTime(2016, 3, 2);
            nodes[4].time = new DateTime(2016, 3, 9);
            nodes[5].time = new DateTime(2016, 4, 1);
            nodes[6].time = new DateTime(2016, 4, 8);
            nodes[7].time = new DateTime(2016, 2, 3);
            nodes[8].time = new DateTime(2016, 2, 29);

            nodes[0].parentNodes.Add(nodes[1]);
            nodes[1].parentNodes.Add(nodes[2]);
            nodes[2].parentNodes.Add(nodes[3]);
            nodes[3].parentNodes.Add(nodes[6]);
            nodes[6].parentNodes.Add(nodes[7]);
            nodes[7].parentNodes.Add(nodes[8]);
            nodes[8].parentNodes.Add(nodes[9]);

            nodes[3].parentNodes.Add(nodes[4]);
            nodes[4].parentNodes.Add(nodes[5]);
            nodes[5].parentNodes.Add(nodes[6]);



            cm1.ClearNodes();

            foreach(CommitsMap.TimeNode node in nodes)
            {
                cm1.AddNode(node);
            }

            cm1.Draw(50);
        }
    }
}
