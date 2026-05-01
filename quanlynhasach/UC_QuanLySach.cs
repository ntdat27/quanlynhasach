using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using quanlynhasach.Controllers;
using quanlynhasach.Models;

namespace quanlynhasach
{
    public partial class UC_QuanLySach : UserControl
    {
        private SachController sachController = new SachController();

        public UC_QuanLySach()
        {
            InitializeComponent();

            // Làm đẹp và cấu hình bảng ngay khi khởi tạo
            FormatDataGridView(dgvDanhSachSach);
            SetupColumns();
            LoadData();

            // Gắn sự kiện click vào dòng để hiện lên textbox
            dgvDanhSachSach.CellClick += dgvDanhSachSach_CellClick;
        }

        #region UI Cấu hình
        private void FormatDataGridView(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 35;

            // Màu tiêu đề
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 63, 159);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        private void SetupColumns()
        {
            dgvDanhSachSach.Columns.Clear();
            dgvDanhSachSach.Columns.Add("MaSach", "Mã");
            dgvDanhSachSach.Columns["MaSach"].Width = 60;

            dgvDanhSachSach.Columns.Add("TenSach", "Tên sách");
            dgvDanhSachSach.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDanhSachSach.Columns.Add("SoLuongTon", "Tồn kho");
            dgvDanhSachSach.Columns["SoLuongTon"].Width = 100;

            dgvDanhSachSach.Columns.Add("GiaBan", "Giá bán");
            dgvDanhSachSach.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvDanhSachSach.Columns["GiaBan"].Width = 120;
        }

        private void LoadData()
        {
            try
            {
                dgvDanhSachSach.Rows.Clear();
                List<Sach> ds = sachController.GetAllSach();
                foreach (var s in ds)
                {
                    dgvDanhSachSach.Rows.Add(s.MaSach, s.TenSach, s.SoLuongTon, s.GiaBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        #endregion

        #region Sự kiện Nút bấm (CRUD)

        // 1. LÀM MỚI (Reset form)
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaSach.Text = ""; // Xóa mã đang chọn
            txtTenSach.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            LoadData();
        }

        // 2. THÊM SÁCH
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sách!");
                return;
            }

            Sach s = new Sach
            {
                TenSach = txtTenSach.Text.Trim(),
                GiaBan = int.Parse(txtGiaBan.Text),
                SoLuongTon = int.Parse(txtSoLuong.Text)
            };

            if (sachController.AddSach(s))
            {
                MessageBox.Show("Thêm sách thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        // 3. SỬA SÁCH
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaSach.Text))
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách từ danh sách bên dưới!");
                return;
            }

            Sach s = new Sach
            {
                MaSach = int.Parse(lblMaSach.Text),
                TenSach = txtTenSach.Text.Trim(),
                GiaBan = int.Parse(txtGiaBan.Text),
                SoLuongTon = int.Parse(txtSoLuong.Text)
            };

            if (sachController.UpdateSach(s))
            {
                MessageBox.Show("Cập nhật thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        // 4. XÓA SÁCH
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaSach.Text)) return;

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (sachController.DeleteSach(int.Parse(lblMaSach.Text)))
                {
                    MessageBox.Show("Đã xóa thành công!");
                    btnLamMoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Không thể xóa sách này (có thể do vướng lịch sử hóa đơn)!");
                }
            }
        }

        // KHI CLICK VÀO BẢNG: Đẩy dữ liệu ngược lên các ô nhập liệu
        private void dgvDanhSachSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSach.Rows[e.RowIndex];
                lblMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuongTon"].Value.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value.ToString();
            }
        }
        #endregion
    }
}