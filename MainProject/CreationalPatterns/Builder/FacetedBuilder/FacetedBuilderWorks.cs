using System;

namespace MainProject.CreationalPatterns.Builder.FacetedBuilder
{
    namespace FBWorks
    {
        public class Person
        {
            // Address
            public string StreetAddress { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
        
            // Employment
            public string CompanyName { get; set; }
            public string Position { get; set; }
            public float AnnualIncome { get; set; }

            public override string ToString()
            {
                return $"{nameof(StreetAddress)}: {StreetAddress}," +
                       $" {nameof(PostalCode)}: {PostalCode}, {nameof(City)}: {City}," +
                       $" {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}:" +
                       $" {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
            }
        
        
        }
        
        public class PersonBuilder
        {
            // This is a reference : [This Paradigm will not work with Value types]
            // [we can make it public {DC}]
            protected Person Person = new Person();
            
            public PersonJobBuilder Works => new PersonJobBuilder(Person);
            public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);

            // this will return Person instead of person builder 
            // if write [Person personBuilder] Instead of [var personBuilder]
            public static implicit operator Person(PersonBuilder pb)
            {
                return pb.Person;
            }
        }

        public class PersonAddressBuilder : PersonBuilder
        {
            public PersonAddressBuilder(Person person)
            {
                Person = person;
            }

            public PersonAddressBuilder At(string streetAddress)
            {
                Person.StreetAddress = streetAddress;
                return this;
            }
            
            public PersonAddressBuilder WithPostalCode(string code)
            {
                Person.PostalCode = code;
                return this;
            }
            
            public PersonAddressBuilder In(string city)
            {
                Person.City = city;
                return this;
            }

        }
        public class PersonJobBuilder : PersonBuilder
        {
            public PersonJobBuilder(Person person )
            {
                Person = person;
            }

            public PersonJobBuilder At(string companyName)
            {
                Person.CompanyName = companyName;
                return this;
            }
            public PersonJobBuilder AsA(string position)
            {
                Person.Position = position;
                return this;
            }
            public PersonJobBuilder Earning(float amount)
            {
                Person.AnnualIncome = amount;
                return this;
            }
        }

        public class FacetedBuilderWorks
        {
            public static void Run()
            {
                // or just
                // Person person = new PersonBuilder()
                var person = (Person) (new PersonBuilder()
                    .Works
                        .At("ABT-TECH")
                        .AsA("Software Developer")
                        .Earning(1200.0f)
                    .Lives
                        .At("NS-R Street")
                        .In("Cairo")
                        .WithPostalCode("56685"));
                
                Console.WriteLine(person);
                
                
                
            
            }
        }

    }
}