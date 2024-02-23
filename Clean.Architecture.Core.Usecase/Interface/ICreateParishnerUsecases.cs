using Clean.Architecture.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase.Interface
{
    public interface ICreateParishnerUsecases
    {
        Parishner AddParishner(Parishner parishner, Guid parishId);
    }
}
