﻿using System;

namespace MainProject.CreationalPatterns.Builder.FluentBuilderInheritanceWithRecursiveGenerics
{
    namespace Solution
    {
        public static class PersonBuilderStartPoint
        {
            // we must have a start point 
            public class Builder : PersonJobBuilder<Builder>
            {
                
            }
            public static Builder New() => new Builder();
        }
        public class Person
        {
            public string Name { get; set; }
            public string Position { get; set; }

           
            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }
        
        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }

        }

        // class Foo : Bar<Foo>
        // Next Means that derived class must inherit from PersonalInfoBuilder<TSelf>
        public class PersonalInfoBuilder<TSelf> : PersonBuilder
            where TSelf : PersonalInfoBuilder<TSelf>
        {
            public TSelf Called(string name)
            {
                person.Name = name;
                return (TSelf)this;
            }
        }

        /*
         * We can just do this : 
         * public class PersonJobBuilder : PersonalInfoBuilder<PersonJobBuilder>
         *
         * But this means we can't go deep and inherited from PersonJobBuilder
         * So if we need to do infinite inheritance we need to make every derived class generic to it self
         * Also notice : With every inheritance level the class definition will be longer and longer 
         */
        public class PersonJobBuilder<TSelf> : PersonalInfoBuilder<PersonJobBuilder<TSelf>>
            where TSelf : PersonJobBuilder<TSelf>
        {
            public TSelf WorksAs(string position)
            {
                person.Position = position;
                return (TSelf)this;
            }
        }
        public static class BeforeBuilderInheritance
        {
            public static void Run()
            {
                var osamaJobBuilder = PersonBuilderStartPoint.New().Called("Osama").WorksAs("Software Developer").Build();
                Console.WriteLine(osamaJobBuilder);
            }
        }
    }
}