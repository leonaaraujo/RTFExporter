namespace RTFExporter
{
  /// <summary>
  /// A paragraph indent configuration
  /// </summary>
  public struct Indent
  {
    public float firstLine;
    public float left;
    public float right;

    /// <summary>
    /// The paragraph indent constructor
    /// </summary>
    /// <param name="firstLine">A space in the first line</param>
    /// <param name="left">A space in the left of the block</param>
    /// <param name="right">A space in the right of the block</param>
    public Indent(float firstLine, float left, float right)
    {
      this.firstLine = firstLine;
      this.left = left;
      this.right = right;
    }
  }

  /// <summary>
  /// The text alignment type
  /// </summary>
  public enum Alignment
  {
    Left,
    Right,
    Center,
    Justified
  }

  /// <summary>
  /// A group of paragraph styling configuration
  /// </summary>
  public class RTFParagraphStyle
  {

    public Indent indent;
    public Alignment alignment;
    public int spaceBefore;
    public int spaceAfter = 100;

    /// <summary>
    /// RTF paragraph style constructor with just alignment
    /// <seealso cref="RTFExporter.Alignment">
    /// </summary>
    /// <param name="alignment">Alignment object</param>
    public RTFParagraphStyle(Alignment alignment)
    {
      this.alignment = alignment;
    }

    /// <summary>
    /// RTF paragraph style constructor with just alignment and indent
    /// <seealso cref="RTFExporter.Alignment">
    /// <seealso cref="RTFExporter.Indent">
    /// </summary>
    /// <param name="alignment">Alignment object</param>
    /// <param name="indent">Indent object</param>
    public RTFParagraphStyle(Alignment alignment, Indent indent)
    {
      this.alignment = alignment;
      this.indent = indent;
    }

    /// <summary>
    /// RTF paragraph style constructor complete
    /// <seealso cref="RTFExporter.Alignment">
    /// <seealso cref="RTFExporter.Indent">
    /// </summary>
    /// <param name="alignment">Alignment object</param>
    /// <param name="indent">Indent object</param>
    /// <param name="spaceBefore">Space vertical before paragraph</param>
    /// <param name="spaceAfter">Space vertical after paragraph</param>
    public RTFParagraphStyle(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter)
    {
      this.alignment = alignment;
      this.indent = indent;
      this.spaceBefore = spaceBefore;
      this.spaceAfter = spaceAfter;
    }

    /// <summary>
    /// RTF paragraph style constructor with parent document
    /// <seealso cref="RTFExporter.RTFDocument">
    /// </summary>
    /// <param name="document">The RTF document to append the paragraph</param>
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
