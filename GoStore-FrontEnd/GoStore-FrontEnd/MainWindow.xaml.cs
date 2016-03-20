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
using System.Windows.Media.Animation;
using LibGit2Sharp;
using Microsoft.Win32;

namespace GoStore_FrontEnd
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        CommitsMap_Manager _cmapManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void frm_TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void winMain_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btn_newRepo_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_newRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_newRepo_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_newRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_openRepo_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_openRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_openRepo_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_openRepo.Background = new SolidColorBrush(clr);
        }

        private void btn_setting_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 218;
            clr.G = 114;
            clr.B = 90;

            btn_setting.Background = new SolidColorBrush(clr);
        }

        private void btn_setting_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_setting.Background = new SolidColorBrush(clr);
        }

        private void btn_max_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 37;
            clr.G = 116;
            clr.B = 210;

            btn_max.Background = new SolidColorBrush(clr);
        }

        private void btn_max_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_max.Background = new SolidColorBrush(clr);
        }

        private void btn_close_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 255;
            clr.G = 100;
            clr.B = 50;

            btn_close.Background = new SolidColorBrush(clr);
        }

        private void btn_close_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_close.Background = new SolidColorBrush(clr);
        }

        private void btn_min_MouseEnter(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 37;
            clr.G = 116;
            clr.B = 210;

            btn_min.Background = new SolidColorBrush(clr);
        }

        private void btn_min_MouseLeave(object sender, MouseEventArgs e)
        {
            Color clr = new Color();
            clr.A = 255;
            clr.R = 210;
            clr.G = 70;
            clr.B = 37;

            btn_min.Background = new SolidColorBrush(clr);
        }

        private void btn_max_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                btn_max.Content = "↓";
            }
            else if(this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                btn_max.Content = "↑";
            }
        }

        private void btn_min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btn_newRepo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            fd.ShowNewFolderButton = true;
            fd.Description = "Select a folder that you want to make it as a repository.";            
            
            string path;

            try
            {
                if (System.Windows.Forms.DialogResult.OK == fd.ShowDialog())
                {
                    path = fd.SelectedPath;

                    Repository.Init(path);

                    _cmapManager = new CommitsMap_Manager(cm1, path);
                    _cmapManager.Load();

                    cm1.Draw(500);

                    SuccessWindow_CreateRepo succWin = new SuccessWindow_CreateRepo();
                    succWin.ShowDialog();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error occurs. Cannot create a new repoistory", "Warning");
            }

        }

        private void btn_openRepo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();
            fd.ShowNewFolderButton = false;
            fd.Description = "Select repoistory's directory";

            string path;

            try
            {
                if (System.Windows.Forms.DialogResult.OK == fd.ShowDialog())
                {
                    path = fd.SelectedPath;

                    _cmapManager = new CommitsMap_Manager(cm1, path);
                    _cmapManager.Load();

                    cm1.Draw(500);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs. Cannot open the repository", "Warning");
            }
        }

        private void btn_setting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Setting winSetting = new Setting();

            winSetting.ShowDialog();
        }    
    }
}
