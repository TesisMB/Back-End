﻿using System;

namespace Back_End.Models
{
    public class PersonsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Address { get; set; }
    }
}
