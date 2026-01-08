using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BLL.DTO;

namespace BLL.Service
{
    public class HOADONservice
    {
        private readonly Random _random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public Hoadon GetById(string id)
        {
            using (var db = new Model1())
            {
                 return db.Hoadons
                    .Include(h => h.Cthds.Select(c => c.Mon))
                    .FirstOrDefault(h => h.Mahd == id);
            }
        }

        public Hoadon GetUnpaidInvoiceByTable(int tableId)
        {
            using (var db = new Model1())
            {
                return db.Hoadons
                    .Include(h => h.Cthds.Select(c => c.Mon))
                    .FirstOrDefault(h => h.Maban == tableId && (h.Status == 0 || h.Status == null));
            }
        }

        // Logic to Create Invoice if not exists, add detail, update total
        public void AddOrder(int tableId, List<Tuple<Mon, int>> items)
        {
             using (var db = new Model1())
             {
                 using (var transaction = db.Database.BeginTransaction())
                 {
                     try
                     {
                         var hoadon = db.Hoadons.FirstOrDefault(h => h.Maban == tableId && (h.Status == 0 || h.Status == null));
                         string currentMahd;
                         
                         if (hoadon == null)
                         {
                             currentMahd = GenerateUniqueMahd(db, "HD");
                             hoadon = new Hoadon
                             {
                                 Mahd = currentMahd,
                                 Ngaylap = DateTime.Now,
                                 Status = 0,
                                 Maban = tableId,
                                 Tongtien = 0
                             };
                             db.Hoadons.Add(hoadon);
                         }
                         else
                         {
                             currentMahd = hoadon.Mahd;
                         }

                         foreach (var item in items)
                         {
                             var mon = item.Item1;
                             var qty = item.Item2;
                             
                             var existingCthd = db.Cthds.FirstOrDefault(ct => ct.Mahd == currentMahd && ct.Mamon == mon.Mamon);

                             if (existingCthd != null)
                             {
                                 existingCthd.Sl += qty;
                             }
                             else
                             {
                                 string randomSuffix = GenerateRandomString(3);
                                 string maCTHD = $"CT{currentMahd.Substring(currentMahd.Length - 3)}{randomSuffix}";
                                 if (maCTHD.Length > 10) maCTHD = maCTHD.Substring(0, 10);
                                 
                                 // Ensure unique ID loop could be here but keeping simple for now
                                 while(db.Cthds.Any(x => x.Macthd == maCTHD))
                                 {
                                      randomSuffix = GenerateRandomString(3);
                                      maCTHD = $"CT{currentMahd.Substring(currentMahd.Length - 3)}{randomSuffix}";
                                      if (maCTHD.Length > 10) maCTHD = maCTHD.Substring(0, 10);
                                 }

                                 db.Cthds.Add(new Cthd
                                 {
                                     Macthd = maCTHD,
                                     Mahd = currentMahd,
                                     Mamon = mon.Mamon,
                                     Sl = qty,
                                     Khuyenmai = 0
                                 });
                             }
                         }

                         db.SaveChanges(); // Save changes to calculate total

                         // Recalculate Total
                         var allDetails = db.Cthds.Where(ct => ct.Mahd == currentMahd).ToList();
                         decimal totalAmount = 0;
                         foreach (var detail in allDetails)
                         {
                            // Need to fetch price again or assume Mon is loaded. Safer to fetch.
                            var price = db.Mons.Where(m => m.Mamon == detail.Mamon).Select(m => m.Giamon).FirstOrDefault() ?? 0;
                            totalAmount += price * (decimal)(detail.Sl ?? 0);
                         }
                         hoadon.Tongtien = totalAmount;

                         // Update Table Status
                         var ban = db.Bans.Find(tableId);
                         if (ban != null) ban.Status = "Có khách";

                         db.SaveChanges();
                         transaction.Commit();
                     }
                     catch
                     {
                         transaction.Rollback();
                         throw;
                     }
                 }
             }
        }
        
