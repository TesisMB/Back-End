﻿using Back_End.Models;
using Entities.DataTransferObjects.Alerts___Dto;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
   public class EmergenciesDisastersDto 
    {
        public int EmergencyDisasterID { get; set; }

        public DateTime EmergencyDisasterStartDate { get; set; }

        public DateTime? EmergencyDisasterEndDate { get; set; }

        public string EmergencyDisasterInstruction { get; set; }

        public EmployeesDto Employees { get; set; }

        public LocationsDto Locations { get; set; }

        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }

        public AlertsDto Alerts { get; set; }

    }
}
