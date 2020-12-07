using System;
using NUnit.Framework;
using SkeMapper.Test.TestModels;
using System.Collections.Generic;
using SkeMapper.Builder;

namespace SkeMapper.Test
{
    //public class TestMapperBase
    //{
    //    protected IMapper Mapper;
    //    protected MapperSettings Settings;

    //    [SetUp]
    //    public void BeforeEach()
    //    {
    //        var mapperSettings = new MapperSettings();

    //        Mapper = MapperBuilder.Instance
    //                              .ApplySettings(mapperSettings)
    //                              .Build();
    //    }

    //    [TearDown]
    //    public void AfterEach()
    //    {
    //        Mapper = null;
    //        Settings = null;
    //    }
    //}

    //public class MapperTests : TestMapperBase
    //{
    //    [Test]
    //    public void MapFromTestModelToDto()
    //    {
    //        Mapper.CreateMap<Person, PersonDto>();

    //        var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
    //        var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };

    //        var result = Mapper.Map<PersonDto>(person);

    //        Assert.AreEqual(personDto.FirstName, result.FirstName);
    //        Assert.AreEqual(personDto.LastName, result.LastName);
    //    }

    //    [Test]
    //    public void MapFromDtoToTestModel()
    //    {
    //        Mapper.CreateMap<PersonDto, Person>();

    //        var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
    //        var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };

    //        var result = Mapper.Map<Person>(personDto);

    //        Assert.AreEqual(person.FirstName, result.FirstName);
    //        Assert.AreEqual(person.LastName, result.LastName);
    //    }

    //    [Test]
    //    public void MapFromTestModelToDtoWithComposition()
    //    {
    //        Mapper.CreateMap<Person, PersonDto>();
    //        Mapper.CreateMap<Phone, PhoneDto>();
    //        Mapper.CreateMap<Contact, ContactDto>();

    //        var contact = new Contact
    //        {
    //            Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
    //            Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
    //        };
    //        // Expected result
    //        var contactDto = new ContactDto
    //        {
    //            Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
    //            Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
    //        };

    //        var result = Mapper.Map<ContactDto>(contact);

    //        Assert.AreEqual(contactDto.Person.FirstName, result.Person.FirstName);
    //        Assert.AreEqual(contactDto.Person.LastName, result.Person.LastName);
    //    }

    //    [Test]
    //    public void MapFromDtoToTestModelWithComposition()
    //    {
    //        Mapper.CreateMap<PersonDto, Person>();
    //        Mapper.CreateMap<PhoneDto, Phone>();
    //        Mapper.CreateMap<ContactDto, Contact>();

    //        // Expected result
    //        var contact = new Contact
    //        {
    //            Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
    //            Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
    //        };
    //        var contactDto = new ContactDto
    //        {
    //            Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
    //            Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
    //        };

    //        var result = Mapper.Map<Contact>(contactDto);

    //        Assert.AreEqual(contact.Person.FirstName, result.Person.FirstName);
    //        Assert.AreEqual(contact.Person.LastName, result.Person.LastName);
    //    }

    //    [Test]
    //    public void MapUnRegisteredModels()
    //    {
    //        Mapper.CreateMap<Contact, Person>();
    //        var contact = new Contact
    //        {
    //            Person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 },
    //            Phone = new Phone { PhoneNumber = "0111111", Prefix = "+355" }
    //        };

    //        Assert.That(() => Mapper.Map<ContactDto>(contact),
    //        Throws.TypeOf<Exception>().With.Message.EqualTo("There is no Mapper configured for this object."));
    //    }

    //    [Test]
    //    public void MapBuiltInCSharpTypes()
    //    {
    //        Assert.That(() => Mapper.CreateMap<string, Person>(),
    //        Throws.TypeOf<Exception>().With.Message.EqualTo("C# built-in types or value types can't be mapped!"));
    //    }

    //    [Test]
    //    public void Map100Times()
    //    {
    //        Mapper.CreateMap<PersonDto, Person>();
    //        Mapper.CreateMap<PhoneDto, Phone>();
    //        Mapper.CreateMap<ContactDto, Contact>();

    //        var contactDto = new ContactDto
    //        {
    //            Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
    //            Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
    //        };

    //        List<Contact> contacts = new List<Contact>();
    //        for (int i = 0; i < 100; i++)
    //            contacts.Add(Mapper.Map<Contact>(contactDto));

    //        foreach (var contact in contacts)
    //            Assert.AreEqual(contact.Person.FirstName, contactDto.Person.FirstName);
    //    }
      
    //    [Test]
    //    public void MapCaseInSensitiveTest()
    //    {
    //        Mapper.CreateMap<Person, PersonDtoCaseInsensitive>();

    //        var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
    //        // excpected result
    //        var personDto = new PersonDtoCaseInsensitive { firstnAme = "Skerdi", lasTname = "Berberi" };

    //        var result = Mapper.Map<PersonDtoCaseInsensitive>(person);

    //        Assert.AreEqual(personDto.firstnAme, result.firstnAme);
    //        Assert.AreEqual(personDto.lasTname, result.lasTname);
    //    }

    //    [Test]
    //    public void MapWithCollectionTypes()
    //    {
    //        Mapper.CreateMap<List<PersonDto>, List<Person>>();
    //        var person = new List<Person> { new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 } };
    //        var personDto = new List<PersonDto> { new PersonDto { FirstName = "Skerdi", LastName = "Berberi" } };
    //        var result = Mapper.Map<List<PersonDto>>(person);
    //    }
    //}
}