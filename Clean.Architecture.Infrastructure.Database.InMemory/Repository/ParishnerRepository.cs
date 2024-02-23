using Clean.Architecture.Core.Model;
using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using Clean.Architecture.Infrastructure.Database.InMemory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.Database.InMemory.Repository
{
    internal class ParishnerRepository : GenericRepository<DbParishner>
    {
        public ParishnerRepository(ParishDBContext context) : base(context) { }

        public DbParishner Get(Guid parishnerId, Guid parishId)
        {
            return dbSet.Where(x => x.Id == parishnerId && x.Parish.Id == parishId).FirstOrDefault();
        }
    }
}
