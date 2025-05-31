using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class InputData
    {
        public Taitrong Taitrong { get; set; }
        public Vatlieu Vatlieu { get; set; } 
        public Vatlieu2 Vatlieu2 { get; set; }
        public List<Ground> GroundList { get; set; } //  Danh sách các lớp đất
        public PhuongAnCoc PhuongAnCoc { get; set; }
        public phuongphapvatlieu Phuongphapvatlieu { get; set; }
        public cocmodel Coc { get; set; }



        public InputData()
        {
            Taitrong = new Taitrong();
            Vatlieu = new Vatlieu();
            Vatlieu2 = new Vatlieu2();
            GroundList = new List<Ground>(); // Khởi tạo danh sách lớp đất
            PhuongAnCoc = new PhuongAnCoc(); // Khởi tạo phương án cọc
            Phuongphapvatlieu = new phuongphapvatlieu();
            Coc = new cocmodel(); // Khởi tạo mô hình cọc
        }
    }
}
