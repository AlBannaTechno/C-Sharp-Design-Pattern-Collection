namespace MainProject.CreationalPatterns.Builder.FluentBuilderInheritanceWithRecursiveGenerics
{
    namespace Solution
    {
        public class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public class PersonalInfoBuilder
        {
            protected Person person = new Person();

            public PersonalInfoBuilder Called(string name)
            {
                person.Name = name;
                return this;
            }
        }

        public class PersonJobBuilder : PersonalInfoBuilder
        {
            public PersonJobBuilder WorksAs(string position)
            {
                person.Position = position;
                return this;
            }
        }
        public static class BeforeBuilderInheritance
        {
            public static void Run()
            {
                // var personJobBuilder = new PersonJobBuilder();
                // personJobBuilder.Called("Osama")
                //     .WorksAs("ss"); // we can not do this 
                // // we not allowed to use the containing type as a return type
                //
                // So We need to make PersonalInfoBuilder.Called Return PersonJobBuilder Not PersonalInfoBuilder :) 
            }
        }
    }
}