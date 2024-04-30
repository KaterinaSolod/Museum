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
    public partial class ZapEks : Form
    {
        Database database = new Database();
        public ZapEks()
        {
            InitializeComponent();
        }

        private void ZapEks_Load(object sender, EventArgs e)
        {

        }

        private void buttonZb_Click(object sender, EventArgs e)
        {
            database.openConnection();
            var Identif = textBoxNom.Text;
            var Nazvanie = textBoxNazva.Text;
            var Vudek = textBoxVud.Text;
            var Autor = textBoxAutor.Text;
            int Rik;
            int Kilkict;
            var Rozmir = textBoxR.Text;
            if (int.TryParse(textBoxRik.Text, out Rik) &&
                int.TryParse(textBoxKilk.Text, out Kilkict))
            {
                var checkQuery = "SELECT COUNT(*) FROM Eksponat WHERE Identif = @Identif";
                var checkCommand = new SqlCommand(checkQuery, database.GetConnection());
                checkCommand.Parameters.AddWithValue("@Identif", Identif);
                int existingRecords = (int)checkCommand.ExecuteScalar();

                if (existingRecords > 0)
                {
                    MessageBox.Show("Запис з таким ідентифікатором вже існує.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var addquery = "INSERT INTO Eksponat (Identif, Nazvanie, Vudek, Autor, Rik, Kilkict, Rozmir) " +
                  "VALUES (@Identif, @Nazvanie, @Vudek, @Autor, @Rik, @Kilkict, @Rozmir)";
                    var command = new SqlCommand(addquery, database.GetConnection());
                    command.Parameters.AddWithValue("@Identif", Identif);
                    command.Parameters.AddWithValue("@Nazvanie", Nazvanie);
                    command.Parameters.AddWithValue("@Vudek", Vudek);
                    command.Parameters.AddWithValue("@Autor", Autor);
                    command.Parameters.AddWithValue("@Rik", Rik);
                    command.Parameters.AddWithValue("@Kilkict", Kilkict);
                    command.Parameters.AddWithValue("@Rozmir", Rozmir);
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
