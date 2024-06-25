using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pF
{
    public class Skincare
    {
        public string Nazev { get; set; }
   
        public string Ucinek { get; set; }  

        public DateTime DatumOtevreni { get; set; }


        public override string ToString()
        {
            return $"Název:{Nazev}, účinek: {Ucinek}, datum otevření: {DatumOtevreni}";
        }
    }
}
