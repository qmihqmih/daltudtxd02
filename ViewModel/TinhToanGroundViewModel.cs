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
        public ICommand LuuCommand { get; }

        public TinhToanGroundViewModel()
        {
            GroundList = new ObservableCollection<Ground>();
            TinhToanCommand = new RelayCommand(TinhToan);
            LuuCommand = new RelayCommand(LuuDuLieu);
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
            if (g.Gamma != null && g.H != null && g.W != null && g.Delta != null)
                return (g.Delta * g.GammaNuoc * (1 + g.W)) / g.Gamma ; 
            return null;
        }

        private double? TinhDoset(Ground g)
        {
            if (g.Wch != null && g.Wd != null && g.W != null) 
                return (g.W - g.Wd) / (g.Wch - g.Wd); 
            return null;
        }

        private double? TinhChiSoDeo(Ground g)
        {
            if (g.Wch != null && g.Wd != null )
                return g.Wch - g.Wd; 
            return null;
        }


        private void LuuDuLieu()
        {
            // Gán lại GroundList đã cập nhật vào DataService
            DataService.Instance.InputData.GroundList = GroundList.ToList();

            MessageBox.Show(" Dữ liệu nền đất đã được lưu!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        // Dùng cho Action<object>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Dùng cho Action không có tham số
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = o => execute(); // gói lại thành Action<object>
            if (canExecute != null)
                _canExecute = o => canExecute();
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }


}
