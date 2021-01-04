using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string sqlconnect = @"Data Source=.;Initial Catalog=QLThuChi;Integrated Security=True";


        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlconnect);
            con.Open();
            String sql = "SELECT MaGiaoDich, Ngay, NoiDung, SoTien, Loai FROM GiaoDich";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();
            con.Close();

            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "#,000";

            dataGridView1.Columns[0].HeaderText = "Mã Giao Dịch";
            dataGridView1.Columns[1].HeaderText = "Ngày";
            dataGridView1.Columns[2].HeaderText = "Nội Dung";
            dataGridView1.Columns[3].HeaderText = "Số Tiền";
            dataGridView1.Columns[4].HeaderText = "Loại";

            numericUpDownSotien.DecimalPlaces = 0;
            numericUpDownSotien.ThousandsSeparator = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMagiaodich.Text = dataGridView1.CurrentRow.Cells["MaGiaoDich"].Value.ToString();

            dtimeNgay.Text = dataGridView1.CurrentRow.Cells["Ngay"].Value.ToString();
            dtimeNgay.CustomFormat = "dd/MM/yyyy";
            dtimeNgay.Format = DateTimePickerFormat.Custom;

            txtNoidung.Text = dataGridView1.CurrentRow.Cells["NoiDung"].Value.ToString();

            numericUpDownSotien.Value = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SoTien"].Value.ToString());

            string loai = dataGridView1.CurrentRow.Cells["Loai"].Value.ToString();
            if (loai.Equals("Chi"))
            {
                radioButtonChi.Checked = true;
            }
            else
            {
                radioButtonThu.Checked = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string magiaodich = txtMagiaodich.Text;
            string ngay = dtimeNgay.Text;
            string noidung = txtNoidung.Text;
            int sotien;
            string loai = (radioButtonChi.Checked == true) ? "Chi" : "Thu";

            if (int.TryParse(numericUpDownSotien.Value.ToString(), out sotien))
            {
                if (magiaodich.Equals("") || noidung.Equals(""))
                {
                    MessageBox.Show("Trường Mã giao dịch và Nội dung không được để trống!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "INSERT GiaoDich VALUES('" + magiaodich + "', '" + ngay + "', N'" + noidung + "', " + sotien + ", '" + loai + "', 1)";
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Thêm thông tin giao dịch thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thông tin giao dịch thất bại!");
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
            string magiaodich = txtMagiaodich.Text;
            string ngay = dtimeNgay.Text;
            string noidung = txtNoidung.Text;
            int sotien;
            string loai = (radioButtonChi.Checked == true) ? "Chi" : "Thu";

            if (int.TryParse(numericUpDownSotien.Value.ToString(), out sotien))
            {
                if (magiaodich.Equals("") || noidung.Equals(""))
                {
                    MessageBox.Show("Trường Mã giao dịch và Nội dung không được để trống!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(sqlconnect);
                    con.Open();

                    SqlCommand command = new SqlCommand();
                    command = con.CreateCommand();
                    command.CommandText = "UPDATE GiaoDich SET Ngay = '" + ngay + "', NoiDung = N'" + noidung + "', SoTien = " + sotien + ", Loai = N'" + loai + "' WHERE MaGiaoDich = '" + magiaodich + "'";
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin giao dịch thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin giao dịch thất bại!");
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
            DialogResult result = MessageBox.Show("Chắc chắn muốn xóa giao dịch này?", "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string magiaodich = txtMagiaodich.Text;

                SqlConnection con = new SqlConnection(sqlconnect);
                con.Open();

                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandText = "DELETE FROM GiaoDich WHERE MaGiaoDich = '" + magiaodich + "'";
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Xóa thông tin giao dịch thành công!");
                }
                else
                {
                    MessageBox.Show("Xóa thông tin giao dịch thất bại!");
                }
                con.Close();
                Form1_Load(sender, e);
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtMagiaodich.Clear();
            dtimeNgay.Value = DateTime.Today;
            txtNoidung.Clear();
            numericUpDownSotien.Value = 0;
            radioButtonThu.Checked = true;
        }
    }
}
