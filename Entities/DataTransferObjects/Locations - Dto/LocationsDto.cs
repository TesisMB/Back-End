﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.DataTransferObjects.Locations___Dto
{
    public class LocationsDto
    {
        public int LocationID { get; set; }
   
        public string LocationDepartmentName { get; set; }

        public string LocationMunicipalityName { get; set; }

        public string LocationCityName { get; set; }

        public decimal LocationLongitude { get; set; }

        public decimal LocationLatitude { get; set; }
    }
}
