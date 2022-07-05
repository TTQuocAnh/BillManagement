using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Reponsity;
using BillObject.Models;

namespace BillManagementWinApp
{
    public partial class FormChiTietHoaDon : Form
    {
        public FormChiTietHoaDon()
        {
            InitializeComponent();
        }

        string searchType = "";

        IKhachHangReponsity khachHangReponsity = new KhachHangReponsity();

        BindingSource source;

        public ChiTietHoaDon ctHoaDonInfo { get; set; }

        private void FormChiTietHoaDon_Load(object sender, EventArgs e)
        {
            txtDiaChi.Enabled = false;
            txtDinhMuc.Enabled = false;
            txtDongia.Enabled = false;
            txtHoTen.Enabled = false;
            txtMa.Enabled = false;
            txtQuocTich.Enabled = false;
            txtSoLuong.Enabled = false;
            cboDoiTuong.Enabled = false;
        }


        private void LoadHoaDon(IEnumerable<ChiTietHoaDon> cthd)
        {
            try
            {
                txtMa.Text = null;
                txtHoTen.Text = null;
                txtDiaChi.Text = null;
                txtDinhMuc.Text = null;
                txtDongia.Text = null;
                txtQuocTich.Text = null;
                txtSoLuong.Text = null;


                source = new BindingSource();
                source.DataSource = cthd.ToList();


                txtDiaChi.DataBindings.Clear();
                txtDinhMuc.DataBindings.Clear();
                txtDongia.DataBindings.Clear();
                txtHoTen.DataBindings.Clear();
                txtMa.DataBindings.Clear();
                txtQuocTich.DataBindings.Clear();
                txtSoLuong.DataBindings.Clear();

                cboDoiTuong.DataBindings.Clear();

                txtMa.DataBindings.Add("Text", source, "MaKH");
                txtHoTen.DataBindings.Add("Text", source, "HoTenKH");
                txtDiaChi.DataBindings.Add("Text", source, "DiaChiKH");
                txtQuocTich.DataBindings.Add("Text", source, "QuocTich");
                txtSoLuong.DataBindings.Add("Text", source, "SoLuongTieuThu");
                txtDongia.DataBindings.Add("Text", source, "DonGia");
                txtDinhMuc.DataBindings.Add("Text", source, "DinhMucTieuThu");
                cboDoiTuong.DataBindings.Add("Text", source, "DoiTuongKH");

                dvgData.DataSource = null;
                dvgData.DataSource = source;

                if (cthd.Count() == 0)
                {
                    btnXoa.Enabled = false;
                }
                else
                {
                    btnXoa.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            FormInsertHoaDon formInsertHoaDon = new FormInsertHoaDon()
            {
                Text = "Thêm mới hoá đơn",
                InsertOrUpdate = false,
                khachHangReponsity = khachHangReponsity
            };
            if (formInsertHoaDon.ShowDialog() == DialogResult.OK)
            {
                LoadHoaDon(khachHangReponsity.GetAll());
                source.Position = source.Position - 1;
            }
        }

        public ChiTietHoaDon GetCTHDObject()
        {
            ChiTietHoaDon cthd = null;
            try
            {
                cthd = new ChiTietHoaDon()
                {
                    MaKh = int.Parse(txtMa.Text),
                    HoTenKh = txtHoTen.Text,
                    DiaChiKh = txtDiaChi.Text,
                    DoiTuongKh = cboDoiTuong.Text,
                    QuocTich = txtQuocTich.Text,
                    SoLuongTieuThu = float.Parse(txtSoLuong.Text),
                    DonGia = float.Parse(txtDongia.Text),
                    DinhMucTieuThu = float.Parse(txtDinhMuc.Text),
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            return cthd;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadHoaDon(khachHangReponsity.GetAll());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var cthd = GetCTHDObject();
                khachHangReponsity.Delete(cthd);
                MessageBox.Show("Xoá thành công !");
                LoadHoaDon(khachHangReponsity.GetAll());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn kết thúc chương trình không ?"
                , "Xác nhận thoát chương trình", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            FormInsertHoaDon formInsertHoaDon = new FormInsertHoaDon()
            {
                Text = "Cập nhật thông tin hoá đơn",
                InsertOrUpdate = true,
                chiTietHoaDon = GetCTHDObject(),
                khachHangReponsity = khachHangReponsity
            };
            if (formInsertHoaDon.ShowDialog() == DialogResult.OK)
            {
                LoadHoaDon(khachHangReponsity.GetAll());
                source.Position = source.Position - 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã cần tìm ..");
            }
            else
            {
                try
                {
                    int Id = int.Parse(txtTimKiem.Text);

                    if (Id != 0)
                    {
                        khachHangReponsity.SearchByID(Id);
                        LoadHoaDon(khachHangReponsity.SearchByID(Id));
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }



        }

        private void btnTimDiaChi_Click(object sender, EventArgs e)
        {
            string diaChi = txtTimkiemDiaChi.Text;
            try
            {
                if (diaChi == null)
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ cần tìm ..");
                }
                else
                {
                    khachHangReponsity.SearchByDiaChi(diaChi);
                    LoadHoaDon(khachHangReponsity.SearchByDiaChi(diaChi));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string quocTich = cboFilter.Text;
            try
            {
                khachHangReponsity.LocTheoQuocTich(quocTich);
                LoadHoaDon(khachHangReponsity.LocTheoQuocTich(quocTich));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            FormThanhToanHoaDon frmThanhToanHoaDon = new FormThanhToanHoaDon()
            {
                Text = "Thanh toán hoá đơn",
                InsertOrUpdate = false,
                chiTietHoaDon = GetCTHDObject(),
                khachHangReponsity = khachHangReponsity
            };
            if (frmThanhToanHoaDon.ShowDialog() == DialogResult.OK)
            {
                LoadHoaDon(khachHangReponsity.GetAll());
                source.Position = source.Position - 1;
            }
        }


    }
}
