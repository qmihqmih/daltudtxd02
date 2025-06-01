using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class PhuongAnCoc
    {
        public string LoaiCoc { get; set; } // "Cọc vuông" hoặc "Cọc tròn"
        public double KichThuoc { get; set; } // Cạnh vuông hoặc Đường kính
        public double ChieuDai { get; set; }  // Chiều dài cọc
        public double Duongkinhcotthep { get; set; }  
    }
}
