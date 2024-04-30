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

namespace Museum
{
    public partial class MUSEU : Form
    {

        Database database = new Database();
        public MUSEU()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += Login_FormClosing;
        }

        private void MUSEU_Load(object sender, EventArgs e)
        {
            textBox4.PasswordChar = '*';
            textBox4.MaxLength = 50;
            textBox3.MaxLength = 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var loginUser = textBox3.Text;
            var passwUser = md5.hashPassword(textBox4.Text);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user, utype from register where login_user='{loginUser}' and password_user = '{passwUser}'";

            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                var user = new Check(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));

                MessageBox.Show("Ви увійшли в систему.", "Вітаємо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (user.Utype)
                {
                    Basa form1 = new Basa(user);
                    this.Hide();
                    form1.ShowDialog();
                    this.Show();
                }
                else // Если пользователь - обычный пользователь
                {
                    User form2 = new User(user);
                    this.Hide();
                    form2.ShowDialog();
                    this.Show();
                }

            }
            else
            {
                MessageBox.Show("Нема такого.", "До побачення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Regist frm_reg = new Regist();
            frm_reg.Show();
            this.Hide();
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
