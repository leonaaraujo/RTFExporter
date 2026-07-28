namespace RTFExporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  /// <summary>
  /// Provides static serialization methods to transform an in-memory <see cref="RTFDocument"/> into raw RTF text syntax or directly save to disk.
  /// </summary>
  public class RTFParser
  {
    private static Dictionary<string, int> fontsIndex = new Dictionary<string, int>();

    /// <summary>Gets the currently active document being parsed.</summary>
    public static RTFDocument Document { get; private set; }

    /// <summary>
    /// Serializes the specified <see cref="RTFDocument"/> and saves the generated RTF string directly to a file path.
    /// </summary>
    /// <param name="path">The target destination file path.</param>
    /// <param name="document">The <see cref="RTFDocument"/> instance to save.</param>
    /// <seealso cref="RTFExporter.RTFDocument"/>
    public static void ToFile(string path, RTFDocument document)
    {
      document.SetFile(path);
      document.Save();
      document.Close();
    }

    /// <summary>
    /// Writes raw string content directly to the specified file path.
    /// </summary>
    /// <param name="path">The target destination file path.</param>
    /// <param name="content">The raw text or RTF content string to write.</param>
    public static void ToFile(string path, string content)
    {
      using (FileStream fs = new FileStream(path, FileMode.Create))
      {
        using (StreamWriter writer = new StreamWriter(fs))
        {
          writer.Write(content);
        }
      }
    }

    /// <summary>
    /// Generates and returns the complete RTF-formatted syntax string for the specified <see cref="RTFDocument"/>.
    /// </summary>
    /// <param name="document">The <see cref="RTFDocument"/> object to format.</param>
    /// <returns>A string containing valid RTF 1.5/ANSI syntax ready for text processors.</returns>
    /// <seealso cref="RTFExporter.RTFDocument"/>
    public static string ToString(RTFDocument document)
    {
      RTFParser.Document = document;

      string str = "{\\rtf1\\ansi\\deff0";

      foreach (RTFParagraph paragraph in document.Paragraphs)
      {
        foreach (RTFText text in paragraph.Text)
        {
          document.Colors.Add(text.Style.Color);

          if (text.Style.FontFamily != string.Empty)
          {
            document.Fonts.Add(text.Style.FontFamily);
          }
        }
      }

      str += FontsParsing();
      str += ColorParsing();

      str += "{\\info {\\author " + document.Author + "}";
      DateTime date = DateTime.Now;
      str += "{\\creatim\\yr" + date.Year + "\\mo" + date.Month + "\\dy" + date.Day + "\\hr" + date.Hour + "\\min" + date.Minute + "}";
      str += "{\\version" + document.Version + "}";
      str += "{\\edmins0}";
      str += "{\\nofpages1}";
      str += "{\\nofwords0}";
      str += "{\\nofchars0}";
      str += "}";

      str += "{\\keywords ";

      foreach (string keyword in document.Keywords)
      {
        str += keyword + " ";
      }

      str += "}";

      switch (document.Orientation)
      {
        case Orientation.Landscape:
          str += "\\landscape";
          break;
        case Orientation.Portrait:
          str += "\\portrait";
          break;
      }

      str += "\\paperw" + Value(document.Width) + "\\paperh" + Value(document.Height) +
        "\\margl" + Value(document.Margin.Left) + "\\margr" + Value(document.Margin.Right) +
        "\\margt" + Value(document.Margin.Top) + "\\margb" + Value(document.Margin.Bottom) + " ";

      str += ParagraphParsing();

      str += "}";
      return str;
    }

    private static string FontsParsing()
    {
      List<string> fonts = new List<string>();

      foreach (string docFonts in Document.Fonts)
      {
        var add = true;

        foreach (string font in fonts)
        {
          if (font == docFonts)
          {
            add = false;
            break;
          }
        }

        if (add)
        {
          fonts.Add(docFonts);
        }
      }

      string str = "{\\fonttbl";

      for (int i = 0; i < fonts.Count; i++)
      {
        str += "{\\f" + i + " " + fonts[i] + ";}";
        try
        {
          fontsIndex.Add(fonts[i], i);
        }
        catch
        {
          // Font repeated
        }
      }

      str += "}";

      return str;
    }

    private static string ColorParsing()
    {
      List<Color> colors = new List<Color>();
      int j = 1;

      for (int i = 0; i < Document.Colors.Count; i++)
      {
        var add = true;

        foreach (Color color in colors)
        {
          if (color.R == Document.Colors[i].R && color.G == Document.Colors[i].G && color.B == Document.Colors[i].B)
          {
            add = false;
            break;
          }
        }

        if (add)
        {
          Document.Colors[i].Index = j;
          j++;

          colors.Add(Document.Colors[i]);
        }
      }

      string str = "{\\colortbl;";

      for (int i = 0; i < colors.Count; i++)
      {
        str += "\\red" + colors[i].R + "\\green" + colors[i].G + "\\blue" + colors[i].B + ";";
      }

      str += "}";

      return str;
    }

    private static string ParagraphParsing()
    {
      string str = string.Empty;

      foreach (RTFParagraph paragraph in Document.Paragraphs)
      {
        str += "\\pard";
        str += "\\sb" + paragraph.Style.SpaceBefore;
        str += "\\sa" + paragraph.Style.SpaceAfter;

        switch (paragraph.Style.Alignment)
        {
          case Alignment.Left:
            str += "\\ql";
            break;
          case Alignment.Right:
            str += "\\qr";
            break;
          case Alignment.Center:
            str += "\\qc";
            break;
          case Alignment.Justified:
            str += "\\qj";
            break;
        }

        str += "\\fi" + Value(paragraph.Style.Indent.FirstLine);
        str += "\\li" + Value(paragraph.Style.Indent.Left);
        str += "\\ri" + Value(paragraph.Style.Indent.Right);

        foreach (RTFText text in paragraph.Text)
        {
          str += "\\plain ";

          if (text.Style.Italic)
          {
            str += "\\i ";
          }

          if (text.Style.Bold)
          {
            str += "\\b ";
          }

          if (text.Style.SmallCaps)
          {
            str += "\\scaps ";
          }

          if (text.Style.AllCaps)
          {
            str += "\\caps ";
          }

          if (text.Style.StrikeThrough)
          {
            str += "\\strike ";
          }

          if (text.Style.Outline)
          {
            str += "\\outl ";
          }

          switch (text.Style.Underline)
          {
            case Underline.Dash:
              str += "\\uldash ";
              break;
            case Underline.DotDash:
              str += "\\uldashd ";
              break;
            case Underline.Dotted:
              str += "\\uld ";
              break;
            case Underline.Double:
              str += "\\uldb ";
              break;
            case Underline.Thick:
              str += "\\ulth ";
              break;
            case Underline.Basic:
              str += "\\ul ";
              break;
            case Underline.Wave:
              str += "\\ulwave ";
              break;
            case Underline.WordsOnly:
              str += "\\ulw ";
              break;
          }

          str += "\\fs" + (2 * text.Style.FontSize) + " ";
          str += "\\f" + fontsIndex[text.Style.FontFamily] + " ";
          str += "\\cf" + text.Style.Color.Index + " ";

          text.Content = text.Content.Replace("\n", "\\line ");
          text.Content = text.Content.Replace("\t", "\\tab ");
          text.Content = text.Content.Replace("<i>", "\\i ");
          text.Content = text.Content.Replace("</i>", "\\i0 ");
          text.Content = text.Content.Replace("<b>", "\\b ");
          text.Content = text.Content.Replace("</b>", "\\b0 ");
          text.Content = text.Content.Replace("€", "\\'80");
          text.Content = text.Content.Replace("‚", "\\'82");
          text.Content = text.Content.Replace("ƒ", "\\'83");
          text.Content = text.Content.Replace("„", "\\'84");
          text.Content = text.Content.Replace("…", "\\'85");
          text.Content = text.Content.Replace("†", "\\'86");
          text.Content = text.Content.Replace("‡", "\\'87");
          text.Content = text.Content.Replace("ˆ", "\\'88");
          text.Content = text.Content.Replace("‰", "\\'89");
          text.Content = text.Content.Replace("Š", "\\'8A");
          text.Content = text.Content.Replace("‹", "\\'8B");
          text.Content = text.Content.Replace("Œ", "\\'8C");
          text.Content = text.Content.Replace("Ž", "\\'8E");
          text.Content = text.Content.Replace("‘", "\\'91");
          text.Content = text.Content.Replace("’", "\\'92");
          text.Content = text.Content.Replace("“", "\\'93");
          text.Content = text.Content.Replace("”", "\\'94");
          text.Content = text.Content.Replace("•", "\\'95");
          text.Content = text.Content.Replace("–", "\\'96");
          text.Content = text.Content.Replace("—", "\\'97");
          text.Content = text.Content.Replace("˜", "\\'98");
          text.Content = text.Content.Replace("™", "\\'99");
          text.Content = text.Content.Replace("š", "\\'9A");
          text.Content = text.Content.Replace("›", "\\'9B");
          text.Content = text.Content.Replace("œ", "\\'9C");
          text.Content = text.Content.Replace("ž", "\\'9E");
          text.Content = text.Content.Replace("Ÿ", "\\'9F");
          text.Content = text.Content.Replace("¡", "\\'A1");
          text.Content = text.Content.Replace("¢", "\\'A2");
          text.Content = text.Content.Replace("£", "\\'A3");
          text.Content = text.Content.Replace("¤", "\\'A4");
          text.Content = text.Content.Replace("¥", "\\'A5");
          text.Content = text.Content.Replace("¦", "\\'A6");
          text.Content = text.Content.Replace("§", "\\'A7");
          text.Content = text.Content.Replace("¨", "\\'A8");
          text.Content = text.Content.Replace("©", "\\'A9");
          text.Content = text.Content.Replace("ª", "\\'AA");
          text.Content = text.Content.Replace("«", "\\'AB");
          text.Content = text.Content.Replace("¬", "\\'AC");
          text.Content = text.Content.Replace("®", "\\'AE");
          text.Content = text.Content.Replace("¯", "\\'AF");
          text.Content = text.Content.Replace("°", "\\'B0");
          text.Content = text.Content.Replace("±", "\\'B1");
          text.Content = text.Content.Replace("²", "\\'B2");
          text.Content = text.Content.Replace("³", "\\'B3");
          text.Content = text.Content.Replace("´", "\\'B4");
          text.Content = text.Content.Replace("µ", "\\'B5");
          text.Content = text.Content.Replace("¶", "\\'B6");
          text.Content = text.Content.Replace("·", "\\'B7");
          text.Content = text.Content.Replace("¸", "\\'B8");
          text.Content = text.Content.Replace("¹", "\\'B9");
          text.Content = text.Content.Replace("º", "\\'BA");
          text.Content = text.Content.Replace("»", "\\'BB");
          text.Content = text.Content.Replace("¼", "\\'BC");
          text.Content = text.Content.Replace("½", "\\'BD");
          text.Content = text.Content.Replace("¾", "\\'BE");
          text.Content = text.Content.Replace("¿", "\\'BF");
          text.Content = text.Content.Replace("À", "\\'C0");
          text.Content = text.Content.Replace("Á", "\\'C1");
          text.Content = text.Content.Replace("Â", "\\'C2");
          text.Content = text.Content.Replace("Ã", "\\'C3");
          text.Content = text.Content.Replace("Ä", "\\'C4");
          text.Content = text.Content.Replace("Å", "\\'C5");
          text.Content = text.Content.Replace("Æ", "\\'C6");
          text.Content = text.Content.Replace("Ç", "\\'C7");
          text.Content = text.Content.Replace("È", "\\'C8");
          text.Content = text.Content.Replace("É", "\\'C9");
          text.Content = text.Content.Replace("Ê", "\\'CA");
          text.Content = text.Content.Replace("Ë", "\\'CB");
          text.Content = text.Content.Replace("Ì", "\\'CC");
          text.Content = text.Content.Replace("Í", "\\'CD");
          text.Content = text.Content.Replace("Î", "\\'CE");
          text.Content = text.Content.Replace("Ï", "\\'CF");
          text.Content = text.Content.Replace("Ð", "\\'D0");
          text.Content = text.Content.Replace("Ñ", "\\'D1");
          text.Content = text.Content.Replace("Ò", "\\'D2");
          text.Content = text.Content.Replace("Ó", "\\'D3");
          text.Content = text.Content.Replace("Ô", "\\'D4");
          text.Content = text.Content.Replace("Õ", "\\'D5");
          text.Content = text.Content.Replace("Ö", "\\'D6");
          text.Content = text.Content.Replace("×", "\\'D7");
          text.Content = text.Content.Replace("Ø", "\\'D8");
          text.Content = text.Content.Replace("Ù", "\\'D9");
          text.Content = text.Content.Replace("Ú", "\\'DA");
          text.Content = text.Content.Replace("Û", "\\'DB");
          text.Content = text.Content.Replace("Ü", "\\'DC");
          text.Content = text.Content.Replace("Ý", "\\'DD");
          text.Content = text.Content.Replace("Þ", "\\'DE");
          text.Content = text.Content.Replace("ß", "\\'DF");
          text.Content = text.Content.Replace("à", "\\'E0");
          text.Content = text.Content.Replace("á", "\\'E1");
          text.Content = text.Content.Replace("â", "\\'E2");
          text.Content = text.Content.Replace("ã", "\\'E3");
          text.Content = text.Content.Replace("ä", "\\'E4");
          text.Content = text.Content.Replace("å", "\\'E5");
          text.Content = text.Content.Replace("æ", "\\'E6");
          text.Content = text.Content.Replace("ç", "\\'E7");
          text.Content = text.Content.Replace("è", "\\'E8");
          text.Content = text.Content.Replace("é", "\\'E9");
          text.Content = text.Content.Replace("ê", "\\'EA");
          text.Content = text.Content.Replace("ë", "\\'EB");
          text.Content = text.Content.Replace("ì", "\\'EC");
          text.Content = text.Content.Replace("í", "\\'ED");
          text.Content = text.Content.Replace("î", "\\'EE");
          text.Content = text.Content.Replace("ï", "\\'EF");
          text.Content = text.Content.Replace("ð", "\\'F0");
          text.Content = text.Content.Replace("ñ", "\\'F1");
          text.Content = text.Content.Replace("ò", "\\'F2");
          text.Content = text.Content.Replace("ó", "\\'F3");
          text.Content = text.Content.Replace("ô", "\\'F4");
          text.Content = text.Content.Replace("õ", "\\'F5");
          text.Content = text.Content.Replace("ö", "\\'F6");
          text.Content = text.Content.Replace("÷", "\\'F7");
          text.Content = text.Content.Replace("ø", "\\'F8");
          text.Content = text.Content.Replace("ù", "\\'F9");
          text.Content = text.Content.Replace("ú", "\\'FA");
          text.Content = text.Content.Replace("û", "\\'FB");
          text.Content = text.Content.Replace("ü", "\\'FC");
          text.Content = text.Content.Replace("ý", "\\'FD");
          text.Content = text.Content.Replace("þ", "\\'FE");
          text.Content = text.Content.Replace("ÿ", "\\'FF");

          str += text.Content;
        }

        str += "\\par ";
      }

      return str;
    }

    private static int Value(float i)
    {
      float result = 0;

      switch (Document.Units)
      {
        case Units.Inch:
          result = i * 1440;
          break;
        case Units.Millimeters:
          result = (i / 25.4f) * 1440;
          break;
        case Units.Centimeters:
          result = (i / 2.54f) * 1440;
          break;
      }

      return (int)Math.Ceiling(result);
    }
  }
}
