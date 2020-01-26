using System;
using System.Collections.Generic;
using Autofac;
using MainProject.StructuralPatterns.Adapter.A6AdapterWithDependencyInjection;

namespace MainProject.StructuralPatterns.Adapter
{
  namespace A6AdapterWithDependencyInjection
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

      public Button(ICommand command)
      {
        _command = command;
      }

      public void Click()
      {
        _command.Execute();
      }
    }

    class Editor
    {
      private readonly IEnumerable<Button> _buttons;

      public Editor(IEnumerable<Button> buttons)
      {
        _buttons = buttons;
      }

      public void ClickAll()
      {
        foreach (var button in _buttons)
        {
          button.Click();
        }
      }
    }


  }

  public static class AdapterWithDiAutofac
  {
    public static void Run()
    {
      var builder = new ContainerBuilder();
      builder.RegisterType<OpenCommand>().As<ICommand>();
      builder.RegisterType<CloseCommand>().As<ICommand>();
      // builder.RegisterType<Button>();
      builder.RegisterAdapter<ICommand, Button>(command => new Button(command));
      builder.RegisterType<Editor>();

      using var container = builder.Build();
      var editor = container.Resolve<Editor>();
      editor.ClickAll();
    }
  }
}
