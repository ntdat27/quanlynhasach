using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using quanlynhasach.Controllers;
using quanlynhasach.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.Kernel.Font;

namespace quanlynhasach
{
    public partial class UC_POS : UserControl
    {
        private SachController sachController = new SachController();
        private KhachHangController khController = new KhachHangController();

        private int phanTramGiamHienTai = 0;
        private int? maKhachHangHienTai = null;

        public UC_POS()
        {
            InitializeComponent();

            FormatDataGridView(dgvSach);
            FormatDataGridView(dgvGioHang);
            SetupColumns();
            LoadDanhSachSach();

            dgvSach.CellDoubleClick += dgvSach_CellDoubleClick;
            dgvGioHang.CellContentClick += dgvGioHang_CellContentClick;

            dgvGioHang.CellEndEdit += dgvGioHang_CellEndEdit;

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            txtSoDienThoai.KeyDown += txtSoDienThoai_KeyDown;
            txtSoDienThoai.Leave += txtSoDienThoai_Leave;
        }

        private void FormatDataGridView(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(63, 81, 181);
            dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 63, 159);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.RowTemplate.Height = 35;
        }

        private void SetupColumns()
        {
            dgvSach.Columns.Add("MaSach", "Mã");
            dgvSach.Columns["MaSach"].Width = 50;
            dgvSach.Columns.Add("TenSach", "Tên sách");
            dgvSach.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSach.Columns.Add("SoLuongTon", "Tồn");
            dgvSach.Columns["SoLuongTon"].Width = 50;
            dgvSach.Columns.Add("GiaBan", "Giá bán");
            dgvSach.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvSach.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvGioHang.Columns.Clear();

            dgvGioHang.ReadOnly = false;

            dgvGioHang.Columns.Add("MaSach", "Mã");
            dgvGioHang.Columns["MaSach"].Visible = false;

            dgvGioHang.Columns.Add("TenSach", "Tên sách");
            dgvGioHang.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGioHang.Columns["TenSach"].ReadOnly = true; 

            DataGridViewButtonColumn btnTru = new DataGridViewButtonColumn();
            btnTru.Name = "colTru";
            btnTru.HeaderText = "";
            btnTru.Text = "-";
            btnTru.UseColumnTextForButtonValue = true;
            btnTru.Width = 30;
            dgvGioHang.Columns.Add(btnTru);

            dgvGioHang.Columns.Add("SoLuong", "SL");
            dgvGioHang.Columns["SoLuong"].Width = 50;
            dgvGioHang.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGioHang.Columns["SoLuong"].ReadOnly = false;

            DataGridViewButtonColumn btnCong = new DataGridViewButtonColumn();
            btnCong.Name = "colCong";
            btnCong.HeaderText = "";
            btnCong.Text = "+";
            btnCong.UseColumnTextForButtonValue = true;
            btnCong.Width = 30;
            dgvGioHang.Columns.Add(btnCong);

            dgvGioHang.Columns.Add("DonGia", "Đơn giá");
            dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns["DonGia"].ReadOnly = true;

            dgvGioHang.Columns.Add("ThanhTien", "Thành tiền");
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvGioHang.Columns["ThanhTien"].ReadOnly = true;
        }

        private void LoadDanhSachSach()
        {
            try
            {
                dgvSach.Rows.Clear();
                List<Sach> danhSach = sachController.GetAllSach();
                foreach (Sach s in danhSach)
                {
                    dgvSach.Rows.Add(s.MaSach, s.TenSach, s.SoLuongTon, s.GiaBan);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void txtSoDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) KiemTraKhachHang();
        }

        private void txtSoDienThoai_Leave(object sender, EventArgs e)
        {
            KiemTraKhachHang();
        }

        private void KiemTraKhachHang()
        {
            string sdt = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(sdt))
            {
                phanTramGiamHienTai = 0;
                maKhachHangHienTai = null;
                lblTenKhachDisplay.Text = "Khách hàng: Khách vãng lai";
                lblHangThanhVien.Text = "Hạng: N/A"; 
                lblHangThanhVien.ForeColor = Color.Gray;
            }
            else
            {
                KhachHang kh = khController.GetKhachHangBySdt(sdt);

                if (kh != null)
                {
                    phanTramGiamHienTai = kh.PhanTramGiam;
                    maKhachHangHienTai = kh.MaKH;

                    lblTenKhachDisplay.Text = "Khách hàng: " + kh.HoTen;

                    lblHangThanhVien.Text = "Hạng: " + kh.TenHang; 
                    lblHangThanhVien.ForeColor = Color.FromArgb(48, 63, 159); 
                }
                else
                {
                    phanTramGiamHienTai = 0;
                    maKhachHangHienTai = null;
                    lblTenKhachDisplay.Text = "Khách hàng: Không tìm thấy";
                    lblHangThanhVien.Text = "Hạng: N/A";
                    lblHangThanhVien.ForeColor = Color.Gray;
                }
            }

            TinhTongTien();
        }

