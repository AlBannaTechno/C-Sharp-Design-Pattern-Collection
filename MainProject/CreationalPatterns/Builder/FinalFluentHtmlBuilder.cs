using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using static System.Console;
namespace MainProject.CreationalPatterns.Builder
{
    public class HtmlTag
    {
        public string Name { get; set; }
        public bool IsTag { get; set; }
        public List<HtmlTag> Children { get; set; } = new List<HtmlTag>();
        public int IndentSize { get; set; }

        private HtmlTag Parent { get; set; }
        public HtmlTag(string name, bool isTag = true, int indentSize = 2)
        {
            Name = name;
            IsTag = isTag;
            IndentSize = indentSize;
        }

        protected string indentTo(int ind)
        {
            return new string(' ', ind);
        }
        protected internal virtual string ToStringImplementation(int indentLevel)
        {
            var sb = new StringBuilder();
            var indent = indentTo(indentLevel * IndentSize);
            if (IsTag)
            {
                sb.AppendLine($"{indent}<{Name}>");
                foreach (var child in Children)
                {
                    sb.Append(child.ToStringImplementation(indentLevel + 1));
                }
                sb.AppendLine($"{indent}</{Name}>");
            }
            else
            {
                sb.AppendLine($"{indent}{Name}");
            }
            return sb.ToString();
        }
         
         public HtmlTag AppendChild(string name, bool isTag = true, int indentSize = 2)
         {
             var el = new HtmlTag(name, isTag, indentSize);
             if (IsTag)
             {
                 el.Parent = this;
                 Children.Add(el);
                 return this;
             }
             // if no parent and this is not a tag we must throw an error
             if (Parent == null)
             {
                 throw new Exception("You can't append to non tag with no parent");
             } 
             el = Parent.AppendChild(name, isTag, indentSize);
             return this;
         }

         public HtmlTag AppendChild(string name, out HtmlTag element,bool isTag = true, int indentSize = 2)
         {
             var el = new HtmlTag(name, isTag, indentSize);
             if (IsTag)
             {
                 el.Parent = this;
                 element = el;
                 Children.Add(el);
                 return this;
             }
             // if no parent and this is not a tag we must throw an error
             if (Parent == null)
             {
                 throw new Exception("You can't append to non tag with no parent");
             } 
             Parent.AppendChild(name, out element ,isTag, indentSize);
             return this;
         }
         public HtmlTag AppendText(string text, int indentSize = 2)
         {
             return AppendChild(text, false, indentSize);
         }
         public HtmlTag AppendText(string text,out HtmlTag element ,int indentSize = 2)
         {
             return AppendChild(text, out element,false, indentSize);
         }
         public HtmlTag ThenAppendChild(string name, int indentSize = 2)
         {
             if (IsTag)
             {
                 var el = new HtmlTag(name, true, indentSize) {Parent = this};
                 Children.Add(el);
                 return el;
             }
             if (Parent == null)
             {
                 throw new Exception("You can't append to non tag with no parent");
             } 
             return Parent.AppendChild(name, true, indentSize);
         }
         public HtmlTag ThenAppendChild(string name,out HtmlTag element , bool isTag = true, int indentSize = 2)
         {
             if (IsTag)
             {
                 element = new HtmlTag(name, isTag, indentSize) {Parent = this};
                 Children.Add(element);
                 return element;
             }
             if (Parent == null)
             {
                 throw new Exception("You can't append to non tag with no parent");
             } 
             Parent.AppendChild(name,out element ,isTag, indentSize);
             return element;
         }
         public HtmlTag ToParent(int upTo = 1)
         {
             if (upTo == 1)
             {
                 return Parent;
             }
             return Parent.ToParent(upTo -1);
         }
        public override string ToString()
        {
            return ToStringImplementation(0);
        }
    }

    public class ContHtmlTag : HtmlTag
    {
        public ContHtmlTag(string name, bool isTag = true, int indentSize = 2) : base(name, isTag, indentSize)
        {
        }

        protected internal override string ToStringImplementation(int indentLevel)
        {
            var sb = new StringBuilder();
            var indent = indentTo(indentLevel * IndentSize);
            if (IsTag)
            {
                sb.AppendLine($"{indent}[{Name}]");
                foreach (var child in Children)
                {
                    sb.Append(child.ToStringImplementation(indentLevel + 1));
                }
                sb.AppendLine($"{indent}[/{Name}]");
            }
            else
            {
                sb.AppendLine($"{indent}{Name}");
            }
            return sb.ToString();
        }
    }
    public class FinalFluentHtmlBuilder
    {
        public static void Run()
        {
            var root = new ContHtmlTag("root");
            root.AppendChild("nest-1") // root
                .AppendChild("nest-2") // root
                .ThenAppendChild("nest-3") // nest-3
                    .AppendChild("nest-3-1") // nest-3
                    .AppendChild("nest-3-2" , out var nest32) // nest-3
                    .ThenAppendChild("nest-3-3") // nest-3-3
                        .AppendChild("[TEXT-nest-3-3 [1]]",false) // nest-3-3
                        .AppendText("[TEXT-nest-3-3 [2]]") // nest-3-3
                        .ToParent() // -> nest-3
                    .ThenAppendChild("nest-3-4", out var nest34) // nest-3-4
                        .AppendChild("nest-3-4-1") // nest-3-4
                        .ToParent(2)  // nest-3-4 => [2 level ] => root
                .AppendChild("nest-4") // root
                .ThenAppendChild("nest-5") // nest-5
                    .AppendText("osama", out var text51);

            nest32.AppendText("From OutSide");
            nest34.AppendText("From Out Side Of [Then]");
            text51.Name = "Changed From Out Side";
            WriteLine(root);
            WriteLine(nest32);
            WriteLine(nest34);
            WriteLine(text51);
        }
    }
}