using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel;
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
    /// Interaction logic for tinhtoanground.xaml
    /// </summary>
    public partial class tinhtoanground : Window
    {
        public tinhtoanground()
        {
            InitializeComponent();
            this.DataContext = new TinhToanGroundViewModel();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Window.GetWindow(this)?.Close();
        }

        private void btnchonphuongancoc_Click(object sender, RoutedEventArgs e)
        {
            phuongancoc phuongancoc = new phuongancoc();
            phuongancoc.ShowDialog();
        }
    }
}
