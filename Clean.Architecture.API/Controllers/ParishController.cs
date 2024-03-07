using Clean.Architecture.API.Entities;
using Clean.Architecture.API.Transforms.Interface;
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
        private readonly IParishTransform parishTransform;

        public ParishController(ICreateParishUsecase createParishUsecase, IGetParishUsecases getParishUsecases, 
            IParishTransform parishTransform)
        {
            this.createParishUsecase = createParishUsecase;
            this.getParishUsecases = getParishUsecases;
            this.parishTransform = parishTransform;
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
            GetParishResponse getParishResponse = this.parishTransform.Transform(parishList);
            if(getParishResponse != null)
            {
                return Ok(getParishResponse);
            }
            else
            {
                return NotFound(parishId);
            }
        }
    }
}
