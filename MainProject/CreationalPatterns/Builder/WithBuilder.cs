using System.Collections.Generic;
using System.Text;
using static System.Console;
namespace MainProject.CreationalPatterns.Builder
{
    class HtmlElement
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();
        public int IndentSize { get; set; }

        public HtmlElement(string name, string text, int indentSize = 2)
        {
            Name = name;
            Text = text;
            IndentSize = indentSize;
        }

        public HtmlElement()
        {
        }

        private string indentTo(int ind)
        {
            return new string(' ', ind);
        }

        private string ToStringImplementation(int indentLevel)
        {
            var sb = new StringBuilder();

            var indent = indentTo(indentLevel * IndentSize);
            
            // tag : open
            sb.AppendLine($"{indent}<{Name}>");
            // internal text
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(indentTo((indentLevel + 1) * IndentSize));
                sb.AppendLine(Text);
            }
            // children
            foreach (var child in Children)
            {
                sb.Append(child.ToStringImplementation(indentLevel + 1));
            }
            
            // tag : close
            sb.AppendLine($"{indent}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string _rootName;
        private HtmlElement _root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            _root.Name = rootName;
        }

        public void AddChild(string name, string text)
        {
            var e = new HtmlElement(name, text);
            _root.Children.Add(e);
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public void Clear()
        {
            _root = new HtmlElement {Name = _rootName};
        }
    }
    public class WithBuilder
    {
        public static void Run()
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Osama");
            builder.AddChild("li", "Nour");
            WriteLine(builder);
        }
    }
}