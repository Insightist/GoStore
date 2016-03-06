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
            btn_test_cm_Click();
        }

        private void btn_test_cm_Click()//object sender, RoutedEventArgs e)
        {
            CommitsMap.TimeNode[] nodes = new CommitsMap.TimeNode[14];
            List<CommitsMap.TimeNode> tempList;

            for (int i = 0; i < nodes.Length; ++i )
            {
                nodes[i] = new CommitsMap.TimeNode();
                nodes[i].name = "#" + i;
                nodes[i].time = new DateTime(2015, 3, 20 - i);
            }


            cm1.ClearNodes();

            cm1.AddNode(nodes[0]);
            cm1.AddNode(nodes[1], nodes[0]);
            cm1.AddNode(nodes[2], nodes[1]);
            cm1.AddNode(nodes[3], nodes[1]);
            cm1.AddNode(nodes[4], nodes[3]);
            cm1.AddNode(nodes[5]);
            cm1.AddNode(nodes[6], nodes[5]);

            tempList = new List<CommitsMap.TimeNode>();
            tempList.Add(nodes[4]);
            tempList.Add(nodes[6]);

            cm1.AddNode(nodes[7], tempList);
            cm1.AddNode(nodes[8], nodes[7]);
            cm1.AddNode(nodes[9], nodes[2]);
            cm1.AddNode(nodes[10], nodes[8]);
            cm1.AddNode(nodes[11], nodes[8]);

            tempList = new List<CommitsMap.TimeNode>();
            tempList.Add(nodes[9]);
            tempList.Add(nodes[10]);

            cm1.AddNode(nodes[12], tempList);

            tempList = new List<CommitsMap.TimeNode>();
            tempList.Add(nodes[11]);
            tempList.Add(nodes[12]);

            cm1.AddNode(nodes[13], tempList);

            cm1.Draw(50);
        }
    }
}
