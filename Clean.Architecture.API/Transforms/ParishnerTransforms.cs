using Clean.Architecture.API.Entities;
using Clean.Architecture.API.Transforms.Interface;
using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Enums;

namespace Clean.Architecture.API.Transforms
{
    internal class ParishnerTransforms : IParishnerTransform
    {
        public T Transform<T>(Parishner parishner)
        {
            if (parishner != null) 
            {
                if (typeof(T) == typeof(GetParishnerDetailsResponse))
                {
                    GetParishnerDetailsResponse getParishnerDetailsResponse = new()
                    {
                        Address = parishner.Address,
                        DateOfBirth = parishner.DateOfBirth,
                        Id = parishner.Id,
                        Name = parishner.Name,
                        Phone = parishner.PhoneNumber,
                        IsCouncilMember = parishner.IsCouncilMember,
                        MemberType = Transform(parishner.ParishnerType)
                    };
                    return (T)Convert.ChangeType(getParishnerDetailsResponse, typeof(T));
                }
                else
                {
                    var getParishnerResponse = new GetParishnerInfoResponse()
                    {
                        Id = parishner.Id,
                        Name = parishner.Name,
                    };
                    return (T)Convert.ChangeType(getParishnerResponse, typeof(T));
                }
            }
            return default;
        }

        public List<T> Transform<T>(List<Parishner> parishners)
        {
            if(parishners != null && parishners.Count > 0)
            {
                if (typeof(T) == typeof(GetParishnerDetailsResponse))
                {
                    var getParishnerDetailsResponses = new List<GetParishnerDetailsResponse>();
                    foreach (Parishner parishner in parishners)
                    {
                        getParishnerDetailsResponses.Add(Transform<GetParishnerDetailsResponse>(parishner));
                    }
                    return (List<T>)Convert.ChangeType(getParishnerDetailsResponses, typeof(List<T>));
                }
            }
            return default;
        }

        public Parishner Transform(NewParishnerRequest newParishnerRequest)
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

        public ParishnerType Transform(MemberType memberType)
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

        public MemberType Transform(ParishnerType parishnerType)
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
