﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_gestion
{
    public interface Achats
    {
        public string Nom { get; set; }
        public string? Description { get; set; }
        public string Magasin { get; set; }
        public string Prix { get; set; }
    }
}
