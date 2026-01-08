using System;

namespace BLL.DTO
{
    public class LichSuHoaDonDTO
    {
        public string MaHD { get; set; }
        public DateTime? NgayLap { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public int SoBan { get; set; }
        public decimal TongTien { get; set; }
        public double GiamGia { get; set; }
    }
}
