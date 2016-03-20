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
    /// SuccessWindow_CreateRepo.xaml 的交互逻辑
    /// </summary>
    public partial class SuccessWindow_CreateRepo : Window
    {
        public SuccessWindow_CreateRepo()
        {
            InitializeComponent();
        }

        private void btn_okay_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 211;
            clr.G = 114;
            clr.B = 91;

            btn_okay.Background = new SolidColorBrush(clr);
        }

        private void btn_okay_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 224;
            clr.G = 64;
            clr.B = 27;

            btn_okay.Background = new SolidColorBrush(clr);
        }

        private void btn_okay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void frm_main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
