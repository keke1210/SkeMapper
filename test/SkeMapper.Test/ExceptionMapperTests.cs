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
        public void TestManyMappers()
        {
            var mapperSettings = new MapperSettings((config) =>
            {
                config.CreateMap<Person, PersonDto>();
            });

            IMapper mapper1 = MapperBuilder.Instance.ApplySettings(mapperSettings).Build();

            IMapper mapper2 = MapperBuilder.Instance.ApplySettings(mapperSettings).Build();
        }


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
    }
}