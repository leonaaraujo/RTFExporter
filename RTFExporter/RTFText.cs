namespace RTFExporter
{
  /// <summary>
  /// Represents a contiguous run of formatted text within an <see cref="RTFParagraph"/>.
  /// Every <see cref="RTFText"/> instance holds its raw string content and character-level style.
  /// </summary>
  public class RTFText
  {
    /// <summary>The character-level styling options applied to this text segment.</summary>
    public RTFTextStyle style;

    /// <summary>The raw string content of this text segment.</summary>
    public string content;

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFText"/> class with default styling (12pt Calibri, Black) and appends it to the specified paragraph.
    /// </summary>
    /// <param name="paragraph">The parent <see cref="RTFParagraph"/> to append this text run to.</param>
    /// <param name="content">The raw string content.</param>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    public RTFText(RTFParagraph paragraph, string content)
    {
      style = new RTFTextStyle(false, false, 12, "Calibri", new Color(0, 0, 0));
      this.content = content;
      paragraph.text.Add(this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFText"/> class with a pre-configured style and appends it to the specified paragraph.
    /// </summary>
    /// <param name="paragraph">The parent <see cref="RTFParagraph"/> to append this text run to.</param>
    /// <param name="content">The raw string content.</param>
    /// <param name="style">A pre-configured <see cref="RTFTextStyle"/> object.</param>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    public RTFText(RTFParagraph paragraph, string content, RTFTextStyle style)
    {
      this.style = style;
      this.content = content;
      paragraph.text.Add(this);
    }

    /// <summary>
    /// Resets the styling of this text run to the default setting (12pt Calibri, Black, No underline).
    /// </summary>
    /// <returns>This <see cref="RTFText"/> instance after styling is updated, allowing method chaining.</returns>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    public RTFText SetStyle()
    {
      style = new RTFTextStyle(false, false, false, false, false, false, 12, "Calibri", Color.black, Underline.None);
      return this;
    }

    /// <summary>
    /// Sets the basic styling options (color, font size, and font family) for this text run.
    /// </summary>
    /// <param name="color">The text foreground <see cref="Color"/>.</param>
    /// <param name="fontSize">The font size in points (pt). Defaults to 12.</param>
    /// <param name="fontFamily">The font family name. Defaults to "Calibri".</param>
    /// <returns>This <see cref="RTFText"/> instance after styling is updated, allowing method chaining.</returns>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// <seealso cref="RTFExporter.Color"/>
    public RTFText SetStyle(Color color, int fontSize = 12, string fontFamily = "Calibri")
    {
      style = new RTFTextStyle(false, false, fontSize, fontFamily, color);
      return this;
    }

    /// <summary>
    /// Sets the color, italic/bold flags, font size, and font family for this text run.
    /// </summary>
    /// <param name="color">The text foreground <see cref="Color"/>.</param>
    /// <param name="italic">Set to <c>true</c> if the text should be italicized. Defaults to <c>false</c>.</param>
    /// <param name="bold">Set to <c>true</c> if the text should be bolded. Defaults to <c>false</c>.</param>
    /// <param name="fontSize">The font size in points (pt). Defaults to 12.</param>
    /// <param name="fontFamily">The font family name. Defaults to "Calibri".</param>
    /// <returns>This <see cref="RTFText"/> instance after styling is updated, allowing method chaining.</returns>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// <seealso cref="RTFExporter.Color"/>
    public RTFText SetStyle(Color color, bool italic = false, bool bold = false, int fontSize = 12, string fontFamily = "Calibri")
    {
      style = new RTFTextStyle(italic, bold, fontSize, fontFamily, color);
      return this;
    }

    /// <summary>
    /// Sets text decoration styles (italic, bold, underline, caps, strikethrough, outline) while preserving default font options.
    /// </summary>
    /// <param name="italic">Set to <c>true</c> if the text should be italicized.</param>
    /// <param name="bold">Set to <c>true</c> if the text should be bolded.</param>
    /// <param name="underline">The <see cref="Underline"/> type applied to the text. Defaults to <see cref="Underline.None"/>.</param>
    /// <param name="smallCaps">Set to <c>true</c> for small capitals formatting.</param>
    /// <param name="strikeThrough">Set to <c>true</c> for strikethrough formatting.</param>
    /// <param name="allCaps">Set to <c>true</c> for all capitals formatting.</param>
    /// <param name="outline">Set to <c>true</c> for outline formatting.</param>
    /// <returns>This <see cref="RTFText"/> instance after styling is updated, allowing method chaining.</returns>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    /// <seealso cref="RTFExporter.Underline"/>
    public RTFText SetStyle(bool italic, bool bold, Underline underline = Underline.None,
      bool smallCaps = false, bool strikeThrough = false, bool allCaps = false, bool outline = false)
    {
      style = new RTFTextStyle(italic, bold, smallCaps, strikeThrough, allCaps, outline, 12, "Calibri", Color.black, underline);
      return this;
    }
  }
}
