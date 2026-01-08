using System;

namespace DuAnDauDoi.Reports
{
    public class HoaDonReportDTO
    {
        public string TenMon { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string MaHD { get; set; }
        public int? SoBan { get; set; }
        public DateTime? NgayLap { get; set; }
        public decimal TongTien { get; set; }
    }
}
