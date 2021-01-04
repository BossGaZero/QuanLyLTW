using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace quanlylap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string sqlconnect = @"Data Source=.;Initial Catalog=CuaHangLaptop;Integrated Security=True";

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();
            String sql = "SELECT MaLT, TenLT, ThuongHieu, CauHinh, GiaBan, MoTa FROM Laptop";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            con.Close();

            dataGridView1.Columns[4].DefaultCellStyle.Format = "#,000";

            dataGridView1.Columns[0].HeaderText = "Mã Laptop";
            dataGridView1.Columns[1].HeaderText = "Tên Laptop";
            dataGridView1.Columns[2].HeaderText = "Thương Hiệu";
            dataGridView1.Columns[3].HeaderText = "Cấu Hình";
            dataGridView1.Columns[4].HeaderText = "Giá Bán";
            dataGridView1.Columns[5].HeaderText = "Mô Tả";

            cbboxThuonghieu.ResetText();
            cbboxThuonghieu.Items.Clear();

            cbboxThuonghieu.Items.Add("Lenovo");
            cbboxThuonghieu.Items.Add("HP");
            cbboxThuonghieu.Items.Add("Dell");
            cbboxThuonghieu.Items.Add("Acer");
            cbboxThuonghieu.Items.Add("Asus");
            cbboxThuonghieu.Items.Add("Samsung");
            cbboxThuonghieu.Items.Add("Toshiba");
            cbboxThuonghieu.Text = "Lenovo";

            numericUpDownGia.DecimalPlaces = 0;
            numericUpDownGia.ThousandsSeparator = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string malap = txtMaLaptop.Text;
            string tenlap = txtTenLaptop.Text;
            string thuonghieu = cbboxThuonghieu.Text;
            string cauhinh = txtCauhinh.Text;
            int gia;
            string mota = txtMota.Text;

            if (int.TryParse(numericUpDownGia.Value.ToString(), out gia))
            {
                if(malap.Equals("") || tenlap.Equals(""))
                {
                    MessageBox.Show("Trường Mã và Tên Laptop không được để trống!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "INSERT Laptop VALUES('" + malap + "', N'" + tenlap + "', " +
                        "N'" + thuonghieu + "', N'" + cauhinh + "', " + gia + ", N'" + mota + "', 1)";
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
            else
            {
                MessageBox.Show("Định dạng giá sai!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string malap = txtMaLaptop.Text;
            string tenlap = txtTenLaptop.Text;
            string thuonghieu = cbboxThuonghieu.Text;
            string cauhinh = txtCauhinh.Text;
            int gia;
            string mota = txtMota.Text;

            if (int.TryParse(numericUpDownGia.Value.ToString(), out gia))
            {
                if (malap.Equals("") || tenlap.Equals(""))
                {
                    MessageBox.Show("Trường Mã và Tên Laptop không được để trống!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "UPDATE Laptop SET TenLT = '"+ tenlap +"', ThuongHieu = N'"+ thuonghieu +"'," +
                        "CauHinh = N'"+ cauhinh +"', GiaBan = "+ gia +", MoTa = N'"+ mota +"' WHERE MaLT = '"+ malap +"'";
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
            else
            {
                MessageBox.Show("Định dạng giá sai!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string malap = txtMaLaptop.Text;

            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();

            SqlCommand command = new SqlCommand();
            command = con.CreateCommand();
            command.CommandText = "DELETE FROM Laptop WHERE MaLT = '" + malap + "'";
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
            Form1_Load(sender, e);
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMaLaptop.Clear();
            txtTenLaptop.Clear();
            cbboxThuonghieu.Text = null;
            txtCauhinh.Clear();
            numericUpDownGia.Value = 1000000;
            txtMota.Clear();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLaptop.Text = dataGridView1.CurrentRow.Cells["MaLT"].Value.ToString();
            txtTenLaptop.Text = dataGridView1.CurrentRow.Cells["TenLT"].Value.ToString();
            cbboxThuonghieu.Text = dataGridView1.CurrentRow.Cells["ThuongHieu"].Value.ToString();
            txtCauhinh.Text = dataGridView1.CurrentRow.Cells["CauHinh"].Value.ToString();
            numericUpDownGia.Value = Convert.ToInt32(dataGridView1.CurrentRow.Cells["GiaBan"].Value.ToString());
            txtMota.Text = dataGridView1.CurrentRow.Cells["MoTa"].Value.ToString();
        }
    }
}
