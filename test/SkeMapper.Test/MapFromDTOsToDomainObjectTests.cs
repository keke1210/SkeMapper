﻿using NUnit.Framework;
using SkeMapper.Builder;
using SkeMapper.Settings;
using SkeMapper.Test.TestModels;
using System.Collections.Generic;

namespace SkeMapper.Test
{
    public class FromDTOsToDomainObjectBaseTest
    {
        protected IMapper Mapper;
        protected MapperSettings Settings;

        [SetUp]
        public void BeforeEach()
        {
            var mapperSettings = new MapperSettings((config) =>
            {
                config.CreateMap<ContactDto, Contact>();
                config.CreateMap<PersonDto, Person>();
                config.CreateMap<PersonDtoCaseInsensitive, Person>();
                config.CreateMap<PhoneDto, Phone>();
            });

            Mapper = MapperBuilder
                        .Instance
                            .ApplySettings(mapperSettings)
                            .Build();
        }

        [TearDown]
        public void AfterEach()
        {
            Mapper = null;
            Settings = null;
        }
    }


    public class MapFromDTOsToDomainObjectTests : FromDTOsToDomainObjectBaseTest
    {
        [Test]
        public void MapFromDomainModelToDto()
        {
            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };
            var result = Mapper.Map<Person>(personDto);

            Assert.AreEqual(personDto.FirstName, result.FirstName);
            Assert.AreEqual(personDto.LastName, result.LastName);
        }

        [Test]
        public void MapFromDomainModelToDtoWithComposition()
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
        public void MapCaseInSensitiveTest()
        {
            // excpected result
            var personDto = new PersonDtoCaseInsensitive { firstnAme = "Skerdi", lasTname = "Berberi" };
            var result = Mapper.Map<Person>(personDto);

            Assert.AreEqual(personDto.firstnAme, result.FirstName);
            Assert.AreEqual(personDto.lasTname, result.LastName);
        }
    }
}