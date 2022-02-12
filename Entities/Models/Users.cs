﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Users", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Users
    {
        [Column("ID")]
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(8)]
        public string UserDni { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserPassword { get; set; }

        public Boolean UserAvailability { get; set; }

        [ForeignKey("FK_RoleID")]
        public Roles Roles { get; set; }

        [Required]
        public int FK_RoleID { get; set; }

        public string ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public Persons Persons { get; set; }

        public Volunteers Volunteers { get; set; }

        public Employees Employees { get; set; }

        [ForeignKey("FK_EstateID")]
        public Estates Estates { get; set; }

        [Required]
        public int FK_EstateID { get; set; }

        [ForeignKey("FK_LocationID")]
        public Locations Locations { get; set; }

        [Required]
        public int FK_LocationID { get; set; }

        public ICollection<Resources_Request> Resources_Requests { get; set; }

        [ForeignKey("FK_UserID")]
        public ICollection<UsersNotifications> UsersNotifications { get; set; }


        [ForeignKey("FK_UserID")]
        public ICollection<UsersChat> UsersChat { get; set; }

        [ForeignKey("FK_UserID")]
        public ICollection<UsersChatRooms> UsersChatRooms { get; set; }
        public Messages Messages { get; set; }

    }
}