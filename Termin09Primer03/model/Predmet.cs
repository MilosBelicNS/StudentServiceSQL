using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Termin09Primer03.model
{
    class Predmet
    {
        public int Id { set; get; }
        public string Naziv { set; get; }

        public List<Nastavnik> Nastavnici { get; set; }
        public List<Student> Studenti { set; get; }

        public Predmet(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
            Studenti = new List<Student>();
            Nastavnici = new List<Nastavnik>();
        }

        public override string ToString()
        {
            return "Predmet [id=" + Id + ", naziv=" + Naziv + "]";
        }
    }
}
