﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("Users", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Users
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(8)]
        public string UserDni { get; set; }

        [MaxLength(100)]
        public string UserPassword { get; set; }

        [Required]
        public Boolean UserAvailability { get; set; }

        [ForeignKey("FK_RoleID")]
        public Roles Roles { get; set; }
        public int FK_RoleID { get; set; }
        public Persons Persons { get; set; }

    }
}