using System;
using System.Collections.Generic;

namespace MainProject.CreationalPatterns.Builder
{
    public class Person
    {
        public string Name, Position;

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
    public class PersonBuilder
    {
        private readonly List<Action<Person>> _actions = new List<Action<Person>>();

        public PersonBuilder ExtendWith(Action<Person> action)
        {
            _actions.Add(action);
            return this;
        }
        public PersonBuilder Called(string name)
        {
            _actions.Add(p => p.Name = name);
            return this;
        } 
        public Person Build()
        {
            var p = new Person();
            _actions.ForEach(a => a(p));
            return p;
        }
    }
    
    // extends 

    public static class PersonBuilderMyExtensions
    {
        public static PersonBuilder WorkAs(this PersonBuilder builder, string position)
        {
            builder.ExtendWith(p => p.Position = position);
            return builder;
        }
    }
    public static class WorkWithFunctionalBuilder
    {
        public static void Run()
        {
            var pb = new PersonBuilder();
            var osama =pb.Called("Osama").WorkAs("Software Developer").Build();
            Console.Write(osama);
        }
    }
}