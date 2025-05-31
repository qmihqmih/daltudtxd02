using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class cocmodel 
    {
        public double gamma = 1.5;
        public double gammaep = 1.2;
        public double pep; //kN


        public double Gamma
        {
            get { return gamma; }
            set
            {
                gamma = value;
                OnPropertyChanged(nameof(Gamma));
            }
        }

        public double Gammaep
        {
            get { return gammaep; }
            set
            {
                gammaep = value;
                OnPropertyChanged(nameof(Gammaep));
            }
        }

        public double Pep
        {
            get { return pep; }
            set
            {
                pep = value;
                OnPropertyChanged(nameof(Pep));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
