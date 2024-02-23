namespace Clean.Architecture.API.Entities
{
    public class GetParishResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public GetParishnerResponse ParishPriest { get; set; }
        public List<GetParishnerResponse> AssistantParishPriests { get; set; }
        public List<GetParishnerResponse> CouncilMembers { get; set; }
        public List<GetParishnerResponse> Parishners { get; set; }
    }
}