        private void TinhTongTien()
        {
            long tongTienHang = 0;

            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    tongTienHang += Convert.ToInt64(row.Cells["ThanhTien"].Value);
                }
            }

            long soTienDuocGiam = (tongTienHang * phanTramGiamHienTai) / 100;
            long thanhTien = tongTienHang - soTienDuocGiam;

            lblTamTinh.Text = string.Format("{0:N0} đ", tongTienHang);

            lblGiamGia.Text = string.Format("- {0:N0} đ ({1}%)", soTienDuocGiam, phanTramGiamHienTai);

            lblTongCong.Text = string.Format("{0:N0} đ", thanhTien);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvSach.Rows.Clear();
            List<Sach> danhSachTimKiem = string.IsNullOrEmpty(keyword) ? sachController.GetAllSach() : sachController.SearchSach(keyword);
            foreach (Sach s in danhSachTimKiem)
            {
                dgvSach.Rows.Add(s.MaSach, s.TenSach, s.SoLuongTon, s.GiaBan);
            }
        }

        private void dgvSach_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSach.Rows[e.RowIndex];
                int maSach = Convert.ToInt32(row.Cells["MaSach"].Value);
                string tenSach = row.Cells["TenSach"].Value.ToString();
                int tonKho = Convert.ToInt32(row.Cells["SoLuongTon"].Value);
                int giaBan = Convert.ToInt32(row.Cells["GiaBan"].Value);

                if (tonKho <= 0)
                {
                    MessageBox.Show("Sách này đã hết hàng trong kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool daTonTai = false;
                foreach (DataGridViewRow cartRow in dgvGioHang.Rows)
                {
                    if (Convert.ToInt32(cartRow.Cells["MaSach"].Value) == maSach)
                    {
                        daTonTai = true; break;
                    }
                }

                if (!daTonTai)
                {
                    dgvGioHang.Rows.Add(maSach, tenSach, null, 1, null, giaBan, giaBan);
                    TinhTongTien();
                }
            }
        }

        private void dgvGioHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string tenCot = dgvGioHang.Columns[e.ColumnIndex].Name;

                if (tenCot == "colCong" || tenCot == "colTru")
                {
                    DataGridViewRow row = dgvGioHang.Rows[e.RowIndex];
                    int maSach = Convert.ToInt32(row.Cells["MaSach"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    int donGia = Convert.ToInt32(row.Cells["DonGia"].Value);

                    if (tenCot == "colCong")
                    {
                        int tonKho = LayTonKho(maSach);
                        if (soLuong >= tonKho)
                        {
                            MessageBox.Show("Không đủ sách trong kho để bán thêm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        soLuong++;
                    }
                    else if (tenCot == "colTru") soLuong--;

                    if (soLuong <= 0) dgvGioHang.Rows.Remove(row);
                    else
                    {
                        row.Cells["SoLuong"].Value = soLuong;
                        row.Cells["ThanhTien"].Value = soLuong * donGia;
                    }
                    TinhTongTien();
                }
            }
        }

        private void dgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvGioHang.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                DataGridViewRow row = dgvGioHang.Rows[e.RowIndex];
                int maSach = Convert.ToInt32(row.Cells["MaSach"].Value);
                int donGia = Convert.ToInt32(row.Cells["DonGia"].Value);

                string input = row.Cells["SoLuong"].Value?.ToString();
                int soLuongMoi;

                if (!int.TryParse(input, out soLuongMoi) || soLuongMoi < 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    row.Cells["SoLuong"].Value = 1;
                    row.Cells["ThanhTien"].Value = donGia;
                    TinhTongTien();
                    return;
                }

                if (soLuongMoi == 0)
                {
                    dgvGioHang.Rows.Remove(row);
                    TinhTongTien();
                    return;
                }

                int tonKho = LayTonKho(maSach);
                if (soLuongMoi > tonKho)
                {
                    MessageBox.Show($"Chỉ còn {tonKho} quyển trong kho! Đã tự động điều chỉnh về số lượng tối đa.", "Vượt quá Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells["SoLuong"].Value = tonKho; 
                    row.Cells["ThanhTien"].Value = tonKho * donGia;
                }
                else
                {
                    row.Cells["SoLuong"].Value = soLuongMoi;
                    row.Cells["ThanhTien"].Value = soLuongMoi * donGia;
                }

                TinhTongTien();
            }
        }

        private int LayTonKho(int maSach)
        {
            foreach (DataGridViewRow sachRow in dgvSach.Rows)
            {
                if (Convert.ToInt32(sachRow.Cells["MaSach"].Value) == maSach)
                {
                    return Convert.ToInt32(sachRow.Cells["SoLuongTon"].Value);
                }
            }
            return 0;
        }

        private bool IsFileLocked(string filePath)
        {
            if (!System.IO.File.Exists(filePath)) return false;
            try
            {
                using (System.IO.FileStream stream = System.IO.File.Open(filePath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (System.IO.IOException) { return true; }
            return false;
        }
        private void ExportInvoiceToPDF(string filePath)
        {
            try
            {
                if (IsFileLocked(filePath))
                {
                    MessageBox.Show("File này đang được mở bởi một chương trình khác!", "Lỗi");
                    return;
                }

                using (PdfWriter writer = new PdfWriter(filePath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        string path = @"C:\Windows\Fonts\";
                        PdfFont fontNormal = PdfFontFactory.CreateFont(path + "arial.ttf", PdfEncodings.IDENTITY_H);
                        PdfFont fontBold = PdfFontFactory.CreateFont(path + "arialbd.ttf", PdfEncodings.IDENTITY_H);
                        PdfFont fontItalic = PdfFontFactory.CreateFont(path + "ariali.ttf", PdfEncodings.IDENTITY_H);

                        document.SetFont(fontNormal);

                        document.Add(new Paragraph("HÓA ĐƠN")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(20)
                            .SetFont(fontBold)); 

                        document.Add(new Paragraph("Nhà Sách Thư Quán").SetTextAlignment(TextAlignment.CENTER));
                        document.Add(new Paragraph("--------------------------------------------------").SetTextAlignment(TextAlignment.CENTER));

                        document.Add(new Paragraph(lblTenKhachDisplay.Text));
                        document.Add(new Paragraph(lblHangThanhVien.Text));
                        document.Add(new Paragraph($"Ngày lập: {DateTime.Now:dd/MM/yyyy HH:mm}"));
                        document.Add(new Paragraph("\n"));

                        Table table = new Table(4).UseAllAvailableWidth();
                        table.AddHeaderCell(new Cell().Add(new Paragraph("Tên sách").SetFont(fontBold)));
                        table.AddHeaderCell(new Cell().Add(new Paragraph("SL").SetFont(fontBold)));
                        table.AddHeaderCell(new Cell().Add(new Paragraph("Đơn giá").SetFont(fontBold)));
                        table.AddHeaderCell(new Cell().Add(new Paragraph("Thành tiền").SetFont(fontBold)));

                        foreach (DataGridViewRow row in dgvGioHang.Rows)
                        {
                            table.AddCell(row.Cells["TenSach"].Value.ToString());
                            table.AddCell(row.Cells["SoLuong"].Value.ToString());
                            table.AddCell(string.Format("{0:N0}", row.Cells["DonGia"].Value));
                            table.AddCell(string.Format("{0:N0}", row.Cells["ThanhTien"].Value));
                        }
                        document.Add(table);

                        document.Add(new Paragraph("\n"));
                        document.Add(new Paragraph($"Tạm tính: {lblTamTinh.Text}").SetTextAlignment(TextAlignment.RIGHT));
                        document.Add(new Paragraph($"Giảm giá: {lblGiamGia.Text}").SetTextAlignment(TextAlignment.RIGHT));
                        document.Add(new Paragraph($"TỔNG CỘNG: {lblTongCong.Text}")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(14)
                            .SetFont(fontBold));

                        document.Add(new Paragraph("\nCảm ơn quý khách và hẹn gặp lại!")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFont(fontItalic));

                        document.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo PDF: " + ex.Message + "\n" + ex.InnerException?.Message);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn sách trước khi thanh toán.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tongTienHang = 0;
            List<ChiTietHoaDon> listChiTiet = new List<ChiTietHoaDon>();

            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                int thanhTien = Convert.ToInt32(row.Cells["ThanhTien"].Value);
                tongTienHang += thanhTien;

                listChiTiet.Add(new ChiTietHoaDon
                {
                    MaSach = Convert.ToInt32(row.Cells["MaSach"].Value),
                    SoLuongBan = Convert.ToInt32(row.Cells["SoLuong"].Value),
                    GiaBan = Convert.ToInt32(row.Cells["DonGia"].Value),
                    ThanhTien = thanhTien
                });
            }

            int tienGiam = (tongTienHang * phanTramGiamHienTai) / 100;
            int khachCanTra = tongTienHang - tienGiam;
            int maNhanVien = 1;

            HoaDonController hdController = new HoaDonController();
            bool result = hdController.ThanhToan(maKhachHangHienTai, maNhanVien, tongTienHang, phanTramGiamHienTai, tienGiam, khachCanTra, listChiTiet);

            if (result)
            {
                DialogResult dr = MessageBox.Show("Thanh toán thành công! Bạn có muốn xuất hóa đơn PDF không?",
            "In hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                    sfd.FileName = $"HD_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportInvoiceToPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                dgvGioHang.Rows.Clear();
                txtSoDienThoai.Clear();
                phanTramGiamHienTai = 0;
                maKhachHangHienTai = null;
                lblTongCong.Text = "0 VNĐ";
                lblTongCong.ForeColor = Color.Black;

                LoadDanhSachSach(); 
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu hóa đơn xuống Database!", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}