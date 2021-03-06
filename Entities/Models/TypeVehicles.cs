﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models
{
    [Table ("TypeVehicles", Schema="dbo")]
    public class TypeVehicles
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(25)]
        public string Type { get; set; }

        [Required]
        [MaxLength(20)]
        public string Mark { get; set; }

        [Required]
        [MaxLength(25)]
        public string Model { get; set; }

        public ICollection<Vehicles> Vehicles { get; set; }

    }
}
