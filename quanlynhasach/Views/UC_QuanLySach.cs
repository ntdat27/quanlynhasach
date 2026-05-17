using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using quanlynhasach.Controllers;
using quanlynhasach.Data;
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
        private string ChuanHoaVanBan(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            text = Regex.Replace(text.Trim(), @"\s+", " ");
            TextInfo textInfo = new CultureInfo("vi-VN", false).TextInfo;
            return textInfo.ToTitleCase(text.ToLower());
        }

        private int GetOrInsertValue(string tableName, string nameColumn, string idColumn, string textValue)
        {
            if (string.IsNullOrWhiteSpace(textValue)) return 0;
            DatabaseHelper db = new DatabaseHelper();

            string checkQuery = $"SELECT {idColumn} FROM {tableName} WHERE {nameColumn} = @textValue";
            MySqlParameter[] checkParams = { new MySqlParameter("@textValue", textValue.Trim()) };

            DataTable dt = db.ExecuteQuery(checkQuery, checkParams);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][idColumn]);
            }
            else
            {
                string insertQuery = $"INSERT INTO {tableName} ({nameColumn}) VALUES (@textValue); SELECT LAST_INSERT_ID();";
                MySqlParameter[] insertParams = { new MySqlParameter("@textValue", textValue.Trim()) };
                return db.ExecuteScalar(insertQuery, insertParams);
            }
        }

        private string GetNameFromDB(string tableName, string nameCol, string idCol, int id)
        {
            if (id <= 0) return "";
            DatabaseHelper db = new DatabaseHelper();

            string query = $"SELECT {nameCol} FROM {tableName} WHERE {idCol} = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", id) };

            DataTable dt = db.ExecuteQuery(query, parameters);
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

        private void btnTeam_Click(object sender, EventArgs e)
        {
            btnThem_Click(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSach.Text) ||
                string.IsNullOrWhiteSpace(txtTheLoai.Text) ||
                string.IsNullOrWhiteSpace(txtTacGia.Text) ||
                string.IsNullOrWhiteSpace(txtNXB.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin: Tên sách, Thể loại, Tác giả và Nhà xuất bản!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtNamXB.Text.Trim(), out int namXB) || namXB <= 0 || namXB > DateTime.Now.Year)
            {
                MessageBox.Show($"Năm xuất bản phải là một số nguyên dương và không được lớn hơn năm hiện tại ({DateTime.Now.Year})!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int.TryParse(txtGiaNhap.Text.Trim(), out int giaNhap);
            int.TryParse(txtGiaBan.Text.Trim(), out int giaBan);
            int.TryParse(txtSoLuong.Text.Trim(), out int soLuong);

            string tenSachChuan = ChuanHoaVanBan(txtTenSach.Text);
            string theLoaiChuan = ChuanHoaVanBan(txtTheLoai.Text);
            string tacGiaChuan = ChuanHoaVanBan(txtTacGia.Text);
            string nxbChuan = ChuanHoaVanBan(txtNXB.Text);

            Sach s = new Sach
            {
                TenSach = tenSachChuan,
                MaTL = GetOrInsertValue("theloai", "TenTL", "MaTL", theLoaiChuan),
                MaTG = GetOrInsertValue("tacgia", "TenTG", "MaTG", tacGiaChuan),
                MaNXB = GetOrInsertValue("nhaxuatban", "TenNXB", "MaNXB", nxbChuan),
                NamXB = namXB,
                GiaNhap = giaNhap >= 0 ? giaNhap : 0,
                GiaBan = giaBan >= 0 ? giaBan : 0,
                SoLuongTon = soLuong >= 0 ? soLuong : 0
            };

            if (sachController.AddSach(s))
            {
                MessageBox.Show("Thêm sách mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null);
            }
            else MessageBox.Show("Thêm thất bại, vui lòng kiểm tra lại kết nối CSDL!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaSach.Text))
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách từ danh sách bên dưới!", "Thông báo");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenSach.Text) ||
                string.IsNullOrWhiteSpace(txtTheLoai.Text) ||
                string.IsNullOrWhiteSpace(txtTacGia.Text) ||
                string.IsNullOrWhiteSpace(txtNXB.Text))
            {
                MessageBox.Show("Không được để trống Tên sách, Thể loại, Tác giả và Nhà xuất bản!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtNamXB.Text.Trim(), out int namXB) || namXB <= 0 || namXB > DateTime.Now.Year)
            {
                MessageBox.Show($"Năm xuất bản phải là một số nguyên dương và không được lớn hơn năm hiện tại ({DateTime.Now.Year})!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int.TryParse(txtGiaNhap.Text.Trim(), out int giaNhap);
            int.TryParse(txtGiaBan.Text.Trim(), out int giaBan);
            int.TryParse(txtSoLuong.Text.Trim(), out int soLuong);

            string tenSachChuan = ChuanHoaVanBan(txtTenSach.Text);
            string theLoaiChuan = ChuanHoaVanBan(txtTheLoai.Text);
            string tacGiaChuan = ChuanHoaVanBan(txtTacGia.Text);
            string nxbChuan = ChuanHoaVanBan(txtNXB.Text);

            Sach s = new Sach
            {
                MaSach = int.Parse(lblMaSach.Text),
                TenSach = tenSachChuan,
                MaTL = GetOrInsertValue("theloai", "TenTL", "MaTL", theLoaiChuan),
                MaTG = GetOrInsertValue("tacgia", "TenTG", "MaTG", tacGiaChuan),
                MaNXB = GetOrInsertValue("nhaxuatban", "TenNXB", "MaNXB", nxbChuan),
                NamXB = namXB,
                GiaNhap = giaNhap >= 0 ? giaNhap : 0,
                GiaBan = giaBan >= 0 ? giaBan : 0,
                SoLuongTon = soLuong >= 0 ? soLuong : 0
            };

            if (sachController.UpdateSach(s))
            {
                MessageBox.Show("Cập nhật thông tin sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // HÀM MỚI: TẠO FORM POP-UP NHẬP HÀNG CHUYÊN NGHIỆP THAY CHO INPUTBOX
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            // Đảm bảo nhân viên phải chọn sách từ GridView trước khi kích hoạt phiếu nhập
            if (string.IsNullOrEmpty(lblMaSach.Text) || !int.TryParse(lblMaSach.Text, out int maSach))
            {
                MessageBox.Show("Vui lòng click chuột chọn một cuốn sách từ bảng danh sách bên dưới trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // KHỞI TẠO POP-UP ĐỘNG: Lập chứng từ phiếu nhập hàng bằng code giao diện trực tiếp
            Form frmPopup = new Form();
            frmPopup.Text = "LẬP PHIẾU NHẬP HÀNG SÁCH";
            frmPopup.Size = new Size(420, 300);
            frmPopup.StartPosition = FormStartPosition.CenterParent;
            frmPopup.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmPopup.MaximizeBox = false;
            frmPopup.MinimizeBox = false;
            frmPopup.BackColor = Color.White;

            // Thiết kế các nhãn điều khiển hiển thị nội dung điền thông tin
            Label lblTitle = new Label() { Text = "NHẬP KHO: " + txtTenSach.Text, Left = 20, Top = 20, Width = 360, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(48, 63, 159) };

            Label lblSL = new Label() { Text = "Số lượng nhập thêm:", Left = 20, Top = 80, Width = 150, Font = new Font("Segoe UI", 9.5F) };
            TextBox txtSL = new TextBox() { Left = 180, Top = 78, Width = 190, Font = new Font("Segoe UI", 9.5F) };

            Label lblGia = new Label() { Text = "Đơn giá nhập hàng (đ):", Left = 20, Top = 130, Width = 150, Font = new Font("Segoe UI", 9.5F) };

            // Lấy sẵn đơn giá nhập hiện tại gán vào ô để nhân viên đỡ phải gõ lại nếu giá nhập không thay đổi
            TextBox txtGia = new TextBox() { Left = 180, Top = 128, Width = 190, Font = new Font("Segoe UI", 9.5F), Text = txtGiaNhap.Text };

            Button btnLuu = new Button() { Text = "XÁC NHẬN NHẬP", Left = 70, Top = 200, Width = 130, Height = 38, BackColor = Color.FromArgb(48, 63, 159), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            Button btnHuy = new Button() { Text = "HỦY BỎ", Left = 220, Top = 200, Width = 130, Height = 38, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };

            // Ép các linh kiện xuất hiện lên Form Pop-up ảo
            frmPopup.Controls.AddRange(new Control[] { lblTitle, lblSL, txtSL, lblGia, txtGia, btnLuu, btnHuy });

            // Đăng ký hành vi phản hồi sự kiện bấm nút trên Pop-up
            btnHuy.Click += (s, args) => frmPopup.Close();

            btnLuu.Click += (s, args) =>
            {
                // Kiểm tra chặn lọc lỗi số lượng nhập hàng
                if (!int.TryParse(txtSL.Text.Trim(), out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng nhập kho bắt buộc phải là một con số nguyên dương lớn hơn 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSL.Focus();
                    return;
                }

                // Kiểm tra chặn lọc lỗi đơn giá vốn nhập hàng
                if (!int.TryParse(txtGia.Text.Trim(), out int giaNhap) || giaNhap <= 0)
                {
                    MessageBox.Show("Đơn giá nhập sách bắt buộc phải là một con số nguyên dương lớn hơn 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGia.Focus();
                    return;
                }

                int maNhanVien = quanlynhasach.Models.Session.MaNV > 0 ? quanlynhasach.Models.Session.MaNV : 1;

                if (sachController.NhapHangVaoKho(maSach, soLuong, giaNhap, maNhanVien))
                {
                    MessageBox.Show($"Lập chứng từ phiếu nhập kho cho {soLuong} cuốn sách thành công! Kho hàng và giá vốn đã được cập nhật.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmPopup.Close();

                    LoadData();

                    // VÁ LỖI HIỂN THỊ: Tính toán luôn Giá vốn Bình quân gia quyền ngay trên Giao diện
                    int tonCu = int.Parse(txtSoLuong.Text == "" ? "0" : txtSoLuong.Text);
                    int giaCu = int.Parse(txtGiaNhap.Text == "" ? "0" : txtGiaNhap.Text);

                    // Công thức: [(Tồn cũ * Giá cũ) + (Nhập mới * Giá mới)] / Tổng số lượng
                    int giaBinhQuanMoi = (int)Math.Round((double)((tonCu * giaCu) + (soLuong * giaNhap)) / (tonCu + soLuong));

                    // Đẩy kết quả đã trộn giá (49000) lên TextBox
                    txtSoLuong.Text = (tonCu + soLuong).ToString();
                    txtGiaNhap.Text = giaBinhQuanMoi.ToString();
                }
                else
                {
                    MessageBox.Show("Lập phiếu nhập thất bại! Bạn hãy kiểm tra lại xem cấu trúc bảng dữ liệu 'phieunhap' và 'chitietphieunhap' đã được tạo dưới MySQL Workbench chưa nhé.", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Hiển thị hộp thoại ép buộc xử lý xong mới cho tương tác ra bên ngoài
            frmPopup.ShowDialog();
        }
    }
}