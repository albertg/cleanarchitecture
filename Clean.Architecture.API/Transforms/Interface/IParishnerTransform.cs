using Clean.Architecture.API.Entities;
using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Enums;

namespace Clean.Architecture.API.Transforms.Interface
{
    public interface IParishnerTransform
    {
        T Transform<T>(Parishner parishner);
        List<T> Transform<T>(List<Parishner> parishners);
        Parishner Transform(NewParishnerRequest newParishnerRequest);
        ParishnerType Transform(MemberType memberType);
        MemberType Transform(ParishnerType parishnerType);
    }
}
