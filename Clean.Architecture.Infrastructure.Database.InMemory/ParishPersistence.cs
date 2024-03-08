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
        private readonly UnitOfWork unitOfWork;

        public ParishPersistence(ParishDBContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        public Parishner GetParishner(Guid parishnerId, Guid parishId)
        {
            DbParishner dbParishner = this.unitOfWork.ParishnerRepository.Get(parishnerId, parishId);            
            return Transform(dbParishner);
        }

        public void UpdateParishner(Parishner parishner, Guid parishId)
        {
            DbParishner dbParishner = this.unitOfWork.ParishnerRepository.Get(parishner.Id, parishId);
            dbParishner.DateOfBirth = parishner.DateOfBirth;
            dbParishner.Phone = parishner.PhoneNumber;
            dbParishner.Address = parishner.Address;
            dbParishner.Name = parishner.Name;
            dbParishner.IsMemberOfCouncil = parishner.IsCouncilMember;
            this.unitOfWork.ParishnerRepository.Save(dbParishner);
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
            Parish? parish = null;
            DbParish dbParish = this.unitOfWork.ParishRepository.Get(parishId);            
            if(dbParish != null)
            {
                parish = new Parish(dbParish.Id, dbParish.Name, dbParish.Address);
                RegisterParishners(parish, dbParish.Parishners);
            }
            
            return parish;
        }

        public List<Parishner> GetParishners(Guid parishId, int page, int pageSize)
        {
            List<DbParishner> dbParishners = this.unitOfWork.ParishnerRepository.GetMany(parishId, page, pageSize);
            return Transform(dbParishners);
        }

        private static List<Parishner> Transform(List<DbParishner> dbParishners)
        {
            List<Parishner> parishners = new();
            foreach(DbParishner dbParishner in dbParishners)
            {
                parishners.Add(Transform(dbParishner));
            }
            return parishners;
        }

        private static Parishner Transform(DbParishner dbParishner)
        {
            Parishner? parishner = null;
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

        private static void RegisterParishners(Parish parish, List<DbParishner> parishners)
        {
            foreach (DbParishner dbParishner in parishners)
            {
                var parishner = new Parishner(dbParishner.Id, dbParishner.Name)
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
            var dbParish = new DbParish()
            {
                Name = parish.Name,
                Address = parish.Address,
                Id = parish.Id
            };
            unitOfWork.ParishRepository.Add(dbParish);
            return dbParish;
        }

        private void CreateDbParishner(Parishner parishner, DbParish parish)
        {
            var dbParishner = new DbParishner()
            {
                Parish = parish,
                Id = parishner.Id,
                Name = parishner.Name,
                DateOfBirth = parishner.DateOfBirth,
                ParishnerType = (int)parishner.ParishnerType,
                IsMemberOfCouncil = parishner.IsCouncilMember,
                Address = parishner.Address,
                Phone = parishner.PhoneNumber
            };
            unitOfWork.ParishnerRepository.Add(dbParishner);
        }
    }
}
