namespace RTFExporter
{
  /// <summary>
  /// The RTF text class, any snippet of text of a paragraph, every text is appended to a paragraph
  /// </summary>
  public class RTFText
  {
    public RTFTextStyle style;
    public string content;

    /// <summary>
    /// The text constructor
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// </summary>
    /// <param name="paragraph">The parent paragraph</param>
    /// <param name="content">The text content itself</param>
    public RTFText(RTFParagraph paragraph, string content)
    {
      style = new RTFTextStyle(false, false, 12, "Calibri", new Color(0, 0, 0));
      this.content = content;
      paragraph.text.Add(this);
    }

    /// <summary>
    /// The text constructor
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// </summary>
    /// <param name="paragraph">The parent paragraph</param>
    /// <param name="content">The text content itself</param>
    /// <param name="style">A pre-configured style object</param>
    public RTFText(RTFParagraph paragraph, string content, RTFTextStyle style)
    {
      this.style = style;
      this.content = content;
      paragraph.text.Add(this);
    }

    /// <summary>
    /// Set a default style to the text (Calibri black 12pt)
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// </summary>
    /// <returns>The RTF text object after style setted</returns>
    public RTFText SetStyle()
    {
      style = new RTFTextStyle(false, false, false, false, false, false, 12, "Calibri", Color.black, Underline.None);
      return this;
    }

    /// <summary>
    /// Set the basic style of the text
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// <seealso cref="RTFExporter.Color"/>
    /// </summary>
    /// <param name="color">The text color</param>
    /// <param name="fontSize">The font size in pt, 12pt as default</param>
    /// <param name="fontFamily">A valid font family, will use Calibri if doesn't exist and as default</param>
    /// <returns>The RTF text object after style setted</returns>
    public RTFText SetStyle(Color color, int fontSize = 12, string fontFamily = "Calibri")
    {
      style = new RTFTextStyle(false, false, fontSize, fontFamily, color);
      return this;
    }

    /// <summary>
    /// Set the style of the text
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// <seealso cref="RTFExporter.Color"/>
    /// </summary>
    /// <param name="color">The text color</param>
    /// <param name="italic">If the text is italic, false as default</param>
    /// <param name="bold">If the text is italic, false as default</param>
    /// <param name="fontSize">The font size in pt, 12pt as default</param>
    /// <param name="fontFamily">A valid font family, will use Calibri if doesn't exist and as default</param>
    /// <returns>The RTF text object after style setted</returns>
    public RTFText SetStyle(Color color, bool italic = false, bool bold = false, int fontSize = 12, string fontFamily = "Calibri")
    {
      style = new RTFTextStyle(italic, bold, fontSize, fontFamily, color);
      return this;
    }

    /// <summary>
    /// Set the style of the text without color, font size and font family
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// <seealso cref="RTFExporter.Underline"/>
    /// </summary>
    /// <param name="italic">If the text is italic, false as default</param>
    /// <param name="bold">If the text is italic, false as default</param>
    /// <param name="underline">The underline type</param>
    /// <param name="smallCaps">Use all small caps?</param>
    /// <param name="strikeThrough">Use strike through?</param>
    /// <param name="allCaps">Use all caps?</param>
    /// <param name="outline">Has outline?</param>
    /// <returns></returns>
    public RTFText SetStyle(bool italic, bool bold, Underline underline = Underline.None,
      bool smallCaps = false, bool strikeThrough = false, bool allCaps = false, bool outline = false)
    {
      style = new RTFTextStyle(italic, bold, smallCaps, strikeThrough, allCaps, outline, 12, "Calibri", Color.black, underline);
      return this;
    }
  }
}
