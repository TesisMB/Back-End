﻿using Back_End.Entities;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    //La interfaz va a ser la que lleve todos los Metodos necesarios para realizar el CRUD en Users
    public interface ICruzRojaRepository<TEntity>
    {

        //lista completa de usuarios
        IEnumerable<TEntity> GetList();

        //lista de usuarios por Id
        TEntity GetListId(int UserID);

        //Añadir un nuevo Usuario
        void Add(TEntity user);

        //Actualizar datos de un Usuario
        void Update(TEntity user);

        //Eliminar un Usuario
        void Delete(TEntity user);

        //Metodo para verificar la existenica de un usuario
        //bool UserExists(int UserID);

        //Metodo para poder verificar que los datos a guardar no falte.
        bool save();
    }
}