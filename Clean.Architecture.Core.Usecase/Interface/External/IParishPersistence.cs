using Clean.Architecture.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase.Interface.External
{
    public interface IParishPersistence
    {
        void AddParish(Parish parish);
        List<Parish> GetParishList();
        Parish GetParishById(Guid parishId);
    }
}
