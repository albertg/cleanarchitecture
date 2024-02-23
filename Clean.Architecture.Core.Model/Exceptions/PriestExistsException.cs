using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Model.Exceptions
{
    public class PriestExistsException : Exception
    {
        public PriestExistsException(string message) : base(message)
        {
            
        }
    }
}
