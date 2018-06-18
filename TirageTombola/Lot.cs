using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TirageTombola
{
    class Lot
    {
        public string Description { get; set; }
        public string Carton { get; set; }
        public int NumeroLot { get; set; }
        public Programme EleveGagnant { get; set; }
    }
}
