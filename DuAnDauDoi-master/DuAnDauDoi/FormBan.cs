using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.Service;
using DAL.Entities;

namespace DuAnDauDoi
{
    public partial class FormBan : Form
    {
        private Button _currentSelectedButton = null;
        private readonly BANservice _banService = new BANservice();
        private readonly HOADONservice _hoadonService = new HOADONservice();
        private readonly Image AvailableImage = global::DuAnDauDoi.Properties.Resources.istockphoto_1158657776_612x612;
        private readonly Image ReservedImage = global::DuAnDauDoi.Properties.Resources.dadat;
        private readonly Image OccupiedImage = global::DuAnDauDoi.Properties.Resources.cokhach;

        public FormBan()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            this.Load += (s, e) => CreateSeats();
        }

        private void CreateSeats()
        {
            flowLayoutPanel1.Controls.Clear();
            var tables = _banService.GetAll();
            var sortedTables = tables
                .OrderBy(t => {
                    int result;
                    return int.TryParse(t.Soban, out result) ? result : 0;
                })
                .ToList();

            foreach (var table in sortedTables) 
            {
                Button seatButton = new Button
                {
                    Width = 150,
                    Height = 150,
                    Text = $"{table.Soban}",
                    Tag = table.Maban,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Margin = new Padding(10),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    FlatStyle = FlatStyle.Flat
                };
                SetButtonColorByStatus(seatButton, table.Status);
                seatButton.Click += SeatButton_Click;
                flowLayoutPanel1.Controls.Add(seatButton);
            }
        }

        private void SetButtonColorByStatus(Button btn, string status)
        {
            if (status == "Trống")
            {
                btn.BackgroundImage = AvailableImage;
                btn.ForeColor = Color.Black;
            }
            else if (status == "Đã đặt bàn")
            {
                btn.BackgroundImage = ReservedImage;
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackgroundImage = OccupiedImage;
                btn.ForeColor = Color.White;
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // 1. Xử lý logic chọn/bỏ chọn UI (giữ nguyên logic cũ của bạn)
            if (_currentSelectedButton == clickedButton)
            {
                ResetSelection();
                return;
            }

            if (_currentSelectedButton != null)
            {
                RefreshButtonAppearance(_currentSelectedButton);
            }
            _currentSelectedButton = clickedButton;

            // 2. Lấy thông tin bàn từ Tag
            int tableId = (int)clickedButton.Tag;
            var table = _banService.GetById(tableId);
            if (table == null) return;


            // 3. LOGIC QUAN TRỌNG: Kiểm tra hóa đơn để mở Form tương ứng
            // Chúng ta sử dụng hàm GetUnpaidInvoiceByTable mà bạn đã dùng trong FormSua
            var currentHoadon = _hoadonService.GetUnpaidInvoiceByTable(table.Maban);

            if (currentHoadon == null)
            {
                // TRƯỜNG HỢP 1: Chưa có hóa đơn -> Mở Form Gọi Món
                FormGoiMon fGoi = new FormGoiMon(table);
                if (fGoi.ShowDialog() == DialogResult.OK)
                {
                    RefreshButtonAppearance(clickedButton);
                }
            }
            else
            {
                // TRƯỜNG HỢP 2: Đã có hóa đơn -> Mở Form Sửa Món
                FormSua fSua = new FormSua(table);
                if (fSua.ShowDialog() == DialogResult.OK)
                {
                    RefreshButtonAppearance(clickedButton);
                }
            }

            // Sau khi đóng form, có thể reset selection hoặc giữ nguyên tùy bạn
            // ResetSelection(); 
        }

        // Cập nhật diện mạo bàn từ Database
        private void RefreshButtonAppearance(Button btn)
        {
            if (btn == null) return;
            int tableId = (int)btn.Tag;
            var table = _banService.GetById(tableId);
            if (table != null)
            {
                btn.Text = $"{table.Soban}";
                SetButtonColorByStatus(btn, table.Status);
            }
        }

        private void ResetSelection()
        {
            if (_currentSelectedButton != null)
            {
                RefreshButtonAppearance(_currentSelectedButton);
                _currentSelectedButton = null;
            }
        }

        private void ExecuteTableAction(Action<Ban> action)
        {
            int tableId = (int)_currentSelectedButton.Tag;
            var table = _banService.GetById(tableId);
            if (table != null)
            {
                action(table);
            }
        }

        private void lịchSửToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLichsu f = new FormLichsu();
            f.ShowDialog();
        }

        private void đặtBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = InputDialog.Show("Đặt Bàn", "Nhập Mã bàn (Maban) cần đặt:");
            if (string.IsNullOrEmpty(input)) return;

            if (!int.TryParse(input, out int tableId))
            {
                MessageBox.Show("Mã bàn phải là số!");
                return;
            }

            var table = _banService.GetById(tableId);

            if (table == null)
            {
                MessageBox.Show("Bàn không tồn tại!");
                return;
            }

            // Kiểm tra hóa đơn
            var hoadon = _hoadonService.GetUnpaidInvoiceByTable(table.Maban);
            if (hoadon != null)
            {
                 MessageBox.Show("Bàn này đang có hóa đơn chưa thanh toán, không thể đặt!");
                 return;
            }

            if (table.Status == "Trống")
            {
                table.Status = "Đã đặt bàn";
                _banService.UpdateStatus(table.Maban, "Đã đặt bàn");
                MessageBox.Show($"Đã đặt bàn số {table.Soban} (Mã: {table.Maban}) thành công!");
            }
            else if (table.Status == "Đã đặt bàn")
            {
                 table.Status = "Trống";
                 _banService.UpdateStatus(table.Maban, "Trống");
                 MessageBox.Show($"Đã hủy đặt bàn số {table.Soban} (Mã: {table.Maban}) thành công!");
            }
            else
            {
                MessageBox.Show($"Bàn đang ở trạng thái '{table.Status}', không thể đặt!");
                return;
            }
            
            // Refresh UI
            CreateSeats();
        }

        private void gộpBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Nhập bàn chuyển đi
            string sourceInput = InputDialog.Show("Gộp Bàn", "Nhập Mã bàn CẦN CHUYỂN (Nguồn):");
            if (string.IsNullOrEmpty(sourceInput)) return;

            // 2. Nhập bàn chuyển tới
            string destInput = InputDialog.Show("Gộp Bàn", "Nhập Mã bàn CHUYỂN TỚI (Đích):");
            if (string.IsNullOrEmpty(destInput)) return;

            if (sourceInput == destInput)
            {
                MessageBox.Show("Hai bàn phải khác nhau!");
                return;
            }

            if (!int.TryParse(sourceInput, out int sourceId) || !int.TryParse(destInput, out int destId))
            {
                 MessageBox.Show("Mã bàn phải là số!");
                 return;
            }

            var sourceTable = _banService.GetById(sourceId);
            var destTable = _banService.GetById(destId);

            if (sourceTable == null || destTable == null)
            {
                MessageBox.Show("Một trong hai bàn không tồn tại!");
                return;
            }

            try 
            {
                _hoadonService.MergeTable(sourceTable.Maban, destTable.Maban);
                CreateSeats();
                MessageBox.Show($"Đã gộp bàn {sourceTable.Soban} vào bàn {destTable.Soban} thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gộp bàn: " + ex.Message);
            }
        }
    }
}