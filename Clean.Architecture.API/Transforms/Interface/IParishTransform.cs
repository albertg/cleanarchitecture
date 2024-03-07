using Clean.Architecture.API.Entities;
using Clean.Architecture.Core.Model;

namespace Clean.Architecture.API.Transforms.Interface
{
    public interface IParishTransform
    {
        GetParishResponse Transform(Parish parish);
        GetParishnerInfoResponse Transform(Parishner parishner);
        List<GetParishnerInfoResponse> Transform(List<Parishner> parishnerList);
    }
}
