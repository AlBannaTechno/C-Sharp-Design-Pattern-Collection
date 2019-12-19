using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
namespace MainProject.CreationalPatterns.Builder
{
    /*
     * [The Best way to build this is to use fluent Api Paradigm] :{
     *      Element.AddNode("root","osama").WithNestedNode("r","nour")
     *
     *  <root>
     *      <nest1>
     *          nest-1[text:first:1]
     *          nest-1[text:first:2]
     *              <nest1-1>
     *                  nest-1-1[text:first:1]
     *                  <nest-1-1-1> Omar </nest-1-1-1>
     *                  nest-1-1[text:last:1]
     *              </nest1-1>
     *          nest-1[text:last:1]
     *          nest-1[text:last:2]
     *      </nest1>
     *      </nest2>
     *          nest-2[text:first:1]
     *          <nest2-1> Soso </nest2-1>
     *          nest-2[text:last:1]
     *      </nest2>
     *  </root>
     * }
     */
   // HtmlFluentNestedBuilder
   class HtmlFluentNestedBuilder
   {
       public string Name { get; set; }
       public string Text { get; set; }
       public List<HtmlFluentNestedBuilder> Children { get; set; } = new List<HtmlFluentNestedBuilder>();
       public int IndentSize { get; set; }

       public HtmlFluentNestedBuilder(string name = null, string text = null, int indentSize = 2)
       {
           Name = name;
           Text = text;
           IndentSize = indentSize;
       }

       public HtmlFluentNestedBuilder()
       {
       }

       public HtmlFluentNestedBuilder AppendElement(string name = null, string text = null, int indentSize = 2)
       {
           // will append element to current root
           var el = new HtmlFluentNestedBuilder(name, text, indentSize);
           Children.Add(el);
           return this;
       }

       public HtmlFluentNestedBuilder ThenAppendElement(string name = null, string text = null, int indentSize = 2)
       {
           
           var el = new HtmlFluentNestedBuilder(name, text, indentSize);
           Children.Last().Children.Add(el);
           return el;           
       }

       #region newNest

       
       public HtmlFluentNestedBuilder AppendToChildren(string name = null, string text = null, int indentSize = 2)
       {
           // will append element to current root
           var el = new HtmlFluentNestedBuilder(name, text, indentSize);
           Children.Add(el);
           return this;
       }
       public HtmlFluentNestedBuilder ThenAppend(string name, string text = null, int indentSize = 2)
       {
           var el = new HtmlFluentNestedBuilder(name, text, indentSize);
           Children.Last().Children.Add(el);
           return el;           
       }
       public HtmlFluentNestedBuilder AppendToParentChildren(string name = null, string text = null, int indentSize = 2)
       {
           
           var el = new HtmlFluentNestedBuilder(name, text, indentSize);
           Children.Last().Children.Add(el);
           return el;           
       }

       #endregion
   
       private string indentTo(int ind)
       {
           return new string(' ', ind);
       }

       private string ToStringImplementation(int indentLevel)
       {
           var sb = new StringBuilder();

           var indent = indentTo(indentLevel * IndentSize);
            
           // tag : open
           if (Name == null)
           {
               sb.AppendLine(Text);
           }
           else
           {
               sb.AppendLine($"{indent}<{Name}>");
           }
           // internal text
           if (Name != null && !string.IsNullOrWhiteSpace(Text))
           {
               sb.Append(indentTo((indentLevel + 1) * IndentSize));
               sb.AppendLine(Text);
           }
           // children
           foreach (var child in Children)
           {
               sb.Append(child.ToStringImplementation(indentLevel + 1));
           }

           if (Name != null)
           {
               // tag : close
               sb.AppendLine($"{indent}</{Name}>");
           }

           return sb.ToString();
       }

       public override string ToString()
       {
           return ToStringImplementation(0);
       }
   }
    public class WithFluentNestedBuilder
    {
        public static void Run()
        {
            var root = new HtmlFluentNestedBuilder("root");
            root
                .AppendElement("nest1","Osama")
                    .ThenAppendElement("nest1-2", "nour")
                    .AppendElement();
            WriteLine(root);
            
        }
    }
}