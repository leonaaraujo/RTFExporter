namespace RTFExporter
{
  /// <summary>
  /// Represents horizontal indentation settings for a paragraph within an RTF document.
  /// </summary>
  public struct Indent
  {
    /// <summary>First line indentation offset in document units (<c>\fi</c>).</summary>
    public float firstLine;

    /// <summary>Left block margin indentation in document units (<c>\li</c>).</summary>
    public float left;

    /// <summary>Right block margin indentation in document units (<c>\ri</c>).</summary>
    public float right;

    /// <summary>
    /// Initializes a new instance of the <see cref="Indent"/> struct with specified offsets.
    /// </summary>
    /// <param name="firstLine">The first-line indent offset in document measurement units.</param>
    /// <param name="left">The left block margin indent offset in document measurement units.</param>
    /// <param name="right">The right block margin indent offset in document measurement units.</param>
    public Indent(float firstLine, float left, float right)
    {
      this.firstLine = firstLine;
      this.left = left;
      this.right = right;
    }
  }

  /// <summary>
  /// Specifies horizontal text alignment for paragraphs inside an RTF document.
  /// </summary>
  public enum Alignment
  {
    /// <summary>Left-aligned paragraph (<c>\ql</c>).</summary>
    Left,
    /// <summary>Right-aligned paragraph (<c>\qr</c>). </summary>
    Right,
    /// <summary>Center-aligned paragraph (<c>\qc</c>).</summary>
    Center,
    /// <summary>Fully justified paragraph (<c>\qj</c>).</summary>
    Justified
  }

  /// <summary>
  /// Represents paragraph-level styling configuration including alignment, indentation, and vertical spacing.
  /// </summary>
  public class RTFParagraphStyle
  {
    /// <summary>The horizontal indentation configuration for the paragraph.</summary>
    public Indent indent;

    /// <summary>The horizontal alignment mode.</summary>
    public Alignment alignment;

    /// <summary>Vertical spacing before the paragraph in twips (<c>\sb</c>).</summary>
    public int spaceBefore;

    /// <summary>Vertical spacing after the paragraph in twips (<c>\sa</c>). Defaults to 100.</summary>
    public int spaceAfter = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraphStyle"/> class with a specified horizontal alignment.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode applied to the paragraph.</param>
    /// <seealso cref="RTFExporter.Alignment"/>
    public RTFParagraphStyle(Alignment alignment)
    {
      this.alignment = alignment;
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
      this.alignment = alignment;
      this.indent = indent;
    }

    /// <summary>
    /// Initializes a complete instance of the <see cref="RTFParagraphStyle"/> class with alignment, indentation, and vertical spacing.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode applied to the paragraph.</param>
    /// <param name="indent">The <see cref="Indent"/> structure specifying offsets.</param>
    /// <param name="spaceBefore">Vertical space before the paragraph (in twips).</param>
    /// <param name="spaceAfter">Vertical space after the paragraph (in twips).</param>
    /// <seealso cref="RTFExporter.Alignment"/>
    /// <seealso cref="RTFExporter.Indent"/>
    public RTFParagraphStyle(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter)
    {
      this.alignment = alignment;
      this.indent = indent;
      this.spaceBefore = spaceBefore;
      this.spaceAfter = spaceAfter;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraphStyle"/> class tailored to the measurement units of a parent document.
    /// </summary>
    /// <param name="document">The parent <see cref="RTFDocument"/> providing measurement units for default indentation.</param>
    /// <seealso cref="RTFExporter.RTFDocument"/>
    public RTFParagraphStyle(RTFDocument document)
    {
      switch (document.units)
      {
        case Units.Inch:
          indent = new Indent(1, 0, 0);
          break;
        case Units.Millimeters:
          indent = new Indent(25.4f, 0, 0);
          break;
        case Units.Centimeters:
          indent = new Indent(2.54f, 0, 0);
          break;
      }
    }
  }
}
