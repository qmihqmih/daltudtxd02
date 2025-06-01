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
    /// Interaction logic for succhiutaicoc.xaml
    /// </summary>
    public partial class succhiutaicoc : Window
    {
        public succhiutaicoc()
        {
            InitializeComponent();
            this.DataContext = new SucChiuTaiCocViewModel();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }
        private void btnBotricoc_Click(object sender, RoutedEventArgs e)
        {
            Botricoc botricoc = new Botricoc();
            botricoc.ShowDialog();
        }
    }
}
