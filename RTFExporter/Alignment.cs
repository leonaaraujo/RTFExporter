namespace RTFExporter
{
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
    Justified,
  }
}