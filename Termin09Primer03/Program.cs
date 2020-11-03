/*
 * Program je stukturiran na sledeci nacin:
 * 
 *     DAO     - Rad sa bazom podataka
 *     MODEL   - Klase koje modeluju entitete
 *     UI      - Interakcija sa korisnikom
 *     UTILS   - Pomocne datoteke
 *     PROGRAM - Glavna petlja programa
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.ui;
using Termin09Primer03.utils;

namespace Termin09Primer03
{
    class Program
    {
        public static SqlConnection conn;

        static void LoadConnection()
        {
            try
            {
                // Ostvarivanje konekcije
                string connectionStringZaPoKuci = @"Data Source=DESKTOP-GC0HF6E\SQLEXPRESS;Initial Catalog=DotNetMilos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultipleActiveResultSets=True; MultiSubnetFailover = False";
                // string connectionStringNaKursu = "Data Source=.\\SQLEXPRESS;Initial Catalog=DotNetKurs;User ID=sa;Password=SqlServer2016;MultipleActiveResultSets=True";

                // Parametar "MultipleActiveResultSets=True" je neophodan kada zelimo da imamo istovremeno
                // otvorena dva data readera ka bazi podataka. Zasto je u ovom programu to neophodno?
                conn = new SqlConnection(connectionStringZaPoKuci);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Main(string[] args)
        {
           LoadConnection();

            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();

                Console.Write("opcija:");
                odluka = IO.OcitajCeoBroj();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        StudentUI.Menu();
                        break;
                    case 2:
                        PredmetUI.Menu();
                        break;
                    case 3:
                        NastavnikUI.Menu();
                        break;
                    case 4:
                        PohadjaUI.Menu();
                        break;
                    case 5:
                        PredajeUI.Menu();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        // Ispis teksta osnovnih opcija
        public static void IspisiMenu()
        {
            Console.WriteLine("Studentska Sluzba - Osnovne opcije:");
            Console.WriteLine("\tOpcija broj 1 - Rad sa studentima");
            Console.WriteLine("\tOpcija broj 2 - Rad sa predmetima");
            Console.WriteLine("\tOpcija broj 3 - Rad sa nastavnicima");
            Console.WriteLine("\tOpcija broj 4 - Rad sa pohadjanjem predmeta");
            Console.WriteLine("\tOpcija broj 5 - Rad sa predavanjem predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - IZLAZ IZ PROGRAMA");
        }
    }
}
