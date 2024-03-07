using Clean.Architecture.API.Entities;
using Clean.Architecture.API.Transforms.Interface;
using Clean.Architecture.Core.Model;

namespace Clean.Architecture.API.Transforms
{
    internal class ParishTransforms : IParishTransform
    {
        public GetParishResponse Transform(Parish parish)
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

        public GetParishnerInfoResponse Transform(Parishner parishner)
        {
            var getParishnerResponse = new GetParishnerInfoResponse()
            {
                Id = parishner.Id,
                Name = parishner.Name,
            };
            return getParishnerResponse;
        }

        public List<GetParishnerInfoResponse> Transform(List<Parishner> parishnerList)
        {
            var getParishnerResponse = new List<GetParishnerInfoResponse>();
            foreach (Parishner parishner in parishnerList)
            {
                getParishnerResponse.Add(Transform(parishner));
            }
            return getParishnerResponse;
        }
    }
}
