namespace RTFExporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;

  /// <summary>
  /// Represents a Rich Text Format (RTF) document. Acts as the root container for paragraphs, color tables, font tables, and document metadata.
  /// </summary>
  /// <remarks>
  /// Implements <see cref="IDisposable"/> to facilitate automatic saving and stream cleanup when used inside a <c>using</c> block.
  /// </remarks>
  public class RTFDocument : IDisposable
  {
    private FileStream fileStream;
    private StreamWriter streamWriter;

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class in-memory with standard 8x11 inch portrait settings.
    /// </summary>
    public RTFDocument()
    {
      this.Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class bound to a output file path.
    /// </summary>
    /// <param name="path">The destination file path where the RTF file will be saved.</param>
    public RTFDocument(string path)
    {
      this.SetFile(path);
      this.Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class bound to an existing <see cref="FileStream"/>.
    /// </summary>
    /// <param name="fileStream">An open file stream with write access.</param>
    public RTFDocument(FileStream fileStream)
    {
      this.SetStream(fileStream);
      this.Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class bound to a file path with customized page parameters.
    /// </summary>
    /// <param name="path">The destination file path.</param>
    /// <param name="width">Page width in specified units. Defaults to 8.</param>
    /// <param name="height">Page height in specified units. Defaults to 11.</param>
    /// <param name="orientation">Page orientation. Defaults to <see cref="Orientation.Portrait"/>.</param>
    /// <param name="units">Measurement units. Defaults to <see cref="Units.Inch"/>.</param>
    /// <seealso cref="RTFExporter.Orientation"/>
    /// <seealso cref="RTFExporter.Units"/>
    public RTFDocument(string path, float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      this.SetFile(path);
      this.Init(width, height, orientation, units);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class bound to a file stream with customized page parameters.
    /// </summary>
    /// <param name="fileStream">An open file stream with write access.</param>
    /// <param name="width">Page width in specified units. Defaults to 8.</param>
    /// <param name="height">Page height in specified units. Defaults to 11.</param>
    /// <param name="orientation">Page orientation. Defaults to <see cref="Orientation.Portrait"/>.</param>
    /// <param name="units">Measurement units. Defaults to <see cref="Units.Inch"/>.</param>
    /// <seealso cref="RTFExporter.Orientation"/>
    /// <seealso cref="RTFExporter.Units"/>
    public RTFDocument(FileStream fileStream, float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      this.SetStream(fileStream);
      this.Init(width, height, orientation, units);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class in-memory with customized page parameters.
    /// </summary>
    /// <param name="width">Page width in specified units. Defaults to 8.</param>
    /// <param name="height">Page height in specified units. Defaults to 11.</param>
    /// <param name="orientation">Page orientation. Defaults to <see cref="Orientation.Portrait"/>.</param>
    /// <param name="units">Measurement units. Defaults to <see cref="Units.Inch"/>.</param>
    /// <seealso cref="RTFExporter.Orientation"/>
    /// <seealso cref="RTFExporter.Units"/>
    public RTFDocument(float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      this.Init(width, height, orientation, units);
    }

    /// <summary>Gets or sets the sequential collection of <see cref="RTFParagraph"/> blocks in the document.</summary>
    public List<RTFParagraph> Paragraphs { get; set; }

    /// <summary>Gets or sets the collection of distinct <see cref="Color"/> definitions registered in the document color table.</summary>
    public List<Color> Colors { get; set; }

    /// <summary>Gets or sets the collection of distinct font families registered in the document font table.</summary>
    public List<string> Fonts { get; set; }

    /// <summary>Gets or sets the author name written into the RTF information group (<c>\info \author</c>).</summary>
    public string Author { get; set; }

    /// <summary>Gets or sets the page width in current document measurement units.</summary>
    public float Width { get; set; }

    /// <summary>Gets or sets the page height in current document measurement units.</summary>
    public float Height { get; set; }

    /// <summary>Gets or sets the page <see cref="Orientation"/>.</summary>
    public Orientation Orientation { get; set; }

    /// <summary>Gets or sets the document <see cref="Margin"/> configuration.</summary>
    public Margin Margin { get; set; }

    /// <summary>Gets or sets the document measurement <see cref="Units"/>.</summary>
    public Units Units { get; set; }

    /// <summary>Gets or sets the document version number recorded in metadata (<c>\versionN</c>). Defaults to 1.</summary>
    public int Version { get; set; }

    /// <summary>Gets or sets a list of keywords recorded in the RTF information group (<c>\keywords</c>).</summary>
    public List<string> Keywords { get; set; }

    /// <summary>
    /// Assigns a destination file path and allocates the underlying <see cref="FileStream"/> and <see cref="StreamWriter"/>.
    /// </summary>
    /// <param name="path">The target file path.</param>
    public void SetFile(string path)
    {
      this.fileStream = new FileStream(path, FileMode.Create);
      this.streamWriter = new StreamWriter(this.fileStream);
    }

    /// <summary>
    /// Binds the document directly to an external <see cref="FileStream"/>.
    /// </summary>
    /// <param name="fileStream">An open file stream with write access.</param>
    public void SetStream(FileStream fileStream)
    {
      this.fileStream = fileStream;
      this.streamWriter = new StreamWriter(fileStream);
    }

    /// <summary>
    /// Initializes page dimension parameters and sets default margins based on the selected measurement units.
    /// </summary>
    /// <param name="width">The page width.</param>
    /// <param name="height">The page height.</param>
    /// <param name="orientation">The page <see cref="Orientation"/>.</param>
    /// <param name="units">The measurement <see cref="Units"/>.</param>
    /// <seealso cref="RTFExporter.Orientation"/>
    /// <seealso cref="RTFExporter.Units"/>
    public void Init(float width, float height, Orientation orientation, Units units)
    {
      this.Paragraphs = new List<RTFParagraph>();
      this.Colors = new List<Color>();
      this.Fonts = new List<string>();
      this.Version = 1;
      this.Keywords = new List<string>();

      this.Width = width;
      this.Height = height;
      this.Orientation = orientation;
      this.Units = units;

      switch (units)
      {
        case Units.Inch:
          this.Margin = new Margin(1, 1, 1, 1);
          break;
        case Units.Millimeters:
          this.Margin = new Margin(25.4f, 25.4f, 25.4f, 25.4f);
          break;
        case Units.Centimeters:
          this.Margin = new Margin(2.54f, 2.54f, 2.54f, 2.54f);
          break;
      }
    }

    /// <summary>
    /// Explicitly sets the document margins in current measurement units.
    /// </summary>
    /// <param name="left">Left margin offset.</param>
    /// <param name="right">Right margin offset.</param>
    /// <param name="top">Top margin offset.</param>
    /// <param name="bottom">Bottom margin offset.</param>
    public void SetMargin(float left, float right, float top, float bottom)
    {
      this.Margin.Left = left;
      this.Margin.Right = right;
      this.Margin.Top = top;
      this.Margin.Bottom = bottom;
    }

    /// <summary>
    /// Creates and appends a new unformatted <see cref="RTFParagraph"/> to this document.
    /// </summary>
    /// <returns>The appended <see cref="RTFParagraph"/> instance.</returns>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    public RTFParagraph AppendParagraph()
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      return paragraph;
    }

    /// <summary>
    /// Creates and appends a new <see cref="RTFParagraph"/> configured with a custom style object.
    /// </summary>
    /// <param name="style">The <see cref="RTFParagraphStyle"/> configuration.</param>
    /// <returns>The appended <see cref="RTFParagraph"/> instance.</returns>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.RTFParagraphStyle"/>
    public RTFParagraph AppendParagraph(RTFParagraphStyle style)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.Style = style;
      return paragraph;
    }

    /// <summary>
    /// Creates and appends a new <see cref="RTFParagraph"/> configured with a specified text alignment.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode.</param>
    /// <returns>The appended <see cref="RTFParagraph"/> instance.</returns>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.Alignment"/>
    public RTFParagraph AppendParagraph(Alignment alignment)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.Style = new RTFParagraphStyle(alignment);
      return paragraph;
    }

    /// <summary>
    /// Creates and appends a new left-aligned <see cref="RTFParagraph"/> configured with custom indentation.
    /// </summary>
    /// <param name="indent">The <see cref="Indent"/> configuration.</param>
    /// <returns>The appended <see cref="RTFParagraph"/> instance.</returns>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.Indent"/>
    public RTFParagraph AppendParagraph(Indent indent)
    {
      return this.AppendParagraph(Alignment.Left, indent);
    }

    /// <summary>
    /// Creates and appends a new <see cref="RTFParagraph"/> configured with alignment and indentation.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode.</param>
    /// <param name="indent">The <see cref="Indent"/> configuration.</param>
    /// <returns>The appended <see cref="RTFParagraph"/> instance.</returns>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.Alignment"/>
    /// <seealso cref="RTFExporter.Indent"/>
    public RTFParagraph AppendParagraph(Alignment alignment, Indent indent)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.Style = new RTFParagraphStyle(alignment, indent);
      return paragraph;
    }

    /// <summary>
    /// Creates and appends a new <see cref="RTFParagraph"/> with comprehensive alignment, indentation, and vertical spacing settings.
    /// </summary>
    /// <param name="alignment">The <see cref="Alignment"/> mode.</param>
    /// <param name="indent">The <see cref="Indent"/> configuration.</param>
    /// <param name="spaceBefore">Vertical space before the paragraph in twips.</param>
    /// <param name="spaceAfter">Vertical space after the paragraph in twips.</param>
    /// <returns>The appended <see cref="RTFParagraph"/> instance.</returns>
    /// <seealso cref="RTFExporter.RTFParagraph"/>
    /// <seealso cref="RTFExporter.Alignment"/>
    /// <seealso cref="RTFExporter.Indent"/>
    public RTFParagraph AppendParagraph(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.Style = new RTFParagraphStyle(alignment, indent, spaceBefore, spaceAfter);
      return paragraph;
    }

    /// <summary>
    /// Closes any open file streams (<see cref="StreamWriter"/> and <see cref="FileStream"/>) associated with this document.
    /// </summary>
    public void Close()
    {
      this.streamWriter.Close();
      this.fileStream.Close();
    }

    /// <summary>
    /// Serializes and writes the current document content to the underlying stream using <see cref="RTFParser"/>.
    /// </summary>
    public void Save()
    {
      this.streamWriter.Write(RTFParser.ToString(this));
    }

    /// <summary>
    /// Disposes the document resource, automatically saving changes and closing underlying streams if initialized with a file or stream.
    /// </summary>
    public void Dispose()
    {
      if (this.fileStream != null && this.streamWriter != null)
      {
        this.Save();
        this.Close();
      }
    }
  }
}
