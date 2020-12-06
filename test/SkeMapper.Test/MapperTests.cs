using NUnit.Framework;
using SkeMapper.Test.TestModels;

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
        public void CreateMapsTest()
        {
            Mapper.CreateMap<Person, PersonDto>();
            Mapper.CreateMap<PersonDto, Person>();
            Assert.AreEqual(Mapper.Pairs[typeof(Person)], typeof(PersonDto));
            Assert.AreEqual(Mapper.Pairs[typeof(PersonDto)], typeof(Person));
        }

        [Test]
        public void GenericMapTest()
        {
            Mapper.CreateMap<Person, PersonDto>();
            Mapper.CreateMap<PersonDto, Person>();

            var person = new Person { FirstName = "Skerdi", LastName = "Berberi", Addres = "PG", Age = 22 };
            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };

            var result = Mapper.Map<PersonDto>(person);

            Assert.AreEqual(personDto.FirstName, result.FirstName);
            Assert.AreEqual(personDto.LastName, result.LastName);
        }
    }
}