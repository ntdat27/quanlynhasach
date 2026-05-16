using ClosedXML.Excel;
using quanlynhasach.Controllers;
using quanlynhasach.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace quanlynhasach
{
    public partial class UC_QuanLyNhanVien : UserControl
    {
        private NhanVienController nvController = new NhanVienController();

        public UC_QuanLyNhanVien()
        {
            InitializeComponent();

            cboChucVu.Items.Clear();
            cboChucVu.Items.Add("Admin");
            cboChucVu.Items.Add("Nhân viên");
            cboChucVu.SelectedIndex = 0; 


            FormatDataGridView(dgvNhanVien);
            SetupColumns();
            LoadData();

         
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
        private DataTable ConvertDgvToDataTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                {
                    dt.Columns.Add(col.HeaderText);
                }
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow) 
                {
                    DataRow dr = dt.NewRow();
                    int colIndex = 0;
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            dr[colIndex] = row.Cells[i].Value ?? "";
                            colIndex++;
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.Rows.Count == 0) return;

            DataTable dtExport = ConvertDgvToDataTable(dgvNhanVien);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.FileName = "NhanVien_" + DateTime.Now.ToString("ddMMyyyy");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Nhân Viên");

                        worksheet.Cell("A1").Value = "DANH SÁCH NHÂN VIÊN NHÀ SÁCH";
                        worksheet.Range("A1:E1").Merge();
                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("A1").Style.Font.FontSize = 16;
                        worksheet.Cell("A1").Style.Font.FontColor = XLColor.White;
                        worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.DarkGreen; 
                        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        var table = worksheet.Cell(3, 1).InsertTable(dtExport);
                        table.Theme = XLTableTheme.TableStyleMedium4;

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất danh sách Nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoaTen = txtHoTen.Text.Trim();
            string tuKhoaSdt = txtSDT.Text.Trim(); 

            DataTable dtKetQua = nvController.SearchNhanVien(tuKhoaTen, tuKhoaSdt);

            dgvNhanVien.Rows.Clear();

            foreach (DataRow row in dtKetQua.Rows)
            {
                dgvNhanVien.Rows.Add(
                    row["MaNV"],
                    row["HoTen"],
                    row["SoDienThoai"],
                    row["TaiKhoan"],
                    row["MatKhau"],
                    row["ChucVu"]
                );
            }
        }
    }
}