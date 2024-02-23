namespace Clean.Architecture.API.Entities
{
    public class GetParishResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public GetParishnerInfoResponse ParishPriest { get; set; }
        public List<GetParishnerInfoResponse> AssistantParishPriests { get; set; }
        public List<GetParishnerInfoResponse> CouncilMembers { get; set; }
        public List<GetParishnerInfoResponse> Parishners { get; set; }
    }
}
