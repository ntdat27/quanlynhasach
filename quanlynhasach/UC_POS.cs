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
        private KhachHangController khController = new KhachHangController(); // Gọi thêm Controller khách hàng

        // 2 Biến toàn cục để lưu thông tin giảm giá
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
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            // Bắt sự kiện gõ phím vào ô Số điện thoại
            txtSoDienThoai.KeyDown += txtSoDienThoai_KeyDown;
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
            // Bảng Sách
            dgvSach.Columns.Add("MaSach", "Mã");
            dgvSach.Columns["MaSach"].Width = 50;
            dgvSach.Columns.Add("TenSach", "Tên sách");
            dgvSach.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSach.Columns.Add("SoLuongTon", "Tồn");
            dgvSach.Columns["SoLuongTon"].Width = 50;
            dgvSach.Columns.Add("GiaBan", "Giá bán");
            dgvSach.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvSach.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Bảng Giỏ hàng
            dgvGioHang.Columns.Clear();
            dgvGioHang.Columns.Add("MaSach", "Mã");
            dgvGioHang.Columns["MaSach"].Visible = false;
            dgvGioHang.Columns.Add("TenSach", "Tên sách");
            dgvGioHang.Columns["TenSach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewButtonColumn btnTru = new DataGridViewButtonColumn();
            btnTru.Name = "colTru";
            btnTru.HeaderText = "";
            btnTru.Text = "-";
            btnTru.UseColumnTextForButtonValue = true;
            btnTru.Width = 30;
            dgvGioHang.Columns.Add(btnTru);

            dgvGioHang.Columns.Add("SoLuong", "SL");
            dgvGioHang.Columns["SoLuong"].Width = 40;
            dgvGioHang.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewButtonColumn btnCong = new DataGridViewButtonColumn();
            btnCong.Name = "colCong";
            btnCong.HeaderText = "";
            btnCong.Text = "+";
            btnCong.UseColumnTextForButtonValue = true;
            btnCong.Width = 30;
            dgvGioHang.Columns.Add(btnCong);

            dgvGioHang.Columns.Add("DonGia", "Đơn giá");
            dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns.Add("ThanhTien", "Thành tiền");
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        // LOGIC XỬ LÝ SỰ KIỆN
        // ========================================================

        // KIỂM TRA SĐT KHI ẤN ENTER
        private void txtSoDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra xem người dùng có ấn phím Enter không
            if (e.KeyCode == Keys.Enter)
            {
                string sdt = txtSoDienThoai.Text.Trim();
                if (string.IsNullOrEmpty(sdt))
                {
                    // Nếu xóa trắng SĐT thì reset lại không giảm giá
                    phanTramGiamHienTai = 0;
                    maKhachHangHienTai = null;
                    TinhTongTien();
                    return;
                }

                KhachHang kh = khController.GetKhachHangBySdt(sdt);
                if (kh != null)
                {
                    phanTramGiamHienTai = kh.PhanTramGiam;
                    maKhachHangHienTai = kh.MaKH;

                    MessageBox.Show($"Tìm thấy khách hàng: {kh.HoTen}\nĐược giảm giá: {kh.PhanTramGiam}%", "Khách hàng thân thiết", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    phanTramGiamHienTai = 0;
                    maKhachHangHienTai = null;
                    MessageBox.Show("Khách hàng chưa đăng ký thành viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Sau khi có % giảm giá thì gọi hàm tính lại tiền
                TinhTongTien();
            }
        }

        // TÍNH LẠI HÀM TỔNG TIỀN (Bổ sung logic chiết khấu)
        private void TinhTongTien()
        {
            int tongTienHang = 0;
            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                tongTienHang += Convert.ToInt32(row.Cells["ThanhTien"].Value);
            }

            // Tính tiền giảm
            int tienGiam = (tongTienHang * phanTramGiamHienTai) / 100;

            // Khách cần trả
            int khachCanTra = tongTienHang - tienGiam;

            // Nếu có giảm giá thì hiển thị rõ ràng trên Label
            if (phanTramGiamHienTai > 0)
            {
                lblTongTien.Text = string.Format("{0:N0} VNĐ (Đã giảm {1}%)", khachCanTra, phanTramGiamHienTai);
            }
            else
            {
                lblTongTien.Text = string.Format("{0:N0} VNĐ", khachCanTra);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            dgvSach.Rows.Clear();
            List<Sach> danhSachTimKiem;

            if (string.IsNullOrEmpty(keyword))
                danhSachTimKiem = sachController.GetAllSach();
            else
                danhSachTimKiem = sachController.SearchSach(keyword);

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
                        daTonTai = true;
                        break;
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
                        int tonKho = 0;
                        foreach (DataGridViewRow sachRow in dgvSach.Rows)
                        {
                            if (Convert.ToInt32(sachRow.Cells["MaSach"].Value) == maSach)
                            {
                                tonKho = Convert.ToInt32(sachRow.Cells["SoLuongTon"].Value);
                                break;
                            }
                        }

                        if (soLuong >= tonKho)
                        {
                            MessageBox.Show("Không đủ sách trong kho để bán thêm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        soLuong++;
                    }
                    else if (tenCot == "colTru")
                    {
                        soLuong--;
                    }

                    if (soLuong <= 0)
                    {
                        dgvGioHang.Rows.Remove(row);
                    }
                    else
                    {
                        row.Cells["SoLuong"].Value = soLuong;
                        row.Cells["ThanhTien"].Value = soLuong * donGia;
                    }

                    TinhTongTien();
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có mua cuốn sách nào không
            if (dgvGioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng chọn sách trước khi thanh toán.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tongTienHang = 0;
            List<ChiTietHoaDon> listChiTiet = new List<ChiTietHoaDon>();

            // Lấy toàn bộ sách trong giỏ hàng đóng gói lại
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

            // Tạm thời cố định MaNV = 1 (Tài khoản Admin) vì ta chưa làm module Đăng nhập
            int maNhanVien = 1;

            // Gọi Controller để thực thi lưu DB
            HoaDonController hdController = new HoaDonController();
            bool result = hdController.ThanhToan(maKhachHangHienTai, maNhanVien, tongTienHang, phanTramGiamHienTai, tienGiam, khachCanTra, listChiTiet);

            if (result)
            {
                MessageBox.Show("Thanh toán thành công! Hệ thống đã cập nhật tồn kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa trắng giỏ hàng và màn hình để đón khách tiếp theo
                dgvGioHang.Rows.Clear();
                txtSoDienThoai.Clear();
                phanTramGiamHienTai = 0;
                maKhachHangHienTai = null;
                lblTongTien.Text = "0 VNĐ";
                lblTongTien.ForeColor = Color.Black;

                // Bắt buộc gọi lại hàm này để load lại số lượng tồn kho mới nhất lên bảng bên trái
                LoadDanhSachSach();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu hóa đơn xuống Database!", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}