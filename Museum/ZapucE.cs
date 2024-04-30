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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Museum
{
    public partial class ZapucE : Form
    {
        Database database = new Database();
        public ZapucE()
        {
            InitializeComponent();
        }

        private void ZapucE_Load(object sender, EventArgs e)
        {
            textBox3.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            int Nomer;
            int Identif;
            DateTime Rozmishennya;

            if (int.TryParse(textBox1.Text, out Nomer) &&
                int.TryParse(textBox2.Text, out Identif) &&
               DateTime.TryParse(textBox3.Text, out Rozmishennya))
            {
                var aquery = "INSERT INTO Zberigat (Nomer, Identif, Rozmishennya) " +
                  "VALUES (@Nomer, @Identif, @Rozmishennya)";
                var command = new SqlCommand(aquery, database.GetConnection());
                command.Parameters.AddWithValue("@Nomer", Nomer);
                command.Parameters.AddWithValue("@Identif", Identif);
                command.Parameters.AddWithValue("@Rozmishennya", Rozmishennya);
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
