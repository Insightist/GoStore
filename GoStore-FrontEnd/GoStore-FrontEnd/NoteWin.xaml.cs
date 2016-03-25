﻿using System;
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
    /// NoteWin.xaml 的交互逻辑
    /// </summary>
    public partial class NoteWin : Window
    {
        #region Properties
        // Properties
        public string notes;

        #endregion

        public NoteWin()
        {
            InitializeComponent();

            notes = String.Empty;
        }
        
        private void noteWin_Loaded(object sender, RoutedEventArgs e)
        {
            txt_notes.Text = notes;
        }


        private void btn_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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

        private void frm_titlebar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void lbl_caption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
