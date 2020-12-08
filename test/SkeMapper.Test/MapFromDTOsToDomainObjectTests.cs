using NUnit.Framework;
using SkeMapper.Builder;
using SkeMapper.Settings;
using SkeMapper.Test.TestModels;
using System.Collections.Generic;

namespace SkeMapper.Test
{
    public class MapFromDTOsToDomainObjectTests
    {
        IMapper Mapper;
        [SetUp]
        public void BeforeEach()
        {
            var mapperSettings = new MapperSettings((config) =>
            {
                config.CreateMap<ContactDto, Contact>();
                config.CreateMap<PersonDto, Person>();
                config.CreateMap<PersonDtoCaseInsensitive, Person>();
                config.CreateMap<PhoneDto, Phone>();

                //config.CreateCollectionMap<List<PersonDto>, List<Person>>();
            });

            Mapper = MapperBuilder.Instance.ApplySettings(mapperSettings).Build();
        }

        [TearDown]
        public void AfterEach()
        {
            Mapper = null;
        }


        [Test]
        public void MapFromDtoToDomainModel()
        {
            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };
            var result = Mapper.Map<Person>(personDto);

            Assert.AreEqual(personDto.FirstName, result.FirstName);
            Assert.AreEqual(personDto.LastName, result.LastName);
        }

        [Test]
        public void MapFromDtoToDomainModelWithComposition()
        {
            // Expected result
            var contactDto = new ContactDto
            {
                Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
                Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
            };

            var result = Mapper.Map<Contact>(contactDto);

            Assert.AreEqual(contactDto.Person.FirstName, result.Person.FirstName);
            Assert.AreEqual(contactDto.Person.LastName, result.Person.LastName);
        }

        [Test]
        public void Map100Times()
        {
            var contactDto = new ContactDto
            {
                Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
                Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
            };

            var contacts = new List<Contact>();
            for (int i = 0; i < 100; i++)
                contacts.Add(Mapper.Map<Contact>(contactDto));

            foreach (var contact in contacts)
                Assert.AreEqual(contact.Person.FirstName, contactDto.Person.FirstName);
        }

        [Test]
        public void MapCaseInSensitiveTest()
        {
            // excpected result
            var personDto = new PersonDtoCaseInsensitive { firstnAme = "Skerdi", lasTname = "Berberi" };
            var result = Mapper.Map<Person>(personDto);

            Assert.AreEqual(personDto.firstnAme, result.FirstName);
            Assert.AreEqual(personDto.lasTname, result.LastName);
        }

        //[Test]
        //public void MapFromCollectionDtoToCollectionDomainModel()
        //{
        //    var personDtoList = new List<PersonDto> 
        //    {
        //        new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
        //        new PersonDto { FirstName = "Altjen", LastName = "Berberi" }
        //    };

        //    var result = Mapper.Map<List<Person>>(personDtoList);
        //}
    }
}
