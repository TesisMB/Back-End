﻿
using Entities.DataTransferObjects.Models.Vehicles___Dto;

namespace Back_End.Models.Vehicles___Dto
{
    public class VehiclesDto
    {
        public string VehiclePatent { get; set; }

        //public string Utility { get; set; }
        public string Type { get; set; }
        //public string Types { get; set; }

        public int VehicleYear { get; set; }

        //public EmployeesVehiclesDto Employees { get; set; }
        public string EmployeeName { get; set; }


    }
}
