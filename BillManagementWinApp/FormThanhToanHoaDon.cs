using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BillObject.Models;
using DataAccess.Reponsity;

namespace BillManagementWinApp
{
    public partial class FormThanhToanHoaDon : Form
    {
        public FormThanhToanHoaDon()
        {
            InitializeComponent();
        }

        public IKhachHangReponsity khachHangReponsity = new KhachHangReponsity();
        public ChiTietHoaDon chiTietHoaDon { get; set; }
        public bool InsertOrUpdate { get; set; }

        private void FormThanhToanHoaDon_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate == false)
            {
                txtDiaChi.Text = chiTietHoaDon.DiaChiKh;
                txtDinhmuc.Text = chiTietHoaDon.DinhMucTieuThu.ToString();
                txtDonGia.Text = chiTietHoaDon.DonGia.ToString();
                txtHoTen.Text = chiTietHoaDon.HoTenKh;
                txtMa.Text = chiTietHoaDon.MaKh.ToString();
                txtQuoctich.Text = chiTietHoaDon.QuocTich;
                txtSoLuong.Text = chiTietHoaDon.SoLuongTieuThu.ToString();
                txtDoituong.Text = chiTietHoaDon.DoiTuongKh;
            }

        }

        private void txtTongChiPhi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnXemChiPhi_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtMa.Text);

            try
            {
                var tt = khachHangReponsity.TinhTienHoaDon(id);
                txtTongChiPhi.Text = tt.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void bnThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn muốn thanh toán hoá đơn này không ?", "Xác nhận thanh toán", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("Thanh toán thành công !");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại !");
            }
        }
    }
}
