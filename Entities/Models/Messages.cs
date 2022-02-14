﻿using Back_End.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Messages", Schema = "dbo")]
    public class Messages
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public String Message { get; set; }

        [Required]
        public Boolean MessageState { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [ForeignKey("FK_ChatRoomID")]
        public ChatRooms ChatRooms { get; set; }

        public int FK_ChatRoomID { get; set; }


        [ForeignKey("FK_UserID")]
        public Users Users { get; set; }
        public int FK_UserID { get; set; }
    }
}
