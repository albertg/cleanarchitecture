using Clean.Architecture.Core.Model;

namespace Clean.Architecture.API.Usecases.Interfaces
{
    public interface IGetPagedParishners
    {
        List<Parishner> GetParishners(Guid parishId, int page, int pageSize);
    }
}
