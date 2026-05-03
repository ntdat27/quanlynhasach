using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using quanlynhasach.Controllers;
using quanlynhasach.Models;

namespace quanlynhasach
{
    public partial class UC_QuanLyKhachHang : UserControl
    {
        private KhachHangController khController = new KhachHangController();

        public UC_QuanLyKhachHang()
        {
            InitializeComponent();

            // Cài đặt các option cho ComboBox Hạng Thành Viên
            SetupComboBox();

            FormatDataGridView(dgvKhachHang);
            SetupColumns();
            LoadData();
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
        }

        private void SetupComboBox()
        {
            // Dictionary để gán Tên hạng đi kèm với Mã hạng
            quanlynhasach.Data.DatabaseHelper db = new quanlynhasach.Data.DatabaseHelper();
            System.Data.DataTable dt = db.ExecuteQuery("SELECT MaHang, TenHang FROM HangThanhVien");

            cboHangThanhVien.DataSource = dt;
            cboHangThanhVien.DisplayMember = "TenHang"; // Hiển thị Chữ
            cboHangThanhVien.ValueMember = "MaHang";    // Lưu ngầm Số
        }

        #region UI & Dữ liệu
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
            dgvKhachHang.Columns.Clear();
            dgvKhachHang.Columns.Add("MaKH", "Mã");
            dgvKhachHang.Columns["MaKH"].Width = 50;

            dgvKhachHang.Columns.Add("HoTen", "Họ tên");
            dgvKhachHang.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvKhachHang.Columns.Add("SoDienThoai", "SĐT");
            dgvKhachHang.Columns["SoDienThoai"].Width = 120;

            dgvKhachHang.Columns.Add("DiemTichLuy", "Điểm");
            dgvKhachHang.Columns["DiemTichLuy"].Width = 80;

            dgvKhachHang.Columns.Add("TenHang", "Hạng Thành Viên");
            dgvKhachHang.Columns["TenHang"].Width = 180;

            dgvKhachHang.Columns.Add("PhanTramGiam", "Giảm (%)");
            dgvKhachHang.Columns["PhanTramGiam"].Width = 80;
        }

        private void LoadData()
        {
            try
            {
                dgvKhachHang.Rows.Clear();
                List<KhachHang> ds = khController.GetAllKhachHang();
                foreach (var kh in ds)
                {
                    dgvKhachHang.Rows.Add(kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.DiemTichLuy, kh.TenHang, kh.PhanTramGiam);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        #endregion

        #region Logic CRUD
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaKH.Text = "";
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiemTichLuy.Clear();
            cboHangThanhVien.SelectedIndex = 0; // Mặc định về Khách vãng lai
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!"); return;
            }

            // 1. MỚI: RÀO CHẮN BẮT BUỘC SĐT PHẢI ĐÚNG 10 SỐ (Chỉ chứa số)
            if (sdt.Length != 10 || !System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập đúng 10 chữ số (ví dụ: 0987654321).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. CHECK TRÙNG SĐT
            if (khController.KiemTraTrungSdt(sdt))
            {
                MessageBox.Show("Số điện thoại này đã được đăng ký cho một khách hàng khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int diem = 0;
            int.TryParse(txtDiemTichLuy.Text, out diem);

            KhachHang kh = new KhachHang
            {
                HoTen = hoTen,
                SoDienThoai = sdt,
                DiemTichLuy = diem,
                // Cách mới để lấy Mã Hạng ẩn bên dưới ComboBox
                MaHang = Convert.ToInt32(cboHangThanhVien.SelectedValue)
            };

            if (khController.AddKhachHang(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaKH.Text)) return;

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            int maKHHienTai = int.Parse(lblMaKH.Text);

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!"); return;
            }

            // 1. MỚI: Check form SĐT 10 số
            if (sdt.Length != 10 || !System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập đúng 10 chữ số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check trùng (Loại trừ chính khách hàng đang được sửa ra)
            if (khController.KiemTraTrungSdt(sdt, maKHHienTai))
            {
                MessageBox.Show("Số điện thoại này đã được đăng ký cho một khách hàng khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int diem = 0;
            int.TryParse(txtDiemTichLuy.Text, out diem);

            KhachHang kh = new KhachHang
            {
                MaKH = maKHHienTai,
                HoTen = hoTen,
                SoDienThoai = sdt,
                DiemTichLuy = diem,
                // Lấy Mã Hạng ẩn bên dưới ComboBox
                MaHang = Convert.ToInt32(cboHangThanhVien.SelectedValue)
            };

            if (khController.UpdateKhachHang(kh))
            {
                MessageBox.Show("Cập nhật thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaKH.Text)) return;

            if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                khController.DeleteKhachHang(int.Parse(lblMaKH.Text));
                btnLamMoi_Click(null, null);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                lblMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtDiemTichLuy.Text = row.Cells["DiemTichLuy"].Value.ToString();

                // Chọn lại giá trị cho ComboBox dựa vào Tên hạng
                cboHangThanhVien.Text = row.Cells["TenHang"].Value.ToString();
            }
        }
        #endregion
    }
}