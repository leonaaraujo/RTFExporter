namespace RTFExporter
{
  using System.Collections.Generic;

  /// <summary>
  /// Represents a structural paragraph block inside an RTF document (<c>\par</c>).
  /// Contains a collection of text segments and paragraph-level styling.
  /// </summary>
  public class RTFParagraph
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RTFParagraph"/> class and automatically appends it to the specified document.
    /// </summary>
    /// <param name="document">The parent <see cref="RTFDocument"/> to append this paragraph to.</param>
    /// <seealso cref="RTFExporter.RTFDocument"/>
    public RTFParagraph(RTFDocument document)
    {
      this.Text = new List<RTFText>();
      this.Style = new RTFParagraphStyle(document);
      document.Paragraphs.Add(this);
    }

    /// <summary>Gets or sets the sequential list of text runs contained inside this paragraph.</summary>
    public List<RTFText> Text { get; set; }

    /// <summary>Gets or sets the paragraph-level styling configuration (alignment, indentation, spacing).</summary>
    public RTFParagraphStyle Style { get; set; }

    /// <summary>
    /// Appends a new unformatted text run to this paragraph.
    /// </summary>
    /// <param name="content">The raw string content to append.</param>
    /// <returns>The newly created <see cref="RTFText"/> instance appended to this paragraph.</returns>
    /// <seealso cref="RTFExporter.RTFText"/>
    public RTFText AppendText(string content)
    {
      RTFText text = new RTFText(this, content);
      return text;
    }

    /// <summary>
    /// Appends a new styled text run to this paragraph.
    /// </summary>
    /// <param name="content">The raw string content to append.</param>
    /// <param name="style">The <see cref="RTFTextStyle"/> to apply to the text.</param>
    /// <returns>The newly created <see cref="RTFText"/> instance appended to this paragraph.</returns>
    /// <seealso cref="RTFExporter.RTFText"/>
    /// <seealso cref="RTFExporter.RTFTextStyle"/>
    public RTFText AppendText(string content, RTFTextStyle style)
    {
      RTFText text = new RTFText(this, content, style);
      return text;
    }
  }
}
