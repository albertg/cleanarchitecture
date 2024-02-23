namespace Clean.Architecture.API.Entities
{
    public class NewParishRequest
    {
        public string ParishName { get; set; }
        public string ParishAddress { get; set; }
        public string PriestName { get; set; }
        public DateTime PriestDateOfBirth { get; set; }
        public string PriestAddress { get; set; }
        public string PriestPhone { get; set; }
    }
}
