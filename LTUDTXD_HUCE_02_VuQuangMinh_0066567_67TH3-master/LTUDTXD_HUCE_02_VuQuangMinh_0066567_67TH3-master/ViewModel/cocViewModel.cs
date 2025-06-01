using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{
    public class cocViewModel : INotifyPropertyChanged
    {
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

        public ICommand KiemTraSucChiuTaiCocCommand { get; }
        public ICommand KiemTraLucEpToiDaCommand { get; }
        public ICommand LuuCommand { get; }

        public cocViewModel()
        {
            LuuCommand = new RelayCommand(LuuDuLieu);
            KiemTraSucChiuTaiCocCommand = new RelayCommand(_ => KiemTraSucChiuTaiCocEp());
            KiemTraLucEpToiDaCommand = new RelayCommand(_ => KiemTraLucEpToiDa());
        }

        public double pep;
        public double Pep
        {
            get => pep;
            set
            {
                pep = value;
                OnPropertyChanged(nameof(Pep));
            }
        }
        private void LuuDuLieu(object obj)
        {
            // Gán dữ liệu vào service dùng chung
            DataService.Instance.InputData.Coc.pep = this.pep;
            
            MessageBox.Show("Giá trị đã được lưu!");
           
        }

        public void KiemTraSucChiuTaiCocEp()
        {
            double? rcoc = DataService.Instance.InputData.Rcocmin;
            double? ntt = DataService.Instance.InputData.Taitrong?.N;
            double gamma = DataService.Instance.InputData.Coc?.Gamma ?? 1.5;

            if (rcoc.HasValue && ntt.HasValue)
            {
                bool ketqua = ntt.Value < rcoc.Value / gamma;
                KetQuaKiemTra1 = ketqua
                    ? $"✅ Đạt: Ntt = {ntt} < Rcọc / γ = {rcoc} / {gamma} = {rcoc.Value / gamma:F2}"
                    : $"❌ Không đạt: Ntt = {ntt} ≥ Rcọc / γ = {rcoc} / {gamma} = {rcoc.Value / gamma:F2}";
            }
            else
            {
                KetQuaKiemTra1 = "⚠️ Thiếu dữ liệu để kiểm tra sức chịu tải.";
            }
        }

        public void KiemTraLucEpToiDa()
        {
            double? rcoc = DataService.Instance.InputData.Rcocmin;
            double? pep = DataService.Instance.InputData.Coc?.Pep;
            double gammaep = DataService.Instance.InputData.Coc?.Gammaep ?? 1.2;

            if (rcoc.HasValue && pep.HasValue)
            {
                bool ketqua = pep < rcoc.Value * gammaep;
                KetQuaKiemTra2 = ketqua
                    ? $"✅ Đạt: Pep = {pep} < Rcọc × γép = {rcoc} × {gammaep} = {rcoc.Value * gammaep:F2}"
                    : $"❌ Không đạt: Pep = {pep} ≥ Rcọc × γép = {rcoc} × {gammaep} = {rcoc.Value * gammaep:F2}";
            }
            else
            {
                KetQuaKiemTra2 = "⚠️ Thiếu dữ liệu Rcọc để kiểm tra lực ép.";
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
