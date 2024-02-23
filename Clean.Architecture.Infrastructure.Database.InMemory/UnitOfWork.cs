using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using Clean.Architecture.Infrastructure.Database.InMemory.Entities;
using Clean.Architecture.Infrastructure.Database.InMemory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.Database.InMemory
{
    internal class UnitOfWork
    {
        private readonly ParishDBContext context;

        public UnitOfWork(ParishDBContext context)
        {
            this.context = context;
        }
        
        internal GenericRepository<DbParish> ParishRepository { get { return new GenericRepository<DbParish>(context); } }
        internal ParishnerRepository ParishnerRepository { get { return new ParishnerRepository(context); } }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
