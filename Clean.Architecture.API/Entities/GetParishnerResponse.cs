namespace Clean.Architecture.API.Entities
{
    public class GetParishnerResponse : GetParishnerInfoResponse
    {        
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
