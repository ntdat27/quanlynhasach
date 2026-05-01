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
            txtTimKiem.TextChanged += txtTimKiem_TextChanged; // Bắt sự kiện gõ phím tìm kiếm
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
        // LOGIC XỬ LÝ SỰ KIỆN (EVENT HANDLERS)
        // ========================================================

        // TÍNH NĂNG TÌM KIẾM (Gõ đến đâu tìm đến đó)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim(); // Trim() để xóa khoảng trắng thừa ở 2 đầu
            dgvSach.Rows.Clear();

            List<Sach> danhSachTimKiem;

            // Nếu ô tìm kiếm trống thì load lại toàn bộ sách
            if (string.IsNullOrEmpty(keyword))
            {
                danhSachTimKiem = sachController.GetAllSach();
            }
            else // Nếu có chữ thì gọi hàm tìm kiếm
            {
                danhSachTimKiem = sachController.SearchSach(keyword);
            }

            // Đổ kết quả tìm được lên lưới
            foreach (Sach s in danhSachTimKiem)
            {
                dgvSach.Rows.Add(s.MaSach, s.TenSach, s.SoLuongTon, s.GiaBan);
            }
        }

        // CLICK ĐÚP THÊM VÀO GIỎ
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

        // NÚT (+) HOẶC (-) TRONG GIỎ HÀNG
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

        private void TinhTongTien()
        {
            int tongTien = 0;
            foreach (DataGridViewRow row in dgvGioHang.Rows)
            {
                tongTien += Convert.ToInt32(row.Cells["ThanhTien"].Value);
            }
            lblTongTien.Text = string.Format("{0:N0} VNĐ", tongTien);
        }
    }
}