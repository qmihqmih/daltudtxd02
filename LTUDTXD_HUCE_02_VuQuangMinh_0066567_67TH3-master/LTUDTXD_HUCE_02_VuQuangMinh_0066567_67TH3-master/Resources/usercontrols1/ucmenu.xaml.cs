using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Resources.usercontrols1
{
    /// <summary>
    /// Interaction logic for ucmenu.xaml
    /// </summary>
    public partial class ucmenu : UserControl
    {
        public ucmenu()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void btnsucchiutaicoc_Click(object sender, RoutedEventArgs e)
        {
            succhiutaicoc sct = new succhiutaicoc();
            sct.ShowDialog();
        }

        private void btntinhtoanground_Click(object sender, RoutedEventArgs e)
        {
            tinhtoanground datnen = new tinhtoanground();
            datnen.ShowDialog();
        }

        private void btncoc_Click(object sender, RoutedEventArgs e)
        {
            coc coc = new coc();
            coc.ShowDialog();
        }

        private void btndaicoc_Click(object sender, RoutedEventArgs e)
        {
            daicoc daicoc = new daicoc();
            daicoc.ShowDialog();
        }

        private void btnvatlieu_Click(object sender, RoutedEventArgs e)
        {
            MaterialWindow vatlieu = new MaterialWindow();
            vatlieu.ShowDialog();
        }

        private void btnground_Click(object sender, RoutedEventArgs e)
        {
            ground datnen = new ground();
            datnen.ShowDialog();
        }

        private void btntaitrong_Click(object sender, RoutedEventArgs e)
        {
            Taitrong taitrong = new Taitrong();
            taitrong.ShowDialog();
        }

        private void Ribbon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void nendat_Click(object sender, RoutedEventArgs e)
        {
            xuatnendat xuatnendat = new xuatnendat();
            xuatnendat.ShowDialog();
        }
    }
}
