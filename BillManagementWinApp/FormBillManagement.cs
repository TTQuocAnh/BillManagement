using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BillManagementWinApp
{
    public partial class FormBillManagement : Form
    {
        public FormBillManagement()
        {
            InitializeComponent();
        }

        string strConn = "Server=(local);uid=sa;pwd=12345;database=CSDL_QLHDTDKL;TrustServerCertificate=True";
        SqlConnection conn = null;

        private void FormBillManagement_Load(object sender, EventArgs e)
        {
            LoadListKH();

        }

        private void OpenConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection(strConn);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }


        private void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void LoadListKH()
        {
            try
            {
                OpenConnection();

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Select * From KhachHang";
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();
                lviData.Items.Clear();

                //chia nhóm hiển thị theo quốc tịch
                lviData.Groups.Clear();
                ListViewGroup lvgKHVN = new ListViewGroup("Khách hàng Việt Nam");
                lviData.Groups.Add(lvgKHVN);
                ListViewGroup lvgKHNN = new ListViewGroup("Khách hàng nước ngoài");
                lviData.Groups.Add(lvgKHNN);

                while (reader.Read())
                {
                    int ma = reader.GetInt32(0);
                    string hoTen = reader.GetString(1);
                    string diaChi = reader.GetString(2);
                    string quocTich = reader.GetString(3);
                    string doiTuong = reader.GetString(4);
                    double soLuong = reader.GetDouble(5);
                    double dinhMuc = reader.GetDouble(6);

                    ListViewItem lvi = new ListViewItem((lviData.Items.Count + 1) + "");

                    lvi.SubItems.Add(ma + "");
                    lvi.SubItems.Add(hoTen);
                    lvi.SubItems.Add(diaChi);
                    lvi.SubItems.Add(quocTich);
                    lvi.SubItems.Add(doiTuong);
                    lvi.SubItems.Add(soLuong + "");
                    lvi.SubItems.Add(dinhMuc + "");


                    lviData.Items.Add(lvi);

                    if (string.Compare(quocTich, "Vietnamese", true) == 0)
                    {
                        lvi.Group = lvgKHVN;
                    }
                    else
                    {
                        lvi.Group = lvgKHNN;
                    }

                    lvi.Tag = ma;

                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void lviData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lviData.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem listViewItem = lviData.SelectedItems[0];
            int ma = (int)listViewItem.Tag;
            LoadChiTietKH(ma);
        }

        private void LoadChiTietKH(int ma)
        {
            try
            {
                //đọc database
                OpenConnection();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Select * from KhachHang where MaKH=@ma";
                command.Connection = conn;

                //tạo parameters
                SqlParameter prmMa = new SqlParameter("@ma", SqlDbType.Int);
                prmMa.Value = ma;
                command.Parameters.Add(prmMa);

                //truy vấn từ database
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string ten = reader.GetString(1);
                    string diachi = reader.GetString(2);
                    string quocTich = reader.GetString(3);
                    string doiTuong = reader.GetString(4);
                    double soLuong = reader.GetDouble(5);
                    double dinhMuc = reader.GetDouble(6);

                    txtMaKH.Text = ma + "";
                    txtHoten.Text = ten;
                    txtDiachi.Text = diachi;
                    txtQuoctich.Text = quocTich;
                    txtDoituong.Text = doiTuong;
                    txtSoLuong.Text = soLuong + "";
                    txtDinhMuc.Text = dinhMuc + "";


                }
                reader.Close();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtHoten.Text = "";
            txtDiachi.Text = "";
            txtQuoctich.Text = "";
            txtDoituong.Text = "";
            txtDinhMuc.Text = "";
            txtSoLuong.Text = "";
            txtMaKH.Focus();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        private void SaveInfo()
        {
            try
            {
                OpenConnection();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                string sql = "Insert into KhachHang values(@MaKH,@HoTenKH,@DiaChiKH,@QuocTich," +
                    "@DoiTuongKH,@SoLuongTieuThu,@DinhMucTieuThu)";
                command.CommandText = sql;
                command.Connection = conn;

                command.Parameters.Add("@MaKH", SqlDbType.Int).Value = txtMaKH.Text;
                command.Parameters.Add("@HoTenKH", SqlDbType.NVarChar).Value = txtHoten.Text;
                command.Parameters.Add("@DiaChiKH", SqlDbType.NVarChar).Value = txtDiachi.Text;
                command.Parameters.Add("@QuocTich", SqlDbType.NVarChar).Value = txtQuoctich.Text;
                command.Parameters.Add("@DoiTuongKH", SqlDbType.NVarChar).Value = txtDoituong.Text;
                command.Parameters.Add("@SoLuongTieuThu", SqlDbType.Decimal).Value = txtSoLuong.Text;
                command.Parameters.Add("@DinhMucTieuThu", SqlDbType.Decimal).Value = txtDinhMuc.Text;

                int kq = command.ExecuteNonQuery();

                if (kq > 0)
                {
                    LoadListKH();
                    MessageBox.Show("Thêm mới hoá đơn thành công", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    btnNew.PerformClick();
                }
                else
                {
                    MessageBox.Show("Thêm mới hoá đơn thành công","",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
