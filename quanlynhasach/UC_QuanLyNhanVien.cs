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
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string chucVu = cboChucVu.Text;

            // 1. Check rỗng
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin!", "Cảnh báo"); return;
            }

            // 2. CHECK TRÙNG SĐT HOẶC TÀI KHOẢN TRƯỚC KHI LƯU
            if (nvController.KiemTraTrung(taiKhoan, sdt))
            {
                MessageBox.Show("Tài khoản hoặc Số điện thoại này đã được sử dụng cho một nhân viên khác! Vui lòng chọn cái khác.", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng lại luôn, không chạy xuống lệnh Add nữa
            }

            // 3. Nếu qua ải thì lưu vào DB
            NhanVien nv = new NhanVien { HoTen = hoTen, SoDienThoai = sdt, TaiKhoan = taiKhoan, MatKhau = matKhau, ChucVu = chucVu };
            if (nvController.AddNhanVien(nv))
            {
                MessageBox.Show("Thêm thành công!");
                LoadData(); // Load lại bảng
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaNV.Text)) return;

            int maNVHienTai = int.Parse(lblMaNV.Text); // Mã của nhân viên đang chọn trên bảng

            // Lấy toàn bộ dữ liệu từ các ô nhập liệu
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string chucVu = cboChucVu.Text;

            // 1. Check rỗng
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin!", "Cảnh báo"); return;
            }

            // 2. Check trùng (loại trừ chính nhân viên này ra)
            if (nvController.KiemTraTrung(taiKhoan, sdt, maNVHienTai))
            {
                MessageBox.Show("Tài khoản hoặc Số điện thoại này đã bị trùng với một người khác!", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. ĐOẠN BỊ THIẾU CỦA BẠN ĐÂY: Lưu xuống DB
            NhanVien nv = new NhanVien
            {
                MaNV = maNVHienTai,
                HoTen = hoTen,
                SoDienThoai = sdt,
                TaiKhoan = taiKhoan,
                MatKhau = matKhau,
                ChucVu = chucVu
            };

            if (nvController.UpdateNhanVien(nv))
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Load lại bảng để thấy chữ thay đổi
                btnLamMoi_Click(null, null); // Xóa trắng các ô TextBox sau khi sửa xong
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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