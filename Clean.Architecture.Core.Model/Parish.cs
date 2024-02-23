using Clean.Architecture.Core.Model.Aggregate;

namespace Clean.Architecture.Core.Model
{
    public class Parish : Model
    {
        public Parish(string name, string address) : base(Guid.NewGuid(), name) 
        { 
            this.Address = address;
            this.Parishners = new Members();
        }

        public Parish(Guid id, string name, string address) : base(id, name)
        {
            this.Address = address;
            this.Parishners = new Members();
        }

        public string Address { get; set; }

        private Members Parishners { get; }

        public Parishner GetPriest()
        {
            return this.Parishners.GetPriest();
        }

        public List<Parishner> GetAssistantPriests()
        {
            return this.Parishners.GetAssistants();
        }

        public List<Parishner> GetMembers()
        {
            return this.Parishners.GetMembers();
        }

        public List<Parishner> GetCouncilMembers()
        {
            return this.Parishners.GetCouncilMembers();
        }

        public void RegisterParishner(Parishner parishner)
        {
            this.Parishners.AddParishner(parishner);
        }
    }
}
