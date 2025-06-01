﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;
using Microsoft.Win32;
using ClosedXML.Excel;


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
        private string _xacDinhLopDat;
        public string XacDinhLopDat
        {
            get => _xacDinhLopDat;
            set
            {
                _xacDinhLopDat = value;
                OnPropertyChanged(nameof(XacDinhLopDat));
            }
        }
        private string _XacDinhTrangThai;
        public string XacDinhTrangThai
        {
            get => _XacDinhTrangThai;
            set
            {
                _XacDinhTrangThai = value;
                OnPropertyChanged(nameof(XacDinhTrangThai));
            }
        }

        public ICommand TinhToanCommand { get; }
        public ICommand LuuCommand { get; }
        public ICommand ExportToExcelCommand { get; }
       

        public TinhToanGroundViewModel()
        {
            GroundList = new ObservableCollection<Ground>();
            TinhToanCommand = new RelayCommand(TinhToan);
            LuuCommand = new RelayCommand(LuuDuLieu);
            ExportToExcelCommand = new RelayCommand(ExportToExcel);
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
                    
                g.E = (int?)TinhE(g);
                g.Doset = (int?)TinhDoset(g);
                g.ChiSoDeo = (int?)TinhChiSoDeo(g);
                g.GroundType = XacDinhLopDatTheoA(g.ChiSoDeo);
                g.GroundState = GetTrangThaiDat(g.GroundType, g.Doset ?? 0);
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
        public string XacDinhLopDatTheoA(double? A)
        {
            if (A == null)
                return "";

            if (A < 7)
                return "Đất á cát (Cát pha)";
            else if (A >= 7 && A < 17)
                return "Đất á sét (Sét pha)";
            else
                return "Đất sét";
        }
        public string GetTrangThaiDat(string tenDat, double doSetB)
        {
            if (tenDat.ToLower().Contains("cát pha"))
            {
                if (doSetB < 0)
                    return "Cứng";
                else if (doSetB <= 1)
                    return "Dẻo";
                else 
                    return "Chảy (nhão)";
            }
            else if (tenDat.ToLower().Contains("sét") || tenDat.ToLower().Contains("sét pha"))
            {
                if (doSetB < 0)
                    return "Cứng (rắn)";
                else if (doSetB <= 0.25)
                    return "Nửa cứng";
                else if (doSetB <= 0.50)
                    return "Dẻo cứng";
                else if (doSetB <= 0.75)
                    return "Dẻo mềm";
                else if (doSetB <= 1.00)
                    return "Dẻo chảy";
                else 
                    return "Chảy (nhão)";
            }

            return "";
        }



        private void LuuDuLieu()
        {
            // Gán lại GroundList đã cập nhật vào DataService
            DataService.Instance.InputData.GroundList = GroundList.ToList();

            MessageBox.Show(" Dữ liệu nền đất đã được lưu!");
        }


        private void ExportToExcel()
        {
            TinhToan();
            var dialog = new SaveFileDialog
            {
                Filter = "Excel File|*.xlsx",
                FileName = "TinhToan.xlsx"
            };

            if (dialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Báo Cáo Tính Toán");
                    worksheet.Cell(1, 1).Value = "Lớp Đất";
                    worksheet.Cell(1, 2).Value = "e(Hệ Số Rỗng)";
                    worksheet.Cell(1, 3).Value = "A (Chỉ số dẻo)";
                    worksheet.Cell(1, 4).Value = "B (Độ sệt)";
                    worksheet.Cell(1, 5).Value = "Loại Đất";
                    worksheet.Cell(1, 6).Value = "Trạng Thái";

                    int row = 2;
                    foreach (var dat in GroundList)
                    {
                        worksheet.Cell(row, 1).Value = dat.Lopdat;
                        worksheet.Cell(row, 2).Value = dat.E;
                        worksheet.Cell(row, 3).Value = dat.ChiSoDeo;
                        worksheet.Cell(row, 4).Value = dat.Doset;
                        worksheet.Cell(row, 5).Value = dat.GroundType;
                        worksheet.Cell(row, 6).Value = dat.GroundState;

                        row++;
                    }

                    worksheet.Columns().AdjustToContents();

                    workbook.SaveAs(dialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo");
                }
            }
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
