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
    public partial class FormInsertHoaDon : Form
    {
        public FormInsertHoaDon()
        {
            InitializeComponent();
        }

        public IKhachHangReponsity khachHangReponsity = new KhachHangReponsity();
        public ChiTietHoaDon chiTietHoaDon { get; set; }
        public bool InsertOrUpdate { get; set; }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var cthd = new ChiTietHoaDon()
                {
                    MaKh = int.Parse(txtMakh.Text),
                    HoTenKh = txthotenkh.Text,
                    DiaChiKh = txtDiaChi.Text,
                    DoiTuongKh = cboDoituongkh.Text,
                    QuocTich = txtQuocTich.Text,
                    SoLuongTieuThu = float.Parse(txtSoLuongTieuThu.Text),
                    DonGia = float.Parse(txtDonGia.Text),
                    DinhMucTieuThu = float.Parse(txtDinhMucTieuThu.Text),

                };

                if (InsertOrUpdate == false)
                {
                    khachHangReponsity.Add(cthd);
                    MessageBox.Show("Tạo hoá đơn mới thành công !!");
                }
                else
                {
                    khachHangReponsity.Update(cthd);
                    MessageBox.Show("Cập nhật hoá đơn thành công !!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Tạo mới hoá đơn !" : "Cập nhật hoá đơn !");
            }
        }

        private void FormInsertHoaDon_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate == true)
            {
                txtDiaChi.Text = chiTietHoaDon.DiaChiKh.ToString();
                txtDinhMucTieuThu.Text = chiTietHoaDon.DinhMucTieuThu.ToString();
                txtDonGia.Text = chiTietHoaDon.DonGia.ToString();
                txthotenkh.Text = chiTietHoaDon.HoTenKh;
                txtMakh.Text = chiTietHoaDon.MaKh.ToString();
                txtQuocTich.Text = chiTietHoaDon.QuocTich;
                txtSoLuongTieuThu.Text = chiTietHoaDon.SoLuongTieuThu.ToString();
                cboDoituongkh.Text = chiTietHoaDon.DoiTuongKh;
            }
        }
    }
}
