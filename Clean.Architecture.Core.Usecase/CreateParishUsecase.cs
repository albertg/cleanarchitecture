using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Enums;
using Clean.Architecture.Core.Usecase.Interface;
using Clean.Architecture.Core.Usecase.Interface.External;

namespace Clean.Architecture.Core.Usecase
{
    public class CreateParishUsecase : ICreateParishUsecase
    {
        private readonly IParishPersistence parishPersistance;

        public CreateParishUsecase(IParishPersistence parishPersistance)
        {
            this.parishPersistance = parishPersistance;
        }

        public Parish CreateParish(Parish parish, Parishner parishPriest)
        {
            //TODO: Check if parish with same name and address already exists
            parishPriest.ParishnerType = ParishnerType.Priest;
            parishPriest.PromoteAsCouncilMember();
            parish.RegisterParishner(parishPriest);
            this.parishPersistance.AddParish(parish);
            return parish;
        }
    }
}
