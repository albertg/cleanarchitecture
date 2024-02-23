using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Exceptions;
using Clean.Architecture.Core.Usecase.Interface;
using Clean.Architecture.Core.Usecase.Interface.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase
{
    public class GetParishnerUsecases : IGetParishnerUsecases
    {
        private readonly IParishnerPersistence parishnerPersistence;

        public GetParishnerUsecases(IParishnerPersistence parishnerPersistence)
        {
            this.parishnerPersistence = parishnerPersistence;
        }

        public Parishner GetParishner(Guid parishnerId, Guid parishId)
        {
            Parishner parishner = this.parishnerPersistence.GetParishner(parishnerId, parishId);
            if(parishner == null)
            {
                throw new NotFoundException($"Parishner with Id {parishnerId} was not found");
            }
            return parishner;
        }
    }
}
