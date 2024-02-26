using Clean.Architecture.API.Usecases.Interfaces;
using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Usecase.Interface.External;
using System.Security.Cryptography.Xml;

namespace Clean.Architecture.API.Usecases
{
    public class GetPagedParishners : IGetPagedParishners
    {
        private readonly IParishnerPersistence parishnerPersistence;

        public GetPagedParishners(IParishnerPersistence parishnerPersistence)
        {
            this.parishnerPersistence = parishnerPersistence;
        }

        public List<Parishner> GetParishners(Guid parishId, int page, int pageSize)
        {
            return this.parishnerPersistence.GetParishners(parishId, page, pageSize);
        }
    }
}
