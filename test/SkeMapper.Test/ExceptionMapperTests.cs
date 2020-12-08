using NUnit.Framework;
using SkeMapper.Test.TestModels;
using SkeMapper.Builder;
using SkeMapper.Settings;
using SkeMapper.Exceptions;

namespace SkeMapper.Test
{
    public class ExceptionMapperTests
    {
        [Test]
        public void ConfigureTwoMappingsForTheSameType()
        {
            var mapperSettings = new MapperSettings((config) =>
            {
                config.CreateMap<Person, PersonDto>();
                config.CreateMap<Person, PersonDtoCaseInsensitive>();
            });

            Assert.That(() => MapperBuilder.Instance.ApplySettings(mapperSettings).Build(),
            Throws.TypeOf<DuplicateRegisteredTypeException>().With.Message
                  .EqualTo("Duplicate types could not be registered as source!"));
        }

        [Test]
        public void ConfigureAMapWithCSharpBuiltInType()
        {
            var mapperSettings = new MapperSettings((config) =>
            {
                config.CreateMap<string, PersonDto>();
            });

            Assert.That(() => MapperBuilder.Instance.ApplySettings(mapperSettings).Build(),
            Throws.TypeOf<RegisterBuiltInTypesException>().With.Message
                    .EqualTo("C# built-in types or value types can't be mapped!"));
        }

        [Test]
        public void MapWithItself()
        {
            var mapper = MapperBuilder.Instance.ApplySettings(new MapperSettings((x) =>
            {
                x.CreateMap<ContactDto, MixedModelContact>();
                x.CreateMap<PersonDto, PersonDto>();
                x.CreateMap<PhoneDto, Phone>();
            })) 
            .Build();

            var contactDto = new ContactDto
            {
                Person = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" },
                Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+355" }
            };

            var result = mapper.Map<MixedModelContact>(contactDto);

            Assert.AreEqual(contactDto.Person.FirstName, result.Person.FirstName);
            Assert.AreEqual(contactDto.Person.LastName, result.Person.LastName);
        }
    }
}