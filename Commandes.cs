using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_gestion
{
    public class Commandes
    {
        public string Date { get; set; } 
        public string Heure { get; set; } 
        public string Plat { get; set; }
        public int Table { get; set; }
        public int Quantite { get; set; }
        public string Type { get; set; }
        public string Specification { get; set; }
    }
}
