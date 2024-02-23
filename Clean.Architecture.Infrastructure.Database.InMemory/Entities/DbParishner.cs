using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.Database.InMemory.Entities
{
    internal class DbParishner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ParishnerType { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DbParish Parish {  get; set; }
    }
}
