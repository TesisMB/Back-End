﻿using Microsoft.EntityFrameworkCore;
using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entities
{
    public class CruzRojaContext : DbContext /*Creo un Context para poder obtenener apenas me autentifico el DNI y el password
                                             y devolver los resultados correspondientes */
                                               

    {    

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        private const string Connection =
       @"Server=DESKTOP-QF24B4L;
            Database=CruzRojaDB;
            Trusted_Connection=True;
            MultipleActiveResultSets=true";


        //Devuelvo la conexion establecida arriba 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Connection);


    }
}
