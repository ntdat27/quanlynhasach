using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using quanlynhasach.Controllers;
using quanlynhasach.Models;

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

            // Đăng ký các sự kiện
            dgvSach.CellDoubleClick += dgvSach_CellDoubleClick;
            dgvGioHang.CellContentClick += dgvGioHang_CellContentClick;

            // SỰ KIỆN MỚI: Bắt sự kiện khi thu ngân gõ tay số lượng rồi click ra chỗ khác
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
            // ================= 1. BẢNG SÁCH =================
            dgvSach.Columns.Add("MaSach", "Mã");
            dgvSach.Columns["MaSach"].Width = 50;
            dgvSach.Columns.Add("TenSach", "Tên sách");
            dgvSach.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSach.Columns.Add("SoLuongTon", "Tồn");
            dgvSach.Columns["SoLuongTon"].Width = 50;
            dgvSach.Columns.Add("GiaBan", "Giá bán");
            dgvSach.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvSach.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // ================= 2. BẢNG GIỎ HÀNG =================
            dgvGioHang.Columns.Clear();

            // MỞ KHÓA BẢNG GIỎ HÀNG: Chỉ cho phép sửa duy nhất cột "SoLuong"
            dgvGioHang.ReadOnly = false;

            dgvGioHang.Columns.Add("MaSach", "Mã");
            dgvGioHang.Columns["MaSach"].Visible = false;

            dgvGioHang.Columns.Add("TenSach", "Tên sách");
            dgvGioHang.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGioHang.Columns["TenSach"].ReadOnly = true; // Khóa cột tên

            DataGridViewButtonColumn btnTru = new DataGridViewButtonColumn();
            btnTru.Name = "colTru";
            btnTru.HeaderText = "";
            btnTru.Text = "-";
            btnTru.UseColumnTextForButtonValue = true;
            btnTru.Width = 30;
            dgvGioHang.Columns.Add(btnTru);

            // Cột Số lượng để ở giữa (Không ReadOnly để thu ngân gõ tay được)
            dgvGioHang.Columns.Add("SoLuong", "SL");
            dgvGioHang.Columns["SoLuong"].Width = 50;
            dgvGioHang.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGioHang.Columns["SoLuong"].ReadOnly = false; // MỞ KHÓA CHỖ NÀY

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

        // ========================================================
        // LOGIC KHÁCH HÀNG & TÍNH TIỀN
        // ========================================================
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
                lblHangThanhVien.Text = "Hạng: N/A"; // Hiện hạng mặc định
                lblHangThanhVien.ForeColor = Color.Gray;
            }
            else
            {
                // Sử dụng Controller để lấy thông tin khách hàng đầy đủ
                KhachHang kh = khController.GetKhachHangBySdt(sdt);

                if (kh != null)
                {
                    phanTramGiamHienTai = kh.PhanTramGiam;
                    maKhachHangHienTai = kh.MaKH;

                    lblTenKhachDisplay.Text = "Khách hàng: " + kh.HoTen;

                    // HIỂN THỊ HẠNG THÀNH VIÊN Ở ĐÂY
                    lblHangThanhVien.Text = "Hạng: " + kh.TenHang; // Ví dụ: Hạng: Thành viên Bạc
                    lblHangThanhVien.ForeColor = Color.FromArgb(48, 63, 159); // Màu xanh đậm cho sang
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

            // CUỐI CÙNG: Gọi tính tiền để cập nhật số tiền giảm theo hạng vừa tìm được
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            long tongTienHang = 0;

            // 1. Tính tiền gốc từ giỏ hàng
            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null)
                {
                    tongTienHang += Convert.ToInt64(row.Cells["ThanhTien"].Value);
                }
            }

            // 2. Tính toán dựa trên % giảm giá đã được xác định ở hàm KiemTraKhachHang
            long soTienDuocGiam = (tongTienHang * phanTramGiamHienTai) / 100;
            long thanhTien = tongTienHang - soTienDuocGiam;

            // 3. Đổ dữ liệu lên giao diện
            lblTamTinh.Text = string.Format("{0:N0} đ", tongTienHang);

            // Hiển thị dòng Giảm giá có kèm % để khách dễ nhìn
            lblGiamGia.Text = string.Format("- {0:N0} đ ({1}%)", soTienDuocGiam, phanTramGiamHienTai);

            // Tổng tiền cuối cùng
            lblTongCong.Text = string.Format("{0:N0} đ", thanhTien);
        }

        // ========================================================
        // LOGIC GIỎ HÀNG & TÌM KIẾM
        // ========================================================
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

        // ========================================================
        // SỰ KIỆN MỚI: KHI THU NGÂN GÕ TAY SỐ LƯỢNG VÀO BẢNG
        // ========================================================
        private void dgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvGioHang.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                DataGridViewRow row = dgvGioHang.Rows[e.RowIndex];
                int maSach = Convert.ToInt32(row.Cells["MaSach"].Value);
                int donGia = Convert.ToInt32(row.Cells["DonGia"].Value);

                string input = row.Cells["SoLuong"].Value?.ToString();
                int soLuongMoi;

                // 1. Kiểm tra xem có gõ chữ bậy bạ (ví dụ gõ chữ "abc") hoặc gõ số âm không
                if (!int.TryParse(input, out soLuongMoi) || soLuongMoi < 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    row.Cells["SoLuong"].Value = 1;
                    row.Cells["ThanhTien"].Value = donGia;
                    TinhTongTien();
                    return;
                }

                // 2. Nếu gõ số 0 thì xóa cuốn sách đó khỏi giỏ
                if (soLuongMoi == 0)
                {
                    dgvGioHang.Rows.Remove(row);
                    TinhTongTien();
                    return;
                }

                // 3. Nếu gõ 1000, kiểm tra xem tồn kho có đủ 1000 không
                int tonKho = LayTonKho(maSach);
                if (soLuongMoi > tonKho)
                {
                    MessageBox.Show($"Chỉ còn {tonKho} quyển trong kho! Đã tự động điều chỉnh về số lượng tối đa.", "Vượt quá Tồn Kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    row.Cells["SoLuong"].Value = tonKho; // Ép về số lượng tối đa trong kho
                    row.Cells["ThanhTien"].Value = tonKho * donGia;
                }
                else
                {
                    // Hợp lệ, gán giá trị mới
                    row.Cells["SoLuong"].Value = soLuongMoi;
                    row.Cells["ThanhTien"].Value = soLuongMoi * donGia;
                }

                TinhTongTien();
            }
        }

        // Hàm phụ trợ để tìm lại số lượng tồn kho của một mã sách
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

        // ========================================================
        // NÚT THANH TOÁN (Lưu xuống Database)
        // ========================================================
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
                MessageBox.Show("Thanh toán thành công! Hệ thống đã cập nhật tồn kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvGioHang.Rows.Clear();
                txtSoDienThoai.Clear();
                phanTramGiamHienTai = 0;
                maKhachHangHienTai = null;
                lblTongCong.Text = "0 VNĐ";
                lblTongCong.ForeColor = Color.Black;

                LoadDanhSachSach(); // Gọi lại kho sách để cập nhật số tồn mới
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu hóa đơn xuống Database!", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}