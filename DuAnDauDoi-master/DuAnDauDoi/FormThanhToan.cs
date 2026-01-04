using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.Service;
using DAL.Entities;
using Microsoft.Reporting.WinForms; // Thêm thư viện này
using System.Collections.Generic;

namespace DuAnDauDoi
{
    public partial class FormThanhToan : Form
    {
        private Ban _table;
        private string _mahd;
        private decimal _total;
        private readonly HOADONservice _hoadonService = new HOADONservice();

        public FormThanhToan()
        {
            InitializeComponent();
            this.Load += FormThanhToan_Load;
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        public List<Reports.HoaDonReportDTO> DataForReport { get; set; }

        private void LoadReport()
        {
            try
            {
                // Check if data is already provided
                if (DataForReport != null && DataForReport.Count > 0)
                {
                    BindReport(DataForReport);
                    return;
                }

                if (string.IsNullOrEmpty(_mahd)) return;

                // 1. Lấy dữ liệu chi tiết hóa đơn từ Service
                var hoadon = _hoadonService.GetById(_mahd);
                if (hoadon == null) return;

                // 2. Chuyển đổi sang danh sách DTO để Report hiểu
                // 2. Chuyển đổi sang danh sách DTO để Report hiểu
                var reportData = hoadon.Cthds.Select(ct => new Reports.HoaDonReportDTO
                {
                    MaHD = hoadon.Mahd,
                    SoBan = hoadon.Maban,
                    NgayLap = hoadon.Ngaylap,
                    TenMon = ct.Mon?.Tenmon,
                    SoLuong = ct.Sl ?? 0,
                    DonGia = ct.Mon?.Giamon ?? 0,
                    ThanhTien = (ct.Sl ?? 0) * (ct.Mon?.Giamon ?? 0),
                    TongTien = hoadon.Tongtien ?? 0
                }).ToList();

                BindReport(reportData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in: " + ex.Message);
            }
        }

        private void BindReport(List<Reports.HoaDonReportDTO> reportData)
        {
             string reportPath = System.IO.Path.Combine(Application.StartupPath, "Reports", "rptHoaDon.rdlc");
             if (!System.IO.File.Exists(reportPath))
             {
                // Fallback for dev environment or different structure
                reportPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.StartupPath, @"..\..\Reports\rptHoaDon.rdlc"));
             }
             
             reportViewer1.LocalReport.ReportPath = reportPath;
             reportViewer1.LocalReport.DataSources.Clear();
             var source = new ReportDataSource("DataSetHoaDon", reportData); 
             reportViewer1.LocalReport.DataSources.Add(source);

             this.reportViewer1.RefreshReport();
        }

        public FormThanhToan(Ban table, string mahd = null, decimal total = 0m) : this()
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _mahd = mahd;
            _total = total;
        }
    }
}