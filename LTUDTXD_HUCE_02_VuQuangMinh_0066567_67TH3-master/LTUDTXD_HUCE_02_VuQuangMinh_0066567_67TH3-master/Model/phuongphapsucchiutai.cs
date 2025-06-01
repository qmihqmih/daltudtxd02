using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class phuongphapvatlieu
    {
        public double m = 0.9;
        public double phi = 1;
        public double Abetong;
        public double Acotthep;
        public double? VatLieu;
        public double M
        {
            get { return m; }
            set
            {
                m = value;
                OnPropertyChanged(nameof(M));
            }
        }

        public double Phi
        {
            get { return phi; }
            set
            {
                phi = value;
                OnPropertyChanged(nameof(Phi));
            }
        }

        public double ABeTong
        {
            get { return Abetong; }
            set
            {
                Abetong = value;
                OnPropertyChanged(nameof(ABeTong));
            }
        }

        public double ACotThep
        {
            get { return Acotthep; }
            set
            {
                Acotthep = value;
                OnPropertyChanged(nameof(ACotThep));
            }
        }

        public double? Vatlieu
        {
            get { return VatLieu; }
            set
            {
                VatLieu = value;
                OnPropertyChanged(nameof(Vatlieu));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
}
