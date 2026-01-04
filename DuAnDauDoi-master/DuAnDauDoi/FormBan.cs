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

            // Cập nhật nhãn nút Đặt bàn (giữ nguyên logic cũ)
            btnDb.Text = (table.Status == "Đã đặt bàn") ? "🕛 Hủy Đặt" : "🕛 Đặt Bàn";

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
            btnDb.Text = "🕛 Đặt Bàn";
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
    }
}