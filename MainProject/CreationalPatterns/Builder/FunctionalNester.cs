using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace MainProject.CreationalPatterns.Builder
{
    namespace FunctionalBuilderWorks
    {

        [AttributeUsage(AttributeTargets.Property)]
        public class AllowChildrenBinderAttribute : Attribute
        {
            
        }
        
         // may use IFunctionalNesterItem : to restrict T of FunctionalNester
         // but this will prevent us from using primitive types or struct .. 
        public interface IFunctionalNesterItem
        {
            public bool AllowChildren { get; set; }
        }
            public class FunctionalNester<T>
    {
        public readonly List<FunctionalNester<T>> Children = new List<FunctionalNester<T>>();
        public T Item { get; set; }
        public FunctionalNester<T> Parent { get; set; }
        public bool AllowChildren { get; set; } = true;
        public readonly Func<FunctionalNester<T>, int ,string> ToStringImplementation;

        public FunctionalNester(Func<FunctionalNester<T>, int ,string> toStringImplementation)
        {
            ToStringImplementation = toStringImplementation;
            
        }
        private (bool allowChildrentAttributeDefined, bool allowChildrenValue) IsAllowingChildren(T item)
        {
            (bool allowChildrentAttributeDefined, bool allowChildrenValue) result = (false ,false);
            // we will use reflection with attributes
            var allowChildrenField = typeof(T).GetProperties().FirstOrDefault(f => f.IsDefined(typeof(AllowChildrenBinderAttribute)));
            if (allowChildrenField != null)
            {
                result.allowChildrentAttributeDefined = true;
                var allowChildren = allowChildrenField.GetValue(item);
                result.allowChildrenValue = (bool) allowChildren;
            }
            return result;
        }
        private void CheckAddChildrenAvailability()
        {
            // we may allow to not throw an error : just print warning or do nothing
            if (!AllowChildren)
            {
                throw new Exception("You can not add any child to this element");
            }
        }
        
        private bool CheckAddChildrenAllowabilityOfThis(T item, bool allowChildren)
        {
            var acResult = IsAllowingChildren(item);
            // decision
            bool allowChildrenDecision;
            if (acResult.allowChildrentAttributeDefined)
            {
                allowChildrenDecision = acResult.allowChildrenValue;
            }
            else
            {
                allowChildrenDecision = allowChildren;
            }

            return allowChildrenDecision;
        }
        public FunctionalNester<T> AddChild(T child, bool allowChildren = true)
        {
            CheckAddChildrenAvailability();
            var allowChildrenDecision = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision
            };
            Children.Add(ch);
            return this;
        }
        public FunctionalNester<T> AddChild(T child, out FunctionalNester<T> element, bool allowChildren = true)
        {
            CheckAddChildrenAvailability();
            var allowChildrenDecision = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision
            };
            Children.Add(ch);
            element = ch;
            return this;
        }
        public FunctionalNester<T> WithChild(T child, bool allowChildren = true)
        {
            CheckAddChildrenAvailability();
            var allowChildrenDecision = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this , AllowChildren = allowChildrenDecision
            };
            Children.Add(ch);
            return ch;
        }
        public FunctionalNester<T> WithChild(T child, out FunctionalNester<T> element, bool allowChildren = true)
        {
            CheckAddChildrenAvailability();
            var allowChildrenDecision = CheckAddChildrenAllowabilityOfThis(child, allowChildren);
            var ch = new FunctionalNester<T>(ToStringImplementation)
            {
                Item = child, Parent = this, AllowChildren = allowChildrenDecision
            };
            Children.Add(ch);
            element = ch;
            return ch;
        }

        public FunctionalNester<T> ToParent(int upTo = 1)
        {
            if (upTo == 1)
            {
                return Parent;
            }

            return Parent.ToParent(upTo - 1);
        }

        public override string ToString()
        {
            if (ToStringImplementation == null)
            {
                throw new Exception("You must provide ToStringImplementation()");
            }
            return ToStringImplementation(this, 0);
        }
    }

    // TODO : we still has an idea : [How to pass check to the Nester Without using interfaces] 
    // So instead of  : [].WithChild(new HtmlTag("Osama Al Banna" , false), false)
    // we want to pass the first [false -> to HtmlTag] then nester should figure how 
    // to use this value to decided allow children or not
    // [Without using interfaces {this is very is with interfaces but after that we can't use struct ...}]
    // 
    class HtmlTag
    {
        public string Name { get; set; }
        [AllowChildrenBinder] 
        public bool IsTag { get; }
        private List<(string attributeName, string attibuteValue)> _attributes;

        public HtmlTag(string name , bool isTag = true)
        {
            Name = name;
            IsTag = isTag;
        }

        public static HtmlTag Build(string name , bool isTag = true)
        {
            return new HtmlTag(name, isTag);
        }

        #region Dont Do This

        // this idea will make every thing very complex
        /**
         * idea : we want to contain the nest in this build
         * but this will leads to trial to duplicate all nester functions ........ So [No]
         */
        internal class NesterContainer
        {
            public FunctionalNester<HtmlTag> Nester;

            public NesterContainer(FunctionalNester<HtmlTag> nester)
            {
                Nester = nester;
            }

            public NesterContainer Operate(HtmlTag tag, Action<FunctionalNester<HtmlTag>,HtmlTag> func)
            {
                if (tag.IsTag == false)
                {
                }
                func(Nester, tag);
                return this;
            }
        }

        #endregion
        
        public static NesterContainer Builder(FunctionalNester<HtmlTag> nester)
        {
            return new NesterContainer(nester);
        }
        

        private void MakeSuitableToAddAttribute()
        {
            if (!IsTag)
            {
                throw new Exception("You can't add attributes to non tag item");
            }
            if (_attributes == null)
            {
                _attributes = new List<(string attributeName, string attibuteValue)>();
            }
        }
        public HtmlTag WithAttribute(string name, string value = null)
        {
            MakeSuitableToAddAttribute();
            _attributes.Add((name,value));
            return this;
        }

        public HtmlTag WithAttributes(IEnumerable<(string attributeName, string attibuteValue)> attributes)
        {
            MakeSuitableToAddAttribute();
            _attributes.AddRange(attributes);
            return this;
        }

        public static string AttributesFormatter(IEnumerable<(string name, string value)> attributes)
        {
            var sb = new StringBuilder();
            foreach (var attribute in attributes)
            {
                sb.Append(" "+attribute.name);
                if (attribute.value != null)
                {
                    sb.AppendFormat("=\"{0}\"", attribute.value);
                }
            }

            return sb.ToString();
        }
        public static string ToStringFormatter(FunctionalNester<HtmlTag> nester, int i)
        {
            if (nester.Item.IsTag)
            {
                var sb = new StringBuilder();
                sb.Append(new string(' ', i * 2));
                var att = nester.Item._attributes == null ? "" : AttributesFormatter(nester.Item._attributes);
                sb.AppendLine($"<{nester.Item.Name}{att}>");

                foreach (var child in nester.Children)
                {
                    sb.Append(nester.ToStringImplementation(child, i + 1));
                }

                sb.Append(new string(' ', i * 2));
                sb.AppendLine($"</{nester.Item.Name}>");
                return sb.ToString();
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append(new string(' ', i * 2));
                sb.AppendLine($"{nester.Item.Name}");

                // theoretically we should prevent add children to no tag 
                foreach (var child in nester.Children)
                {
                    sb.Append(nester.ToStringImplementation(child, i + 1));
                }
                return sb.ToString();
            }
         
        }
        
    }
    public static class TestFunctionalTester
    {
        public static void Run()
        {
            TestAdvancedHtmlTagFormatter();
        }

        private static void TestAdvancedHtmlTagFormatter()
        {
            var root = new FunctionalNester<HtmlTag>(HtmlTag.ToStringFormatter)
                {Item = new HtmlTag("root")};
        


            root
            .WithChild(HtmlTag.Build("child-1")) // root
                .WithChild(new HtmlTag("Osama Al Banna" , false))
                    // .AddChild(new HtmlTag("Any")) // if we un Comment this : this will cause an exception 
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