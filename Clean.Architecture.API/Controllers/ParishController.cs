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

        [HttpPost("create")]
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
        public List<GetParishResponse> Get()
        {
            List<Parish> parishList = this.getParishUsecases.GetAllParishes();
            List<GetParishResponse> getParishResponse = Transform(parishList);
            return getParishResponse;
        }

        private List<GetParishResponse> Transform(List<Parish> parishList)
        {
            List<GetParishResponse> getParishResponse = new List<GetParishResponse>();
            foreach(Parish parish in parishList)
            {
                GetParishResponse getParish = new GetParishResponse()
                {
                    Address = parish.Address,
                    Id = parish.Id,
                    Name = parish.Name,
                    ParishPriest = Transform(parish.GetPriest()),
                    AssistantParishPriests = Transform(parish.GetAssistantPriests()),
                    CouncilMembers = Transform(parish.GetCouncilMembers()),
                    Parishners = Transform(parish.GetMembers())
                };
                getParishResponse.Add(getParish);
            }
            return getParishResponse;
        }

        private GetParishnerResponse Transform(Parishner parishner)
        {
            GetParishnerResponse getParishnerResponse = new GetParishnerResponse()
            {
                Address = parishner.Address,
                Id = parishner.Id,
                DateOfBirth = parishner.DateOfBirth,
                Name = parishner.Name,
                Phone = parishner.PhoneNumber
            };
            return getParishnerResponse;
        }

        private List<GetParishnerResponse> Transform(List<Parishner> parishnerList)
        {
            List<GetParishnerResponse> getParishnerResponse = new List<GetParishnerResponse>();
            foreach(Parishner parishner in parishnerList)
            {
                getParishnerResponse.Add(Transform(parishner));
            }
            return getParishnerResponse;
        }
    }
}
