using static System.Console;
namespace MainProject.CreationalPatterns.Builder
{
    public class HtmlFluentBuilder
    {
        private readonly string _rootName;
        private HtmlElement _root = new HtmlElement();

        public HtmlFluentBuilder(string rootName)
        {
            _rootName = rootName;
            _root.Name = rootName;
        }

        public HtmlFluentBuilder AddChild(string name, string text)
        {
            var e = new HtmlElement(name, text);
            _root.Children.Add(e);
            return this;
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
    public class WithFluentBuilder
    {
        public static void Run()
        {
            var builder = new HtmlFluentBuilder("ul");
            builder.AddChild("li", "Osama").AddChild("li", "Nour");
            WriteLine(builder);
        }
    }
}