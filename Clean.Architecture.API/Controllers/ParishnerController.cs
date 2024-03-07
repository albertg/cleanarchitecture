using Clean.Architecture.API.Entities;
using Clean.Architecture.API.Transforms.Interface;
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
        private readonly IParishnerTransform parishnerTransform;

        public ParishnerController(ICreateParishnerUsecases createParishnerUsecases, IGetParishnerUsecases getParishnerUsecases,
            IModifyParishnerUsecases modifyParishnerUsecases, IGetPagedParishners getPagedParishners, IParishnerTransform parishnerTransform)
        {
            this.createParishnerUsecases = createParishnerUsecases;
            this.getParishnerUsecases = getParishnerUsecases;
            this.modifyParishnerUsecases = modifyParishnerUsecases;
            this.getPagedParishners = getPagedParishners;
            this.parishnerTransform = parishnerTransform;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Guid> AddParishner([FromBody] NewParishnerRequest newParishnerRequest)
        {
            Parishner parishner = this.parishnerTransform.Transform(newParishnerRequest);
            parishner = this.createParishnerUsecases.AddParishner(parishner, newParishnerRequest.ParishId);
            return parishner.Id;
        }

        [HttpGet("get")]
        [ProducesResponseType(typeof(GetParishnerDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetParishnerDetailsResponse> GetParishner(Guid parishnerId, Guid parishId)
        {
            Parishner parishner = this.getParishnerUsecases.GetParishner(parishnerId, parishId);
            GetParishnerDetailsResponse getParishnerDetailsResponse = this.parishnerTransform.Transform<GetParishnerDetailsResponse>(parishner);//Transform(parishner);
            if(getParishnerDetailsResponse != null)
            {
                return Ok(getParishnerDetailsResponse);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet("getmany")]
        public List<GetParishnerDetailsResponse> GetParishners(Guid parishId, int currentPage, int pageSize)
        {
            List<Parishner> parishners = this.getPagedParishners.GetParishners(parishId, currentPage, pageSize);
            return this.parishnerTransform.Transform<GetParishnerDetailsResponse>(parishners);
        }

        [HttpPut("promote")]
        public void PromoteParishner(Guid parishnerId, Guid parishId)
        {
            this.modifyParishnerUsecases.PromoteParishnerAsCouncilMember(parishnerId, parishId);
        }        
    }
}
