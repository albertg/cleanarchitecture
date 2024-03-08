using Clean.Architecture.Core.Model.Enums;
using Clean.Architecture.Core.Model.Exceptions;

namespace Clean.Architecture.Core.Model.Aggregate
{
    public class Members
    {
        private readonly List<Parishner> parishnerList;

        public Members()
        {
            this.parishnerList = new List<Parishner>();
        }

        public void AddParishner(Parishner parishner)
        {
            if(parishner.ParishnerType == ParishnerType.Priest && PriestExists())
            {
                throw new PriestExistsException("A parish priest has already been assigned to the parish");
            }
            this.parishnerList.Add(parishner);
        }

        public List<Parishner> GetMembers()
        {
            return this.parishnerList.Where(p => p.ParishnerType == ParishnerType.Parishner).ToList();
        }

        public Parishner GetPriest()
        {
            return parishnerList.First(p => p.ParishnerType == ParishnerType.Priest);
        }

        public List<Parishner> GetAssistants()
        {
            return this.parishnerList.Where(p => p.ParishnerType == ParishnerType.AssistantPriest).ToList();
        }

        public List<Parishner> GetCouncilMembers()
        {
            return this.parishnerList.Where(p => p.IsCouncilMember).ToList();
        }

        private bool PriestExists()
        {
            return parishnerList.Count(p => p.ParishnerType == ParishnerType.Priest) == 1;
        }
    }
}
