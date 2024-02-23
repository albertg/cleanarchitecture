using Clean.Architecture.Core.Model;

namespace Clean.Architecture.Core.Usecase.Interface
{
    public interface ICreateParishUsecase
    {
        Parish CreateParish(Parish parish, Parishner parishPriest);
    }
}
