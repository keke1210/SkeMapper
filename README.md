## SkeMapper

[![NuGet](https://img.shields.io/nuget/v/SkeMapper.svg)](https://www.nuget.org/packages/SkeMapper/)
[![NuGetCount](https://img.shields.io/nuget/dt/SkeMapper.svg
)](https://www.nuget.org/packages/SkeMapper/)


SkeMapper is a simple object-to-object mapping library that can be used to map objects belonging to dissimilar types.

* Very simple mapping library that maps entities by properties in a case-insensitive way.
  * Easy to initialize
  * Easy to edit

* [.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)
  * .NET Core 3.1 & .NET Framework 4.8



## Set up
At first we need to declare the mappings of the library.
```csharp
var mapperSettings = new MapperSettings((x) =>
{
    x.CreateMap<PersonDtoCaseInsensitive, PersonDto>();
    x.CreateMap<Person, PersonDtoCaseInsensitive>();
});

// We can get an instance of the mapper like this
IMapper mapper = MapperBuilder.Instance.ApplySettings(mapperSettings).Build();
```
It can also be used with the Microsoft.DependencyInjection to set the class as a singleton. 
```csharp
// Startup.cs
using SkeMapper.Extensions;

public void ConfigureServices(IServiceCollection services)
{   
    // ...
    services.AddSkeMapper((x) =>
    {
        x.CreateMap<ContactDto, Contact>();
        x.CreateMap<PersonDto, Person>();
        x.CreateMap<PersonDtoCaseInsensitive, Person>();
        x.CreateMap<PhoneDto, Phone>();
    });
}
```
or add the mapper settings directly 
```csharp
services.AddSkeMapper(mapperSettings);
```
And then get the instance like :
```csharp
 var mapper = serviceProvider.GetService<IMapper>();
```
## Usage

Example: Mapping an object which is composed with other objects
```csharp
using SkeMapper.Builder;
using SkeMapper.Settings;

IMapper mapper = MapperBuilder.Instance.ApplySettings(new MapperSettings((x) =>
                                        {
                                            x.CreateMap<Contact, ContactDto>();
                                            x.CreateMap<Person, PersonDto>();
                                            x.CreateMap<Phone, PhoneDto>();
                                        }))
                                        .Build();

var contact = new Contact
              {
                  Person = new Person { FirstName = "John", LastName = "Doe", Addres = "US", Age = 22 },
                  Phone = new Phone { PhoneNumber = "0111111", Prefix = "+01" }
              };

var result = mapper.Map<ContactDto>(contact);
// result: new ContactDto 
//         { 
//              Person = new PersonDto { FirstName = "John", LastName = "Doe" },
//              Phone = new PhoneDto { PhoneNumber = "0111111", Prefix = "+01" }    
//         }

```

Example: Mapping through the list with LINQ and returning a collection with mapped objects.
```csharp
using System.Linq;
// ...
var peopleList = new List<Person>
                {
                    new Person { FirstName = "Skerdi", LastName = "Berberi" },
                    new Person { FirstName = "Altjen", LastName = "Berberi" },
                    new Person { FirstName = "John", LastName = "Doe" }
                };

var listResult = peopleList.Select(person => mapper.Map<PersonDto>(person)).ToList();
```
Example: Mapping against two models where ones property letters are the same but the ``Letter Case`` is different. 
```csharp
public class PersonDtoCaseInsensitive
{
    public string firstnAme { get; set; }
    public string lasTname { get; set; }
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}

IMapper mapper = MapperBuilder.Instance.ApplySettings(new MapperSettings((x) =>
                                        {
                                            x.CreateMap<PersonDtoCaseInsensitive, Person>();
                                        }))
                                        .Build();

var personDto = new PersonDtoCaseInsensitive { firstnAme = "John", lasTname = "Doe" };
var result = mapper.Map<Person>(personDto);
// result: new Person 
//         { 
//             FirstName = "John",
//             LastName = "Doe",
//             ...
//         }
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

Licensed under the [MIT](LICENSE) License.