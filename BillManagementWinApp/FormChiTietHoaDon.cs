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

        IKhachHangReponsity khachHangReponsity = new KhachHangReponsity();

        BindingSource source;

        public ChiTietHoaDon ChiTietHoaDonInfo { get; set; }

        private void FormChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon(khachHangReponsity.GetAll());
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
                txtTinhTien.Text = null;

                source = new BindingSource();
                source.DataSource = cthd.ToList();

                txtDiaChi.Clear();
                txtDinhMuc.Clear();
                txtDongia.Clear();
                txtHoTen.Clear();
                txtMa.Clear();
                txtQuocTich.Clear();
                txtSoLuong.Clear();
                txtTinhTien.Clear();

                txtMa.DataBindings.Add("Text", source, "MaKH");
                txtHoTen.DataBindings.Add("Text", source, "HoTenKH");
                txtDiaChi.DataBindings.Add("Text", source, "DiaChiKH");
                txtQuocTich.DataBindings.Add("Text", source, "QuocTich");
                txtSoLuong.DataBindings.Add("Text", source, "SoLuongTieuThu");
                txtDongia.DataBindings.Add("Text", source, "DonGia");
                txtDinhMuc.DataBindings.Add("Text", source, "DinhMucTieuThu");
                cboDoiTuong.DataBindings.Add("Text", source, "DoiTuongKH");

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
            txtDiaChi.Clear();
            txtDinhMuc.Clear();
            txtDongia.Clear();
            txtHoTen.Clear();
            txtMa.Clear();
            txtQuocTich.Clear();
            txtSoLuong.Clear();
            txtTinhTien.Clear();
            cboDoiTuong.SelectedIndex = -1;

            txtMa.Focus();
        }






        private void dvgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
