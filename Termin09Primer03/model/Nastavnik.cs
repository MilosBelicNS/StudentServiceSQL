using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin09Primer03.model
{
    class Nastavnik
    {

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Zvanje { get; set; }

        public List<Predmet>  Predmeti { get; set; }
        public List<Student> Studenti { get; set; }



        public Nastavnik(int id, string ime, string prezime, string zvanje)
        {

            Predmeti = new List<Predmet>();
            Studenti = new List<Student>();

            Id = id;
            Ime = ime;
            Prezime = prezime;
            Zvanje = zvanje;

        }

        public override string ToString()
        {
            string ispis = "Nastavnik [" + Ime + " " + Prezime + " " + Zvanje + "]";
            foreach (Predmet p in Predmeti)
                ispis += p.Naziv + ",";


            return ispis;


        }
    }
}
