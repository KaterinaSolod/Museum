using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace Museum
{
    public partial class Regist : Form
    {
        Database database = new Database();
        public Regist()
        {
            InitializeComponent();
            this.FormClosing += Regist_FormClosing;
        }

        private void Regist_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var login = textBox1.Text;
            var passw = md5.hashPassword(textBox2.Text);
          
            if (checkuser())

            {
                return;
            }

            string querystring = $"insert into register(login_user, password_user, utype) values ('{login}', '{passw}', 0) ";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            database.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт створено", "Перемога");
                MUSEU form_loginMus = new MUSEU();
                this.Hide();
                form_loginMus.ShowDialog();
            }
            else
            {
                MessageBox.Show("Володя все фигня");
            }

             database.closeConnection();

        }
        private void Regist_FormClosing(object sender, FormClosingEventArgs e)
        {
            MUSEU loginForm = new MUSEU();
            loginForm.Show();
        }
        private Boolean checkuser()
        {
            var loginUser = textBox1.Text;
            var passwUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user, utype from register where login_user = '{loginUser}' and password_user = '{passwUser}'";
            SqlCommand command = new SqlCommand(querystring, database.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Володя, не дури");
                return true;
            }
            else return false;
        }
    }
}