        public void UpdateOrder(string invoiceId, int tableId, List<Tuple<int, int>> newItems)
        {
            using (var db = new Model1())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var hoadon = db.Hoadons.FirstOrDefault(h => h.Mahd == invoiceId);
                        if (hoadon == null) return;

                        // 1. Remove old details
                        var oldCthds = db.Cthds.Where(c => c.Mahd == invoiceId).ToList();
                        db.Cthds.RemoveRange(oldCthds);

                        if (newItems == null || newItems.Count == 0)
                        {
                            // If no items, remove invoice and set table to empty
                            db.Hoadons.Remove(hoadon);
                            var ban = db.Bans.Find(tableId);
                            if (ban != null) ban.Status = "Trống";
                        }
                        else
                        {
                            // 2. Add new details
                            int i = 1;
                            decimal total = 0;
                            foreach (var item in newItems)
                            {
                                int mamonId = item.Item1;
                                int qty = item.Item2;
                                var mon = db.Mons.Find(mamonId);
                                decimal price = mon?.Giamon ?? 0;
                                total += price * qty;
                                
                                string randomSuffix = GenerateRandomString(3);
                                string maCTHD = $"CT{invoiceId.Substring(invoiceId.Length - 3)}{randomSuffix}";
                                 while(db.Cthds.Any(x => x.Macthd == maCTHD))
                                 {
                                      randomSuffix = GenerateRandomString(3);
                                      maCTHD = $"CT{invoiceId.Substring(invoiceId.Length - 3)}{randomSuffix}";
                                 }

                                db.Cthds.Add(new Cthd
                                {
                                    Macthd = maCTHD,
                                    Mahd = invoiceId,
                                    Mamon = mamonId,
                                    Sl = qty
                                });
                            }
                            hoadon.Tongtien = total;
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void PayOrder(string invoiceId, decimal givenAmount)
        {
             using (var db = new Model1())
            {
                var hoadon = db.Hoadons.Find(invoiceId);
                if (hoadon != null)
                {
                    hoadon.Status = 1;
                    hoadon.Ngayxuat = DateTime.Now;
                    // Ensure Tongtien is correct or re-calculate? define flow. Assuming updated.
                    
                    var ban = db.Bans.Find(hoadon.Maban);
                    if (ban != null) ban.Status = "Trống";

                    db.SaveChanges();
                }
            }
        }

        public List<LichSuHoaDonDTO> GetHistory(string searchText)
        {
             using (var db = new Model1())
            {
                var query = db.Hoadons.Where(h => h.Status.HasValue && h.Status.Value == 1);

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(h => h.Mahd.Contains(searchText) || 
                                           (h.Maban.HasValue && h.Maban.Value.ToString().Contains(searchText)));
                }

                return query.OrderByDescending(h => h.Ngayxuat)
                    .Select(h => new LichSuHoaDonDTO
                    {
                        MaHD = h.Mahd ?? "",
                        NgayLap = (DateTime?)h.Ngaylap,
                        NgayThanhToan = h.Ngayxuat,
                        SoBan = h.Maban ?? 0,
                        TongTien = h.Tongtien ?? 0,
                        GiamGia = h.KHUYENMAI_HD ?? 0
                    }).ToList();
            }
        }

        public DailyReportResult GetDailyReport()
        {
            using (var db = new Model1())
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                var hoaDonHomNay = db.Hoadons
                    .Where(h => h.Status.HasValue && h.Status.Value == 1 && h.Ngayxuat >= today && h.Ngayxuat < tomorrow)
                    .ToList();

                decimal tongDoanhThu = hoaDonHomNay.Sum(h => h.Tongtien ?? 0);
                int soHoaDon = hoaDonHomNay.Count;

                var maHoaDonHomNay = hoaDonHomNay.Select(h => h.Mahd).ToList();
                int tongSoMon = db.Cthds
                    .Where(ct => maHoaDonHomNay.Contains(ct.Mahd))
                    .Sum(ct => (int?)(ct.Sl ?? 0)) ?? 0;

                return new DailyReportResult
                {
                    TongDoanhThu = tongDoanhThu,
                    TongSoMon = tongSoMon,
                    SoHoaDon = soHoaDon
                };
            }
        }

        public DailyReportResult GetDailyReportByDate(DateTime selectedDate)
        {
            using (var db = new Model1())
            {
                DateTime startDate = selectedDate.Date;
                DateTime endDate = startDate.AddDays(1);

                var hoaDonTheoNgay = db.Hoadons
                    .Where(h => h.Status.HasValue && h.Status.Value == 1 && h.Ngayxuat >= startDate && h.Ngayxuat < endDate)
                    .ToList();

                decimal tongDoanhThu = hoaDonTheoNgay.Sum(h => h.Tongtien ?? 0);
                int soHoaDon = hoaDonTheoNgay.Count;

                var maHoaDonTheoNgay = hoaDonTheoNgay.Select(h => h.Mahd).ToList();
                int tongSoMon = db.Cthds
                    .Where(ct => maHoaDonTheoNgay.Contains(ct.Mahd))
                    .Sum(ct => (int?)(ct.Sl ?? 0)) ?? 0;

                return new DailyReportResult
                {
                    TongDoanhThu = tongDoanhThu,
                    TongSoMon = tongSoMon,
                    SoHoaDon = soHoaDon
                };
            }
        }

