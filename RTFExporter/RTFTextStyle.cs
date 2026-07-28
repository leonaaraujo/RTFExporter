namespace RTFExporter
{
  /// <summary>
  /// Represents character-level styling options for text runs inside an RTF document.
  /// </summary>
  public class RTFTextStyle
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RTFTextStyle"/> class with basic font and color settings.
    /// </summary>
    /// <param name="italic">Set to <c>true</c> to enable italic styling.</param>
    /// <param name="bold">Set to <c>true</c> to enable bold styling.</param>
    /// <param name="fontSize">Font size in points (pt).</param>
    /// <param name="fontFamily">Font family name. Defaults to Calibri if unresolvable.</param>
    /// <param name="color">The RGB <see cref="Color"/> applied to the text.</param>
    /// <seealso cref="RTFExporter.Color"/>
    public RTFTextStyle(bool italic, bool bold, int fontSize, string fontFamily, Color color)
    {
      this.Italic = italic;
      this.Bold = bold;
      this.FontSize = fontSize;
      this.FontFamily = fontFamily;
      this.Color = color;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFTextStyle"/> class comprehensive with all character styling options.
    /// </summary>
    /// <param name="italic">Set to <c>true</c> to enable italic styling.</param>
    /// <param name="bold">Set to <c>true</c> to enable bold styling.</param>
    /// <param name="smallCaps">Set to <c>true</c> to enable small capitals formatting.</param>
    /// <param name="strikeThrough">Set to <c>true</c> to enable strikethrough formatting.</param>
    /// <param name="allCaps">Set to <c>true</c> to enable all capitals formatting.</param>
    /// <param name="outline">Set to <c>true</c> to enable outline formatting.</param>
    /// <param name="fontSize">Font size in points (pt).</param>
    /// <param name="fontFamily">Font family name.</param>
    /// <param name="color">The RGB <see cref="Color"/> applied to the text.</param>
    /// <param name="underline">The <see cref="Underline"/> formatting style.</param>
    /// <seealso cref="RTFExporter.Color"/>
    /// <seealso cref="RTFExporter.Underline"/>
    public RTFTextStyle(
      bool italic,
      bool bold,
      bool smallCaps,
      bool strikeThrough,
      bool allCaps,
      bool outline,
      int fontSize,
      string fontFamily,
      Color color,
      Underline underline)
    {
      this.Italic = italic;
      this.Bold = bold;
      this.SmallCaps = smallCaps;
      this.StrikeThrough = strikeThrough;
      this.AllCaps = allCaps;
      this.Outline = outline;
      this.FontSize = fontSize;
      this.FontFamily = fontFamily;
      this.Color = color;
      this.Underline = underline;
    }

    /// <summary>Gets or sets a value indicating whether the text is italicized (<c>\i</c>).</summary>
    public bool Italic { get; set; }

    /// <summary>Gets or sets a value indicating whether the text is bolded indicates whether (<c>\b</c>).</summary>
    public bool Bold { get; set; }

    /// <summary>Gets or sets a value indicating whether small caps formatting is enabled (<c>\scaps</c>).</summary>
    public bool SmallCaps { get; set; }

    /// <summary>Gets or sets a value indicating whether strikethrough formatting is enabled (<c>\strike</c>).</summary>
    public bool StrikeThrough { get; set; }

    /// <summary>Gets or sets a value indicating whether all capitals formatting is enabled (<c>\caps</c>).</summary>
    public bool AllCaps { get; set; }

    /// <summary>Gets or sets a value indicating whether outline formatting is enabled (<c>\outl</c>).</summary>
    public bool Outline { get; set; }

    /// <summary>Gets or sets the font size in typographical points (e.g., 12 for 12pt). Converted internally to half-points (<c>\fs</c>) during export.</summary>
    public int FontSize { get; set; }

    /// <summary>Gets or sets the name of the font family (e.g., "Calibri", "Arial", "Courier").</summary>
    public string FontFamily { get; set; }

    /// <summary>Gets or sets the foreground text color.</summary>
    public Color Color { get; set; }

    /// <summary>Gets or sets the underline formatting style applied to the text.</summary>
    public Underline Underline { get; set; }
  }
}
