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
            parishPriest.ParishnerType = ParishnerType.Priest;
            parish.RegisterParishner(parishPriest);
            this.parishPersistance.AddParish(parish);
            return parish;
        }
    }
}
