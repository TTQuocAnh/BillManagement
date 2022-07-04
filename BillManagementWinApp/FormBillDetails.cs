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
using BillManagementWinApp;

namespace BillManagementWinApp
{
    public partial class FormBillDetails : Form
    {
        public FormBillDetails()
        {
            InitializeComponent();
        }

        string strConn = "Server=(local);uid=sa;pwd=12345;database=CSDL_QLHDTDKL;TrustServerCertificate=True";
        SqlConnection conn = null;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormBillManagement().Show();
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

        private void FormBillDetails_Load(object sender, EventArgs e)
        {
            txtMakh.Text = "";
            txtHotenKh.Text = "";
            txtDiaChi.Text = "";
            txtQuocTich.Text = "";
            txtDoiTuongKh.Text = "";
            txtSoLuongTt.Text = "";
            txtDinhMucTt.Text = "";
            txtMakh.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ThemMoi();
        }

        private void ThemMoi()
        {
            
        }
    }
}
