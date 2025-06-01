using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model
{
    public class Ground : INotifyPropertyChanged
    {
        private int? lopdat;
        private double? phi;
        private double? h;
        private double? gamma;
        private double? c;
        private double? modun;
        private double? delta;
        private double? e;
        private string groundtype;
        private double? doset;
        private int? spt;
        private double? cpt;
        private double? w;
        private double? wd;
        private double? wch;
        private double? chisodeo;
        private double? gammanuoc = 9.81; // giá trị mặc định của trọng lượng riêng nước (kN/m3)

        public int? Lopdat
        {
            get
            {
                return lopdat;
            }
            set
            {
                lopdat = value;
                OnPropertyChanged("Lopdat");
            }
        }

        public double? Phi
        {
            get
            {
                return phi;
            }
            set
            {
                phi = value;
                OnPropertyChanged("Phi");
            }
        }

        public double? H
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
                OnPropertyChanged("H");
            }
        }

        public double? Gamma
        {
            get
            {
                return gamma;
            }
            set
            {
                gamma = value;
                OnPropertyChanged("Gamma");
            }
        }

        public double? C
        {
            get
            {
                return c;
            }
            set
            {
                c = value;
                OnPropertyChanged("C");
            }
        }

        public double? Modun
        {
            get
            {
                return modun;
            }
            set
            {
                modun = value;
                OnPropertyChanged("Modun");
            }
        }

        public double? Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
                OnPropertyChanged("Delta");
            }
        }

        public double? E
        {
            get
            {
                return e;
            }
            set
            {
                e = value;
                OnPropertyChanged("E");
            }
        }

        public string GroundType
        {
            get
            {
                return groundtype;
            }
            set
            {
                groundtype = value;
                OnPropertyChanged("GroundType");
            }
        }

        public double? Doset
        {
            get
            {
                return doset;
            }
            set
            {
                doset = value;
                OnPropertyChanged("Doset");
            }
        }

        public int? Spt
        {
            get
            {
                return spt;
            }
            set
            {
                spt = value;
                OnPropertyChanged("Spt");
            }
        }

        public double? Cpt
        {
            get
            {
                return cpt;
            }
            set
            {
                cpt = value;
                OnPropertyChanged("Cpt");
            }
        }

        public double? W
        {
            get => w;
            set { w = value; OnPropertyChanged(nameof(W)); }
        }

        public double? Wd
        {
            get => wd;
            set { wd = value; OnPropertyChanged(nameof(Wd)); }
        }

        public double? Wch
        {
            get => wch;
            set { wch = value; OnPropertyChanged(nameof(Wch)); }
        }

        public double? ChiSoDeo
        {
            get => chisodeo;
            set { chisodeo = value; OnPropertyChanged(nameof(ChiSoDeo)); }
        }

        public double? GammaNuoc
        {
            get => gammanuoc;
            set { gammanuoc = value; OnPropertyChanged(nameof(GammaNuoc)); }
        }

        public string GroundState { get; internal set; }

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
