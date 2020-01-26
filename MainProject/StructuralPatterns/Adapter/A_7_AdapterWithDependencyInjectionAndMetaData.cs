using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Features.Metadata;
using MoreLinq.Extensions;

namespace MainProject.StructuralPatterns.Adapter
{
  namespace A6AdapterWithDependencyInjectionAndMetaData
  {
    interface ICommand
    {
      void Execute();
    }

    class OpenCommand : ICommand
    {
      public void Execute()
      {
        Console.WriteLine("Execute Open Command");
      }
    }

    class CloseCommand : ICommand
    {
      public void Execute()
      {
        Console.WriteLine("Execute Close Command");
      }
    }

    class Button
    {
      private readonly ICommand _command;
      private readonly string _name;

      public Button(ICommand command, string name)
      {
        _command = command;
        _name = name;
      }

      public void Click()
      {
        _command.Execute();
      }

      public void Print()
      {
        Console.WriteLine($"Button : {_name}");
      }
    }

    class Editor
    {
      public Editor(IEnumerable<Button> buttons)
      {
        Buttons = buttons;
      }

      public IEnumerable<Button> Buttons { get; }

      public void ClickAll()
      {
        foreach (var button in Buttons)
        {
          button.Click();
        }
      }
    }

    public static class AdapterWithDiAutofacMetaData
    {
      public static void Run()
      {
        var builder = new ContainerBuilder();
        builder.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("name", "Open");
        builder.RegisterType<CloseCommand>().As<ICommand>().WithMetadata("name", "Close");
        // builder.RegisterType<Button>();
        // builder.RegisterAdapter<ICommand, Button>(command => new Button(command));
        builder.RegisterAdapter<Meta<ICommand>, Button>(command =>
          new Button(command.Value, (string)command.Metadata["name"]));
        builder.RegisterType<Editor>();

        using var container = builder.Build();
        var editor = container.Resolve<Editor>();
        // editor.ClickAll();
        editor.Buttons.ForEach(but => but.Print());
      }
    }
  }
}
