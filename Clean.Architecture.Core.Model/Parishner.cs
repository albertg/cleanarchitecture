using Clean.Architecture.Core.Model.Enums;

namespace Clean.Architecture.Core.Model
{
    public class Parishner : Model
    {
        private bool isCouncilMember = false;

        public Parishner(string name) : base(Guid.NewGuid(), name)
        {
        }

        public Parishner(Guid id, string name) : base(id, name)
        {
        }

        public ParishnerType ParishnerType { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsCouncilMember 
        { 
            get { return this.isCouncilMember; } 
            private set { this.isCouncilMember = value; } 
        }

        public void PromoteAsCouncilMember()
        {
            this.isCouncilMember = true;
        }

        public void RemoveFromCouncil()
        {
            this.isCouncilMember = false;
        }
    }
}
