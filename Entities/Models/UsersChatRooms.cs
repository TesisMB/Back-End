﻿using Back_End.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("UsersChatRooms", Schema = "dbo")]
    public class UsersChatRooms
    {
        public int FK_UserID { get; set; }
        public int FK_ChatRoomID { get; set; }

        public Users Users { get; set; }
        public ChatRooms Chat { get; set; }
    }
}
