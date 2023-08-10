using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_24_2020
{
    public class DBConnection
    {
        private static readonly object kljuc = new object();
        public static SqlConnection conn { get; set; }

        private DBConnection()
        {
             string konekcioniString = @"Data Source=(localdb)\ds; Initial Catalog = memoryGame;Integrated Security = True";
             conn = new SqlConnection(konekcioniString);
        }

        private static DBConnection instance = null;

        public static SqlConnection dajKonekciju
        {
            get
            {
                if (instance == null)
                {
                    lock (kljuc)
                    {
                        if (instance == null)
                        {
                            instance = new DBConnection();
                        }
                    }
                }
                return conn;
            }
        }

    }
}
