using quanlynhasach.Controllers;
using quanlynhasach.Data;
using quanlynhasach.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace quanlynhasach
{
    public partial class UC_QuanLySach : UserControl
    {
        private SachController sachController = new SachController();

        public UC_QuanLySach()
        {
            InitializeComponent();
            LoadComboBoxes();
            FormatDataGridView(dgvDanhSachSach);
            SetupColumns();
            LoadData();

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

            // Thêm các cột ẩn (hoặc hiện tùy bạn) để lấy dữ liệu khi click
            dgvDanhSachSach.Columns.Add("MaTL", "Mã TL");
            dgvDanhSachSach.Columns["MaTL"].Visible = false;

            dgvDanhSachSach.Columns.Add("MaTG", "Mã TG");
            dgvDanhSachSach.Columns["MaTG"].Visible = false;

            dgvDanhSachSach.Columns.Add("MaNXB", "Mã NXB");
            dgvDanhSachSach.Columns["MaNXB"].Visible = false;

            dgvDanhSachSach.Columns.Add("NamXB", "Năm XB");
            dgvDanhSachSach.Columns["NamXB"].Width = 80;

            dgvDanhSachSach.Columns.Add("GiaNhap", "Giá nhập");
            dgvDanhSachSach.Columns["GiaNhap"].Visible = false; // Có thể ẩn đi cho gọn

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
                    // Chú ý thứ tự add phải khớp với thứ tự add Columns ở trên
                    dgvDanhSachSach.Rows.Add(s.MaSach, s.TenSach, s.MaTL, s.MaTG, s.MaNXB, s.NamXB, s.GiaNhap, s.SoLuongTon, s.GiaBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        #endregion

        #region Sự kiện Nút bấm (CRUD)

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaSach.Text = "";
            txtTenSach.Clear();
            cboTheLoai.SelectedIndex = -1;
            cboTacGia.SelectedIndex = -1;
            cboNXB.SelectedIndex = -1;
            txtNamXB.Clear();
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            LoadData();
        }

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
                MaTL = Convert.ToInt32(cboTheLoai.SelectedValue),
                MaTG = Convert.ToInt32(cboTacGia.SelectedValue),
                MaNXB = Convert.ToInt32(cboNXB.SelectedValue),
                NamXB = string.IsNullOrEmpty(txtNamXB.Text) ? 0 : int.Parse(txtNamXB.Text),
                GiaNhap = string.IsNullOrEmpty(txtGiaNhap.Text) ? 0 : int.Parse(txtGiaNhap.Text),
                GiaBan = string.IsNullOrEmpty(txtGiaBan.Text) ? 0 : int.Parse(txtGiaBan.Text),
                SoLuongTon = string.IsNullOrEmpty(txtSoLuong.Text) ? 0 : int.Parse(txtSoLuong.Text)
            };

            if (sachController.AddSach(s))
            {
                MessageBox.Show("Thêm sách thành công!");
                btnLamMoi_Click(null, null);
            }
            else MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại kiểu dữ liệu!");
        }

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
                MaTL = Convert.ToInt32(cboTheLoai.SelectedValue),
                MaTG = Convert.ToInt32(cboTacGia.SelectedValue),
                MaNXB = Convert.ToInt32(cboNXB.SelectedValue),
                NamXB = string.IsNullOrEmpty(txtNamXB.Text) ? 0 : int.Parse(txtNamXB.Text),
                GiaNhap = string.IsNullOrEmpty(txtGiaNhap.Text) ? 0 : int.Parse(txtGiaNhap.Text),
                GiaBan = string.IsNullOrEmpty(txtGiaBan.Text) ? 0 : int.Parse(txtGiaBan.Text),
                SoLuongTon = string.IsNullOrEmpty(txtSoLuong.Text) ? 0 : int.Parse(txtSoLuong.Text)
            };

            if (sachController.UpdateSach(s))
            {
                MessageBox.Show("Cập nhật thành công!");
                btnLamMoi_Click(null, null);
            }
        }

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

        private void dgvDanhSachSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSach.Rows[e.RowIndex];
                lblMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value?.ToString();

                // Đổ ngược dữ liệu vào text box
                cboTheLoai.SelectedValue = row.Cells["MaTL"].Value;
                cboTacGia.SelectedValue = row.Cells["MaTG"].Value;
                cboNXB.SelectedValue = row.Cells["MaNXB"].Value;
                txtNamXB.Text = row.Cells["NamXB"].Value?.ToString();
                txtGiaNhap.Text = row.Cells["GiaNhap"].Value?.ToString();

                txtSoLuong.Text = row.Cells["SoLuongTon"].Value?.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString();
            }
        }
        private void LoadComboBoxes()
        {
            DatabaseHelper db = new DatabaseHelper();

            // 1. Đổ dữ liệu Thể Loại
            DataTable dtTL = db.ExecuteQuery("SELECT MaTL, TenTL FROM theloai");
            cboTheLoai.DataSource = dtTL;
            cboTheLoai.DisplayMember = "TenTL"; // Hiển thị Chữ
            cboTheLoai.ValueMember = "MaTL";    // Lưu ngầm Số

            // 2. Đổ dữ liệu Tác Giả
            DataTable dtTG = db.ExecuteQuery("SELECT MaTG, TenTG FROM tacgia");
            cboTacGia.DataSource = dtTG;
            cboTacGia.DisplayMember = "TenTG";
            cboTacGia.ValueMember = "MaTG";

            // 3. Đổ dữ liệu NXB
            DataTable dtNXB = db.ExecuteQuery("SELECT MaNXB, TenNXB FROM nhaxuatban");
            cboNXB.DataSource = dtNXB;
            cboNXB.DisplayMember = "TenNXB";
            cboNXB.ValueMember = "MaNXB";
        }
        #endregion
    }
}