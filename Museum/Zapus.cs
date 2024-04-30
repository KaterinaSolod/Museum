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
    public partial class Zapus : Form
    {
        Database database = new Database();
        public Zapus()
        {
            InitializeComponent();
        }

        private void Zapus_Load(object sender, EventArgs e)
        {
            textBoxProved.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void buttonSoxr_Click(object sender, EventArgs e)
        {
            database.openConnection();
            
            var Vud = textBoxVud.Text;
            var Nazva = textBoxNazva.Text;
            DateTime Provedennya;
            float Truvalist;
            var Kyrator = textBoxKyrator.Text;
            int Vartist;

            if (float.TryParse(textBoxTruvalist.Text, out Truvalist) &&
                int.TryParse(textBoxVartist.Text, out Vartist) &&
                DateTime.TryParse(textBoxProved.Text, out Provedennya))
            {
                var addquery = "INSERT INTO Podiya (Vud, Nazva, Provedennya, Truvalist, Kyrator, Vartist) " +
                  "VALUES (@Vud, @Nazva, @Provedennya, @Truvalist, @Kyrator, @Vartist)";
                var command = new SqlCommand (addquery, database.GetConnection());
                command.Parameters.AddWithValue("@Vud", Vud);
                command.Parameters.AddWithValue("@Nazva", Nazva);
                command.Parameters.AddWithValue("@Provedennya", Provedennya);
                command.Parameters.AddWithValue("@Truvalist", Truvalist);
                command.Parameters.AddWithValue("@Kyrator", Kyrator);
                command.Parameters.AddWithValue("@Vartist", Vartist);
                command.ExecuteNonQuery ();
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
