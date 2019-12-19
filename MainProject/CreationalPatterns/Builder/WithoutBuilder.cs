using System.Text;
using static System.Console;
namespace MainProject.CreationalPatterns.Builder
{
    public class WithoutBuilder
    {
        public static void Run()
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);
            sb.Clear();
            var words = new[] {"first", "second"};
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            WriteLine(sb);
        }
    }
}