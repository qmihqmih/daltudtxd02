using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{

    public class MainViewModel
    {
        public TinhToanGroundViewModel TinhToanGroundVM { get; set; }
        public SucChiuTaiCocViewModel SucChiuTaiCocVM { get; set; }
        

        public MainViewModel()
        {
            TinhToanGroundVM = new TinhToanGroundViewModel();
            SucChiuTaiCocVM = new SucChiuTaiCocViewModel();
            
        }

        // 👉 Forward các command ra ngoài để XAML có thể binding được
        public ICommand ExportToExcelCommand => TinhToanGroundVM.ExportToExcelCommand;
        public ICommand ExportsucchiutaiCommand => SucChiuTaiCocVM.ExportsucchiutaiCommand;
       
    }
}
