﻿using Back_End.Models.VolunteersSkills___Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class VolunteersDto
    {
        public int ID { get; set; }

        public string VolunteerAvatar { get; set; }

        public string VolunteerDescription { get; set; }
        public UsersDto Users { get; set; }

        public ICollection<VolunteersSkillsDto> VolunteersSkills { get; set; }

    }
}