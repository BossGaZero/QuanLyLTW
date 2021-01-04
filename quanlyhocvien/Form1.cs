using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Windows.Forms;

namespace quanlyhocvien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string sqlconnect = @"Data Source=.;Initial Catalog=QLHocSinhSinhVien;Integrated Security=True";

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();
            String sql = "SELECT MSSV, Ho, Ten, Lop, NgaySinh, DiaChi FROM SinhVien";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            con.Close();

            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns[0].HeaderText = "MSSV";
            dataGridView1.Columns[1].HeaderText = "Họ";
            dataGridView1.Columns[2].HeaderText = "Tên";
            dataGridView1.Columns[3].HeaderText = "Lớp";
            dataGridView1.Columns[4].HeaderText = "Ngày Sinh";
            dataGridView1.Columns[5].HeaderText = "Địa Chỉ";

            cbboxLop.ResetText();
            cbboxLop.Items.Clear();

            cbboxLop.Items.Add("CĐN QTM17A");
            cbboxLop.Items.Add("CĐN QTM17B");
            cbboxLop.Items.Add("CĐN QTM17C");
            cbboxLop.Text = "CĐN QTM17A";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string mssv = txtMssv.Text;
            string ho = txtHo.Text;
            string ten = txtTen.Text;
            string lop = cbboxLop.Text;
            string ngaysinh = dtimeNgaysinh.Text;
            string diachi = txtDiachi.Text;

            if(mssv.Equals("") || ho.Equals("") || ten.Equals(""))
            {
                MessageBox.Show("Trường MSSV, Họ và Tên không được để trống!");
            }
            else
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();

                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandText = "INSERT SinhVien VALUES('"+ mssv +"', N'"+ ho +"', "+
                    "N'"+ ten +"', N'"+ lop +"', '"+ ngaysinh +"', N'"+ diachi +"', 1)";
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Thêm thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Thêm thông tin thất bại!");
                }
                con.Close();
                Form1_Load(sender, e);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string mssv = txtMssv.Text;
            string ho = txtHo.Text;
            string ten = txtTen.Text;
            string lop = cbboxLop.Text;
            string ngaysinh = dtimeNgaysinh.Text;
            string diachi = txtDiachi.Text;

            if (mssv.Equals("") || ho.Equals("") || ten.Equals(""))
            {
                MessageBox.Show("Trường MSSV, Họ và Tên không được để trống!");
            }
            else
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();

                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandText = "UPDATE SinhVien SET Ho = N'" + ho + "', Ten = N'" + ten + "'," +
                    "Lop = N'" + lop + "', NgaySinh = '" + ngaysinh + "', DiaChi = N'" + diachi + "' WHERE MSSV = '" + mssv + "'";
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!");
                }
                con.Close();
                Form1_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mssv = txtMssv.Text;

            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();

            SqlCommand command = new SqlCommand();
            command = con.CreateCommand();
            command.CommandText = "DELETE FROM SinhVien WHERE MSSV = '"+mssv+"'";
            int i = command.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Xóa thông tin thành công!");
            }
            else
            {
                MessageBox.Show("Xóa thông tin thất bại!");
            }
            con.Close();
            Form1_Load(sender,e);
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMssv.Clear();
            txtHo.Clear();
            txtTen.Clear();
            cbboxLop.Text = null;
            txtDiachi.Clear();
            dtimeNgaysinh.Value = DateTime.Today;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMssv.Text = dataGridView1.CurrentRow.Cells["MSSV"].Value.ToString();
            txtHo.Text = dataGridView1.CurrentRow.Cells["Ho"].Value.ToString();
            txtTen.Text = dataGridView1.CurrentRow.Cells["Ten"].Value.ToString();
            cbboxLop.Text = dataGridView1.CurrentRow.Cells["Lop"].Value.ToString();

            dtimeNgaysinh.Text = dataGridView1.CurrentRow.Cells["NgaySinh"].Value.ToString();
            dtimeNgaysinh.CustomFormat = "dd/MM/yyyy";
            dtimeNgaysinh.Format = DateTimePickerFormat.Custom;

            txtDiachi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
        }
    }
}
