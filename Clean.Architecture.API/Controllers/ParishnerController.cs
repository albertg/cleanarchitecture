using Clean.Architecture.API.Entities;
using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Enums;
using Clean.Architecture.Core.Usecase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Clean.Architecture.API.Controllers
{
    [Route("api/parishner")]
    [ApiController]
    public class ParishnerController : ControllerBase
    {
        private readonly ICreateParishnerUsecases createParishnerUsecases;

        public ParishnerController(ICreateParishnerUsecases createParishnerUsecases)
        {
            this.createParishnerUsecases = createParishnerUsecases;
        }

        [HttpPost("add")]
        public Guid Post([FromBody] NewParishnerRequest newParishnerRequest)
        {
            Parishner parishner = Transform(newParishnerRequest);
            parishner = this.createParishnerUsecases.AddParishner(parishner, newParishnerRequest.ParishId);
            return parishner.Id;
        }

        private Parishner Transform(NewParishnerRequest newParishnerRequest)
        {
            Parishner parishner = new Parishner(newParishnerRequest.Name)
            {
                Address = newParishnerRequest.Address,
                DateOfBirth = newParishnerRequest.DateOfBirth,
                PhoneNumber = newParishnerRequest.Phone,
                ParishnerType = Transform(newParishnerRequest.TypeOfMember)
            };
            return parishner;
        }

        private ParishnerType Transform(MemberType memberType)
        {
            ParishnerType parishnerType = ParishnerType.Parishner;
            switch (memberType)
            {
                case MemberType.Assistant:
                    parishnerType = ParishnerType.AssistantPriest; break;
                case MemberType.ParishPriest:
                    parishnerType = ParishnerType.Priest; break;
            }
            return parishnerType;
        }
    }
}
