using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin09Primer03.dao;
using Termin09Primer03.model;
using Termin09Primer03.utils;

namespace Termin09Primer03.ui
{
    class PredajeUI
    {


        private static void IspisiMenu()
        {
            Console.WriteLine("Rad sa predmetima nastavnika - opcije:");
            Console.WriteLine("\tOpcija broj 1 - predmeti koje nastavnik predaje");
            Console.WriteLine("\tOpcija broj 2 - nastavnici koji predaju predmet");
            Console.WriteLine("\tOpcija broj 3 - dodavanje nastavnika na predmet");
            Console.WriteLine("\tOpcija broj 4 - uklanjanje nastavnika sa predmeta");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - Nazad");
        }

        public static void Menu()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                IspisiMenu();
                Console.Write("opcija:");
                odluka = IO.OcitajCeoBroj();
                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz");
                        break;
                    case 1:
                        IspisiPredmeteZaNastavnika();
                        break;
                    case 2:
                        IspisiNastavnikeZaPredmet();
                        break;
                    case 3:
                        dodajNastavnikaNaPredmet();
                        break;
                    case 4:
                        ukloniNastavnikaSaPredmeta();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        private static void IspisiPredmeteZaNastavnika()
        {
            // Najpre pronadjemo nastavnika za kojeg zelimo ispis predmeta
            Nastavnik nastavnik = NastavnikUI.PronadjiNastavnika();

            if (nastavnik != null)
            {
                // Ukoliko smo ga pronasli, zahtevamo od baze listu predmeta ovog nastavnika
                List<Predmet> predmeti = PredajeDAO.GetPredmetiByNastavnikId(Program.conn, nastavnik.Id);


                // Ispisujemo dobijenu listu predmeta
                foreach (Predmet p in predmeti)
                {
                    Console.WriteLine(p);
                }
            }
        }

        private static void IspisiNastavnikeZaPredmet()
        {
            // Najpre pronadjemo predmet za koji zelimo ispis nastavnika
            Predmet predmet = PredmetUI.PronadjiPredmet();
            if (predmet != null)
            {
                // Ukoliko smo pronasli predmet, zahtevamo od baze listu studenata koji ga pohadjaju
                List<Nastavnik> nastavnici = PredajeDAO.GetNastavniciByPredmetId(
                        Program.conn, predmet.Id);

                // Ispisujemo dobijenu listu studenata
                foreach (Nastavnik n in nastavnici)
                {
                    Console.WriteLine(n);
                }
            }
        }

        private static void dodajNastavnikaNaPredmet()
        {
            // Najpre pronadjemo nastavnika kojeg zelimo da dodamo na predmet
            Nastavnik nastavnik = NastavnikUI.PronadjiNastavnika();

            // Pronadjemo predmet na koji zelimo da dodamo nastavnika
            Predmet predmet = PredmetUI.PronadjiPredmet();

            // Ukoliko je uspesan pronalazak i predmeta i nastavnika
            if (nastavnik != null && predmet != null)
            {
                // Onda njihovu relaciju uspostavljamo ubacivanjem novog sloga u tabelu pohadja
                PredajeDAO.Add(Program.conn, nastavnik.Id, predmet.Id);
            }
        }

        private static void ukloniNastavnikaSaPredmeta()
        {
            // Najpre pronadjemo nastavnika kojeg zelimo da uklonimo sa predmeta
            Nastavnik nastavnik = NastavnikUI.PronadjiNastavnika();

            // Pronadjemo predmet sa kojeg zelimo da ukloniko studenta
            Predmet predmet = PredmetUI.PronadjiPredmet();

            // Ukoliko je uspesan pronalazak i predmeta i nastavnik
            if (nastavnik != null && predmet != null)
            {
                // Onda njihovu relaciju brisemo izbacivanjem sloga iz tabele predaje
                PohadjaDAO.Delete(Program.conn, nastavnik.Id, predmet.Id);
            }
        }


    }
}
