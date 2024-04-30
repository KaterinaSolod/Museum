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

namespace Museum
{
    public partial class Keryv : Form
    {
        Database database = new Database();
        public Keryv()
        {
            InitializeComponent();
        }

        private void CreateColums()
        {
            dataGridView1.Columns.Add("id_user", "id");
            dataGridView1.Columns.Add("login_user", "Логін");
            dataGridView1.Columns.Add("password_user", "Пароль");
            var checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Роль";
            dataGridView1.Columns.Add(checkColumn);
        }

        private void ReadRow(IDataRecord record)
        {
            dataGridView1.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetBoolean(3));
        }

        private void Rpd()
        {
            dataGridView1.Rows.Clear();
            string querystr = $"select * from register";
            SqlCommand command = new SqlCommand(querystr, database.GetConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadRow(reader);
            }
            reader.Close();

            database.closeConnection();
        }

        private void Keryv_Load(object sender, EventArgs e)
        {
            CreateColums();
            Rpd();
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            database.openConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            { 
                var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                var utype = dataGridView1.Rows[index].Cells[3].Value.ToString();
                var changestr = $"update register set utype = '{utype}' where id_user = '{id}'";
                var command = new SqlCommand(changestr, database.GetConnection());
                command.ExecuteNonQuery();
            }
            database.closeConnection();
            Rpd();
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var isAdmin = Convert.ToBoolean(dataGridView1.Rows[SelectedRowIndex].Cells[3].Value);
            if (isAdmin)
            {
                MessageBox.Show("Ви не можете видалити адміністратора.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            database.openConnection();
            var id = Convert.ToInt32(dataGridView1.Rows[SelectedRowIndex].Cells[0].Value);
            var deletequer = $"delete from register where id_user = {id}";
            var command = new SqlCommand(deletequer, database.GetConnection());
            command.ExecuteNonQuery();
            database.closeConnection();
            dataGridView1.Rows.RemoveAt(SelectedRowIndex);
        }
    }
}
