using KTGK.BUS;
using KTGK.DAL.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTGK.GUI
{
    public partial class Form1 : Form
    {
        private readonly SanPhamService sanphamService = new SanPhamService();
        private readonly LoaiService loaiService = new LoaiService();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var listSanPham = sanphamService.GetAll();
            var listLoai = loaiService.GetAll();
            FillLoaiCombobox(listLoai);
            BindGrid(listSanPham);
        }
        private void BindGrid(List<SANPHAM> listStudent)
        {
            dgvSanPham.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvSanPham.Rows.Add();
                dgvSanPham.Rows[index].Cells[0].Value = item.MASANPHAM;
                dgvSanPham.Rows[index].Cells[1].Value = item.TENSANPHAM;
                dgvSanPham.Rows[index].Cells[2].Value = item.NGAYNHAP;
                dgvSanPham.Rows[index].Cells[3].Value= item.LOAISP.TENLOAI;
                
            }
        }
        private void FillLoaiCombobox(List<LOAISP> listLoai)
        {
            listLoai.Insert(0, new LOAISP());
            this.cmbloai.DataSource = listLoai;
            this.cmbloai.DisplayMember = "TENLOAI";
            this.cmbloai.ValueMember = "MALOAI";
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            var sanPham = new SANPHAM
            {
                MASANPHAM = txtmasp.Text,
                TENSANPHAM = txttensp.Text,
                NGAYNHAP = dateTimePicker1.Value,
                MALOAI = cmbloai.SelectedValue.ToString()
            };

            sanphamService.Add(sanPham);
            BindGrid(sanphamService.GetAll());
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                // Tạo một đối tượng SANPHAM mới với thông tin đã nhập
                var sanPham = new SANPHAM
                {
                    MASANPHAM = dgvSanPham.CurrentRow.Cells[0].Value.ToString(), // Mã sản phẩm hiện tại
                    TENSANPHAM = txttensp.Text, // Tên sản phẩm từ TextBox
                    NGAYNHAP = dateTimePicker1.Value, // Ngày nhập từ DateTimePicker
                    MALOAI = cmbloai.SelectedValue.ToString() // Mã loại từ ComboBox
                };
                sanphamService.Update(sanPham);
                BindGrid(sanphamService.GetAll());
            }
        }


        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                // Lấy mã sản phẩm từ dòng hiện tại
                string maSanPham = dgvSanPham.CurrentRow.Cells[0].Value.ToString();

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?",
                                                     "Xác nhận xóa!",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Gọi phương thức xóa
                    sanphamService.Delete(maSanPham);
                    // Tải lại dữ liệu vào DataGridView
                    BindGrid(sanphamService.GetAll());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu dòng hợp lệ
            {
                // Lấy thông tin từ dòng được chọn
                var row = dgvSanPham.Rows[e.RowIndex];
                txtmasp.Text = row.Cells[0].Value.ToString(); // Mã sản phẩm
                txttensp.Text = row.Cells[1].Value.ToString(); // Tên sản phẩm
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells[2].Value); // Ngày nhập
                cmbloai.SelectedValue = row.Cells[3].Value.ToString(); // Mã loại
            }
        }

        private void btntim_Click(object sender, EventArgs e)
        {
            /*
            string tenSanPham = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(tenSanPham))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm để tìm kiếm.");
                return;
            }

            var sanPham = sanphamService.FindByTenSanPham(tenSanPham);

            if (sanPham != null)
            {
                txtmasp.Text = sanPham.MASANPHAM;
                txttensp.Text = sanPham.TENSANPHAM;
                dateTimePicker1.Value = sanPham.NGAYNHAP ?? DateTime.Now; // Kiểm tra null
                cmbloai.SelectedValue = sanPham.MALOAI; // Chọn loại tương ứng
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm với tên đã nhập.");
            }
            */
            string tenSanPham = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(tenSanPham))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm để tìm kiếm.");
                return;
            }

            var sanPham = sanphamService.FindByTenSanPham(tenSanPham);

            if (sanPham != null)
            {
                // Hiển thị thông tin sản phẩm trong TextBox
                txtmasp.Text = sanPham.MASANPHAM;
                txttensp.Text = sanPham.TENSANPHAM;
                dateTimePicker1.Value = sanPham.NGAYNHAP ?? DateTime.Now; // Kiểm tra null
                cmbloai.SelectedValue = sanPham.MALOAI; // Chọn loại tương ứng

                // Đưa dữ liệu vào DataGridView
                dgvSanPham.Rows.Clear(); // Xóa dữ liệu cũ
                int index = dgvSanPham.Rows.Add();
                dgvSanPham.Rows[index].Cells[0].Value = sanPham.MASANPHAM;
                dgvSanPham.Rows[index].Cells[1].Value = sanPham.TENSANPHAM;
                dgvSanPham.Rows[index].Cells[2].Value = sanPham.NGAYNHAP?.ToString("dd/MM/yyyy"); // Ngày nhập
                dgvSanPham.Rows[index].Cells[3].Value = sanPham.MALOAI;
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm với tên đã nhập.");
            }
        }
    

        private void btnthoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }

}
