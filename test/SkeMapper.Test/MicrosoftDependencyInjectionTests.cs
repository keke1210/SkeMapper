using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SkeMapper.Extensions;
using SkeMapper.Test.TestModels;

namespace SkeMapper.Test
{
    public class MicrosoftDependencyInjectionTests
    {
        private ServiceProvider serviceProvider { get; set; }
        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            services.AddSkeMapper((x) =>
            {
                x.CreateMap<ContactDto, Contact>();
                x.CreateMap<PersonDto, Person>();
                x.CreateMap<PersonDtoCaseInsensitive, Person>();
                x.CreateMap<PhoneDto, Phone>();
            });

            serviceProvider = services.BuildServiceProvider();
        }
        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestMethod1()
        {
            var mapper = serviceProvider.GetService<IMapper>();

            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };
            var result = mapper.Map<Person>(personDto);

            Assert.AreEqual(personDto.FirstName, result.FirstName);
            Assert.AreEqual(personDto.LastName, result.LastName);
        }

        [Test]
        public void TestMethod2()
        {
            var mapper = serviceProvider.GetService<IMapper>();

            var personDto = new PersonDto { FirstName = "Skerdi", LastName = "Berberi" };
            var result = mapper.Map<Person>(personDto);

            Assert.AreEqual(personDto.FirstName, result.FirstName);
            Assert.AreEqual(personDto.LastName, result.LastName);
        }
    }
}
