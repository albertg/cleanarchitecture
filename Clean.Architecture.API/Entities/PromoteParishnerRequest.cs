namespace Clean.Architecture.API.Entities
{
    public class PromoteParishnerRequest
    {
        public Guid ParishId { get; set; }
        public Guid ParishnerId { get; set; }
    }
}
