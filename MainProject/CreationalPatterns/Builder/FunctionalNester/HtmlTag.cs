using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject.CreationalPatterns.Builder.FunctionalNester
{
    class HtmlTag
    {
        public string Name { get; set; }
        [AllowChildrenBinder(ThrownError = false)] 
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
}