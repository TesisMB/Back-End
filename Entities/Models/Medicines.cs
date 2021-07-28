﻿using Back_End.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("Medicines", Schema = "dbo")]
    public class Medicines
    {
        [Key]
        [Column("ID")]
        public int MedicineID { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineName { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineQuantity { get; set; }


        [Required]
        public DateTimeOffset MedicineExpirationDate { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineLab { get; set; }

        [Required]
        [MaxLength(50)]
        public String MedicineDrug { get; set; }

        [Required]
        [MaxLength(10)]
        public String MedicineWeight { get; set; }

        public String MedicineUtility { get; set; }

        [Required]
        public Boolean MedicineAvailability { get; set; }

        [Required]
        public int FK_EstateID { get; set; }

        [ForeignKey("FK_EstateID")]
        public Estates Estates { get; set; }

    }
}
