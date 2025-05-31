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
    /// Interaction logic for phuongancoc.xaml
    /// </summary>
    public partial class phuongancoc : Window
    {
        public phuongancoc()
        {
            InitializeComponent();
            this.DataContext = new PhuongAnCocViewModel();
        }
    }
}
