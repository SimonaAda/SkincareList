using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pF
{
    public class Skincare
    {
        public string Znacka { get; set; }
        public string Nazev { get; set; }
        public string Ucinek { get; set; }  

        public DateTime DatumOtevreni { get; set; }


        public Skincare (string znacka, string nazev, string ucinek, DateTime datumOtevreni)
        {
            Znacka = znacka;
            Nazev = nazev;
            Ucinek = ucinek;
            DatumOtevreni = datumOtevreni;
        }

        public override string ToString()
        {
            return $"Značka:{Znacka}, název:{Nazev}, účinek: {Ucinek}, datum otevření {DatumOtevreni}";
        }
    }
}
