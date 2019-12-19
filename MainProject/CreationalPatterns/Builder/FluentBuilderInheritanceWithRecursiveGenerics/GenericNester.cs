using System.Collections.Generic;

namespace MainProject.CreationalPatterns.Builder.FluentBuilderInheritanceWithRecursiveGenerics
{
    public abstract class GeneralNester
    {
        // public List<T> Children { get; set; }
    }

    public static class HtmlTagNesterStartPoint
    {
        public class Builder : HtmlTagNester<Builder>
        {
            
        }
        public static Builder New() => new Builder();
    }

    // First Direct Child [1]
    public class GeneralTagNester<TSelf> : GeneralNester
    where TSelf : GeneralTagNester<TSelf>
    {
        public List<TSelf> Children { get; set; } = new List<TSelf>();
    }

    // second level of direct child [2]
    public class HtmlTagNester<TSelf> : GeneralTagNester<HtmlTagNester<TSelf>>
        where TSelf : HtmlTagNester<TSelf>
    {
        
    }
    public class GenericNesterWorks
    {
        public static void Run()
        {
            var htmlBuilder = HtmlTagNesterStartPoint.New();
        }
    }
}