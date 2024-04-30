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
    public partial class ZapZal : Form
    {
        Database database = new Database();
        public ZapZal()
        {
            InitializeComponent();
        }

        private void ZapZal_Load(object sender, EventArgs e)
        {

        }

        private void buttonZbr_Click(object sender, EventArgs e)
        {
            database.openConnection();
            int Nomer;
            var Naimenyvannya = textBoxNZ.Text;
            var Style = textBoxStyle.Text;
            int Plosha;
            int Poverx;
            int Maxkilkist;
            var Naglyadach = textBoxNg.Text;

            if (int.TryParse(textBoxNO.Text, out Nomer) &&
                int.TryParse(textBoxPlosha.Text, out Plosha) &&
                int.TryParse(textBoxPoverx.Text, out Poverx) && int.TryParse(textBoxMax.Text, out Maxkilkist))
            {
                var aquery = "INSERT INTO Zal (Nomer, Naimenyvannya, Style, Plosha , Poverx, Maxkilkist, Naglyadach) " +
                  "VALUES (@Nomer, @Naimenyvannya, @Style, @Plosha, @Poverx, @Maxkilkist, @Naglyadach)";
                var command = new SqlCommand(aquery, database.GetConnection());
                command.Parameters.AddWithValue("@Nomer", Nomer);
                command.Parameters.AddWithValue("@Naimenyvannya", Naimenyvannya);
                command.Parameters.AddWithValue("@Style", Style);
                command.Parameters.AddWithValue("@Plosha", Plosha);
                command.Parameters.AddWithValue("@Poverx", Poverx);
                command.Parameters.AddWithValue("@Maxkilkist", Maxkilkist);
                command.Parameters.AddWithValue("@Naglyadach", Naglyadach);
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
