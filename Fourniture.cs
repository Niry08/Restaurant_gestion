﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_gestion
{
    public class Fourniture : Achats
    {
        public string Nom { get; set; }
        public string? Description { get; set; }
        public string Magasin { get; set; }
        public double Prix { get; set; }

        public string QuantiteNbre { get; set; }
    }
}
