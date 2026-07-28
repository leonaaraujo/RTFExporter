namespace RTFExporter
{
  /// <summary>
  /// Represents paragraph-level styling configuration including alignment, indentation, and vertical spacing.
  /// </summary>
  public class RTFParagraphStyle
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraphStyle"/> class with a specified horizontal alignment.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode applied to the paragraph.</param>
    /// <seealso cref="RTFExporter.Alignment"/>
    public RTFParagraphStyle(Alignment alignment)
    {
      this.SpaceAfter = 100;
      this.Alignment = alignment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraphStyle"/> class with horizontal alignment and indentation settings.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode applied to the paragraph.</param>
    /// <param name="indent">The <see cref="Indent"/> structure specifying offsets.</param>
    /// <seealso cref="RTFExporter.Alignment"/>
    /// <seealso cref="RTFExporter.Indent"/>
    public RTFParagraphStyle(Alignment alignment, Indent indent)
    {
      this.SpaceAfter = 100;
      this.Alignment = alignment;
      this.Indent = indent;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraphStyle"/> class complete with alignment, indentation, and vertical spacing.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode applied to the paragraph.</param>
    /// <param name="indent">The <see cref="Indent"/> structure specifying offsets.</param>
    /// <param name="spaceBefore">Vertical space before the paragraph (in twips).</param>
    /// <param name="spaceAfter">Vertical space after the paragraph (in twips).</param>
    /// <seealso cref="RTFExporter.Alignment"/>
    /// <seealso cref="RTFExporter.Indent"/>
    public RTFParagraphStyle(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter)
    {
      this.Alignment = alignment;
      this.Indent = indent;
      this.SpaceBefore = spaceBefore;
      this.SpaceAfter = spaceAfter;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraphStyle"/> class tailored to the measurement units of a parent document.
    /// </summary>
    /// <param name="document">The parent <see cref="RTFDocument"/> providing measurement units for default indentation.</param>
    /// <seealso cref="RTFExporter.RTFDocument"/>
    public RTFParagraphStyle(RTFDocument document)
    {
      switch (document.Units)
      {
        case Units.Inch:
          this.Indent = new Indent(1, 0, 0);
          break;
        case Units.Millimeters:
          this.Indent = new Indent(25.4f, 0, 0);
          break;
        case Units.Centimeters:
          this.Indent = new Indent(2.54f, 0, 0);
          break;
      }
    }

    /// <summary>Gets or sets the horizontal indentation configuration for the paragraph.</summary>
    public Indent Indent { get; set; }

    /// <summary>Gets or sets the horizontal alignment mode.</summary>
    public Alignment Alignment { get; set; }

    /// <summary>Gets or sets vertical spacing before the paragraph in twips (<c>\sb</c>).</summary>
    public int SpaceBefore { get; set; }

    /// <summary>Gets or sets vertical spacing after the paragraph in twips (<c>\sa</c>). Defaults to 100.</summary>
    public int SpaceAfter { get; set; }
  }
}
