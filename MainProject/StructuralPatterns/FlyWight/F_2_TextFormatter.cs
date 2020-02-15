using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject.StructuralPatterns.FlyWight
{
  namespace F2TextFormatter
  {
    public static class TextFormatting
    {
      public static void Run()
      {
        var text = new FormattedText("Osama Saad Kamal Al Banna");
        var saadFormatting = text.GetRange(6, 9);
        saadFormatting.Capitalize = true;

        Console.WriteLine(text);
      }
    }
    class FormattedText
    {
      private readonly string _plainText;
      private readonly List<TextFormatRange> _formatRanges = new List<TextFormatRange>();

      public FormattedText(string plainText)
      {
        _plainText = plainText;
      }

      public override string ToString()
      {
        var sb = new StringBuilder();
        for (int i = 0; i < _plainText.Length; i++)
        {
          var c = _plainText[i];
          foreach (var formatRange in _formatRanges)
          {
            if (formatRange.Covers(i))
            {
              // formatting
              if (formatRange.Capitalize)
              {
                c = char.ToUpper(c);
              }
            }
          }
          sb.Append(c);
        }

        return sb.ToString();
      }

      public TextFormatRange GetRange(int start, int end)
      {
        var range = new TextFormatRange{Start = start, End = end};
        _formatRanges.Add(range);
        return range;
      }

      public class TextFormatRange
      {
        public int Start, End;
        public bool Capitalize, Bold, Italic, Underline;

        public bool Covers(int position)
        {
          return position <= End && position >= Start;
        }
      }
    }
  }
}
