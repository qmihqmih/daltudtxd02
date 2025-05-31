using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Model;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.Service;
using LTUDTXD_HUCE_2_VuQuangMinh_0066567_67TH3.View;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

public class SucChiuTaiCocViewModel : INotifyPropertyChanged
{
    private double? _cpt, _spt, _thongke, _vatlieu, _min;

    public double? CPT
    {
        get => _cpt;
        set { _cpt = value; OnPropertyChanged(); UpdateMin(); }
    }

    public double? SPT
    {
        get => _spt;
        set { _spt = value; OnPropertyChanged(); UpdateMin(); }
    }

    public double? ThongKe
    {
        get => _thongke;
        set { _thongke = value; OnPropertyChanged(); UpdateMin(); }
    }

    public double? VatLieu
    {
        get => _vatlieu;
        set { _vatlieu = value; OnPropertyChanged(); UpdateMin(); }
    }

    public double? Min
    {
        get => _min;
        set { _min = value; OnPropertyChanged(); }
    }

    public ICommand TinhToanCPTCommand { get; }
    public ICommand TinhToanSPTCommand { get; }
    public ICommand TinhToanThongKeCommand { get; }
    public ICommand TinhToanVatLieuCommand { get; }

    public SucChiuTaiCocViewModel()
    {
        TinhToanCPTCommand = new RelayCommand(_ => CPT = TinhCPT());
        TinhToanSPTCommand = new RelayCommand(_ => SPT = TinhSPT());
        TinhToanThongKeCommand = new RelayCommand(_ => ThongKe = TinhThongKe());
        TinhToanVatLieuCommand = new RelayCommand(_ => VatLieu = TinhVatLieu());

        LuuCommand = new RelayCommand(_ => LuuDuLieu());
    }

    private void UpdateMin()
    {
        var values = new[] { CPT, SPT, ThongKe, VatLieu };
        Min = values.Where(v => v.HasValue).Min();
    }

    private double TinhCPT() => 1200;       // giả sử
    private double TinhSPT() => 1000;       // giả sử
    private double TinhThongKe() => 1100;   // giả sử
    private double TinhVatLieu()
    {
        var a = DataService.Instance.InputData.Vatlieu;
        var b = DataService.Instance.InputData.PhuongAnCoc;
        var c = DataService.Instance.InputData.Vatlieu2;
        var phuongphapvatlieu = DataService.Instance.InputData.Phuongphapvatlieu;

        if (a == null || b == null || c == null)
            return 0;

        double Abetong = 0;
        double Acotthep = 0;

        if (b.LoaiCoc == "Cọc vuông")
        {
            // Cọc vuông
            double d = b.KichThuoc;
            double dst = b.Duongkinhcotthep;
            Abetong = d * d - 4 * Math.PI * dst / 1000 * dst / 1000 / 4;
            Acotthep = 4 * Math.PI * dst/1000 * dst/1000 / 4;
        }
        else if (b.LoaiCoc == "Cọc tròn")
        {
            // Cọc tròn
            double d = b.KichThuoc;
            double dst = b.Duongkinhcotthep;
            Abetong = Math.PI * d * d / 4 - 4 * Math.PI * dst / 1000 * dst / 1000 / 4;
            Acotthep = 4 * Math.PI * dst / 1000 * dst / 1000 / 4;
        }

        return phuongphapvatlieu.M * phuongphapvatlieu.Phi * (a.Rb * Abetong + c.Rsc * Acotthep);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    public ICommand LuuCommand { get; }
    private void LuuDuLieu()
    {
        VatLieu = TinhVatLieu(); 
        DataService.Instance.InputData.Phuongphapvatlieu.Vatlieu = this.VatLieu;

        if (Min.HasValue)
            DataService.Instance.InputData.Rcocmin = Min.Value;

        MessageBox.Show("Kết quả đã được lưu!");
    }

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

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
    public void Execute(object parameter) => _execute(parameter);

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}