        private string GenerateRandomString(int length)
        {
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++) stringChars[i] = Chars[_random.Next(Chars.Length)];
            return new string(stringChars);
        }

        private string GenerateUniqueMahd(Model1 context, string prefix = "HD")
        {
            string newMahd;
            do
            {
                newMahd = $"{prefix}{GenerateRandomString(3)}";
            } while (context.Hoadons.Any(h => h.Mahd == newMahd));
            return newMahd;
        }
        public void MergeTable(int sourceTableId, int destTableId)
        {
            using (var db = new Model1())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var sourceHoadon = db.Hoadons.FirstOrDefault(h => h.Maban == sourceTableId && (h.Status == 0 || h.Status == null));
                        if (sourceHoadon == null) return; // Không có gì để chuyển

                        var destHoadon = db.Hoadons.FirstOrDefault(h => h.Maban == destTableId && (h.Status == 0 || h.Status == null));

                        if (destHoadon == null)
                        {
                            // Trường hợp 1: Bàn đích chưa có hóa đơn -> Chuyển toàn bộ hóa đơn sang bàn đích
                            sourceHoadon.Maban = destTableId;
                            
                            // Cập nhật trạng thái bàn
                            var sourceBan = db.Bans.Find(sourceTableId);
                            if (sourceBan != null) sourceBan.Status = "Trống";

                            var destBan = db.Bans.Find(destTableId);
                            if (destBan != null) destBan.Status = "Có khách";
                        }
                        else
                        {
                            // Trường hợp 2: Bàn đích đã có hóa đơn -> Gộp chi tiết hóa đơn
                            var sourceDetails = db.Cthds.Where(c => c.Mahd == sourceHoadon.Mahd).ToList();
                            
                            foreach (var detail in sourceDetails)
                            {
                                // Kiểm tra xem món này đã có trong hóa đơn đích chưa
                                var destDetail = db.Cthds.FirstOrDefault(c => c.Mahd == destHoadon.Mahd && c.Mamon == detail.Mamon);
                                if (destDetail != null)
                                {
                                    destDetail.Sl += detail.Sl;
                                }
                                else
                                {
                                    // Tạo chi tiết mới cho hóa đơn đích
                                    string randomSuffix = GenerateRandomString(3);
                                    string maCTHD = $"CT{destHoadon.Mahd.Substring(destHoadon.Mahd.Length - 3)}{randomSuffix}";
                                    while (db.Cthds.Any(x => x.Macthd == maCTHD))
                                    {
                                        randomSuffix = GenerateRandomString(3);
                                        maCTHD = $"CT{destHoadon.Mahd.Substring(destHoadon.Mahd.Length - 3)}{randomSuffix}";
                                    }

                                    db.Cthds.Add(new Cthd
                                    {
                                        Macthd = maCTHD,
                                        Mahd = destHoadon.Mahd,
                                        Mamon = detail.Mamon,
                                        Sl = detail.Sl,
                                        Khuyenmai = detail.Khuyenmai
                                    });
                                }
                            }

                            // Cập nhật tổng tiền bàn đích (cần tính lại toàn bộ để chính xác)
                            // Hiện tại chúng ta cộng dồn nhưng tốt nhất là tính lại từ đầu hoặc cộng từ sourceHoadon.Tongtien
                            // Ở đây ta tính đơn giản là cộng thêm sourceHoadon.Tongtien (nếu sourceHoadon.Tongtien đã đúng)
                            destHoadon.Tongtien = (destHoadon.Tongtien ?? 0) + (sourceHoadon.Tongtien ?? 0);

                                                        // Xóa hóa đơn bàn nguồn
                                                        db.Cthds.RemoveRange(sourceDetails); // Xóa chi tiết bàn nguồn
                                                        db.Hoadons.Remove(sourceHoadon);     // Xóa hóa đơn bàn nguồn

                                                        // Cập nhật trạng thái bàn nguồn thành Trống
                                                        var sourceBan = db.Bans.Find(sourceTableId);
                                                        if (sourceBan != null) sourceBan.Status = "Trống";
                                                    }

                                                    db.SaveChanges();
                                                    transaction.Commit();
                                                }
                                                catch
                                                {
                                                    transaction.Rollback();
                                                    throw;
                                                }
                                            }
                                        }
                                    }
                                }

                                public class DailyReportResult
                                {
                                    public decimal TongDoanhThu { get; set; }
                                    public int TongSoMon { get; set; }
                                    public int SoHoaDon { get; set; }
                                }
                            }
