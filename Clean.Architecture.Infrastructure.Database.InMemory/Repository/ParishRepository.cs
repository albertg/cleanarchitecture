using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using Clean.Architecture.Infrastructure.Database.InMemory.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.Database.InMemory.Repository
{
    internal class ParishRepository : GenericRepository<DbParish>
    {
        public ParishRepository(ParishDBContext context) : base(context) { }

        public override DbParish Get(Guid id)
        {
            return dbSet.Where(p => p.Id == id).Include(ps => ps.Parishners.Where(x => x.ParishnerType == 0 || x.ParishnerType == 1 || x.IsMemberOfCouncil == true)).SingleOrDefault();
        }
    }
}
