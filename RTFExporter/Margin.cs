namespace RTFExporter
{
  /// <summary>
  /// Represents document margins (left, right, top, bottom) in current document measurement units.
  /// </summary>
  public class Margin
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Margin"/> class with explicit values for all four sides.
    /// </summary>
    /// <param name="left">Left margin offset.</param>
    /// <param name="right">Right margin offset.</param>
    /// <param name="top">Top margin offset.</param>
    /// <param name="bottom">Bottom margin offset.</param>
    public Margin(float left, float right, float top, float bottom)
    {
      this.Left = left;
      this.Right = right;
      this.Top = top;
      this.Bottom = bottom;
    }

    /// <summary>Gets or sets left margin offset.</summary>
    public float Left { get; set; }

    /// <summary>Gets or sets right margin offset.</summary>
    public float Right { get; set; }

    /// <summary>Gets or sets top margin offset.</summary>
    public float Top { get; set; }

    /// <summary>Gets or sets bottom margin offset.</summary>
    public float Bottom { get; set; }
  }
}
