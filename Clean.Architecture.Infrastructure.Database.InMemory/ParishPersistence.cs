using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Model.Aggregate;
using Clean.Architecture.Core.Model.Enums;
using Clean.Architecture.Core.Model.Exceptions;
using Clean.Architecture.Core.Usecase.Interface.External;
using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using Clean.Architecture.Infrastructure.Database.InMemory.Entities;

namespace Clean.Architecture.Infrastructure.Database.InMemory
{
    public class ParishPersistence : IParishPersistence, IParishnerPersistence
    {
        private UnitOfWork unitOfWork;

        public ParishPersistence(ParishDBContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        public List<Parish> GetParishList()
        {
            List<DbParish> dbParishList = this.unitOfWork.ParishRepository.GetAll();
            List<Parish> parishList = new List<Parish>();
            foreach(DbParish dbParish in dbParishList)
            {
                Parish parish = new Parish(dbParish.Id, dbParish.Name, dbParish.Address);
                RegisterParishners(parish, dbParish.Parishners);
                parishList.Add(parish);
            }
            return parishList;
        }

        public Parishner GetParishner(Guid parishnerId, Guid parishId)
        {
            DbParishner dbParishner = this.unitOfWork.ParishnerRepository.Get(parishnerId, parishId);            
            return Transform(dbParishner);
        }

        public void UpdateParishner(Parishner parishner)
        {
            throw new NotImplementedException();
        }

        public void AddParish(Parish parish)
        {
            DbParish dbParish = CreateParish(parish);
            CreateDbParishner(parish.GetPriest(), dbParish);
            unitOfWork.Save();
        }

        public void AddParishner(Parishner parishner, Guid parishId)
        {
            DbParish dbParish = this.unitOfWork.ParishRepository.Get(parishId);
            CreateDbParishner(parishner, dbParish);
            unitOfWork.Save();
        }

        public Parish GetParishById(Guid parishId)
        {
            DbParish dbParish = this.unitOfWork.ParishRepository.Get(parishId);
            Parish parish = new Parish(dbParish.Id, dbParish.Name, dbParish.Address);
            RegisterParishners(parish, dbParish.Parishners);
            return parish;
        }

        private Parishner Transform(DbParishner dbParishner)
        {
            Parishner parishner = null;
            if (dbParishner != default)
            {
                parishner = new Parishner(dbParishner.Id, dbParishner.Name)
                {
                    Address = dbParishner.Address,
                    DateOfBirth = dbParishner.DateOfBirth,
                    ParishnerType = (ParishnerType)dbParishner.ParishnerType,
                    PhoneNumber = dbParishner.Phone
                };
                if (dbParishner.IsMemberOfCouncil)
                {
                    parishner.PromoteAsCouncilMember();
                }
            }
            return parishner;
        }

        private void RegisterParishners(Parish parish, List<DbParishner> parishners)
        {
            foreach (DbParishner dbParishner in parishners)
            {
                Parishner parishner = new Parishner(dbParishner.Id, dbParishner.Name)
                {
                    ParishnerType = (ParishnerType)dbParishner.ParishnerType,
                    Address = dbParishner.Address,
                    DateOfBirth = dbParishner.DateOfBirth,
                    PhoneNumber = dbParishner.Phone
                };
                if (dbParishner.IsMemberOfCouncil)
                {
                    parishner.PromoteAsCouncilMember();
                }
                parish.RegisterParishner(parishner);
            }
        }

        private DbParish CreateParish(Parish parish)
        {
            DbParish dbParish = new DbParish();
            dbParish.Name = parish.Name;
            dbParish.Address = parish.Address;
            dbParish.Id = parish.Id;
            unitOfWork.ParishRepository.Add(dbParish);
            return dbParish;
        }

        private DbParishner CreateDbParishner(Parishner parishner, DbParish parish)
        {
            DbParishner dbParishner = new DbParishner();
            dbParishner.Parish = parish;
            dbParishner.Id = parishner.Id;
            dbParishner.Name = parishner.Name;
            dbParishner.DateOfBirth = parishner.DateOfBirth;
            dbParishner.ParishnerType = (int)parishner.ParishnerType;
            dbParishner.IsMemberOfCouncil = parishner.IsCouncilMember;
            dbParishner.Address = parishner.Address;
            dbParishner.Phone = parishner.PhoneNumber;
            unitOfWork.ParishnerRepository.Add(dbParishner);
            return dbParishner;
        }
    }
}
