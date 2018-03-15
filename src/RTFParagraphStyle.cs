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
    /// <param name="firstLine">a space in the first line</param>
    /// <param name="left">a space in the left of the block</param>
    /// <param name="right">a space in the right of the block</param>
    public Indent(float firstLine, float left, float right)
    {
      this.firstLine = firstLine;
      this.left = left;
      this.right = right;
    }
  }

  /// <summary>
  /// The alignment type
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

    public RTFParagraphStyle(Alignment alignment)
    {
      this.alignment = alignment;
    }

    public RTFParagraphStyle(Alignment alignment, Indent indent)
    {
      this.alignment = alignment;
      this.indent = indent;
    }

    public RTFParagraphStyle(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter)
    {
      this.alignment = alignment;
      this.indent = indent;
      this.spaceBefore = spaceBefore;
      this.spaceAfter = spaceAfter;
    }

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
