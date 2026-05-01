using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using quanlynhasach.Controllers;
using quanlynhasach.Models;

namespace quanlynhasach
{
    public partial class UC_QuanLyNhanVien : UserControl
    {
        private NhanVienController nvController = new NhanVienController();

        public UC_QuanLyNhanVien()
        {
            InitializeComponent();

            // Tự động thêm các option cho ComboBox Chức vụ
            cboChucVu.Items.Clear();
            cboChucVu.Items.Add("Admin");
            cboChucVu.Items.Add("Nhân viên");
            cboChucVu.SelectedIndex = 0; // Chọn mặc định là Admin

            FormatDataGridView(dgvNhanVien);
            SetupColumns();
            LoadData();

            // KÍCH HOẠT NÚT BẤM BẰNG CODE
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
        }

        #region Làm đẹp Giao diện
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
            dgvNhanVien.Columns.Clear();
            dgvNhanVien.Columns.Add("MaNV", "Mã");
            dgvNhanVien.Columns["MaNV"].Width = 50;

            dgvNhanVien.Columns.Add("HoTen", "Họ và tên");
            dgvNhanVien.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvNhanVien.Columns.Add("SoDienThoai", "SĐT");
            dgvNhanVien.Columns["SoDienThoai"].Width = 120;

            dgvNhanVien.Columns.Add("TaiKhoan", "Tài khoản");
            dgvNhanVien.Columns["TaiKhoan"].Width = 150;

            // Cột mật khẩu thực tế không nên hiển thị, nhưng nếu hiển thị thì thay bằng dấu sao ***
            dgvNhanVien.Columns.Add("MatKhau", "Mật khẩu");
            dgvNhanVien.Columns["MatKhau"].Visible = false; // Ẩn luôn cho bảo mật

            dgvNhanVien.Columns.Add("ChucVu", "Chức vụ");
            dgvNhanVien.Columns["ChucVu"].Width = 120;
        }

        private void LoadData()
        {
            try
            {
                dgvNhanVien.Rows.Clear();
                List<NhanVien> ds = nvController.GetAllNhanVien();
                foreach (var nv in ds)
                {
                    // Truyền đủ các cột tương ứng
                    dgvNhanVien.Rows.Add(nv.MaNV, nv.HoTen, nv.SoDienThoai, nv.TaiKhoan, nv.MatKhau, nv.ChucVu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        #endregion

        #region Logic Nút bấm (CRUD)
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaNV.Text = "";
            txtHoTen.Clear();
            txtSDT.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            cboChucVu.SelectedIndex = 0;
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTaiKhoan.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Tài khoản và mật khẩu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NhanVien nv = new NhanVien
            {
                HoTen = txtHoTen.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                TaiKhoan = txtTaiKhoan.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                ChucVu = cboChucVu.SelectedItem.ToString()
            };

            if (nvController.AddNhanVien(nv))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa từ bảng bên dưới!");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNV = int.Parse(lblMaNV.Text),
                HoTen = txtHoTen.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                TaiKhoan = txtTaiKhoan.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                ChucVu = cboChucVu.SelectedItem.ToString()
            };

            if (nvController.UpdateNhanVien(nv))
            {
                MessageBox.Show("Cập nhật thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaNV.Text)) return;

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (nvController.DeleteNhanVien(int.Parse(lblMaNV.Text)))
                {
                    MessageBox.Show("Đã xóa nhân viên!");
                    btnLamMoi_Click(null, null);
                }
            }
        }

        // Khi click vào dòng trong bảng
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                lblMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString(); // Kéo mật khẩu lên lại ô textbox để sửa
                cboChucVu.SelectedItem = row.Cells["ChucVu"].Value.ToString();
            }
        }
        #endregion
    }
}