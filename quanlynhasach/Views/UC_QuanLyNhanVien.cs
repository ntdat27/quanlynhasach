using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using quanlynhasach.Controllers;
using quanlynhasach.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
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

            // Cột mật khẩu ẩn đi hoàn toàn khỏi tầm mắt nhưng vẫn giữ giá trị hash ngầm để lôi lại khi Sửa
            dgvNhanVien.Columns.Add("MatKhau", "Mật khẩu");
            dgvNhanVien.Columns["MatKhau"].Visible = false;

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
                    dgvNhanVien.Rows.Add(nv.MaNV, nv.HoTen, nv.SoDienThoai, nv.TaiKhoan, nv.MatKhau, nv.ChucVu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private string ChuanHoaHoTen(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            text = Regex.Replace(text.Trim(), @"\s+", " ");
            TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
            return textInfo.ToTitleCase(text.ToLower());
        }
        #endregion

        #region Logic Nút bấm (CRUD)
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaNV.Text = "";
            txtHoTen.Clear();
            txtSDT.Clear();
            txtTaiKhoan.Clear();

            // THAY ĐỔI: Xóa chữ và MỞ KHÓA lại ô mật khẩu để nhập cho nhân viên mới hoàn toàn
            txtMatKhau.Clear();
            txtMatKhau.Enabled = true;

            cboChucVu.SelectedIndex = 0;
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTen = ChuanHoaHoTen(txtHoTen.Text);
            string sdt = txtSDT.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string chucVu = cboChucVu.Text;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin họ tên, tài khoản và mật khẩu!", "Cảnh báo");
                return;
            }

            if (taiKhoan.Contains(" "))
            {
                MessageBox.Show("Tài khoản đăng nhập không được chứa khoảng trắng!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Phải bao gồm đúng 10 chữ số và bắt đầu bằng số 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nvController.KiemTraTrung(taiKhoan, sdt))
            {
                MessageBox.Show("Tài khoản hoặc Số điện thoại này đã được sử dụng cho một nhân viên khác! Vui lòng chọn cái khác.", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NhanVien nv = new NhanVien { HoTen = hoTen, SoDienThoai = sdt, TaiKhoan = taiKhoan, MatKhau = matKhau, ChucVu = chucVu };
            if (nvController.AddNhanVien(nv))
            {
                LichSuController.GhiLog(Session.MaNV, $"Thêm mới nhân viên: {hoTen} ({taiKhoan})");
                MessageBox.Show("Thêm nhân viên mới thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaNV.Text)) return;

            int maNVHienTai = int.Parse(lblMaNV.Text);

            string hoTen = ChuanHoaHoTen(txtHoTen.Text);
            string sdt = txtSDT.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string chucVu = cboChucVu.Text;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin Họ tên và Tài khoản!", "Cảnh báo");
                return;
            }

            if (taiKhoan.Contains(" "))
            {
                MessageBox.Show("Tài khoản đăng nhập không được chứa khoảng trắng!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Phải bao gồm đúng 10 số và bắt đầu bằng số 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nvController.KiemTraTrung(taiKhoan, sdt, maNVHienTai))
            {
                MessageBox.Show("Tài khoản hoặc Số điện thoại này đã bị trùng với một người khác!", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // THAY ĐỔI: Vì ô mật khẩu đã bị khóa cứng (Enabled = false) nên khi bấm Sửa, 
            // hệ thống mặc định lôi lại chuỗi mã hóa mật khẩu cũ đang có sẵn ở dòng DataGridView để nộp lên DB
            string matKhauGiuNguyen = dgvNhanVien.CurrentRow.Cells["MatKhau"].Value.ToString();

            NhanVien nv = new NhanVien
            {
                MaNV = maNVHienTai,
                HoTen = hoTen,
                SoDienThoai = sdt,
                TaiKhoan = taiKhoan,
                MatKhau = matKhauGiuNguyen, // Giữ nguyên, không đổi mật khẩu ở form này
                ChucVu = chucVu
            };

            if (nvController.UpdateNhanVien(nv))
            {
                LichSuController.GhiLog(Session.MaNV, $"Cập nhật thông tin nhân viên có ID: {maNVHienTai}");
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaNV.Text)) return;
            int maNVCanXoa = int.Parse(lblMaNV.Text);

            if (maNVCanXoa == Session.MaNV)
            {
                MessageBox.Show("Bạn không thể tự xóa tài khoản của chính mình khi đang đăng nhập hệ thống!", "Cảnh báo bảo vệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (nvController.DeleteNhanVien(maNVCanXoa))
                {
                    LichSuController.GhiLog(Session.MaNV, $"Xóa nhân viên có ID: {maNVCanXoa}");
                    MessageBox.Show("Đã xóa nhân viên thành công!");
                    btnLamMoi_Click(null, null);
                }
            }
        }

        // Khi click vào dòng trong bảng dgvNhanVien
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                lblMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                cboChucVu.SelectedItem = row.Cells["ChucVu"].Value.ToString();

                // THAY ĐỔI: Khóa cứng và làm xám/đen ô mật khẩu lại để chặn không cho nhập sửa lung tung
                txtMatKhau.Text = "";
                txtMatKhau.Enabled = false;
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