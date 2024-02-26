using Clean.Architecture.API.Entities;
using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Usecase.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.API.Controllers
{
    [Route("api/parish")]
    [ApiController]
    public class ParishController : ControllerBase
    {
        private readonly ICreateParishUsecase createParishUsecase;
        private readonly IGetParishUsecases getParishUsecases;

        public ParishController(ICreateParishUsecase createParishUsecase, IGetParishUsecases getParishUsecases)
        {
            this.createParishUsecase = createParishUsecase;
            this.getParishUsecases = getParishUsecases;
        }

        [HttpPost("add")]
        public Guid Post([FromBody] NewParishRequest newParish)
        {
            Parish parish = new Parish(newParish.ParishName, newParish.ParishAddress);
            Parishner parishPriest = new Parishner(newParish.PriestName);
            parishPriest.Address = newParish.PriestAddress;
            parishPriest.PhoneNumber = newParish.PriestPhone;
            parishPriest.DateOfBirth = newParish.PriestDateOfBirth;
            parish = this.createParishUsecase.CreateParish(parish, parishPriest);
            return parish.Id;
        }

        [HttpGet(Name = "get")]
        public GetParishResponse Get(Guid parishId)
        {
            Parish parishList = this.getParishUsecases.GetParish(parishId);
            GetParishResponse getParishResponse = Transform(parishList);
            return getParishResponse;
        }

        private GetParishResponse Transform(Parish parish)
        {
            GetParishResponse getParishResponse = new GetParishResponse()
            {
                Address = parish.Address,
                Id = parish.Id,
                Name = parish.Name,
                ParishPriest = Transform(parish.GetPriest()),
                AssistantParishPriests = Transform(parish.GetAssistantPriests()),
                CouncilMembers = Transform(parish.GetCouncilMembers())
            };
            return getParishResponse;
        }

        private GetParishnerInfoResponse Transform(Parishner parishner)
        {
            GetParishnerInfoResponse getParishnerResponse = new GetParishnerInfoResponse()
            {
                Id = parishner.Id,
                Name = parishner.Name,
            };
            return getParishnerResponse;
        }

        private List<GetParishnerInfoResponse> Transform(List<Parishner> parishnerList)
        {
            List<GetParishnerInfoResponse> getParishnerResponse = new List<GetParishnerInfoResponse>();
            foreach(Parishner parishner in parishnerList)
            {
                getParishnerResponse.Add(Transform(parishner));
            }
            return getParishnerResponse;
        }
    }
}
