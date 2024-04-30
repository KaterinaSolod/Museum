using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Museum
{
    public partial class User : Form
    {
        Database database = new Database();
        private Check _user;
        private const string connectionString = "Data Source=DESKTOP-KKKBFA0\\MSSQLSERVER01;Initial Catalog=OBDZK;Integrated Security=True";

        public User(Check user)
        {
            _user = user;
            InitializeComponent();
            this._user = user;
        }

        private void User_Load(object sender, EventArgs e)
        {
            lb1.Text = $"{_user.Login}: {_user.Status}";
            FillDataGridView();
            FillSecondDataGridView();
        }

        private void FillDataGridView()
        {
            string query = @"
            SELECT Zal.Naimenyvannya AS 'Зал', Podiya.Vud AS 'Вид', Podiya.Nazva AS 'Назва', Podiya.Provedennya AS 'Дата', Podiya.Kyrator AS 'Куратор', Podiya.Vartist AS 'Вартість'
            FROM Proveden
            JOIN Zal ON Proveden.Nomer = Zal.Nomer
            JOIN Podiya ON Proveden.id = Podiya.id";

            using (SqlConnection connection = database.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                System.Data.DataTable table = new System.Data.DataTable();
              adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void FillSecondDataGridView()
        {
            try
            {
                string query = @"
                SELECT Eksponat.Vudek AS 'Вид', Eksponat.Nazvanie AS 'Назва', Eksponat.Autor AS 'Автор', Zal.Naimenyvannya AS 'Зал', Zal.Poverx AS 'Поверх'
                FROM Zberigat
                JOIN Zal ON Zberigat.Nomer = Zal.Nomer
                JOIN Eksponat ON Zberigat.Identif = Eksponat.Identif";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable table = new System.Data.DataTable();
                    adapter.Fill(table);
                    dataGridView2.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateDataGridView()
        {
            string searchTerm1 = textBox1.Text.Trim();
            string searchTerm2 = textBox3.Text.Trim();
            string searchTerm3 = combo2.SelectedItem?.ToString();
            string searchTerm4 = comboBox1.SelectedItem?.ToString();

            string query = @"
        SELECT Zal.Naimenyvannya AS 'Зал', 
               Podiya.Vud AS 'Вид', 
               Podiya.Nazva AS 'Назва', 
               Podiya.Provedennya AS 'Дата', 
               Podiya.Kyrator AS 'Куратор', 
               Podiya.Vartist AS 'Вартість'
        FROM Proveden
        JOIN Zal ON Proveden.Nomer = Zal.Nomer
        JOIN Podiya ON Proveden.id = Podiya.id
        WHERE 1=1";

            if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2) || !string.IsNullOrEmpty(searchTerm3) || !string.IsNullOrEmpty(searchTerm4))

            {
                query += " AND (";

                if (!string.IsNullOrEmpty(searchTerm1))
                {
                    query += "Zal.Naimenyvannya LIKE @searchTerm1";
                }

                if (!string.IsNullOrEmpty(searchTerm2))
                {
                    if (!string.IsNullOrEmpty(searchTerm1))
                    {
                        query += " AND ";
                    }
                    query += "Podiya.Nazva LIKE @searchTerm2";
                }

                if (!string.IsNullOrEmpty(searchTerm3))
                {
                    if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2))
                    {
                        query += " AND ";
                    }
                    query += "Podiya.Kyrator LIKE @searchTerm3";
                }
                if (!string.IsNullOrEmpty(searchTerm4))
                {
                    if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2) || !string.IsNullOrEmpty(searchTerm3))
                    {
                        query += " AND ";
                    }
                    query += "Podiya.Vud LIKE @searchTerm4";
                }

                query += ")";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                if (!string.IsNullOrEmpty(searchTerm1))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm1", "%" + searchTerm1 + "%");
                }

                if (!string.IsNullOrEmpty(searchTerm2))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm2", "%" + searchTerm2 + "%");
                }

                if (!string.IsNullOrEmpty(searchTerm3))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm3", "%" + searchTerm3 + "%");
                }

                if (!string.IsNullOrEmpty(searchTerm4))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm4", "%" + searchTerm4 + "%");
                }

                System.Data.DataTable table = new System.Data.DataTable();

                adapter.Fill(table);

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("За введеними даними нічого не знайдено.");
                }

                dataGridView1.DataSource = table;
            }
        }

        private void UpdateDataGrid2()
        {
            string searchTerm1 = textBox2.Text.Trim();
            string searchTerm2 = textBox6.Text.Trim();
            string searchTerm3 = textBox7.Text.Trim();
            string searchTerm4 = textBox8.Text.Trim();
            string searchTerm5 = textBox9.Text.Trim();
            string query = @"
        SELECT Eksponat.Vudek AS 'Вид', 
               Eksponat.Nazvanie AS 'Назва', 
               Eksponat.Autor AS 'Автор', 
               Zal.Naimenyvannya AS 'Зал', 
               Zal.Poverx AS 'Поверх'
        FROM Zberigat
        JOIN Zal ON Zberigat.Nomer = Zal.Nomer
        JOIN Eksponat ON Zberigat.Identif = Eksponat.Identif
        WHERE 1=1";

            if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2) || !string.IsNullOrEmpty(searchTerm3) || !string.IsNullOrEmpty(searchTerm4) || !string.IsNullOrEmpty(searchTerm5))

            {
                query += " AND (";

                if (!string.IsNullOrEmpty(searchTerm1))
                {
                    query += "Eksponat.Vudek LIKE @searchTerm1";
                }

                if (!string.IsNullOrEmpty(searchTerm2))
                {
                    if (!string.IsNullOrEmpty(searchTerm1))
                    {
                        query += " AND ";
                    }
                    query += "Eksponat.Nazvanie LIKE @searchTerm2";
                }

                if (!string.IsNullOrEmpty(searchTerm3))
                {
                    if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2))
                    {
                        query += " AND ";
                    }
                    query += "Eksponat.Autor LIKE @searchTerm3";
                }
                if (!string.IsNullOrEmpty(searchTerm4))
                {
                    if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2) || !string.IsNullOrEmpty(searchTerm3))
                    {
                        query += " AND ";
                    }
                    query += "Zal.Naimenyvannya LIKE @searchTerm4";
                }
                if (!string.IsNullOrEmpty(searchTerm5))
                {
                    if (!string.IsNullOrEmpty(searchTerm1) || !string.IsNullOrEmpty(searchTerm2) || !string.IsNullOrEmpty(searchTerm3) || !string.IsNullOrEmpty(searchTerm4))
                    {
                        query += " AND ";
                    }
                    query += "Zal.Poverx LIKE @searchTerm5";
                }

                query += ")";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                if (!string.IsNullOrEmpty(searchTerm1))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm1", "%" + searchTerm1 + "%");
                }

                if (!string.IsNullOrEmpty(searchTerm2))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm2", "%" + searchTerm2 + "%");
                }

                if (!string.IsNullOrEmpty(searchTerm3))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm3", "%" + searchTerm3 + "%");
                }

                if (!string.IsNullOrEmpty(searchTerm4))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm4", "%" + searchTerm4 + "%");
                }
                if (!string.IsNullOrEmpty(searchTerm5))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@searchTerm5", "%" + searchTerm5 + "%");
                }
                System.Data.DataTable table = new System.Data.DataTable();

                adapter.Fill(table);

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("За введеними даними нічого не знайдено.");
                }

                dataGridView2.DataSource = table;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            UpdateDataGridView();
        }

        private void buttonSaveToWord_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Word Documents (*.docx)|*.docx";
            saveFileDialog1.Title = "Зберегти як Word документ";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Document wordDoc = wordApp.Documents.Add();
                wordApp.Visible = true;

                // Додавання таблиці до документу Word
                Microsoft.Office.Interop.Word.Tables tables = wordDoc.Tables;
                Microsoft.Office.Interop.Word.Table table = tables.Add(wordDoc.Range(), dataGridView1.Rows.Count + 1, dataGridView1.Columns.Count);

                // Заповнення даними таблиці
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dataGridView1.Columns[i].HeaderText;
                    table.Cell(1, i + 1).Borders.Enable = 1; // Включення рамок для заголовків стовпців
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        table.Cell(j + 2, i + 1).Range.Text = dataGridView1[i, j].Value.ToString();
                        table.Cell(j + 2, i + 1).Borders.Enable = 1; // Включення рамок для комірок даних
                    }
                }

                // Збереження документу
                wordDoc.SaveAs(saveFileDialog1.FileName);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            saveFileDialog2.Filter = "Word Documents (*.docx)|*.docx";
            saveFileDialog2.Title = "Зберегти як Word документ";
            saveFileDialog2.ShowDialog();

            if (saveFileDialog2.FileName != "")
            {
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Document wordDoc = wordApp.Documents.Add();
                wordApp.Visible = true;

                // Додавання таблиці до документу Word
                Microsoft.Office.Interop.Word.Tables tables = wordDoc.Tables;
                Microsoft.Office.Interop.Word.Table table = tables.Add(wordDoc.Range(), dataGridView2.Rows.Count + 1, dataGridView2.Columns.Count);

                // Заповнення даними таблиці
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dataGridView2.Columns[i].HeaderText;
                    table.Cell(1, i + 1).Borders.Enable = 1; // Включення рамок для заголовків стовпців
                    for (int j = 0; j < dataGridView2.Rows.Count; j++)
                    {
                        table.Cell(j + 2, i + 1).Range.Text = dataGridView2[i, j].Value.ToString();
                        table.Cell(j + 2, i + 1).Borders.Enable = 1; // Включення рамок для комірок даних
                    }
                }

                // Збереження документу
                wordDoc.SaveAs(saveFileDialog2.FileName);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid2();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid2();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid2();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid2();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid2();
        }

        private void combo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }
    }
}
