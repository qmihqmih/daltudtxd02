using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service
{
    public class DataService //dùng để lưu và chia sẻ dữ liệu giữa các ViewModel
    {
        private static DataService _instance;

        // Singleton pattern: đảm bảo chỉ có một instance duy nhất
        public static DataService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataService();
                return _instance;
            }
        }

        // Dữ liệu đầu vào chung
        public InputData InputData { get; set; }

        private DataService()
        {
            InputData = new InputData();
        }
    }
}
