namespace RTFExporter
{
  /// <summary>
  /// Specifies the style of underline applied to an RTF text run.
  /// </summary>
  public enum Underline
  {
    /// <summary>No underline (<c>\ul0</c>).</summary>
    None,
    /// <summary>Standard single line underline (<c>\ul</c>).</summary>
    Basic,
    /// <summary>Double line underline (<c>\uldb</c>).</summary>
    Double,
    /// <summary>Thick line underline (<c>\ulth</c>).</summary>
    Thick,
    /// <summary>Underline applied to words only, skipping spaces (<c>\ulw</c>).</summary>
    WordsOnly,
    /// <summary>Wavy underline (<c>\ulwave</c>).</summary>
    Wave,
    /// <summary>Dotted underline (<c>\uld</c>).</summary>
    Dotted,
    /// <summary>Dashed underline (<c>\uldash</c>).</summary>
    Dash,
    /// <summary>Dot-dash underline (<c>\uldashd</c>).</summary>
    DotDash
  }

  /// <summary>
  /// Represents character-level styling options for text runs inside an RTF document.
  /// </summary>
  public class RTFTextStyle
  {
    /// <summary>Indicates whether the text is italicized (<c>\i</c>).</summary>
    public bool italic;

    /// <summary>Indicates whether the text is bolded (<c>\b</c>).</summary>
    public bool bold;

    /// <summary>Indicates whether small caps formatting is enabled (<c>\scaps</c>).</summary>
    public bool smallCaps;

    /// <summary>Indicates whether strikethrough formatting is enabled (<c>\strike</c>).</summary>
    public bool strikeThrough;

    /// <summary>Indicates whether all capitals formatting is enabled (<c>\caps</c>).</summary>
    public bool allCaps;

    /// <summary>Indicates whether outline formatting is enabled (<c>\outl</c>).</summary>
    public bool outline;

    /// <summary>The font size in typographical points (e.g., 12 for 12pt). Converted internally to half-points (<c>\fs</c>) during export.</summary>
    public int fontSize;

    /// <summary>The name of the font family (e.g., "Calibri", "Arial", "Courier").</summary>
    public string fontFamily;

    /// <summary>The foreground text color.</summary>
    public Color color;

    /// <summary>The underline formatting style applied to the text.</summary>
    public Underline underline;

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
      this.italic = italic;
      this.bold = bold;
      this.fontSize = fontSize;
      this.fontFamily = fontFamily;
      this.color = color;
    }

    /// <summary>
    /// Initializes a comprehensive instance of the <see cref="RTFTextStyle"/> class with all character styling options.
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
    public RTFTextStyle(bool italic, bool bold, bool smallCaps, bool strikeThrough, bool allCaps,
      bool outline, int fontSize, string fontFamily, Color color, Underline underline)
    {
      this.italic = italic;
      this.bold = bold;
      this.smallCaps = smallCaps;
      this.strikeThrough = strikeThrough;
      this.allCaps = allCaps;
      this.outline = outline;
      this.fontSize = fontSize;
      this.fontFamily = fontFamily;
      this.color = color;
      this.underline = underline;
    }
  }
}
