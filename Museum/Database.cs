using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Museum
{
    internal class Database
    {
        SqlConnection scn = new SqlConnection(@"Data Source = DESKTOP-KKKBFA0\MSSQLSERVER01; Initial Catalog = OBDZK; Integrated Security = True");

        public void openConnection()
        {
            if (scn.State == System.Data.ConnectionState.Closed)
            { 
            scn.Open();
            }
        }

        public void closeConnection()
        {
            if (scn.State == System.Data.ConnectionState.Open)
            {
                scn.Close();
            }
        }

        public SqlConnection GetConnection()
        { 
            return scn; 
        }
    }
}
