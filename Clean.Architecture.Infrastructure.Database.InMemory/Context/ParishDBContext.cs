using Clean.Architecture.Core.Model;
using Clean.Architecture.Infrastructure.Database.InMemory.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.Database.InMemory.Context
{
    public class ParishDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ParishDb");
        }

        internal DbSet<DbParish> Parishes { get; set; }
        internal DbSet<DbParishner> Parishners { get; set; }
    }
}
