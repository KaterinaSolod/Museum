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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using System.Data.SqlClient;

namespace Museum
{
    public partial class ZapucPod : Form
    {
        Database database = new Database();
        public ZapucPod()
        {
            InitializeComponent();
        }

        private void ZapucPod_Load(object sender, EventArgs e)
        {
            textBox4.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            int Nomer;
            int id;
            int Kod;
            DateTime Provedennya;

            if (int.TryParse(textBox1.Text, out Nomer) &&
                int.TryParse(textBox2.Text, out id) &&
                int.TryParse(textBox3.Text, out Kod) &&
               DateTime.TryParse(textBox4.Text, out Provedennya))
            {
                var aquery = "INSERT INTO Proveden (Nomer, id, Kod, Provedennya) " +
                  "VALUES (@Nomer, @id, @Kod, @Provedennya)";
                var command = new SqlCommand(aquery, database.GetConnection());
                command.Parameters.AddWithValue("@Nomer", Nomer);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@Kod", Kod);
                command.Parameters.AddWithValue("@Provedennya", Provedennya);
                command.ExecuteNonQuery();
                MessageBox.Show("Успішно створено.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректні дані.");
            }
            database.closeConnection();
        }
    }
}
