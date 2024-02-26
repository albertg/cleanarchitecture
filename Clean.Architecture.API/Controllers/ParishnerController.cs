using Clean.Architecture.API.Entities;
using Clean.Architecture.API.Usecases.Interfaces;
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
        private readonly IGetParishnerUsecases getParishnerUsecases;
        private readonly IModifyParishnerUsecases modifyParishnerUsecases;
        private readonly IGetPagedParishners getPagedParishners;

        public ParishnerController(ICreateParishnerUsecases createParishnerUsecases, IGetParishnerUsecases getParishnerUsecases,
            IModifyParishnerUsecases modifyParishnerUsecases, IGetPagedParishners getPagedParishners)
        {
            this.createParishnerUsecases = createParishnerUsecases;
            this.getParishnerUsecases = getParishnerUsecases;
            this.modifyParishnerUsecases = modifyParishnerUsecases;
            this.getPagedParishners = getPagedParishners;
        }

        [HttpPost("add")]
        public Guid AddParishner([FromBody] NewParishnerRequest newParishnerRequest)
        {
            Parishner parishner = Transform(newParishnerRequest);
            parishner = this.createParishnerUsecases.AddParishner(parishner, newParishnerRequest.ParishId);
            return parishner.Id;
        }

        [HttpGet("get")]
        public GetParishnerDetailsResponse GetParishner(Guid parishnerId, Guid parishId)
        {
            Parishner parishner = this.getParishnerUsecases.GetParishner(parishnerId, parishId);
            return Transform(parishner);
        }

        [HttpGet("getmany")]
        public List<GetParishnerDetailsResponse> GetParishners(Guid parishId, int currentPage, int pageSize)
        {
            List<Parishner> parishners = this.getPagedParishners.GetParishners(parishId, currentPage, pageSize);
            return Transform(parishners);
        }

        [HttpPut("promote")]
        public void PromoteParishner(Guid parishnerId, Guid parishId)
        {
            this.modifyParishnerUsecases.PromoteParishnerAsCouncilMember(parishnerId, parishId);
        }

        private List<GetParishnerDetailsResponse> Transform(List<Parishner> parishners)
        {
            List<GetParishnerDetailsResponse> getParishnerDetailsResponses = new List<GetParishnerDetailsResponse>();
            foreach(Parishner parishner in parishners)
            {
                getParishnerDetailsResponses.Add(Transform(parishner));
            }
            return getParishnerDetailsResponses;
        }

        private GetParishnerDetailsResponse Transform(Parishner parishner)
        {
            GetParishnerDetailsResponse getParishnerResponse = new GetParishnerDetailsResponse()
            {
                Address = parishner.Address,
                DateOfBirth = parishner.DateOfBirth,
                Id = parishner.Id,
                Name = parishner.Name,
                Phone = parishner.PhoneNumber,
                IsCouncilMember = parishner.IsCouncilMember,
                MemberType = Transform(parishner.ParishnerType)
            };
            return getParishnerResponse;
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

        private MemberType Transform(ParishnerType parishnerType)
        {
            MemberType memberType = MemberType.Member;
            switch (parishnerType)
            {
                case ParishnerType.AssistantPriest:
                    memberType = MemberType.Assistant; break;
                case ParishnerType.Priest:
                    memberType = MemberType.ParishPriest; break;
            }
            return memberType;
        }
    }
}
