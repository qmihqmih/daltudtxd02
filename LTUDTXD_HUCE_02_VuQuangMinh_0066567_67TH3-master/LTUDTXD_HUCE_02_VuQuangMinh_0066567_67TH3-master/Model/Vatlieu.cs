using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class Vatlieu
    {
        public string capben;
        public string mac;
        public double rb;
        public double rbt;
        public double eb;
        public string Capben
        {
            get
            {
                return capben;
            }
            set
            {
                capben = value;
                OnPropertyChanged("Capben");
            }
        }
        public string Mac
        {
            get
            {
                return mac;
            }
            set
            {
                mac = value;
                OnPropertyChanged("Mac");
            }
        }
        public double Rb
        {
            get
            {
                return rb;
            }
            set
            {
                rb = value;
                OnPropertyChanged("Rb");
            }
        }
        public double Rbt
        {
            get
            {
                return rbt;
            }
            set
            {
                rbt = value;
                OnPropertyChanged("Rbt");
            }
        }
        public double Eb
        {
            get
            {
                return eb;
            }
            set
            {
                eb = value;
                OnPropertyChanged("Eb");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
