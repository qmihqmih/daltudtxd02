using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using System.Collections.ObjectModel;
using System.Windows;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{
    public class GroundViewModel
    {
        private ObservableCollection<Ground> _GroundList;

        public ICommand SaveCommand { get; set; }  //lệnh lưu dữ liệu
        public GroundViewModel()
        {
            _GroundList = new ObservableCollection<Ground>
        {
            new Ground { Lopdat = 1, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null },
            new Ground { Lopdat = 2, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null },
            new Ground { Lopdat = 3, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null },
            new Ground { Lopdat = 4, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null },
            new Ground { Lopdat = 5, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null },
            new Ground { Lopdat = 6, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null },
            new Ground { Lopdat = 7, Phi = null, H = null, Gamma = null, C =null, Modun = null, Delta = null, E = null, GroundType = " ", Doset = null, Spt = null, Cpt = null, ChiSoDeo = null, GammaNuoc = 9.81, W = null, Wch = null, Wd = null }
        };
            SaveCommand = new RelayCommand(SaveGroundData);
        }

        private Ground _selectedGround;
        public Ground SelectedGround
        {
            get => _selectedGround;
            set
            {
                _selectedGround = value;
                OnPropertyChanged(nameof(SelectedGround));
            }
        }

        public ObservableCollection<Ground> Ground
        {
            get { return _GroundList; }
            set { _GroundList = value; OnPropertyChanged(nameof(Ground)); } // Đảm bảo cập nhật UI khi thay đổi 
        }

        private void SaveGroundData()
        {
            if (_GroundList != null && _GroundList.Count > 0)
            {
                // Lưu toàn bộ danh sách vào InputData
                DataService.Instance.InputData.GroundList = _GroundList.ToList();
                MessageBox.Show("Đã lưu danh sách lớp đất vào InputData!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Danh sách lớp đất rỗng, không thể lưu!", "Cảnh báo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region RelayCommand
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute();
            }

            public void Execute(object parameter)
            {
                _execute();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
        #endregion
    }
}
