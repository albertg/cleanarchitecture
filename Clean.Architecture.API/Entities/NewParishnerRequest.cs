using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Clean.Architecture.API.Entities
{
    public class NewParishnerRequest
    {
        public Guid ParishId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        //For swagger to display the enum values as string. Here and in program.cs
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MemberType TypeOfMember { get; set; }
    }
}
