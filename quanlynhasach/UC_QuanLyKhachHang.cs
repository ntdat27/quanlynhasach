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

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
        }

        private void SetupComboBox()
        {
            // Dictionary để gán Tên hạng đi kèm với Mã hạng
            Dictionary<int, string> hangThanhVien = new Dictionary<int, string>();
            hangThanhVien.Add(1, "Khách vãng lai");
            hangThanhVien.Add(2, "Khách hàng thân thiết");
            hangThanhVien.Add(3, "Thành viên CLB");

            cboHangThanhVien.DataSource = new BindingSource(hangThanhVien, null);
            cboHangThanhVien.DisplayMember = "Value"; // Hiện tên
            cboHangThanhVien.ValueMember = "Key";   // Lưu mã ngầm
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

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

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