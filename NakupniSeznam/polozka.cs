using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakupniSeznam
{
    public class Polozka
    {
        public string Nazev { get; set; }
        public int Mnozstvi { get; set; }

        public Polozka(string nazev, int mnozstvi)
        {
            Nazev = nazev;
            Mnozstvi = mnozstvi;
        }

        public Polozka() { }
    }
}
