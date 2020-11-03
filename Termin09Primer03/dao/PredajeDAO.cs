using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.model;

namespace Termin09Primer03.dao
{
    class PredajeDAO
    {

        // Spisak predmeta koje predaje nastavnik sa datim ID-jem
        public static List<Predmet> GetPredmetiByNastavnikId(SqlConnection conn,int id)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            List<Predmet> retVal = new List<Predmet>();
            try
            {
                string queryString = "SELECT predmet_id FROM predaje " +
                            "WHERE nastavnik_id = " + id;

                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int predmetId = (int)rdr["predmet_id"];
                    retVal.Add(PredmetDAO.GetPredmetById(conn, predmetId));
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



        // Spisak nastavnika koji predaju predmet sa datim ID-jem
        public static List<Nastavnik> GetNastavniciByPredmetId(SqlConnection conn,int id)
        {
          //  SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            List<Nastavnik> retVal = new List<Nastavnik>();
            try
            {
                string queryString = "SELECT nastavnik_id FROM predaje WHERE " +
                                "predmet_id = " + id;
                SqlCommand cmd = new SqlCommand(queryString, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    int nastavnikId = (int)rdr["nastavnik_id"];
                    retVal.Add(NastavnikDAO.GetNastavnikById(conn,nastavnikId));
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



        // Ubacivanje nove relacije predavanja
        public static bool Add(SqlConnection conn, int nastavnikId, int predmetId)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "INSERT INTO predaje " +
                        "(nastavnik_id, predmet_id) values (@nastavnik_id, @predmet_id)";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@nastavnik_id", nastavnikId);
                cmd.Parameters.AddWithValue("@predmet_id", predmetId);

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

        // Brisanje relacije predavanja
        public static bool Delete(SqlConnection conn, int nastavnikId, int predmetId)
        {
            //SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "DELETE FROM predaje WHERE nastavnik_id = " + nastavnikId + " AND predmet_id = " + predmetId;
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

        // Brisanje svih predavanja odredjenog nastavnika
        public static bool DeletePredavanjaNastavnika(SqlConnection conn,int nastavnikId)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "DELETE FROM predaje WHERE nastavnik_id = " + nastavnikId;
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

        // Brisanje svih predavanja odredjenog predmeta
        public static bool DeletePredavanjaPredmeta(SqlConnection conn, int predmetId)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                string update = "DELETE FROM predaje WHERE predmet_id = " + predmetId;
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

        // Izmena svih predavanja datog predmeta
        public static bool Update(SqlConnection conn, Nastavnik nastavnik)
        {
            //SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                // Obrisemo prethodna predavanja
                retVal = DeletePredavanjaNastavnika(conn, nastavnik.Id);

                // Ako je brisanje uspelo idemo na dodavanje
                if (retVal)
                {
                    foreach (Predmet predmet in nastavnik.Predmeti)
                    {
                        retVal = Add(conn,nastavnik.Id, predmet.Id);
                        if (retVal == false)
                            throw new Exception("Dodavanje nije uspelo");
                    }
                }
                else
                {
                    throw new Exception("Brisanje nije uspelo");
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

        // Izmena svih pohadjanja datog predmeta
        public static bool Update(SqlConnection conn, Predmet predmet)
        {
           // SqlConnection conn = DaoConnection.TraziNovuKonekciju();
            bool retVal = false;
            try
            {
                // Obrisemo prethodna pohadjaja
                retVal = DeletePredavanjaPredmeta(conn,predmet.Id);

                // Ako je brisanje uspelo idemo na dodavanje
                if (retVal)
                {
                    foreach (Nastavnik nastavnik in predmet.Nastavnici)
                    {
                        retVal = Add(conn,nastavnik.Id, predmet.Id);
                        if (retVal == false)
                            throw new Exception("Dodavanje nije valjalo");
                    }
                }
                else
                {
                    throw new Exception("Brisanje nije valjalo");
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
    }
}
