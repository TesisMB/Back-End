﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    [Table("Roles", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class RolesDto
    {

        [Key]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }


        public ICollection<Users> Users { get; set; }
            = new List<Users>();
    }
}
