namespace Clean.Architecture.API.Entities
{
    public class GetParishnerDetailsResponse : GetParishnerResponse
    {
        public bool IsCouncilMember { get; set; }
        public MemberType MemberType { get; set; }
    }
}
