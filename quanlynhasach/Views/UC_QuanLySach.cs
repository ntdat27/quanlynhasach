using quanlynhasach.Controllers;
using quanlynhasach.Data;
using quanlynhasach.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace quanlynhasach
{
    public partial class UC_QuanLySach : UserControl
    {
        private SachController sachController = new SachController();

        public UC_QuanLySach()
        {
            InitializeComponent();
            FormatDataGridView(dgvDanhSachSach);
            SetupColumns();
            LoadData();
            dgvDanhSachSach.CellClick += dgvDanhSachSach_CellClick;
            KiemTraPhanQuyen();
        }

        private void KiemTraPhanQuyen()
        {
            // Gọi trực tiếp biến tĩnh Session.MaNV và Session.ChucVu
            if (quanlynhasach.Models.Session.MaNV > 0)
            {
                string role = quanlynhasach.Models.Session.ChucVu;
                bool laQuanLy = (role == "Admin" || role == "Quản lý kho");

                btnThem.Enabled = laQuanLy;
                btnSua.Enabled = laQuanLy;
                btnXoa.Enabled = laQuanLy;
                btnNhapHang.Enabled = laQuanLy;
            }
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
            dgvDanhSachSach.Columns.Add("MaTL", "Mã TL");
            dgvDanhSachSach.Columns["MaTL"].Visible = false;
            dgvDanhSachSach.Columns.Add("MaTG", "Mã TG");
            dgvDanhSachSach.Columns["MaTG"].Visible = false;
            dgvDanhSachSach.Columns.Add("MaNXB", "Mã NXB");
            dgvDanhSachSach.Columns["MaNXB"].Visible = false;
            dgvDanhSachSach.Columns.Add("NamXB", "Năm XB");
            dgvDanhSachSach.Columns["NamXB"].Width = 80;
            dgvDanhSachSach.Columns.Add("GiaNhap", "Giá nhập");
            dgvDanhSachSach.Columns["GiaNhap"].Visible = false;
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
                    dgvDanhSachSach.Rows.Add(s.MaSach, s.TenSach, s.MaTL, s.MaTG, s.MaNXB, s.NamXB, s.GiaNhap, s.SoLuongTon, s.GiaBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        #endregion

        #region Hàm Hỗ Trợ Xử Lý DB
        // Tự động kiểm tra và thêm mới Danh mục nếu người dùng gõ nội dung chưa có
        private int GetOrInsertValue(string tableName, string nameColumn, string idColumn, string textValue)
        {
            if (string.IsNullOrWhiteSpace(textValue)) return 0;
            DatabaseHelper db = new DatabaseHelper();
            string safeText = textValue.Trim().Replace("'", "''");
            string checkQuery = $"SELECT {idColumn} FROM {tableName} WHERE {nameColumn} = N'{safeText}'";
            DataTable dt = db.ExecuteQuery(checkQuery);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][idColumn]);
            }
            else
            {
                string insertQuery = $"INSERT INTO {tableName} ({nameColumn}) VALUES (N'{safeText}'); SELECT LAST_INSERT_ID();";
                return db.ExecuteScalar(insertQuery);
            }
        }

        // Lấy ngược Tên chữ từ Mã ID để điền lên TextBox khi click vào Bảng
        private string GetNameFromDB(string tableName, string nameCol, string idCol, int id)
        {
            if (id <= 0) return "";
            DatabaseHelper db = new DatabaseHelper();
            DataTable dt = db.ExecuteQuery($"SELECT {nameCol} FROM {tableName} WHERE {idCol} = {id}");
            if (dt.Rows.Count > 0) return dt.Rows[0][0].ToString();
            return "";
        }
        #endregion

        #region Sự kiện Nút bấm (CRUD)
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaSach.Text = "";
            txtTenSach.Clear();
            txtTheLoai.Clear();
            txtTacGia.Clear();
            txtNXB.Clear();
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
                MaTL = GetOrInsertValue("theloai", "TenTL", "MaTL", txtTheLoai.Text),
                MaTG = GetOrInsertValue("tacgia", "TenTG", "MaTG", txtTacGia.Text),
                MaNXB = GetOrInsertValue("nhaxuatban", "TenNXB", "MaNXB", txtNXB.Text),
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
                MaTL = GetOrInsertValue("theloai", "TenTL", "MaTL", txtTheLoai.Text),
                MaTG = GetOrInsertValue("tacgia", "TenTG", "MaTG", txtTacGia.Text),
                MaNXB = GetOrInsertValue("nhaxuatban", "TenNXB", "MaNXB", txtNXB.Text),
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

        private DataTable ConvertDgvToDataTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible) dt.Columns.Add(col.HeaderText);
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

        private void dgvDanhSachSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachSach.Rows[e.RowIndex];
                lblMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value?.ToString();

                txtTheLoai.Text = GetNameFromDB("theloai", "TenTL", "MaTL", Convert.ToInt32(row.Cells["MaTL"].Value ?? 0));
                txtTacGia.Text = GetNameFromDB("tacgia", "TenTG", "MaTG", Convert.ToInt32(row.Cells["MaTG"].Value ?? 0));
                txtNXB.Text = GetNameFromDB("nhaxuatban", "TenNXB", "MaNXB", Convert.ToInt32(row.Cells["MaNXB"].Value ?? 0));

                txtNamXB.Text = row.Cells["NamXB"].Value?.ToString();
                txtGiaNhap.Text = row.Cells["GiaNhap"].Value?.ToString();
                txtSoLuong.Text = row.Cells["SoLuongTon"].Value?.ToString();
                txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString();
            }
        }
        #endregion

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachSach.Rows.Count == 0) return;
            DataTable dtExport = ConvertDgvToDataTable(dgvDanhSachSach);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.Title = "Lưu danh sách Sách";
            sfd.FileName = "KhoSach_" + DateTime.Now.ToString("ddMMyyyy");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Kho Sách");

                        worksheet.Cell("A1").Value = "DANH SÁCH SÁCH TRONG KHO";
                        worksheet.Range("A1:G1").Merge();
                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("A1").Style.Font.FontSize = 16;
                        worksheet.Cell("A1").Style.Font.FontColor = XLColor.White;
                        worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.MidnightBlue;
                        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        var table = worksheet.Cell(3, 1).InsertTable(dtExport);
                        table.Theme = XLTableTheme.TableStyleMedium2;

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất danh sách Sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTenSach.Text.Trim();
            List<Models.Sach> listKetQua = sachController.SearchSach(tuKhoa);
            dgvDanhSachSach.Rows.Clear();
            foreach (Models.Sach s in listKetQua)
            {
                dgvDanhSachSach.Rows.Add(s.MaSach, s.TenSach, s.MaTL, s.MaTG, s.MaNXB, s.NamXB, s.GiaNhap, s.SoLuongTon, s.GiaBan);
            }
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(lblMaSach.Text, out int maSach))
            {
                MessageBox.Show("Vui lòng click chọn một cuốn sách từ danh sách bên dưới trước khi nhập hàng!");
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập số lượng sách cộng thêm vào kho:", "Nhập hàng", "0");

            if (int.TryParse(input, out int soLuongThem) && soLuongThem > 0)
            {
                if (sachController.NhapHang(maSach, soLuongThem))
                {
                    MessageBox.Show($"Đã nhập thêm {soLuongThem} cuốn vào kho thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Lỗi khi kết nối CSDL để nhập hàng!");
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Số lượng nhập vào phải là một con số hợp lệ lớn hơn 0!");
            }
        }
    }
}