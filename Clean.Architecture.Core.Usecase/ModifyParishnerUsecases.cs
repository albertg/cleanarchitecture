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
    public class ModifyParishnerUsecases : IModifyParishnerUsecases
    {
        private readonly IParishnerPersistence parishnerPersistence;

        public ModifyParishnerUsecases(IParishnerPersistence parishnerPersistence)
        {
            this.parishnerPersistence = parishnerPersistence;
        }

        public void PromoteParishnerAsCouncilMember(Guid parishnerId, Guid parishId)
        {
            Parishner parishner = this.parishnerPersistence.GetParishner(parishnerId, parishId);
            if (parishner != null)
            {
                parishner.PromoteAsCouncilMember();
                this.parishnerPersistence.UpdateParishner(parishner, parishId);
            }
        }
    }
}
