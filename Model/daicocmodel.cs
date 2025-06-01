using System;
using System.ComponentModel;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class daicocmodel : INotifyPropertyChanged
    {
        private double _hc;     // Chiều dài cột (m)
        private double _bc;     // Chiều rộng cột (m)
        private double _daidc;  // Chiều dài đài cọc (m)
        private double _rongdc; // Chiều rộng đài cọc (m)
        private double _caodc;  // Chiều cao đài cọc (m)
        private double _c1;
        private double _c2;

        public double Hc
        {
            get => _hc;
            set { _hc = value; OnPropertyChanged(nameof(Hc)); }
        }

        public double Bc
        {
            get => _bc;
            set { _bc = value; OnPropertyChanged(nameof(Bc)); }
        }

        public double Daidc
        {
            get => _daidc;
            set { _daidc = value; OnPropertyChanged(nameof(Daidc)); }
        }

        public double Rongdc
        {
            get => _rongdc;
            set { _rongdc = value; OnPropertyChanged(nameof(Rongdc)); }
        }

        public double Caodc
        {
            get => _caodc;
            set { _caodc = value; OnPropertyChanged(nameof(Caodc)); }
        }

        public double C1
        {
            get => _c1;
            set { _c1 = value; OnPropertyChanged(nameof(C1)); }
        }

        public double C2
        {
            get => _c2;
            set { _c2 = value; OnPropertyChanged(nameof(C2)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
