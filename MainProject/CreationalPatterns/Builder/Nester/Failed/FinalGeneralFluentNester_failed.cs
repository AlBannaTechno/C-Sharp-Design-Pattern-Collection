using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject.CreationalPatterns.Builder.Nester.Failed
{

    public class NesterBase<T>
    {
        public string Name { get; set; }
        public bool IsTag { get; set; }
        public T Data { get; set; }
        public List<NesterBase<T>> Children { get; set; } = new List<NesterBase<T>>();
        public int IndentSize { get; set; }
        public Func<int,NesterBase<T>, string> ToStringImplementation; 

        public NesterBase<T> Parent { get; set; }
        public NesterBase(string name, bool isTag = true, int indentSize = 2, Func<int, NesterBase<T>, string> toStringImplementation = null)
        {
            Name = name;
            IsTag = isTag;
            IndentSize = indentSize;
            if (toStringImplementation != null)
            {
                ToStringImplementation = toStringImplementation;
            }
            else
            {
                ToStringImplementation = ToStringImp;
            }
        }

        public string indentTo(int ind)
        {
            return new string(' ', ind);
        }
        // pass current element
        private string ToStringImp(int indentLevel, NesterBase<T> current)
        {
            var sb = new StringBuilder();
            var indent = indentTo(indentLevel * current.IndentSize);
            if (IsTag)
            {
                sb.AppendLine($"{indent}<{current.Name}>");
                foreach (var child in current.Children)
                {
                    sb.Append(child.ToStringImplementation(indentLevel + 1, current));
                }
                sb.AppendLine($"{indent}</{current.Name}>");
            }
            else
            {
                sb.AppendLine($"{indent}{current.Name}");
            }
            return sb.ToString();
        }
         
         public NesterBase<T> AppendChild(string name, bool isTag = true, int indentSize = 2)
         {
             var el = new NesterBase<T>(name, isTag, indentSize);
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

         public NesterBase<T> AppendChild(string name, out NesterBase<T> element,bool isTag = true, int indentSize = 2)
         {
             var el = new NesterBase<T>(name, isTag, indentSize);
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
         public NesterBase<T> AppendText(string text, int indentSize = 2)
         {
             return AppendChild(text, false, indentSize);
         }
         public NesterBase<T> AppendText(string text,out NesterBase<T> element ,int indentSize = 2)
         {
             return AppendChild(text, out element,false, indentSize);
         }
         public NesterBase<T> ThenAppendChild(string name, int indentSize = 2)
         {
             if (IsTag)
             {
                 var el = new NesterBase<T>(name, true, indentSize) {Parent = this};
                 Children.Add(el);
                 return el;
             }
             if (Parent == null)
             {
                 throw new Exception("You can't append to non tag with no parent");
             } 
             return Parent.AppendChild(name, true, indentSize);
         }
         public NesterBase<T> ThenAppendChild(string name,out NesterBase<T> element , bool isTag = true, int indentSize = 2)
         {
             if (IsTag)
             {
                 element = new NesterBase<T>(name, isTag, indentSize) {Parent = this};
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
         public NesterBase<T> ToParent(int upTo = 1)
         {
             if (upTo == 1)
             {
                 return Parent;
             }
             return Parent.ToParent(upTo -1);
         }
        public override string ToString()
        {
            return ToStringImplementation(0, this);
        }
    }

    public class ContNesterBase : NesterBase<string>
    {
        public ContNesterBase(string name, bool isTag = true, int indentSize = 2) : 
            base(name, isTag, indentSize, ToStringImplementation)
        {
        }


        private static string ToStringImplementation(int indentLevel , NesterBase<string> current)
        {
            var sb = new StringBuilder();
            var indent = current.indentTo(indentLevel * current.IndentSize);
            if (current.IsTag)
            {
                sb.AppendLine($"{indent}[{current.Name}] : [{current.Data}]");
                foreach (var child in current.Children)
                {
                    sb.Append(child.ToStringImplementation(indentLevel + 1, current));
                }
                sb.AppendLine($"{indent}[/{current.Name}]");
            }
            else
            {
                sb.AppendLine($"{indent}{current.Name}");
            }
            return sb.ToString();
        }
    }
    public static class FinalGeneralFluentNester
    {
        public static void Run()
        {
            var root = new ContNesterBase("root"){Data = "DATA HERE"};
            root.AppendChild("nest1")
                .AppendChild("nest2");
            
            Console.WriteLine(root);
        }
    }
}