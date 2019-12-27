using static System.Console;
namespace MainProject.CreationalPatterns.Prototype
{
    // Constructor Paradigm borrowed from C++
    namespace P_2_CopyConstructor
    {
         class Person
        {
            public Person(string[] names, Address address)
            {
                Names = names;
                Address = address;
            }

            public Person(Person person)
            {
                Names = (string[]) person.Names.Clone();
                Address = new Address(person.Address);
            }

            public string[] Names { get; set; }
            public Address Address { get; set; }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }
        }
        class Address
        {
            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName;
                HouseNumber = houseNumber;
            }

            public Address(Address address)
            {
                StreetName = address.StreetName;
                HouseNumber = address.HouseNumber;
            }

            public string StreetName { get; set; }
            public int HouseNumber { get; set; }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }

        public static class CopyConstructor
        {
            public static void Run()
            {
                var osama = new Person(new []{"Osama", "Al Banna"}, new Address("Old", 16));
                var ahmed = new Person(osama) {Names = {[0] = "Ahmed"}, Address = {HouseNumber = 25}};
                WriteLine(osama);
                WriteLine(ahmed);
            }
        }
    }
}
