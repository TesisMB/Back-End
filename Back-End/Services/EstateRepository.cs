﻿using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public class EstateRepository : ICruzRojaRepository<Estate>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public EstateRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public IEnumerable<Estate> GetList()
        {
            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
            return _context.Estate
                    .ToList();
        }

        public void Add(Estate entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Estate entity)
        {
            if (entity == null) //Verifico que el Usuario no sea null
            {
                throw new ArgumentNullException(nameof(entity));
            }
            //Se retorna al Controller que no hay errores
            _context.Estate.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
          
        }

        public Estate GetListId(int EntityID)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            throw new NotImplementedException();
        }

        public void Update(Estate entity)
        {
            throw new NotImplementedException();
        }
    }
}
