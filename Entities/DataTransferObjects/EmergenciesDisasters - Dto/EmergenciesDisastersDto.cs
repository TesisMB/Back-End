﻿using Back_End.Models;
using Entities.DataTransferObjects.Alerts___Dto;
using Entities.DataTransferObjects.Locations___Dto;
using Entities.DataTransferObjects.TypesEmergenciesDisasters___Dto;
using System;

namespace Entities.DataTransferObjects.EmergenciesDisasters___Dto
{
    public class EmergenciesDisastersDto
    {
        public int EmergencyDisasterID { get; set; }

        public string EmergencyDisasterStartDate { get; set; }

        public string? EmergencyDisasterEndDate { get; set; }

        public string EmergencyDisasterInstruction { get; set; }

        public EmployeesDto Employees { get; set; }

        public LocationsDto Locations { get; set; }

        public TypesEmergenciesDisastersDto TypesEmergenciesDisasters { get; set; }

        public AlertsDto Alerts { get; set; }

    }
}
