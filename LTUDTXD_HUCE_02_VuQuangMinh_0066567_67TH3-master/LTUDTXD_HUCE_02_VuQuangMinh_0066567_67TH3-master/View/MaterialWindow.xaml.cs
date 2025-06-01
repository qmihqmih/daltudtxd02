﻿using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel;
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

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.View
{
    /// <summary>
    /// Interaction logic for MaterialWindow.xaml
    /// </summary>
    public partial class MaterialWindow : Window
    {
        public MaterialWindow()
        {
            InitializeComponent();
            this.DataContext = new VatlieuViewModel(); // Gán ViewModel
            this.DataContext = new Vatlieu2ViewModel();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Window.GetWindow(this)?.Close();
        }
    }
}
