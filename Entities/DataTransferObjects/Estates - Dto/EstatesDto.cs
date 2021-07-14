﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Models.Employees___Dto
{
    public class EstatesDto
    {
        public int EstateID { get; set; }
        public string EstatePhone { get; set; }
        public LocationAddressDto LocationAddress { get; set; }
        public ICollection<EstatesTimesDto> EstatesTimes { get; set; }
    }
}
