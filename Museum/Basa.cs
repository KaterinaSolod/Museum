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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using Microsoft.Office.Interop.Word;

namespace Museum
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Basa : Form
    {
        private readonly Check _user;
        Database database = new Database();
        int selectedRow;
        private const string connectionString = "Data Source=DESKTOP-KKKBFA0\\MSSQLSERVER01;Initial Catalog=OBDZK;Integrated Security=True";
        public Basa(Check user)
        {
            _user = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Utype ()
        {
            керуванняToolStripMenuItem.Enabled = _user.Utype;
            textBoxRed1.Enabled = false;
            textBoxN.Enabled = false;
            textBox1.Enabled = false;
            textBox8.Enabled = false;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lb.Text = $"{_user.Login}: {_user.Status}";
            Utype();
            CreateColums();
            RefreshDataGrid(dataGridView1);
            RefreshData(dataGridView2);
            RefreshDataGrid3(dataGridView3);
            RefreshDataGrid4(dataGridView4);
            FillDataGridView(dataGridView5);
            FillDataGridView6(dataGridView6);
            textBoxRed4.Text = DateTime.Now.ToString("yyyy-MM-dd");
            textBox11.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void ConfigureDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id","id");
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns.Add("Vud", "Вид");
            dataGridView1.Columns.Add("Nazva", "Назва");
            dataGridView1.Columns.Add("Provedennya", "Проведення");
            dataGridView1.Columns.Add("Truvalist", "Тривалість");
            dataGridView1.Columns.Add("Kyrator", "Куратор");
            dataGridView1.Columns.Add("Vartist", "Вартість");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
            ConfigureDataGridView(dataGridView1);
            dataGridView2.Columns.Add("Nomer", "Номер");
            dataGridView2.Columns["Nomer"].Visible = false;
            dataGridView2.Columns.Add("Naimenyvannya", "Найменування");
            dataGridView2.Columns.Add("Style", "Стиль");
            dataGridView2.Columns.Add("Plosha", "Площа");
            dataGridView2.Columns.Add("Poverx", "Поверх");
            dataGridView2.Columns.Add("Maxkilkist", "Макс.кількість");
            dataGridView2.Columns.Add("Naglyadach", "Наглядач");
            dataGridView2.Columns.Add("IsNew", String.Empty);
            dataGridView2.Columns["IsNew"].Visible = false;
            ConfigureDataGridView(dataGridView2);
            dataGridView3.Columns.Add("Identif", "Айді");
            dataGridView3.Columns["Identif"].Visible = false;
            dataGridView3.Columns.Add("Nazvanie", "Назва");
            dataGridView3.Columns.Add("Vudek", "Вид");
            dataGridView3.Columns.Add("Autor", "Автор");
            dataGridView3.Columns.Add("Rik", "Рік");
            dataGridView3.Columns.Add("Kilkiсt", "Кількість");
            dataGridView3.Columns.Add("Rozmir", "Розмір");
            dataGridView3.Columns.Add("IsNew", String.Empty);
            dataGridView3.Columns["IsNew"].Visible = false;
            ConfigureDataGridView(dataGridView3);
            dataGridView4.Columns.Add("Kod", "Код");
            dataGridView4.Columns["Kod"].Visible = false;
            dataGridView4.Columns.Add("Prizvushe", "Прізвище");
            dataGridView4.Columns.Add("Imya", "Ім'я");
            dataGridView4.Columns.Add("Pruinyattya", "Прийняття");
            dataGridView4.Columns.Add("Posada", "Посада");
            dataGridView4.Columns.Add("IsNew", String.Empty);
            dataGridView4.Columns["IsNew"].Visible = false;
            ConfigureDataGridView(dataGridView4);
        }

        private void Clear()
        {
            textBoxRed1.Text = "";
            textBoxRed2.Text = "";
            textBoxRed3.Text = "";
            textBoxRed4.Text = "";
            textBoxRed5.Text = "";
            textBoxRed6.Text = "";
            textBoxRed7.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
           /* textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";*/
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            int intValue = record.GetInt32(0);
            string firstStringValue = record.GetString(1);
            string secondStringValue = record.GetString(2);

            DateTime dateTimeValue;
            if (!record.IsDBNull(3))
            {
                dateTimeValue = record.GetDateTime(3);
            }
            else
            {
                dateTimeValue = DateTime.MinValue;
            }

            float floatValue = record.GetFloat(4);
            string sixthColumnValue = record.GetString(5);
            int seventhColumnValue = record.GetInt32(6);

            string formattedDate = dateTimeValue.ToString("yyyy-MM-dd");

            dgw.Rows.Add(intValue, firstStringValue, secondStringValue, formattedDate, floatValue, sixthColumnValue, seventhColumnValue, RowState.ModifiedNew);
        }

        private void ReadSingleRow2 (DataGridView dgw2, IDataRecord record)
        {
            dgw2.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4), record.GetInt32(5), record.GetString(6), RowState.ModifiedNew);
        }
        private void ReadSingleRow3(DataGridView dgw3, IDataRecord record)
        {
            dgw3.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetInt32(4), record.GetInt32(5), record.GetString(6), RowState.ModifiedNew);
        }
        private void ReadSingleRow4(DataGridView dgw4, IDataRecord record)
        {

            DateTime dateTimeValue;
            if (!record.IsDBNull(3))
            {
                dateTimeValue = record.GetDateTime(3);
            }
            else
            {
                dateTimeValue = DateTime.MinValue;
            }
            string formattedDate = dateTimeValue.ToString("yyyy-MM-dd");
            dgw4.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), formattedDate, record.GetString(4), RowState.ModifiedNew);
        }
        private void ReadSingleRow5(DataGridView dgw5, IDataRecord record)
        {

            DateTime dateTimeValue;
            if (!record.IsDBNull(3))
            {
                dateTimeValue = record.GetDateTime(3);
            }
            else
            {
                dateTimeValue = DateTime.MinValue;
            }
            string formattedDate = dateTimeValue.ToString("yyyy-MM-dd");
            dgw5.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), formattedDate);
        }
        private void RefreshData(DataGridView dgw2)
        {
            dgw2.Rows.Clear();
            string queryS = $"select * from Zal";
            SqlCommand command = new SqlCommand(queryS, database.GetConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow2(dgw2, reader);
            }
            reader.Close();
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from Podiya";
            SqlCommand command = new SqlCommand(queryString, database.GetConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void RefreshDataGrid3(DataGridView dgw3)
        {
            dgw3.Rows.Clear();

            string queryString = $"select * from Eksponat";
            SqlCommand command = new SqlCommand(queryString, database.GetConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow3(dgw3, reader);
            }
            reader.Close();
        }

        private void RefreshDataGrid4(DataGridView dgw4)
        {
            dgw4.Rows.Clear();

            string queryString = $"select * from Spivrobitnuk";
            SqlCommand command = new SqlCommand(queryString, database.GetConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow4(dgw4, reader);
            }
            reader.Close();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
        }
        private void deleteRow1()
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Visible = false;

            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }
            dataGridView2.Rows[index].Cells[7].Value = RowState.Deleted;
        }
        private void deleteRow2()
        {
            int index = dataGridView3.CurrentCell.RowIndex;
            dataGridView3.Rows[index].Visible = false;

            if (dataGridView3.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView3.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }
            dataGridView3.Rows[index].Cells[7].Value = RowState.Deleted;
        }
        private void deleteRow3()
        {
            int index = dataGridView4.CurrentCell.RowIndex;
            dataGridView4.Rows[index].Visible = false;

            if (dataGridView4.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView4.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
            dataGridView4.Rows[index].Cells[5].Value = RowState.Deleted;
        }
        private void Updatte()
        {
            database.openConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $" delete from Podiya where id = {id}";

                    var command = new SqlCommand(deleteQuery, database.GetConnection());
                    command.ExecuteNonQuery();
                }
                /*if (rowState == RowState.Modified)
                {
                  /* var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                     var Vud = dataGridView1.Rows[index].Cells[1].Value.ToString();
                     var Nazva = dataGridView1.Rows[index].Cells[2].Value.ToString();
                     var Provedennya = dataGridView1.Rows[index].Cells[3].Value.ToString();
                     var Truvalist = dataGridView1.Rows[index].Cells[4].Value.ToString();
                     var Kyrator = dataGridView1.Rows[index].Cells[5].Value.ToString();
                     var Vartist = dataGridView1.Rows[index].Cells[6].Value.ToString();

                var changeQuery = "update Podiya set Vud = @Vud, Nazva = @Nazva, Provedennya = @Provedennya, Truvalist = @Truvalist, Kyrator = @Kyrator, Vartist = @Vartist where id = @id";

                var command = new SqlCommand(changeQuery, database.GetConnection());
                command.Parameters.AddWithValue("@Vud", Vud);
                command.Parameters.AddWithValue("@Nazva", Nazva);
                command.Parameters.AddWithValue("@Provedennya", Provedennya);
                command.Parameters.AddWithValue("@Truvalist", Truvalist); // Переконайтеся, що Truvalist містить числове значення
                command.Parameters.AddWithValue("@Kyrator", Kyrator);
                command.Parameters.AddWithValue("@Vartist", Vartist);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                 }*/
            }
            database.closeConnection();
        }

        private void Updatte2()
        {
            database.openConnection();
            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView2.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Nomer = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                    var deleteQ = $" delete from Zal where Nomer = {Nomer}";

                    var command = new SqlCommand(deleteQ, database.GetConnection());
                    command.ExecuteNonQuery();
                }
            }
            database.closeConnection();
        }
        private void Updatte3()
        {
            database.openConnection();
            for (int index = 0; index < dataGridView3.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView3.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Identif = Convert.ToInt32(dataGridView3.Rows[index].Cells[0].Value);
                    var deletQ = $" delete from Eksponat where Identif = {Identif}";

                    var command = new SqlCommand(deletQ, database.GetConnection());
                    command.ExecuteNonQuery();
                }
            }
            database.closeConnection();
        }
        private void Updatte4()
        {
            database.openConnection();
            for (int index = 0; index < dataGridView4.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView4.Rows[index].Cells[5].Value;

                if (rowState == RowState.Existed)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Kod = Convert.ToInt32(dataGridView4.Rows[index].Cells[0].Value);
                    var delQ = $" delete from Spivrobitnuk where Kod = {Kod}";

                    var command = new SqlCommand(delQ, database.GetConnection());
                    command.ExecuteNonQuery();
                }
            }
            database.closeConnection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBoxRed1.Text = row.Cells[0].Value.ToString();
                textBoxRed2.Text = row.Cells[1].Value.ToString();
                textBoxRed3.Text = row.Cells[2].Value.ToString();
                textBoxRed4.Text = row.Cells[3].Value.ToString();
                textBoxRed5.Text = row.Cells[4].Value.ToString();
                textBoxRed6.Text = row.Cells[5].Value.ToString();
                textBoxRed7.Text = row.Cells[6].Value.ToString();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                textBoxN.Text = row.Cells[0].Value.ToString();
                textBoxH.Text = row.Cells[1].Value.ToString();
                textBoxS.Text = row.Cells[2].Value.ToString();
                textBoxP.Text = row.Cells[3].Value.ToString();
                textBoxPo.Text = row.Cells[4].Value.ToString();
                textBoxM.Text = row.Cells[5].Value.ToString();
                textBoxG.Text = row.Cells[6].Value.ToString();
            }
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[6].Value.ToString();
            }
        }
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[selectedRow];
                textBox8.Text = row.Cells[0].Value.ToString();
                textBox9.Text = row.Cells[1].Value.ToString();
                textBox10.Text = row.Cells[2].Value.ToString();
                textBox11.Text = row.Cells[3].Value.ToString();
                textBox12.Text = row.Cells[4].Value.ToString();
            }
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBoxRed1.Text;
            var Vud = textBoxRed2.Text;
            var Nazva = textBoxRed3.Text;
            DateTime Provedennya;
            float Truvalist;
            var Kyrator = textBoxRed6.Text;
            int Vartist;
            
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (float.TryParse(textBoxRed5.Text, out Truvalist) &&
                    int.TryParse(textBoxRed7.Text, out Vartist) &&
                    DateTime.TryParse(textBoxRed4.Text, out Provedennya))
                {
                    string formattedProvedennya = Provedennya.ToString("yyyy-MM-dd");

                    dataGridView1.Rows[selectedRowIndex].SetValues(id, Vud, Nazva, formattedProvedennya, Truvalist, Kyrator, Vartist);

                    dataGridView1.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;


                    var changeQuery = "update Podiya set Vud = @Vud, Nazva = @Nazva, Provedennya = @Provedennya, Truvalist = @Truvalist, Kyrator = @Kyrator, Vartist = @Vartist where id = @id";

                    var command = new SqlCommand(changeQuery, database.GetConnection());
                    command.Parameters.AddWithValue("@Vud", Vud);
                    command.Parameters.AddWithValue("@Nazva", Nazva);
                    command.Parameters.AddWithValue("@Provedennya", Provedennya);
                    command.Parameters.AddWithValue("@Truvalist", Truvalist);
                    command.Parameters.AddWithValue("@Kyrator", Kyrator);
                    command.Parameters.AddWithValue("@Vartist", Vartist);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Короче напартачено с датой либо тривалістю либо ценой.");
                }
            }
        }

        private void Change2()
        {
            var selectedRow = dataGridView2.CurrentCell.RowIndex;
            var Nomer = textBoxN.Text;
            var Naimenyvannya = textBoxH.Text;
            var Style = textBoxS.Text;
            int Plosha;
            int Poverx;
            int Maxkilkist;
            var Naglyadach = textBoxG.Text;

            if (dataGridView2.Rows[selectedRow].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBoxP.Text, out Plosha) &&
                    int.TryParse(textBoxPo.Text, out Poverx) &&
                    int.TryParse(textBoxM.Text, out Maxkilkist))
                {

                    dataGridView2.Rows[selectedRow].SetValues(Nomer, Naimenyvannya, Style, Plosha, Poverx, Maxkilkist, Naglyadach);

                    dataGridView2.Rows[selectedRow].Cells[7].Value = RowState.Modified;


                    var changeQuery = "update Zal set Naimenyvannya = @Naimenyvannya, Style = @Style, Plosha = @Plosha, Poverx = @Poverx, Maxkilkist = @Maxkilkist, Naglyadach = @Naglyadach where Nomer = @Nomer";

                    var command = new SqlCommand(changeQuery, database.GetConnection());
                    command.Parameters.AddWithValue("@Naimenyvannya", Naimenyvannya);
                    command.Parameters.AddWithValue("@Style", Style);
                    command.Parameters.AddWithValue("@Plosha", Plosha);
                    command.Parameters.AddWithValue("@Poverx", Poverx);
                    command.Parameters.AddWithValue("@Maxkilkist", Maxkilkist);
                    command.Parameters.AddWithValue("@Naglyadach", Naglyadach);
                    command.Parameters.AddWithValue("@Nomer", Nomer);

                    command.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Це для второй форми теть.");
                }
            }
        }
        private void Change3()
        {
            var selectedRow = dataGridView3.CurrentCell.RowIndex;
            int Identif;
            var Nazvanie = textBox2.Text;
            var Vudek = textBox3.Text;
            var Autor = textBox4.Text;
            int Rik;
            int Kilkict;
            var Rozmir = textBox7.Text;

            if (dataGridView3.Rows[selectedRow].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox1.Text, out Identif) &&
                    int.TryParse(textBox5.Text, out Rik) &&
                    int.TryParse(textBox6.Text, out Kilkict))
                {

                    dataGridView3.Rows[selectedRow].SetValues(Identif, Nazvanie, Vudek, Autor, Rik, Kilkict, Rozmir);

                    dataGridView3.Rows[selectedRow].Cells[7].Value = RowState.Modified;


                    var chQuery = "update Eksponat set Nazvanie = @Nazvanie, Vudek = @Vudek, Autor = @Autor, Rik = @Rik, Kilkict = @Kilkict, Rozmir = @Rozmir where Identif = @Identif";

                    var command = new SqlCommand(chQuery, database.GetConnection());
                    command.Parameters.AddWithValue("@Nazvanie", Nazvanie);
                    command.Parameters.AddWithValue("@Vudek", Vudek);
                    command.Parameters.AddWithValue("@Autor", Autor);
                    command.Parameters.AddWithValue("@Rik", Rik);
                    command.Parameters.AddWithValue("@Kilkict", Kilkict);
                    command.Parameters.AddWithValue("@Rozmir", Rozmir);
                    command.Parameters.AddWithValue("@Identif", Identif);

                    command.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Це для третьєй форми теть.");
                }
            }
        }
        private void Change4()
        {
            var selectedRow = dataGridView4.CurrentCell.RowIndex;
            int Kod;
            var Prizvushe = textBox9.Text;
            var Imya = textBox10.Text;
            DateTime Pruinyattya;
            var Posada = textBox12.Text;

            if (dataGridView4.Rows[selectedRow].Cells[0].Value.ToString() != string.Empty)
            {
                if (int.TryParse(textBox8.Text, out Kod) &&
                    DateTime.TryParse(textBox11.Text, out Pruinyattya))
                {
                    string formattedPr = Pruinyattya.ToString("yyyy-MM-dd");

                    dataGridView4.Rows[selectedRow].SetValues(Kod, Prizvushe, Imya, formattedPr, Posada);

                    dataGridView4.Rows[selectedRow].Cells[5].Value = RowState.Modified;


                    var cQuery = "update Spivrobitnuk set Prizvushe = @Prizvushe, Imya = @Imya, Pruinyattya = @Pruinyattya, Posada = @Posada where Kod = @Kod";

                    var command = new SqlCommand(cQuery, database.GetConnection());
                    command.Parameters.AddWithValue("@Prizvushe", Prizvushe);
                    command.Parameters.AddWithValue("@Imya", Imya);
                    command.Parameters.AddWithValue("@Pruinyattya", Pruinyattya);
                    command.Parameters.AddWithValue("@Posada", Posada);
                    command.Parameters.AddWithValue("@Kod", Kod);

                    command.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Це для 4 форми теть.");
                }
            }
        }

        private void buttPerez_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void buttNovuiZapus_Click(object sender, EventArgs e)
        {
            Zapus addform = new Zapus();
            addform.Show();
        }
        private void buttVudalennya_Click(object sender, EventArgs e)
        {
            deleteRow();
            Clear();
        }

        private void buttonZberegtu_Click(object sender, EventArgs e)
        {
            Updatte();
            Clear();
        }
        private void buttRedaguv_Click(object sender, EventArgs e)
        {
            Change();
            Clear();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPge2_Click(object sender, EventArgs e)
        {
                
        }

        private void керуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Keryv ker_frm = new Keryv();
            ker_frm.Show();
        }

        private void buttonPerezav_Click(object sender, EventArgs e)
        {
            RefreshData(dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change2();
            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Updatte2();
            Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RefreshDataGrid3(dataGridView3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Change3();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Updatte3();
            Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Updatte4();
            Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Change4();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RefreshDataGrid4(dataGridView4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZapZal adform = new ZapZal();
            adform.Show();
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchquer = $"select * from Podiya where concat (id, Vud, Nazva, Provedennya, " +
                $"Truvalist, Kyrator, Vartist) like '%" + textBox13.Text + "%'";
            SqlCommand com = new SqlCommand(searchquer, database.GetConnection());
            database.openConnection();
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }
        private void Search2(DataGridView dgw2)
        {
            dgw2.Rows.Clear();
            string searchquery = $"select * from Zal where concat (Nomer, Naimenyvannya, Style, Plosha, Poverx, Maxkilkist, Naglyadach) like '%" + textBoxSearch2.Text + "%'";
            SqlCommand comp = new SqlCommand(searchquery, database.GetConnection());
            database.openConnection();
            SqlDataReader r = comp.ExecuteReader();
            while (r.Read())
            {
                ReadSingleRow2(dgw2, r);
            }
            r.Close();
        }
        private void Search3(DataGridView dgw3)
        {
            dgw3.Rows.Clear();
            string searchhquery = $"select * from Eksponat where concat (Identif, Nazvanie, Vudek, Autor, Rik, Kilkict, Rozmir) like '%" + textBoxSearch3.Text + "%'";
            SqlCommand comp = new SqlCommand(searchhquery, database.GetConnection());
            database.openConnection();
            SqlDataReader rea = comp.ExecuteReader();
            while (rea.Read())
            {
                ReadSingleRow3(dgw3, rea);
            }
            rea.Close();
        }
        private void Search4(DataGridView dgw4)
        {
            dgw4.Rows.Clear();
            string searchhquery = $"select * from Spivrobitnuk where concat (Kod, Prizvushe, Imya, Pruinyattya, Posada) like '%" + textBoxSearch4.Text + "%'";
            SqlCommand comp = new SqlCommand(searchhquery, database.GetConnection());
            database.openConnection();
            SqlDataReader rea = comp.ExecuteReader();
            while (rea.Read())
            {
                ReadSingleRow4(dgw4, rea);
            }
            rea.Close();
        }
        private void Search5(DataGridView dgw5)
        {
            string searchhquery = $"SELECT * FROM Proveden " +
                                  $"INNER JOIN Podiya ON Proveden.id = Podiya.id " +
                                  $"INNER JOIN Zal ON Proveden.Nomer = Zal.Nomer " +
                                  $"INNER JOIN Spivrobitnuk ON Proveden.Kod = Spivrobitnuk.Kod " +
                                  $"WHERE CONCAT(Proveden.id, Proveden.Provedennya, Podiya.Nazva, Zal.Nomer, Spivrobitnuk.Kod) LIKE '%{textBox14.Text}%'";

            SqlCommand command = new SqlCommand(searchhquery, database.GetConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow5(dgw5, reader);
            }

            reader.Close();
        }


        private void FillDataGridView(DataGridView dgw5)
        {
            //dgw5.Rows.Clear();
            string query = @"
            SELECT Zal.Naimenyvannya AS 'Зал', Podiya.Vud AS 'Вид', Podiya.Nazva AS 'Назва', Podiya.Provedennya AS 'Дата', Podiya.Kyrator AS 'Куратор', Podiya.Vartist AS 'Вартість'
            FROM Proveden
            JOIN Zal ON Proveden.Nomer = Zal.Nomer
            JOIN Podiya ON Proveden.id = Podiya.id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                System.Data.DataTable table = new System.Data.DataTable();
                adapter.Fill(table);
                dataGridView5.DataSource = table;
            }
        }
        private void FillDataGridView6(DataGridView dgw6)
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
                dataGridView6.DataSource = table;
            }
        }
        private void DeleteSelectedRowFromDatabaseAndDataGridView(DataGridView dataGridView5)
        {
            if (dataGridView5.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView5.SelectedCells[0].RowIndex;
                var idToDelete = Convert.ToString(dataGridView5.Rows[rowIndex].Cells["Дата"].Value);
                dataGridView5.Rows.RemoveAt(rowIndex);

                string deleteQuery = "DELETE FROM Proveden WHERE Provedennya = @Provedennya";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@Provedennya", idToDelete);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть рядок для видалення.", "Помилка видалення", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void textBoxSearch2_TextChanged(object sender, EventArgs e)
        {
            Search2(dataGridView2);
        }

        private void textBoxSearch3_TextChanged(object sender, EventArgs e)
        {
            Search3(dataGridView3);
        }

        private void textBoxSearch4_TextChanged(object sender, EventArgs e)
        {
            Search4(dataGridView4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteRow1();
        }

        private void bttnVudalEksp_Click(object sender, EventArgs e)
        {
            deleteRow2();
        }

        private void bttnVSpivr_Click(object sender, EventArgs e)
        {
            deleteRow3();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ZapEks eksform = new ZapEks();
            eksform.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ZalSpiv spform = new ZalSpiv();
            spform.Show();
        }

        private void файлToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Отримати активну вкладку
            TabPage activeTab = tabControl1.SelectedTab;

            // Знайти DataGridView на активній вкладці
            DataGridView dataGridView = activeTab.Controls.OfType<DataGridView>().FirstOrDefault();

            if (dataGridView != null)
            {
                // Сховати останній стовпець
                if (dataGridView.Columns.Count > 0)
                {
                    dataGridView.Columns[dataGridView.Columns.Count - 1].Visible = false;
                }

                // Створити новий документ Word
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Add();

                // Додати таблицю в документ Word
                Microsoft.Office.Interop.Word.Table table = doc.Tables.Add(doc.Range(), dataGridView.RowCount + 1, dataGridView.ColumnCount - 1);

                // Заповнити таблицю з DataGridView
                for (int i = 0; i < dataGridView.ColumnCount - 1; i++)
                {
                    table.Cell(1, i + 1).Range.Text = dataGridView.Columns[i].HeaderText;
                }

                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount - 1; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // Додати рамки до таблиці
                table.Borders.Enable = 1;

                // Зберегти документ Word
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Word документи (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    doc.SaveAs(saveFileDialog.FileName);
                    doc.Close();
                    wordApp.Quit();
                    MessageBox.Show("Файл збережено.");
                }
                else
                {
                    doc.Close();
                    wordApp.Quit();
                }
            }
            else
            {
                MessageBox.Show("На активній вкладці немає таблиці для збереження.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FillDataGridView(dataGridView5);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ZapucPod zform = new ZapucPod();
            zform.ShowDialog();
        }

       /* private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[selectedRow];
                textBox15.Text = row.Cells[0].Value.ToString();
                textBox16.Text = row.Cells[1].Value.ToString();
                textBox17.Text = row.Cells[2].Value.ToString();
                textBox18.Text = row.Cells[3].Value.ToString();
                textBox19.Text = row.Cells[4].Value.ToString();
                textBox20.Text = row.Cells[5].Value.ToString();
            }
        }*/

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            Search5(dataGridView5);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DeleteSelectedRowFromDatabaseAndDataGridView(dataGridView5);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ZapucE zapform = new ZapucE();
            zapform.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FillDataGridView6(dataGridView6);
        }
    }
}