namespace RTFExporter
{
  /// <summary>
  /// Represents horizontal indentation settings for a paragraph within an RTF document.
  /// </summary>
  public struct Indent
  {
    /// <summary>First line indentation offset in document units (<c>\fi</c>).</summary>
    public float FirstLine;

    /// <summary>Left block margin indentation in document units (<c>\li</c>).</summary>
    public float Left;

    /// <summary>Right block margin indentation in document units (<c>\ri</c>).</summary>
    public float Right;

    /// <summary>
    /// Initializes a new instance of the <see cref="Indent"/> struct with specified offsets.
    /// </summary>
    /// <param name="firstLine">The first-line indent offset in document measurement units.</param>
    /// <param name="left">The left block margin indent offset in document measurement units.</param>
    /// <param name="right">The right block margin indent offset in document measurement units.</param>
    public Indent(float firstLine, float left, float right)
    {
      this.FirstLine = firstLine;
      this.Left = left;
      this.Right = right;
    }
  }
}