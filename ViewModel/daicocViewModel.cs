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
using ClosedXML.Excel;
using Microsoft.Win32;

namespace LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.ViewModel
{

    public class daicocViewModel : INotifyPropertyChanged //OnPropertyChanged(...): giúp View tự động cập nhật nếu giá trị thay đổi
    {
        private double _hc, _bc, _daidc, _rongdc, _caodc, _rb, _c1, _c2;
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

        public double C1
        {
            get => _c1;
            set { _c1 = value; OnPropertyChanged(nameof(C1)); }
        }

        public double C2
        {
            get => _c2;
            set { _c2 = value; OnPropertyChanged(nameof(C2)); }
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


        public ICommand LuuCommand { get; }
        public ICommand KiemTraChieuCaoDaiCocCommand { get; }
        public ICommand KiemTraDamThungCommand { get; }
        public ICommand ExportdaicocCommand { get; }

        public daicocViewModel()
        {
            LuuCommand = new RelayCommand(LuuDuLieu);
            KiemTraChieuCaoDaiCocCommand = new RelayCommand(_ => KiemTraChieuCaoDaiCoc());
            KiemTraDamThungCommand = new RelayCommand(_ => KiemTraDamThung());
            ExportdaicocCommand = new RelayCommand(_ => Exportdaicoc());
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
            DataService.Instance.InputData.Daicoc.C1 = this.C1;
            DataService.Instance.InputData.Daicoc.C2 = this.C2;

            MessageBox.Show("Dữ liệu nhập được lưu!");          
        }

        public void KiemTraChieuCaoDaiCoc()
        {
            double? caodc = DataService.Instance.InputData.Daicoc.Caodc; //m
            double? bc = this.Bc; //m
            double? hc = this.Hc; //m
            double? n = DataService.Instance.InputData.Taitrong?.N;
            double? rb = this.Rb; //MPa            

            if (caodc.HasValue && n.HasValue && bc.HasValue && hc.HasValue && this.Hc > 0 && this.Bc > 0 && this.Caodc > 0)
            {
                bool ketqua = n.Value / 2 * (bc.Value + hc.Value) * (caodc.Value - 0.03)  <= rb.Value * 1000;
                KetQuaKiemTra1 = ketqua
                    ? $"✅ Đạt: N/2 × (bc + hc) × (h - 0.03) = {n.Value / 2 * (bc.Value + hc.Value) * (caodc.Value - 0.03):F2} ≤ Rb = {rb*1000:F2}"
                    : $"❌ Không đạt: N/2 × (bc + hc) × (h - 0.03) = {n.Value / 2 * (bc.Value + hc.Value) * (caodc.Value - 0.03):F2} > Rb = {rb*1000:F2}";

            }
            else
            {
                KetQuaKiemTra1 = "⚠️ Thiếu dữ liệu để kiểm tra sức chịu tải.";
            }
        }

        public void KiemTraDamThung()
        {
            double? caodc = DataService.Instance.InputData.Daicoc.Caodc; // m
            double? bc = this.Bc; // m
            double? hc = this.Hc; // m
            double? c1 = this.C1; // m
            double? c2 = this.C2; // m
            double? rbt = DataService.Instance.InputData.Vatlieu.Rbt; // MPa
            double? b = this.Rongdc; // m

            if (caodc.HasValue && bc.HasValue && hc.HasValue && c1.HasValue && c2.HasValue && rbt.HasValue
                && bc > 0 && hc > 0 && caodc > 0 && c1 > 0 && c2 > 0)
            {
                double h = caodc.Value;
                double alpha1 = 1.5 * Math.Sqrt(1 + Math.Pow(h / c1.Value, 2));
                double alpha2 = 1.5 * Math.Sqrt(1 + Math.Pow(h / c2.Value, 2));

                double tongPhanLuc = (alpha1 * (bc.Value + c1.Value) + alpha2 * (hc.Value + c2.Value)) * (h - 0.03) * rbt.Value * 1000; 
                double sucChiuTaiTH1 = rbt.Value * (bc.Value + h - 0.03) * (h - 0.03) * 1000;
                double sucChiuTaiTH2 = rbt.Value * (bc.Value + b.Value) * (h - 0.03) * 1000;

                if (b > bc + 2 * h)
                {
                    bool ketqua = tongPhanLuc <= sucChiuTaiTH1;

                    KetQuaKiemTra2 = ketqua
                        ? $"✅ Đạt: Phản lực tổng ({tongPhanLuc:F2} ) ≤ Sức chịu tải ({sucChiuTaiTH1:F2} )"
                        : $"❌ Không đạt: Phản lực tổng ({tongPhanLuc:F2} ) > Sức chịu tải ({sucChiuTaiTH1:F2} )";
                }
                else
                {
                    bool ketqua = tongPhanLuc <= sucChiuTaiTH2;
                    KetQuaKiemTra2 = ketqua
                        ? $"✅ Đạt: Phản lực tổng ({tongPhanLuc:F2} ) ≤ Sức chịu tải ({sucChiuTaiTH2:F2} )"
                        : $"❌ Không đạt: Phản lực tổng ({tongPhanLuc:F2} ) > Sức chịu tải ({sucChiuTaiTH2:F2} )";
                }
            }
            else
            {
                KetQuaKiemTra2 = "⚠️ Thiếu dữ liệu để kiểm tra đâm thủng.";
            }
        }

        private void Exportdaicoc()
        {
            KiemTraChieuCaoDaiCoc();
            KiemTraDamThung();
            var dialog = new SaveFileDialog
            {
                Filter = "Excel File|*.xlsx",
                FileName = "Suc chiu tai.xlsx"
            };
            if (dialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Báo Cáo suc chiu tai");

                    // Ghi tiêu đề cột
                    worksheet.Cell(1, 1).Value = "Chiều Dài";
                    worksheet.Cell(1, 2).Value = "Chiều Rộng";
                    worksheet.Cell(1, 3).Value = "Chiều cao";
                    worksheet.Cell(1, 4).Value = "C1";
                    worksheet.Cell(1, 5).Value = "C2";

                    // Ghi dữ liệu
                    worksheet.Cell(2, 1).Value = Daidc;
                    worksheet.Cell(2, 2).Value = Rongdc;
                    worksheet.Cell(2, 3).Value = Caodc;
                    worksheet.Cell(2, 4).Value = C1;
                    worksheet.Cell(2, 5).Value = C2;

                    // Lưu file
                    workbook.SaveAs(dialog.FileName);
                    MessageBox.Show("Đã xuất file Excel thành công!");
                }
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
