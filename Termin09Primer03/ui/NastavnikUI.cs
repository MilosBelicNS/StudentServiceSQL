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
    class NastavnikUI
    {

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
                        IspisiSveNastavnike();
                        break;
                    case 2:
                        UnosNovogNastavnika();
                        break;
                    case 3:
                        IzmenaPodatakaONastavniku();
                        break;
                    case 4:
                        BrisanjePodatakaONastavniku();
                        break;
                    default:
                        Console.WriteLine("Nepostojeca komanda");
                        break;
                }
            }
        }

        public static void IspisiMenu()
        {
            Console.WriteLine("Rad sa nastavnicima - opcije:");
            Console.WriteLine("\tOpcija broj 1 - ispis svih nastavnika");
            Console.WriteLine("\tOpcija broj 2 - unos novog nastavnika");
            Console.WriteLine("\tOpcija broj 3 - izmena nastavnika");
            Console.WriteLine("\tOpcija broj 4 - brisanje nastavnika");
            Console.WriteLine("\t\t ...");
            Console.WriteLine("\tOpcija broj 0 - Nazad");
        }


        /** METODE ZA ISPIS NASTAVNIKA ****/
        // Ispisi sve nastavnike
        public static void IspisiSveNastavnike()
        {
            List<Nastavnik> sviNastavnici = NastavnikDAO.GetAll(Program.conn);
            for (int i = 0; i < sviNastavnici.Count; i++)
            {
                Console.WriteLine(sviNastavnici[i]);
            }
        }

        /** METODE ZA PRETRAGU NASTAVNIKA ****/
        // Pronadji nastavnika

        public static Nastavnik PronadjiNastavnikaPoId(int id)
        {
            Nastavnik retVal = NastavnikDAO.GetNastavnikById(Program.conn, id);

            return retVal;


        }


        public static Nastavnik PronadjiNastavnika()
        {
            Nastavnik retVal = null;
            Console.Write("Unesi id nastavnika:");
            int nId = IO.OcitajCeoBroj();
            retVal = PronadjiNastavnikaPoId(nId);
            if (retVal == null)
                Console.WriteLine("Nastavnik sa id " + nId
                         + " ne postoji u evidenciji");
            return retVal;
        }

        /** METODE ZA UNOS, IZMENU I BRISANJE NASTAVNIKA ****/
        // Unos novog NASTAVNIKA
        public static void UnosNovogNastavnika()
        {
           
            Console.Write("Unesi ime:");
            string naIme = IO.OcitajTekst();
            Console.Write("Unesi prezime:");
            string naPrezime = IO.OcitajTekst();
            Console.Write("Unesi zvanje:");
            string naZvanje = IO.OcitajTekst();

            Nastavnik nastavnik = new Nastavnik(0, naIme, naPrezime, naZvanje);
            // Ovde se moze proveravati i povratna vrednost i onda ispisivati poruka
            NastavnikDAO.Add(Program.conn, nastavnik);
        }


        // Izmena nastavnika
        public static void IzmenaPodatakaONastavniku()
        {
            Nastavnik nastavnik = PronadjiNastavnika();
            if (nastavnik != null)
            {
                

                Console.Write("Unesi ime :");
                string naIme = IO.OcitajTekst();
                nastavnik.Ime = naIme;

                Console.Write("Unesi prezime:");
                string naPrezime = IO.OcitajTekst();
                nastavnik.Prezime = naPrezime;

                Console.Write("Unesi zvanje:");
                string naZvanje = IO.OcitajTekst();
                nastavnik.Zvanje = naZvanje;

                NastavnikDAO.Update(Program.conn, nastavnik);
            }
        }

        // Brisanje studenta
        public static void BrisanjePodatakaONastavniku()
        {
            Nastavnik nastavnik = PronadjiNastavnika();
            if (nastavnik != null)
            {
                NastavnikDAO.Delete(Program.conn, nastavnik.Id);
            }
        }

    }
}
