namespace Clean.Architecture.API.Entities
{
    public class NewParishRequest
    {
        public string ParishName { get; set; } = string.Empty;
        public string ParishAddress { get; set; } = string.Empty;
        public string PriestName { get; set; } = string.Empty;
        public DateTime PriestDateOfBirth { get; set; }
        public string PriestAddress { get; set; } = string.Empty;
        public string PriestPhone { get; set; } = string.Empty;
    }
}
