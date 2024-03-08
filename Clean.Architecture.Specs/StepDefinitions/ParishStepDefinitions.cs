using Clean.Architecture.Core.Model;
using Clean.Architecture.Core.Usecase;
using Clean.Architecture.Core.Usecase.Interface;
using Clean.Architecture.Core.Usecase.Interface.External;
using Clean.Architecture.Infrastructure.Database.InMemory;
using Clean.Architecture.Infrastructure.Database.InMemory.Context;
using System;
using TechTalk.SpecFlow;

namespace Clean.Architecture.Specs.StepDefinitions
{
    [Binding]
    public class ParishStepDefinitions
    {
        private readonly ICreateParishUsecase createParishUsecase;
        Parish parish;
        Parishner priest;
        Parish result;

        public ParishStepDefinitions()
        {
            ParishDBContext dbContext = new();
            IParishPersistence parishPersistence = new ParishPersistence(dbContext);
            createParishUsecase = new CreateParishUsecase(parishPersistence);
        }

        [Given(@"We have '([^']*)' as parishName, '([^']*)' as parishAddress")]
        public void GivenWeHaveAsParishNameAsParishAddress(string parishName, string parishAddress)
        {
            this.parish = new Parish(parishName, parishAddress);
        }

        [Given(@"We have '([^']*)' as priestName, '([^']*)' as priestDoB, '([^']*)' as priestAddress, '([^']*)' as priestPhone")]
        public void GivenWeHaveAsPriestNameAsPriestDoBAsPriestAddressAsPriestPhone(string priestName, string priestDoB, string priestAddress, 
            string priestPhone)
        {
            this.priest = new Parishner(priestName)
            {
                DateOfBirth = Convert.ToDateTime(priestDoB),
                Address = priestAddress,
                PhoneNumber = priestPhone
            };
        }

        [When(@"We Save these details")]
        public void WhenWeSaveTheseDetails()
        {
            result = this.createParishUsecase.CreateParish(this.parish, this.priest);
        }

        [Then(@"We should recieve a parishId")]
        public void ThenWeShouldRecieveAParishId()
        {
            result.Id.Should().NotBe(Guid.Empty);
        }
    }
}
