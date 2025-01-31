﻿using Back_End.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Locations", Schema = "dbo")]
    public class Locations
    {
        [Key]
        [Column("ID")]
        public int LocationID { get; set; }

        [Required]
        [MaxLength(25)]
        public string LocationDepartmentName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LocationMunicipalityName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LocationCityName { get; set; }

        public ICollection<Estates> Estates { get; set; }

    }
}
