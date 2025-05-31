using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{
    public class TinhToanGroundViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Ground> _groundList;
        public ObservableCollection<Ground> GroundList
        {
            get => _groundList;
            set
            {
                _groundList = value;
                OnPropertyChanged(nameof(GroundList));
            }
        }

        public ICommand TinhToanCommand { get; }

        public TinhToanGroundViewModel()
        {
            GroundList = new ObservableCollection<Ground>();
            TinhToanCommand = new RelayCommand(TinhToan);
        }

        private void TinhToan()
        {
            var data = DataService.Instance.InputData.GroundList;

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu lớp đất để tính toán!", "Cảnh báo");
                return;
            }

            GroundList.Clear();

            foreach (var g in data)
            {
                // Tính lại E và Doset nếu cần
                g.E = (int?)TinhE(g);
                g.Doset = (int?)TinhDoset(g);
                g.ChiSoDeo = (int?)TinhChiSoDeo(g);

                GroundList.Add(g);
            }
        }

        private double? TinhE(Ground g)
        {
            if (g.Gamma != null && g.H != null)
                return g.Gamma * g.H * 10; // ví dụ đơn giản
            return null;
        }

        private double? TinhDoset(Ground g)
        {
            if (g.C != null && g.Modun != null && g.Modun != 0) 
                return g.C / g.Modun; // ví dụ đơn giản
            return null;
        }

        private double? TinhChiSoDeo(Ground g)
        {
            if (g.C != null && g.Modun != null && g.Modun != 0)
                return g.C / g.Modun; // ví dụ đơn giản
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private Action<object> confirmPhuongAn;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> confirmPhuongAn)
        {
            this.confirmPhuongAn = confirmPhuongAn;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
