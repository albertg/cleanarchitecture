namespace Clean.Architecture.API.Entities
{
    public class NewParishnerRequest
    {
        public Guid ParishId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
