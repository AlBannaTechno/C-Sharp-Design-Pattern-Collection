using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;

namespace MainProject.CreationalPatterns.Builder.FunctionalNester
{
    namespace FunctionalBuilderWorks
    {
    public static class TestFunctionalTester
    {
        public static void Run()
        {
            TestAdvancedHtmlTagFormatter();
        }

        // TODO :: Allow Add Child without create new instance of HtmlTag [we should use params {and reflection}]
        private static void TestAdvancedHtmlTagFormatter()
        {
            var root = new FunctionalNester<HtmlTag>(HtmlTag.ToStringFormatter)
                {Item = new HtmlTag("root")};
            root
            .WithChild(HtmlTag.Build("child-1")) // root
                .WithChild(new HtmlTag("Osama Al Banna" , false))
                    .AddChild(new HtmlTag("Any")) // if we un Comment this : this will cause an exception 
                .ToParent(2)
            .WithChild(new HtmlTag("child-2").WithAttribute("disabled")) // child-2
                .WithChild(new HtmlTag("child-2-1").WithAttribute("href" ,"google") , out var child21) // child-2-1 
                .ToParent(2) // -> root
            .AddChild(new HtmlTag("child-3")); // root
            child21.Item.Name = "ModifiedFromOutSide";
            child21.Item.WithAttributes(new []{("Att1","Val1"),("Att2","Val2")});
            Console.WriteLine(root);
            Console.WriteLine(child21);
        }

        public static void TestHtmlTagFormatter()
        {
            var ft = new FunctionalNester<string>(HtmlTagFormatter) {Item = "root"};

            ft
                .AddChild("child-1") // root
                .WithChild("child-2") // child-2
                    .WithChild("child-2-1" , out var child21) // child-2-1 
                    .ToParent(2) // -> root
                .AddChild("child-3"); // root

            child21.Item = "Modified From OutSide";
            Console.WriteLine(ft);
            Console.WriteLine(child21);
            
        }
        private static string HtmlTagFormatter(FunctionalNester<string> nester, int i)
        {
            var sb = new StringBuilder();
            sb.Append(new string(' ', i * 2));
            sb.AppendLine($"<{nester.Item}>");

            foreach (var child in nester.Children)
            {
                sb.Append(nester.ToStringImplementation(child, i + 1));
            }

            sb.Append(new string(' ', i * 2));
            sb.AppendLine($"</{nester.Item}>");
            return sb.ToString();
        }
    }

    }
}