﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models
{
    [Table("Schedules", Schema = "dbo")] //tabla y esquema al que pertence la clase en la base de datos de Sql
    public class Schedules
    {
        [Key]
        public int ID { get; set; }

        public String ScheduleDate { get; set; }

        public Times Times { get; set; }

    }
}
