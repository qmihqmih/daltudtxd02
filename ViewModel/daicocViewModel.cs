using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel.GroundViewModel;
using System.Windows.Input;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;
using System.Windows;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{

    public class daicocViewModel : INotifyPropertyChanged //OnPropertyChanged(...): giúp View tự động cập nhật nếu giá trị thay đổi
    {
        private double _hc, _bc, _daidc, _rongdc, _caodc, _rb;
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

        public double Rb
        {
            get => _rb;
            set { _rb = value; OnPropertyChanged(nameof(Rb)); }
        }

        private string _ketQuaKiemTra1;
        public string KetQuaKiemTra1
        {
            get => _ketQuaKiemTra1;
            set { _ketQuaKiemTra1 = value; OnPropertyChanged(); }
        }

        private string _ketQuaKiemTra2;
        public string KetQuaKiemTra2
        {
            get => _ketQuaKiemTra2;
            set { _ketQuaKiemTra2 = value; OnPropertyChanged(); }
        }

        private string _ketQuaKiemTra3;
        public string KetQuaKiemTra3
        {
            get => _ketQuaKiemTra3;
            set { _ketQuaKiemTra3 = value; OnPropertyChanged(); }
        }

        public ICommand LuuCommand { get; }
        public ICommand KiemTraChieuCaoDaiCocCommand { get; }

        public daicocViewModel()
        {
            LuuCommand = new RelayCommand(LuuDuLieu);
            KiemTraChieuCaoDaiCocCommand = new RelayCommand(_ => KiemTraChieuCaoDaiCoc());

            // Gán giá trị sẵn có từ DataService
            Rb = DataService.Instance.InputData.Vatlieu.Rb;
            

        }

        private void LuuDuLieu(object obj)
        {
            // Gán dữ liệu vào service dùng chung
            DataService.Instance.InputData.Daicoc.Hc = this.Hc;
            DataService.Instance.InputData.Daicoc.Bc = this.Bc;
            DataService.Instance.InputData.Daicoc.Daidc = this.Daidc;
            DataService.Instance.InputData.Daicoc.Rongdc = this.Rongdc;
            DataService.Instance.InputData.Daicoc.Caodc = this.Caodc;

            MessageBox.Show("Dữ liệu đã được lưu!");
           
        }

        public void KiemTraChieuCaoDaiCoc()
        {
            double? caodc = DataService.Instance.InputData.Daicoc.Caodc;
            double? bc = this.Bc;
            double? hc = this.Hc;
            double? n = DataService.Instance.InputData.Taitrong?.N;
            double? rb = this.Rb;


            if (caodc.HasValue && n.HasValue && bc.HasValue && hc.HasValue)
            {
                bool ketqua = n.Value / 2 * (bc.Value + hc.Value) * (caodc.Value - 0.03)  <= rb.Value ;
                KetQuaKiemTra1 = ketqua
                    ? $"✅ Đạt: N/2 × (bc + hc) × (h - 0.03) = {n.Value / 2 * (bc.Value + hc.Value) * (caodc.Value - 0.03):F2} ≤ Rb = {rb:F2}"
                    : $"❌ Không đạt: N/2 × (bc + hc) × (h - 0.03) = {n.Value / 2 * (bc.Value + hc.Value) * (caodc.Value - 0.03):F2} > Rb = {rb:F2}";

            }
            else
            {
                KetQuaKiemTra1 = "⚠️ Thiếu dữ liệu để kiểm tra sức chịu tải.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }


    }
}
