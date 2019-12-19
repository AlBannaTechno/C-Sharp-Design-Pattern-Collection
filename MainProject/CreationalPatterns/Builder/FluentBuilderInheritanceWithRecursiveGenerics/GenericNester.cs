using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace MainProject.CreationalPatterns.Builder.FluentBuilderInheritanceWithRecursiveGenerics
{
    namespace Final
    {
            public abstract class GeneralNester
            {
                // public List<T> Children { get; set; }
            }
        
            public static class HtmlTagNesterStartPoint
            {
                public class Builder : HtmlTagNester<Builder>
                {
                    public Builder(string name, bool isTag = true, int indentSize = 2) : base(name, isTag, indentSize)
                    {
                    }
                }
                public static Builder New(string name, bool isTag = true, int indentSize = 2) => 
                    new Builder(name, isTag, indentSize);
            }
        
            public static class BracketsHtmlTagNesterStartPoint
            {
                public class Builder : BracketsHtmlTagNester<Builder>
                {
                    public Builder(string name, bool isTag = true, int indentSize = 2) : base(name, isTag, indentSize)
                    {
                    }
                }
                public static Builder New(string name, bool isTag = true, int indentSize = 2) => 
                    new Builder(name, isTag, indentSize);
            }
            
            // First Direct Child [1]
            public class GeneralTagNester<TSelf> : GeneralNester
            where TSelf : GeneralTagNester<TSelf> 
            {
                public List<TSelf> Children { get; set; } = new List<TSelf>();
                public TSelf Parent { get; set; }

                public TSelf BuildNew()
                {
                    var c= this.MemberwiseClone() as TSelf;
                    return c ;
                }
            }
        
            // second level of direct child [2]
            public class HtmlTagNester<TSelf> : GeneralTagNester<HtmlTagNester<TSelf>>
                where TSelf : HtmlTagNester<TSelf>
            {
                public string Name { get; set; }
                public bool IsTag { get; set; }
                public int IndentSize { get; set; }
                
                public HtmlTagNester(string name, bool isTag = true, int indentSize = 2)
                {
                    Name = name;
                    IsTag = isTag;
                    IndentSize = indentSize;
                }
                protected string indentTo(int ind)
                {
                    return new string(' ', ind);
                }
                
                // ### TODO :: Need To Use This
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
                
                public TSelf AppendChild(string name, bool isTag = true, int indentSize = 2)
                {
                    var el = this.BuildNew();
                    el.Name = name;
                    el.IsTag = isTag;
                    el.IndentSize = indentSize;
                    el.Children.Clear();
                    
                    // var el = new HtmlTagNester<TSelf>(name, isTag, indentSize);
                    if (IsTag)
                    {
                        el.Parent = this;
                        Children.Add(el);
                        return (TSelf)this;
                    }
                    // if no parent and this is not a tag we must throw an error
                    if (Parent == null)
                    {
                        throw new Exception("You can't append to non tag with no parent");
                    } 
                    el = Parent.AppendChild(name, isTag, indentSize);
                    return (TSelf)this;
                }
                 
                // Stack over flow problem 
                public HtmlTagNester<TSelf> AppendChild(string name, out HtmlTagNester<TSelf> element,bool isTag = true, int indentSize = 2)
                {
                    var el = new HtmlTagNester<TSelf>(name, isTag, indentSize);
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
                
                public HtmlTagNester<TSelf> AppendText(string text, int indentSize = 2)
                {
                    return AppendChild(text, false, indentSize);
                }
                
                public HtmlTagNester<TSelf> AppendText(string text,out HtmlTagNester<TSelf> element ,int indentSize = 2)
                {
                    return AppendChild(text, out element,false, indentSize);
                }
                
                public HtmlTagNester<TSelf> ThenAppendChild(string name, int indentSize = 2)
                {
                    if (IsTag)
                    {
                        var el = new HtmlTagNester<TSelf>(name, true, indentSize) {Parent = this};
                        Children.Add(el);
                        return el;
                    }
                    if (Parent == null)
                    {
                        throw new Exception("You can't append to non tag with no parent");
                    } 
                    return Parent.AppendChild(name, true, indentSize);
                }
                
                public HtmlTagNester<TSelf> ThenAppendChild(string name,out HtmlTagNester<TSelf> element , bool isTag = true, int indentSize = 2)
                {
                    if (IsTag)
                    {
                        element = new HtmlTagNester<TSelf>(name, isTag, indentSize) {Parent = this};
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
                
                public HtmlTagNester<TSelf> ToParent(int upTo = 1)
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
        
            public class BracketsHtmlTagNester<TSelf> :
                HtmlTagNester<BracketsHtmlTagNester<TSelf>>
            where TSelf : BracketsHtmlTagNester<TSelf>
            {
                public BracketsHtmlTagNester(string name, bool isTag = true, int indentSize = 2) 
                    : base(name, isTag, indentSize)
                {
                }
        
                public (char open, char close) BracketsType = ('[',']');
                
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
                
                // may be we need to test this
                public override string ToString()
                {
                    return ToStringImplementation(0);
                }
            }
            public class GenericNesterWorks
            {
                public static void Run()
                {
                    // var htmlBuilder = HtmlTagNesterStartPoint.New("root : [Start Generic 01]");
        
                    var bracketsHtmlBuild = BracketsHtmlTagNesterStartPoint.New("Root [Brackets]");

                    var a = bracketsHtmlBuild.AppendChild("ss");
                    
                    //
                    // bracketsHtmlBuild.AppendChild("nest-1") // root
                    //     .AppendChild("nest-2") // root
                    //     .ThenAppendChild("nest-3") // nest-3
                    //         .AppendChild("nest-3-1") // nest-3
                    //         .AppendChild("nest-3-2" , out var nest32) // nest-3
                    //         .ThenAppendChild("nest-3-3") // nest-3-3
                    //             .AppendChild("[TEXT-nest-3-3 [1]]",false) // nest-3-3
                    //             .AppendText("[TEXT-nest-3-3 [2]]") // nest-3-3
                    //             .ToParent() // -> nest-3
                    //         .ThenAppendChild("nest-3-4", out var nest34) // nest-3-4
                    //             .AppendChild("nest-3-4-1") // nest-3-4
                    //             .ToParent(2)  // nest-3-4 => [2 level ] => root
                    //     .AppendChild("nest-4") // root
                    //     .ThenAppendChild("nest-5") // nest-5
                    //         .AppendText("osama", out var text51);
                    //
                    // nest32.AppendText("From OutSide");
                    // nest34.AppendText("From Out Side Of [Then]");
                    // text51.Name = "Changed From Out Side";
                    
                    // WriteLine(htmlBuilder);
                    WriteLine(bracketsHtmlBuild);
                    // WriteLine(nest32);
                    // WriteLine(nest32);
                    // WriteLine(nest34);
                    // WriteLine(text51);
        
                }
            }
    }
}