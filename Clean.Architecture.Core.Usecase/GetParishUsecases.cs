using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Usecase.Interface;
using Clean.Architecture.Core.Usecase.Interface.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase
{
    public class GetParishUsecases : IGetParishUsecases
    {
        private readonly IParishPersistence parishPersistance;

        public GetParishUsecases(IParishPersistence parishPersistance)
        {
            this.parishPersistance = parishPersistance;
        }

        public List<Parish> GetAllParishes()
        {
            return this.parishPersistance.GetParishList();
        }
    }
}
