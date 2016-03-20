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
using System.Windows.Shapes;

namespace GoStore_FrontEnd
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void btn_close_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_close.Background = new SolidColorBrush(clr);
            btn_close.Foreground = Brushes.White;
        }

        private void btn_close_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 255;
            clr.G = 255;
            clr.B = 255;

            btn_close.Background = new SolidColorBrush(clr);

            clr.A = 255;
            clr.R = 97;
            clr.G = 97;
            clr.B = 97;
            btn_close.Foreground = new SolidColorBrush(clr);

        }

        private void frm_itembar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
