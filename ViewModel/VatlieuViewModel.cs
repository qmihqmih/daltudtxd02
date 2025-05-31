using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;


namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{
    public class VatlieuViewModel 
    {
        private ObservableCollection<Vatlieu> _VatlieuList;

        public VatlieuViewModel()
        {
            LuuCommand = new RelayCommand(LuuDuLieu);

            _VatlieuList = new ObservableCollection<Vatlieu>
        {
            new Vatlieu { capben="B15", mac="M200", rb=8.5, rbt=0.75, eb=23000},
            new Vatlieu { capben="B20", mac="M250", rb=11.5, rbt=0.9, eb=27000},
            new Vatlieu { capben="B25", mac="M350", rb=14.5, rbt=1.05, eb=30000},
            new Vatlieu { capben="B30", mac="M400", rb=17, rbt=1.2, eb=32500},
            new Vatlieu { capben="B35", mac="M450", rb=19.5, rbt=1.3, eb=34500},
            new Vatlieu { capben="B40", mac="M500", rb=22, rbt=1.4, eb=36000},
        };
        }

        private Vatlieu _VatlieuDuocChon;
        public Vatlieu VatlieuDuocChon
        {
            get => _VatlieuDuocChon;
            set
            {
                _VatlieuDuocChon = value;
                OnPropertyChanged(nameof(VatlieuDuocChon));
            }
        }
        public ICommand LuuCommand { get; }

        private void LuuDuLieu()
        {
            if (VatlieuDuocChon != null)
            {
                DataService.Instance.InputData.Vatlieu = VatlieuDuocChon;
                MessageBox.Show($"Đã lưu vật liệu: {VatlieuDuocChon.capben}");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vật liệu!");
            }
        }


        public ObservableCollection<Vatlieu> Vatlieu 
        {
            get { return _VatlieuList; }
            set { _VatlieuList = value; OnPropertyChanged(nameof(Vatlieu)); } // Đảm bảo cập nhật UI khi thay đổi 
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
