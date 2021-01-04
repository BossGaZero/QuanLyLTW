using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string sqlconnect = @"Data Source=.;Initial Catalog=QLDuAn;Integrated Security=True";


        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();
            String sql = "SELECT MaDuAn, TenDuAn, NgayBatDau, NgayKetThuc, HoanThanh FROM DuAn";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            con.Close();

            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";

            dataGridView1.Columns[0].HeaderText = "Mã Dự Án";
            dataGridView1.Columns[1].HeaderText = "Tên Dự Án";
            dataGridView1.Columns[2].HeaderText = "Ngày Bắt Đầu";
            dataGridView1.Columns[3].HeaderText = "Ngày Kết Thúc";
            dataGridView1.Columns[4].HeaderText = "Hoàn Thành";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaduan.Text = dataGridView1.CurrentRow.Cells["MaDuAn"].Value.ToString();
            txtTenduan.Text = dataGridView1.CurrentRow.Cells["TenDuAn"].Value.ToString();

            dtimeBatdau.Text = dataGridView1.CurrentRow.Cells["NgayBatDau"].Value.ToString();
            dtimeBatdau.CustomFormat = "dd/MM/yyyy";
            dtimeBatdau.Format = DateTimePickerFormat.Custom;

            dtimeKetthuc.Text = dataGridView1.CurrentRow.Cells["NgayKetThuc"].Value.ToString();
            dtimeKetthuc.CustomFormat = "dd/MM/yyyy";
            dtimeKetthuc.Format = DateTimePickerFormat.Custom;

            bool hoanthanh = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["HoanThanh"].Value.ToString());
            if(hoanthanh == true)
            {
                checkBoxHoanthanh.Checked = true;
            }
            else
            {
                checkBoxHoanthanh.Checked = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maduan = txtMaduan.Text;
            string tenduan = txtTenduan.Text;
            DateTime batdau, ketthuc;
            int hoanthanh = (checkBoxHoanthanh.Checked == true) ? 1 : 0;

            if (maduan.Equals("") || tenduan.Equals(""))
            {
                MessageBox.Show("Trường Mã và Tên Dự án không được để trống!");
            }
            else
            {
                if(DateTime.TryParse(dtimeBatdau.Value.ToString(), out batdau) && 
                    DateTime.TryParse(dtimeKetthuc.Value.ToString(), out ketthuc) && batdau < ketthuc)
                {
                    string ngaybatdau = batdau.ToString();
                    string ngayketthuc = ketthuc.ToString();

                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "INSERT DuAn VALUES('" + maduan + "', N'" + tenduan + "', " +
                        "'" + ngaybatdau + "', '" + ngayketthuc + "', " + hoanthanh + ", 1)";
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Thêm thông tin dự án thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thông tin dự án thất bại!");
                    }
                    con.Close();
                    Form1_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc!");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maduan = txtMaduan.Text;
            string tenduan = txtTenduan.Text;
            string ngaybatdau = dtimeBatdau.Text;
            string ngayketthuc = dtimeKetthuc.Text;
            int hoanthanh = (checkBoxHoanthanh.Checked == true) ? 1 : 0;

            if (maduan.Equals("") || tenduan.Equals(""))
            {
                MessageBox.Show("Trường Mã và Tên Dự án không được để trống!");
            }
            else
            {
                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();

                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandText = "UPDATE DuAn SET TenDuAn = N'"+ tenduan +"', NgayBatDau = '"+ ngaybatdau +"'," +
                    "NgayKetThuc = '"+ ngayketthuc +"', HoanThanh = "+ hoanthanh +" WHERE MaDuAn = '"+ maduan +"'";
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cập nhật thông tin dự án thành công!");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin dự án thất bại!");
                }
                con.Close();
                Form1_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Chắc chắn muốn xóa dự án này?", "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if(result == DialogResult.Yes)
            {
                string maduan = txtMaduan.Text;

                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();

                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandText = "DELETE FROM DuAn WHERE MaDuAn = '" + maduan + "'";
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Xóa thông tin dự án thành công!");
                }
                else
                {
                    MessageBox.Show("Xóa thông tin dự án thất bại!");
                }
                con.Close();
                Form1_Load(sender, e);
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMaduan.Clear();
            txtTenduan.Clear();
            dtimeBatdau.Value = DateTime.Today;
            dtimeKetthuc.Value = DateTime.Today;
            checkBoxHoanthanh.Checked = false;
        }
    }
}
