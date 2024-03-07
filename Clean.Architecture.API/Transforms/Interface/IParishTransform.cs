using Clean.Architecture.API.Entities;
using Clean.Architecture.Core.Model;

namespace Clean.Architecture.API.Transforms.Interface
{
    internal interface IParishTransform
    {
        GetParishResponse Transform(Parish parish);

    }
}
