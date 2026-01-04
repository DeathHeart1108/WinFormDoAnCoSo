using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.Service;
using DAL.Entities;

namespace DuAnDauDoi
{
    public partial class FormHoaDon : Form
    {
        private Ban _table;
        private Hoadon _currentHoadon;
        private string _maHD_LichSu = null;
        private readonly HOADONservice _hoadonService = new HOADONservice();

        public FormHoaDon()
        {
            InitializeComponent();
            btnThanhToan.Click += (s, e) => Close();
            btnIn.Click += BtnIn_Click;
        }

        // Constructor 1: Mở từ bàn đang chọn (để thanh toán)
        // Constructor 1: Mở từ bàn đang chọn (để thanh toán)
        public FormHoaDon(Ban table) : this()
        {
            this._table = table;
            lbBAN.Text = $"Số Bàn: {_table.Soban}";
            LoadData(isHistory: false);
        }

        // Constructor 2: Mở từ FormLichsu (để xem lại)
        public FormHoaDon(string maHD) : this()
        {
            _maHD_LichSu = maHD;
            LoadData(isHistory: true);
        }

        private void LoadData(bool isHistory)
        {
            dgvMon.Rows.Clear();
            
            if (isHistory)
            {
                _currentHoadon = _hoadonService.GetById(_maHD_LichSu);
            }
            else
            {
                _currentHoadon = _hoadonService.GetUnpaidInvoiceByTable(_table.Maban);
            }

            if (_currentHoadon == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu hóa đơn!");
                return;
            }

            lbHd.Text = $"Hóa Đơn: {_currentHoadon.Mahd}";
            lbBAN.Text = $"Số Bàn: {_currentHoadon.Maban}"; 

            foreach (var cthd in _currentHoadon.Cthds)
            {
                var mon = cthd.Mon;
                decimal unitPrice = mon?.Giamon ?? 0m;
                decimal lineTotal = unitPrice * (cthd.Sl ?? 0);

                dgvMon.Rows.Add(
                    mon?.Tenmon ?? $"Mã: {cthd.Mamon}",
                    cthd.Sl,
                    lineTotal
                );
            }

            lbTongTien.Text = $"Tổng Tiền: {CalculateTotal():N0} VND";
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (_currentHoadon == null) return;

            // Truyền dữ liệu sang FormThanhToan
            var listDetails = _currentHoadon.Cthds.Select(ct => new Reports.HoaDonReportDTO
            {
                MaHD = _currentHoadon.Mahd,
                SoBan = _currentHoadon.Maban,
                NgayLap = _currentHoadon.Ngaylap,
                TenMon = ct.Mon?.Tenmon,
                SoLuong = ct.Sl ?? 0,
                DonGia = ct.Mon?.Giamon ?? 0,
                TongTien = _currentHoadon.Tongtien ?? 0,
                ThanhTien = (ct.Sl ?? 0) * (ct.Mon?.Giamon ?? 0)
            }).ToList();

            FormThanhToan frmPrint = new FormThanhToan(_table, _currentHoadon.Mahd, CalculateTotal());
            frmPrint.DataForReport = listDetails;
            frmPrint.ShowDialog();
        }

        private decimal CalculateTotal()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in dgvMon.Rows)
            {
                if (row.IsNewRow) continue;
                var val = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.OwningColumn.Name == "ColGia")?.Value
                          ?? row.Cells[2].Value;

                if (val != null && decimal.TryParse(val.ToString(), out decimal parsed))
                    total += parsed;
            }
            return total;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_currentHoadon == null) return;
            DialogResult result = MessageBox.Show($"Xác nhận thanh toán cho {_table?.Soban}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _hoadonService.PayOrder(_currentHoadon.Mahd, 0);
                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}