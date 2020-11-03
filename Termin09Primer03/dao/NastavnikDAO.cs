using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.model;

namespace Termin09Primer03.dao
{
    class NastavnikDAO
    {


        // Trazimo NASTAVNIKA sa datim ID-jem
        public static Nastavnik GetNastavnikById(SqlConnection conn , int id)
        {
            
            Nastavnik nastavnik = null;
            try
            {
                string query = "SELECT ime, prezime, zvanje " +
                                "FROM nastavnici WHERE nastavnik_id = "
                                + id;
                SqlCommand cmd = new SqlCommand(query, conn);

                // Da li je ovde moguc SQL injection i zasto nije?
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    string ime = (string)rdr["ime"];
                    string prezime = (string)rdr["ime"];
                    string zvanje = (string)rdr["zvanje"];
                  

                    nastavnik = new Nastavnik(id, ime, prezime, zvanje);
                }
                rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //finally
            //{
            //    conn.Close();
            //}
            return nastavnik;
        }

       

        // Trazimo sve nastavnike koji postoje u bazi podataka
        public static List<Nastavnik> GetAll(SqlConnection conn)
        {
            //SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            List<Nastavnik> retVal = new List<Nastavnik>();
            try
            {
                string query = "SELECT nastavnik_id, ime, prezime, zvanje FROM nastavnici ";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["nastavnik_id"];
                    string ime = (string)rdr["ime"];
                    string prezime = (string)rdr["prezime"];
                    string zvanje = (string)rdr["zvanje"];

                    // Preuzimanje predmeta koje student predaje
                    string upitZaPredmete = "select predmet_id from " +
                            "predaje where nastavnik_id = " + id;
                    SqlCommand cmd2 = new SqlCommand(upitZaPredmete, conn);
                    SqlDataReader rdrPredmeti = cmd2.ExecuteReader();
                    List<Predmet> predmetiKojePredaje = new List<Predmet>();
                    while (rdrPredmeti.Read())
                    {
                        int idPredmeta = (int)rdrPredmeti["predmet_id"];
                        Predmet p = PredmetDAO.GetPredmetById(conn, idPredmeta);
                        predmetiKojePredaje.Add(p);
                    }
                    rdrPredmeti.Close();

                    Nastavnik nastavnik = new Nastavnik(id, ime, prezime, zvanje);
                    nastavnik.Predmeti = predmetiKojePredaje;
                    retVal.Add(nastavnik);
                }
                rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //finally
            //{
            //    conn.Close();
            //}
            return retVal;
        }

        // Ubacivanje novog nastavnika u bazu podataka
        public static bool Add(SqlConnection conn ,Nastavnik nastavnik)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "INSERT INTO nastavnici (ime, " +
                        "prezime, zvanje) values (@ime, @prezime, @zvanje)";
                SqlCommand cmd = new SqlCommand(update, conn);

               
                cmd.Parameters.AddWithValue("@ime", nastavnik.Ime);
                cmd.Parameters.AddWithValue("@prezime", nastavnik.Prezime);
                cmd.Parameters.AddWithValue("@zvanje", nastavnik.Zvanje);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    retVal = true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //finally
            //{
            //    conn.Close();
            //}

            return retVal;
        }

        // Menjanje podataka o nastavniku
        public static bool Update(SqlConnection conn ,Nastavnik nastavnik)
        {
          //  SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "UPDATE nastavnici SET ime=@ime, prezime=@prezime, zvanje=@zvanje WHERE nastavnik_id=@nastavnik_id";
                SqlCommand cmd = new SqlCommand(update, conn);

                cmd.Parameters.AddWithValue("@ime", nastavnik.Ime);
                cmd.Parameters.AddWithValue("@prezime", nastavnik.Prezime);
                cmd.Parameters.AddWithValue("@zvanje", nastavnik.Zvanje);
                cmd.Parameters.AddWithValue("@student_id", nastavnik.Id);

                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //finally
            //{
            //    conn.Close();
            //}
            return retVal;
        }

        // Brisanje studenta iz baze podataka
        public static bool Delete(SqlConnection conn ,int id)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "DELETE FROM nastavnici WHERE " +
                        "nastavnik_id = " + id;
                SqlCommand cmd = new SqlCommand(update, conn);

                if (cmd.ExecuteNonQuery() == 1)
                    retVal = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //finally
            //{
            //    conn.Close();
            //}
            return retVal;
        }
    }
}
