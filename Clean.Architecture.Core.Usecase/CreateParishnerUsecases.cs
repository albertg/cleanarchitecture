using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Enums;
using Clean.Architecture.Core.Usecase.Interface;
using Clean.Architecture.Core.Usecase.Interface.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Usecase
{
    public class CreateParishnerUsecases : ICreateParishnerUsecases
    {
        private readonly IParishPersistence parishPersistance;
        private readonly IParishnerPersistence parishnerPersistence;

        public CreateParishnerUsecases(IParishPersistence parishPersistance, IParishnerPersistence parishnerPersistence)
        {
            this.parishPersistance = parishPersistance;
            this.parishnerPersistence = parishnerPersistence;
        }

        public Parishner AddParishner(Parishner parishner, Guid parishId)
        {
            parishner.ParishnerType = ParishnerType.Parishner;
            Parish parish = this.parishPersistance.GetParishById(parishId);
            parish.RegisterParishner(parishner);
            this.parishnerPersistence.AddParishner(parishner, parishId);
            return parishner;
        }
    }
}
