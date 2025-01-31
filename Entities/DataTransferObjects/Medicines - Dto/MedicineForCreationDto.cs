﻿using Microsoft.AspNetCore.Http;
using System;

namespace Entities.DataTransferObjects.Medicines___Dto
{
    public class MedicineForCreationDto
    {
        public string MedicineExpirationDate { get; set; }

        public String MedicineLab { get; set; }

        public String MedicineDrug { get; set; }

        public float MedicineWeight { get; set; }

        public string MedicineUnits { get; set; }
    }
}
