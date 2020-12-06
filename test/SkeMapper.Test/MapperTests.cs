using NUnit.Framework;
using SkeMapper.Test.TestModels;
using System;

namespace SkeMapper.Test
{
    public class TestMapperBase
    {
        protected SkeMapper Mapper;

        [SetUp]
        public void BeforeEach()
        {
            Mapper = new SkeMapper();
        }

        [TearDown]
        public void AfterEach()
        {
            Mapper = null;
        }
    }


    public class MapperTests : TestMapperBase
    {
        [Test]
        public void MapFromTestModelToDto()
        {
            Mapper.CreateMap<Person, PersonDto>();

            var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };

            var result = Mapper.Map<PersonDto>(person);

            Assert.AreEqual(personDto.FirstName, result.FirstName);
            Assert.AreEqual(personDto.LastName, result.LastName);
        }

        [Test]
        public void MapFromDtoToTestModel()
        {
            Mapper.CreateMap<PersonDto, Person>();

            var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };

            var result = Mapper.Map<Person>(personDto);

            Assert.AreEqual(person.FirstName, result.FirstName);
            Assert.AreEqual(person.LastName, result.LastName);
        }

        [Test]
        public void MapFromTestModelToDtoWithComposition()
        {
            Mapper.CreateMap<Person, PersonDto>();
            Mapper.CreateMap<Phone, PhoneDto>();
            Mapper.CreateMap<Contact, ContactDto>();

            var contact = new Contact
            {
                Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
                Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
            };
            // Expected result
            var contactDto = new ContactDto
            {
                Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
                Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
            };

            var result = Mapper.Map<ContactDto>(contact);

            Assert.AreEqual(contactDto.Person.FirstName, result.Person.FirstName);
            Assert.AreEqual(contactDto.Person.LastName, result.Person.LastName);
        }

        [Test]
        public void MapFromDtoToTestModelWithComposition()
        {
            Mapper.CreateMap<PersonDto, Person>();
            Mapper.CreateMap<PhoneDto, Phone>();
            Mapper.CreateMap<ContactDto, Contact>();

            var contact = new Contact
            {
                Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
                Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
            };
            // Expected result
            var contactDto = new ContactDto
            {
                Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
                Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
            };

            var result = Mapper.Map<Contact>(contactDto);

            Assert.AreEqual(contact.Person.FirstName, result.Person.FirstName);
            Assert.AreEqual(contact.Person.LastName, result.Person.LastName);
        }

        [Test]
        public void MapUnRegisteredModels()
        {
            Mapper.CreateMap<Contact, Person>();
            var contact = new Contact
            {
                Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
                Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
            };

            Assert.That(() => Mapper.Map<ContactDto>(contact),
            Throws.TypeOf<Exception>().With.Message.EqualTo("There is no Mapper configured for this object."));
        }

        [Test]
        public void MapPrimitiveOrValueTypes()
        {
            Assert.That(() => Mapper.CreateMap<string, Person>(),
            Throws.TypeOf<Exception>().With.Message.EqualTo("Primitive Or value types could not be mapped!"));
        }
    }
}