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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Guid> Post([FromBody] NewParishRequest newParish)
        {
            var parish = new Parish(newParish.ParishName, newParish.ParishAddress);
            var parishPriest = new Parishner(newParish.PriestName)
            {
                Address = newParish.PriestAddress,
                PhoneNumber = newParish.PriestPhone,
                DateOfBirth = newParish.PriestDateOfBirth
            };
            parish = this.createParishUsecase.CreateParish(parish, parishPriest);
            return Ok(parish.Id);
        }

        [HttpGet(Name = "get")]
        [ProducesResponseType(typeof(GetParishResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetParishResponse> Get(Guid parishId)
        {
            Parish parishList = this.getParishUsecases.GetParish(parishId);
            GetParishResponse getParishResponse = Transform(parishList);
            if(getParishResponse != null)
            {
                return Ok(getParishResponse);
            }
            else
            {
                return NotFound(parishId);
            }
        }

        private static GetParishResponse Transform(Parish parish)
        {
            GetParishResponse? getParishResponse = null;
            if (parish != null)
            {
                getParishResponse = new GetParishResponse()
                {
                    Address = parish.Address,
                    Id = parish.Id,
                    Name = parish.Name,
                    ParishPriest = Transform(parish.GetPriest()),
                    AssistantParishPriests = Transform(parish.GetAssistantPriests()),
                    CouncilMembers = Transform(parish.GetCouncilMembers())
                };
            }
            return getParishResponse;
        }

        private static GetParishnerInfoResponse Transform(Parishner parishner)
        {
            var getParishnerResponse = new GetParishnerInfoResponse()
            {
                Id = parishner.Id,
                Name = parishner.Name,
            };
            return getParishnerResponse;
        }

        private static List<GetParishnerInfoResponse> Transform(List<Parishner> parishnerList)
        {
            var getParishnerResponse = new List<GetParishnerInfoResponse>();
            foreach(Parishner parishner in parishnerList)
            {
                getParishnerResponse.Add(Transform(parishner));
            }
            return getParishnerResponse;
        }
    }
}
