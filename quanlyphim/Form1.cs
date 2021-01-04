using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace quanlyphim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string sqlconnect = @"Data Source=.;Initial Catalog=QLPhim;Integrated Security=True";

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();
            String sql = "SELECT MaPhim, TenPhim, TheLoai, DinhDang3D, ThoiLuong, MoTa FROM Phim";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            con.Close();

            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dataGridView1.Columns[0].HeaderText = "Mã Phim";
            dataGridView1.Columns[1].HeaderText = "Tên Phim";
            dataGridView1.Columns[2].HeaderText = "Thể Loại";
            dataGridView1.Columns[3].HeaderText = "Định Dạng 3D";
            dataGridView1.Columns[4].HeaderText = "Thời Lượng";
            dataGridView1.Columns[5].HeaderText = "Mô Tả";

            cbboxTheloai.ResetText();
            cbboxTheloai.Items.Clear();

            cbboxTheloai.Items.Add("Tình Cảm");
            cbboxTheloai.Items.Add("Hành Động");
            cbboxTheloai.Items.Add("Giả Tưởng");
            cbboxTheloai.Items.Add("Hoạt Hình");
            cbboxTheloai.Items.Add("Kinh Dị");
            cbboxTheloai.Items.Add("Hài Kịch");
            if(ds.Tables[0].Rows.Count > 0)
            {
                cbboxTheloai.SelectedText = ds.Tables[0].Rows[0][2].ToString();
            }
            else
            {
                cbboxTheloai.SelectedText = "Tình Cảm";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maphim = txtMaphim.Text;
            string tenphim = txtTenphim.Text;
            string theloai = cbboxTheloai.Text;
            int dinhdang3d = (checkBox3d.Checked == true) ? 1 : 0;
            int thoiluong;
            string mota = txtMota.Text;

            if (int.TryParse(numericUpDownThoiluong.Value.ToString(), out thoiluong))
            {
                if (maphim.Equals("") || tenphim.Equals(""))
                {
                    MessageBox.Show("Trường Mã và Tên Phim không được để trống!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "INSERT Phim VALUES('" + maphim + "', N'" + tenphim + "', " +
                        "N'" + theloai + "', " + dinhdang3d + ", " + thoiluong + ", N'" + mota + "', 1)";
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
                MessageBox.Show("Định dạng thời lượng sai!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maphim = txtMaphim.Text;
            string tenphim = txtTenphim.Text;
            string theloai = cbboxTheloai.Text;
            int dinhdang3d = (checkBox3d.Checked == true) ? 1 : 0;
            int thoiluong;
            string mota = txtMota.Text;

            if (int.TryParse(numericUpDownThoiluong.Value.ToString(), out thoiluong))
            {
                if (maphim.Equals("") || tenphim.Equals(""))
                {
                    MessageBox.Show("Trường Mã và Tên Phim không được để trống!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "UPDATE Phim SET TenPhim = N'"+ tenphim +"', TheLoai = N'"+ theloai +"'," +
                        "DinhDang3D = "+ dinhdang3d +", ThoiLuong = "+ thoiluong +", MoTa = N'"+ mota +"' WHERE MaPhim = '"+ maphim +"'";
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
                MessageBox.Show("Định dạng thời lượng sai!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maphim = txtMaphim.Text;

            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();

            SqlCommand command = new SqlCommand();
            command = con.CreateCommand();
            command.CommandText = "DELETE FROM Phim WHERE MaPhim = '" + maphim + "'";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaphim.Text = dataGridView1.CurrentRow.Cells["MaPhim"].Value.ToString();
            txtTenphim.Text = dataGridView1.CurrentRow.Cells["TenPhim"].Value.ToString();
            cbboxTheloai.Text = dataGridView1.CurrentRow.Cells["TheLoai"].Value.ToString();
            bool check3d = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["DinhDang3D"].Value.ToString());
            if(check3d == true)
            {
                checkBox3d.Checked = true;
            }
            else
            {
                checkBox3d.Checked = false;
            }
            numericUpDownThoiluong.Value = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ThoiLuong"].Value.ToString());
            txtMota.Text = dataGridView1.CurrentRow.Cells["MoTa"].Value.ToString();
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMaphim.Clear();
            txtTenphim.Clear();
            cbboxTheloai.Text = null;
            checkBox3d.Checked = false;
            numericUpDownThoiluong.Value = 0;
            txtMota.Clear();
        }
    }
}
