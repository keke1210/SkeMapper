using NUnit.Framework;
using SkeMapper.Builder;
using SkeMapper.Settings;
using SkeMapper.Test.TestModels;
using System.Collections.Generic;
using System.Linq;

namespace SkeMapper.Test
{
    public class MapFromDomainObjectsToDTOsTests
    {
        IMapper Mapper;
        [SetUp]
        public void BeforeEach()
        {

            Mapper = MapperBuilder.Instance.ApplySettings(new MapperSettings((config) =>
                                    {
                                        config.CreateMap<Contact, ContactDto>();
                                        config.CreateMap<Person, PersonDto>();
                                        config.CreateMap<Phone, PhoneDto>();
                                    }))
                                    .Build();
        }

        [TearDown]
        public void AfterEach()
        {
            Mapper = null;
        }

        [Test]
        public void MapFromDomainModelToDto()
        {
            var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
            var result = Mapper.Map<PersonDto>(person);

            Assert.AreEqual(person.FirstName, result.FirstName);
            Assert.AreEqual(person.LastName, result.LastName);
        }

        [Test]
        public void MapFromDomainModelToDtoWithComposition()
        {
            var contact = new Contact
            {
                Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
                Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
            };
            var result = Mapper.Map<ContactDto>(contact);

            Assert.AreEqual(contact.Person.FirstName, result.Person.FirstName);
            Assert.AreEqual(contact.Person.LastName, result.Person.LastName);
        }

        [Test]
        public void Map100Times()
        {
            var contact = new Contact 
            { 
                Person = new Person { FirstName = "Skerdi", LastName = "Berberi" },
                Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
            };

            var contactDtos = new List<ContactDto>();
            for (int i = 0; i < 100; i++)
                contactDtos.Add(Mapper.Map<ContactDto>(contact));

            foreach (var contactDto in contactDtos)
                Assert.AreEqual(contact.Person.FirstName, contactDto.Person.FirstName);
        }

        [Test]
        public void MapFromDtoCollectionToDomainModelCollection()
        {
            var personist = new List<Person>
             {
                 new Person { FirstName = "Skerdi", LastName = "Berberi" },
                 new Person { FirstName = "Altjen", LastName = "Berberi" },
                 new Person { FirstName = "Test123", LastName = "Test1234" },
             };

            var listResult = personist.Select(x => Mapper.Map<PersonDto>(x)).ToArray();

            Assert.AreEqual(listResult[0].FirstName, personist[0].FirstName);
            Assert.AreEqual(listResult[1].FirstName, personist[1].FirstName);
            Assert.AreEqual(listResult[2].FirstName, personist[2].FirstName);
        }
    }
}
