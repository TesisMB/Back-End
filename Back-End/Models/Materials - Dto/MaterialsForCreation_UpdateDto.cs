﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    public class MaterialsForCreation_UpdateDto
    {
        public int MaterialQuantity { get; set; }

        public string MaterialName { get; set; }

        public bool MaterialAvailability { get; set; }

        public string MaterialUtility { get; set; }
    }
}