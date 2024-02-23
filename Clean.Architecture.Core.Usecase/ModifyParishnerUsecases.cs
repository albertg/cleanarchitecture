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
        private readonly IParishPersistence parishPersistance;

        public ModifyParishnerUsecases(IParishnerPersistence parishnerPersistence, IParishPersistence parishPersistance)
        {
            this.parishnerPersistence = parishnerPersistence;
            this.parishPersistance = parishPersistance;
        }

        public void PromoteParishnerAsCouncilMember(Guid parishId, Guid parishnerId)
        {
            //Parish parish = this.parishPersistance.GetParishById(parishId);
            //if (parish != null)
            //{
            //    Parishner parishner = this.parishnerPersistence.GetParishner(parishId, parishnerId);
            //    if (parishner != null)
            //    {
            //        parishner.PromoteAsCouncilMember();
            //        this.parishnerPersistence.UpdateParishner(parishner);
            //    }
            //}
        }
    }
}
