using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_24_2020
{
    public class User
    {
        public string ime;
        public int score;
        public int vreme;
        public string rang;

        public User(string ime, int score, int vreme, string rang)
        {
            this.ime = ime;
            this.score = score;
            this.vreme = vreme;
            this.rang = rang;
        }
    }
    public class baza
    {

        SqlConnection conn = DBConnection.dajKonekciju;
        public bool konekcija = true;

        public List<User> dajNajboljeg(string rang)
        {
            try { 
            List<User> najboljiScore = new List<User>();

            conn.Open();

            string query = @"select * from korisnik where rang = @rang Order by score";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@rang", rang);
            SqlDataReader rea = command.ExecuteReader();

            if (rea.Read())
            {
                najboljiScore.Add(new User((string)rea["ime"], (int)rea["score"], (int)rea["broj_sekundi"], (string)rea["rang"]));
            }

            conn.Close();

            return najboljiScore;
            }
            catch
            {
                konekcija = false;
                return null;
            }
        }

        public void upisiRezultat(string ime, int score, int vreme, string rang)
        {
            conn.Open();

            string query = @"insert into korisnik(ime,score,broj_sekundi,rang) values(@ime, @score, @vreme, @rang)";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@ime", ime);
            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@vreme", vreme);
            command.Parameters.AddWithValue("@rang", rang);
            SqlDataReader rea = command.ExecuteReader();

            conn.Close();
        }

    }
}
