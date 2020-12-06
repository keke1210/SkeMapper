namespace SkeMapper.Test.TestModels
{
    public class Contact
    {
        public Person Person { get; set; }
        public Phone Phone { get; set; }
    }

    public class Phone
    {
        public string PhoneNumber { get; set; }
        public string Prefix { get; set; }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Addres { get; set; }
    }

    public class ContactDto
    {
        public PersonDto Person { get; set; }
        public PhoneDto Phone { get; set; }
    }

    public class PhoneDto
    {
        public string PhoneNumber { get; set; }
        public string Prefix { get; set; }
    }

    public class PersonDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public struct PersonStruct
    {

    }

    public class PersonDtoCaseInsensitive
    {
        public string firstnAme { get; set; }
        public string lasTname { get; set; }
    }
}
