using System;
using static System.Console;
namespace MainProject.CreationalPatterns.Prototype
{
    namespace P_1_WithICloneable
    {

        // using Just ICloneable is very bad because it not support generic
        interface IGenericCloneable<T> : ICloneable
        {
            new T Clone();
        }
        public class Person: IGenericCloneable<Person>
        {
            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public string[] Names { get; set; }
            public Address Address { get; set; }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }

            object ICloneable.Clone()
            {
                return Clone();
            }

            public Person Clone()
            {
                return new Person((string[])Names.Clone(), Address.Clone());
            }
        }
        public class Address : IGenericCloneable<Address>
        {
            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName;
                HouseNumber = houseNumber;
            }

            public string StreetName { get; set; }
            public int HouseNumber { get; set; }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }

            object ICloneable.Clone()
            {
                return Clone();
            }

            public Address Clone()
            {
                return new Address(StreetName, HouseNumber);
            }

        }
        public static class WithICloneable
        {
            public static void Run()
            {
                var osama = new Person(new []{"osama","sad"}, new Address("AlEyman", 16));
                var ahmed = osama.Clone();
                ahmed.Names[0] = "Ahmed";
                ahmed.Address.HouseNumber = 26;
                ahmed.Address.StreetName = "Altaqwa";
                WriteLine(osama);
                WriteLine(ahmed);
            }
        }
    }
}
