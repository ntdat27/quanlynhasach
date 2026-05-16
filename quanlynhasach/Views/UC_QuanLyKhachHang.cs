using ClosedXML.Excel;
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
    public partial class UC_QuanLyKhachHang : UserControl
    {
        private KhachHangController khController = new KhachHangController();

        public UC_QuanLyKhachHang()
        {
            InitializeComponent();

            FormatDataGridView(dgvKhachHang);
            SetupColumns();
            LoadData();
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;

            // Khóa cứng các ô thông tin tự động, chỉ cho phép hiển thị xem
            txtHangThanhVien.ReadOnly = true;
        }

        #region UI & Dữ liệu hiển thị
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

            dgvKhachHang.Columns.Add("TongTienDaMua", "Tổng chi tiêu");
            dgvKhachHang.Columns["TongTienDaMua"].Width = 120;
            dgvKhachHang.Columns["TongTienDaMua"].DefaultCellStyle.Format = "N0";

            dgvKhachHang.Columns.Add("TenHang", "Hạng Thành Viên");
            dgvKhachHang.Columns["TenHang"].Width = 150;

            dgvKhachHang.Columns.Add("PhanTramGiam", "Giảm (%)");
            dgvKhachHang.Columns["PhanTramGiam"].Width = 90;
            dgvKhachHang.Columns["PhanTramGiam"].DefaultCellStyle.Format = "N0";
        }

        private void LoadData()
        {
            try
            {
                dgvKhachHang.Rows.Clear();
                List<KhachHang> ds = khController.GetAllKhachHang();
                foreach (var kh in ds)
                {
                    // Đọc trực tiếp các thuộc tính thông minh tự tính toán từ Model KhachHang
                    dgvKhachHang.Rows.Add(kh.MaKH, kh.HoTen, kh.SoDienThoai, kh.TongTienDaMua, kh.TenHang, kh.PhanTramGiam);
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

        #region Logic CRUD Khách Hàng
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lblMaKH.Text = "";
            txtHoTen.Clear();
            txtSDT.Clear();
            txtHangThanhVien.Text = "Khách hàng thân thiết";
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTen = ChuanHoaHoTen(txtHoTen.Text);
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ tên và Số điện thoại!");
                return;
            }

            if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập đúng 10 chữ số và bắt đầu bằng số 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (khController.KiemTraTrungSdt(sdt))
            {
                MessageBox.Show("Số điện thoại này đã được sử dụng cho một khách hàng khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHang kh = new KhachHang
            {
                HoTen = hoTen,
                SoDienThoai = sdt,
                TongTienDaMua = 0 // Khách mới mặc định chi tiêu bằng 0
            };

            if (khController.AddKhachHang(kh))
            {
                LichSuController.GhiLog(Session.MaNV, $"Đăng ký khách hàng mới: {hoTen} ({sdt})");
                MessageBox.Show("Thêm khách hàng thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaKH.Text)) return;

            string hoTen = ChuanHoaHoTen(txtHoTen.Text);
            string sdt = txtSDT.Text.Trim();
            int maKHHienTai = int.Parse(lblMaKH.Text);

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Phải bao gồm đúng 10 số và bắt đầu bằng số 0.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (khController.KiemTraTrungSdt(sdt, maKHHienTai))
            {
                MessageBox.Show("Số điện thoại này đã được sử dụng cho một khách hàng khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy lại tổng tiền cũ từ hàng đang chọn để tránh ghi đè làm mất tích lũy của khách
            decimal tongTienHienTai = Convert.ToDecimal(dgvKhachHang.CurrentRow.Cells["TongTienDaMua"].Value);

            KhachHang kh = new KhachHang
            {
                MaKH = maKHHienTai,
                HoTen = hoTen,
                SoDienThoai = sdt,
                TongTienDaMua = tongTienHienTai
            };

            if (khController.UpdateKhachHang(kh))
            {
                LichSuController.GhiLog(Session.MaNV, $"Cập nhật thông tin khách hàng ID: {maKHHienTai}");
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                btnLamMoi_Click(null, null);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblMaKH.Text)) return;
            int maKH = int.Parse(lblMaKH.Text);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (khController.DeleteKhachHang(maKH))
                {
                    LichSuController.GhiLog(Session.MaNV, $"Xóa khách hàng ID: {maKH}");
                    MessageBox.Show("Đã xóa dữ liệu khách hàng thành công!");
                    btnLamMoi_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Không thể xóa khách hàng này (Dữ liệu đã vướng vào lịch sử các hóa đơn cũ)!");
                }
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                lblMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();

                // Đổ dữ liệu tổng tiền (định dạng số) và chữ tên Hạng lên các ô hiển thị TextBox tương ứng
                txtHangThanhVien.Text = row.Cells["TenHang"].Value?.ToString();
            }
        }
        #endregion

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

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.Rows.Count == 0) return;

            DataTable dtExport = ConvertDgvToDataTable(dgvKhachHang);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.FileName = "KhachHang_" + DateTime.Now.ToString("ddMMyyyy");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Khách Hàng");

                        worksheet.Cell("A1").Value = "DANH SÁCH KHÁCH HÀNG THÂN THIẾT";
                        worksheet.Range("A1:F1").Merge();
                        worksheet.Cell("A1").Style.Font.Bold = true;
                        worksheet.Cell("A1").Style.Font.FontSize = 16;
                        worksheet.Cell("A1").Style.Font.FontColor = XLColor.White;
                        worksheet.Cell("A1").Style.Fill.BackgroundColor = XLColor.Purple;
                        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        var table = worksheet.Cell(3, 1).InsertTable(dtExport);
                        table.Theme = XLTableTheme.TableStyleMedium12;

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Xuất danh sách Khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoaTen = txtHoTen.Text.Trim();
            string tuKhoaSdt = txtSDT.Text.Trim();

            DataTable dtKetQua = khController.SearchKhachHang(tuKhoaTen, tuKhoaSdt);
            dgvKhachHang.Rows.Clear();

            foreach (DataRow row in dtKetQua.Rows)
            {
                decimal tongTien = row["TongTienDaMua"] != DBNull.Value ? Convert.ToDecimal(row["TongTienDaMua"]) : 0;

                // Sử dụng một đối tượng tạm thời để mượn logic tính hạng và % giảm tự động
                KhachHang khTemp = new KhachHang { TongTienDaMua = tongTien };

                dgvKhachHang.Rows.Add(
                    row["MaKH"],
                    row["HoTen"],
                    row["SoDienThoai"],
                    tongTien,
                    khTemp.TenHang,
                    khTemp.PhanTramGiam
                );
            }
        }
    }
}