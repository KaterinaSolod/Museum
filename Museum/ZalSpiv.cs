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
    public partial class ZalSpiv : Form
    {
        Database database = new Database();
        public ZalSpiv()
        {
            InitializeComponent();
        }

        private void ZalSpiv_Load(object sender, EventArgs e)
        {
            textBoxPr.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            var Kod = textBoxK.Text;
            var Prizvushe = textBoxP.Text;
            DateTime Pruinyattya;
            var Imya = textBoxI.Text;
            var Posada = textBoxPosada.Text;

            if (DateTime.TryParse(textBoxPr.Text, out Pruinyattya))
            {
                var checkQuery = "SELECT COUNT(*) FROM Spivrobitnuk WHERE Kod = @Kod";
                var checkCommand = new SqlCommand(checkQuery, database.GetConnection());
                checkCommand.Parameters.AddWithValue("@Kod", Kod);
                int existingRecords = (int)checkCommand.ExecuteScalar();

                if (existingRecords > 0)
                {
                    MessageBox.Show("Запис з таким кодом вже існує.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var addquery = "INSERT INTO Spivrobitnuk (Kod, Prizvushe, Imya, Pruinyattya, Posada) " +
                      "VALUES (@Kod, @Prizvushe, @Imya, @Pruinyattya, @Posada)";
                    var command = new SqlCommand(addquery, database.GetConnection());
                    command.Parameters.AddWithValue("@Kod", Kod);
                    command.Parameters.AddWithValue("@Prizvushe", Prizvushe);
                    command.Parameters.AddWithValue("@Imya", Imya);
                    command.Parameters.AddWithValue("@Pruinyattya", Pruinyattya);
                    command.Parameters.AddWithValue("@Posada", Posada);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Успішно створено.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректні дані.");
            }
            database.closeConnection();
        }

    }
}
