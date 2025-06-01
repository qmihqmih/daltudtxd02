using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;


namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{
    public class Vatlieu2ViewModel
    {
        private ObservableCollection<Vatlieu2> _Vatlieu2List;

        public Vatlieu2ViewModel()
        {
            LuuCommand = new RelayCommand(LuuDuLieu);

            _Vatlieu2List = new ObservableCollection<Vatlieu2>
        {
            new Vatlieu2 { loaithep="CB240-T", rs=210, rsw=170, rsc=210, es=200000},
            new Vatlieu2 { loaithep="CB300-T", rs=260, rsw=210, rsc=260, es=200000},
            new Vatlieu2 { loaithep="CB300-V", rs=260, rsw=210, rsc=260, es=200000},
            new Vatlieu2 { loaithep="CB400-V", rs=350, rsw=280, rsc=350, es=200000},
            new Vatlieu2 { loaithep="CB500-V", rs=435, rsw=300, rsc=435, es=200000}
        };
        }

        private Vatlieu2 _ThepCauTao;
        public Vatlieu2 ThepCauTao
        {
            get => _ThepCauTao;
            set
            {
                _ThepCauTao = value;
                OnPropertyChanged(nameof(ThepCauTao));
            }
        }

        private Vatlieu2 _ThepChiuLuc;
        public Vatlieu2 ThepChiuLuc
        {
            get => _ThepChiuLuc;
            set
            {
                _ThepChiuLuc = value;
                OnPropertyChanged(nameof(ThepChiuLuc));
            }
        }

        public ICommand LuuCommand { get; }

        private void LuuDuLieu()
        {
            if (ThepCauTao != null && ThepChiuLuc != null)
            {
                DataService.Instance.InputData.Vatlieu2 = ThepCauTao;
                DataService.Instance.InputData.Vatlieu2 = ThepChiuLuc;
                MessageBox.Show($"Đã lưu thép cấu tạo: {ThepCauTao.loaithep}, thép chịu lực: {ThepChiuLuc.loaithep}");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thép cấu tạo và thép chịu lực!");
            }
        }


        public ObservableCollection<Vatlieu2> Vatlieu2
        {
            get { return _Vatlieu2List; }
            set { _Vatlieu2List = value; OnPropertyChanged(nameof(Vatlieu2)); } // Đảm bảo cập nhật UI khi thay đổi 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
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
}
