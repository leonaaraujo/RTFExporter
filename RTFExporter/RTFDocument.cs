using System;
using System.Collections.Generic;
using System.IO;

namespace RTFExporter
{
  /// <summary>
  /// Represents document margins (left, right, top, bottom) in current document measurement units.
  /// </summary>
  public class Margin
  {
    /// <summary>Left margin offset.</summary>
    public float left;

    /// <summary>Right margin offset.</summary>
    public float right;

    /// <summary>Top margin offset.</summary>
    public float top;

    /// <summary>Bottom margin offset.</summary>
    public float bottom;

    /// <summary>
    /// Initializes a new instance of the <see cref="Margin"/> class with explicit values for all four sides.
    /// </summary>
    /// <param name="left">Left margin offset.</param>
    /// <param name="right">Right margin offset.</param>
    /// <param name="top">Top margin offset.</param>
    /// <param name="bottom">Bottom margin offset.</param>
    public Margin(float left, float right, float top, float bottom)
    {
      this.left = left;
      this.right = right;
      this.top = top;
      this.bottom = bottom;
    }
  }

  /// <summary>
  /// Specifies the page orientation of an RTF document.
  /// </summary>
  public enum Orientation
  {
    /// <summary>Landscape orientation (<c>\landscape</c>).</summary>
    Landscape,
    /// <summary>Portrait orientation (<c>\portrait</c>).</summary>
    Portrait
  }

  /// <summary>
  /// Specifies the measurement units used for document dimensions, margins, and paragraph spacing.
  /// </summary>
  public enum Units
  {
    /// <summary>Inches (converted to twips at 1440 twips/inch).</summary>
    Inch,
    /// <summary>Millimeters.</summary>
    Millimeters,
    /// <summary>Centimeters.</summary>
    Centimeters
  }

  /// <summary>
  /// Represents a Rich Text Format (RTF) document. Acts as the root container for paragraphs, color tables, font tables, and document metadata.
  /// </summary>
  /// <remarks>
  /// Implements <see cref="IDisposable"/> to facilitate automatic saving and stream cleanup when used inside a <c>using</c> block.
  /// </remarks>
  public class RTFDocument : IDisposable
  {
    /// <summary>The sequential collection of <see cref="RTFParagraph"/> blocks in the document.</summary>
    public List<RTFParagraph> paragraphs = new List<RTFParagraph>();

    /// <summary>The collection of distinct <see cref="Color"/> definitions registered in the document color table.</summary>
    public List<Color> colors = new List<Color>();

    /// <summary>The collection of distinct font families registered in the document font table.</summary>
    public List<string> fonts = new List<string>();

    /// <summary>The author name written into the RTF information group (<c>\info \author</c>).</summary>
    public string author;

    /// <summary>The page width in current document measurement units.</summary>
    public float width;

    /// <summary>The page height in current document measurement units.</summary>
    public float height;

    /// <summary>The page <see cref="Orientation"/>.</summary>
    public Orientation orientation;

    /// <summary>The document <see cref="Margin"/> configuration.</summary>
    public Margin margin;

    /// <summary>The document measurement <see cref="Units"/>.</summary>
    public Units units;

    private FileStream fileStream;
    private StreamWriter streamWriter;

    /// <summary>The document version number recorded in metadata (<c>\versionN</c>). Defaults to 1.</summary>
    public int version = 1;

    /// <summary>A list of keywords recorded in the RTF information group (<c>\keywords</c>).</summary>
    public List<string> keywords = new List<string>();

    /// <summary>
    /// Initializes a new in-memory instance of the <see cref="RTFDocument"/> class with standard 8x11 inch portrait settings.
    /// </summary>
    public RTFDocument()
    {
      Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class bound to a output file path.
    /// </summary>
    /// <param name="path">The destination file path where the RTF file will be saved.</param>
    public RTFDocument(string path)
    {
      SetFile(path);
      Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RTFDocument"/> class bound to an existing <see cref="FileStream"/>.
    /// </summary>
    /// <param name="fileStream">An open file stream with write access.</param>
    public RTFDocument(FileStream fileStream)
    {
      SetStream(fileStream);
      Init(8, 11, Orientation.Portrait, Units.Inch);
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
      SetFile(path);
      Init(width, height, orientation, units);
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
      SetStream(fileStream);
      Init(width, height, orientation, units);
    }

    /// <summary>
    /// Initializes a new in-memory instance of the <see cref="RTFDocument"/> class with customized page parameters.
    /// </summary>
    /// <param name="width">Page width in specified units. Defaults to 8.</param>
    /// <param name="height">Page height in specified units. Defaults to 11.</param>
    /// <param name="orientation">Page orientation. Defaults to <see cref="Orientation.Portrait"/>.</param>
    /// <param name="units">Measurement units. Defaults to <see cref="Units.Inch"/>.</param>
    /// <seealso cref="RTFExporter.Orientation"/>
    /// <seealso cref="RTFExporter.Units"/>
    public RTFDocument(float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      Init(width, height, orientation, units);
    }

    /// <summary>
    /// Assigns a destination file path and allocates the underlying <see cref="FileStream"/> and <see cref="StreamWriter"/>.
    /// </summary>
    /// <param name="path">The target file path.</param>
    public void SetFile(string path)
    {
      fileStream = new FileStream(path, FileMode.Create);
      streamWriter = new StreamWriter(fileStream);
    }

    /// <summary>
    /// Binds the document directly to an external <see cref="FileStream"/>.
    /// </summary>
    /// <param name="fileStream">An open file stream with write access.</param>
    public void SetStream(FileStream fileStream)
    {
      this.fileStream = fileStream;
      streamWriter = new StreamWriter(fileStream);
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
      this.width = width;
      this.height = height;
      this.orientation = orientation;
      this.units = units;

      switch (units)
      {
        case Units.Inch:
          margin = new Margin(1, 1, 1, 1);
          break;
        case Units.Millimeters:
          margin = new Margin(25.4f, 25.4f, 25.4f, 25.4f);
          break;
        case Units.Centimeters:
          margin = new Margin(2.54f, 2.54f, 2.54f, 2.54f);
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
      margin.left = left;
      margin.right = right;
      margin.top = top;
      margin.bottom = bottom;
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
      paragraph.style = style;
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
      paragraph.style = new RTFParagraphStyle(alignment);
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
      return AppendParagraph(Alignment.Left, indent);
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
      paragraph.style = new RTFParagraphStyle(alignment, indent);
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
      paragraph.style = new RTFParagraphStyle(alignment, indent, spaceBefore, spaceAfter);
      return paragraph;
    }

    /// <summary>
    /// Closes any open file streams (<see cref="StreamWriter"/> and <see cref="FileStream"/>) associated with this document.
    /// </summary>
    public void Close()
    {
      streamWriter.Close();
      fileStream.Close();
    }

    /// <summary>
    /// Serializes and writes the current document content to the underlying stream using <see cref="RTFParser"/>.
    /// </summary>
    public void Save()
    {
      streamWriter.Write(RTFParser.ToString(this));
    }

    /// <summary>
    /// Disposes the document resource, automatically saving changes and closing underlying streams if initialized with a file or stream.
    /// </summary>
    public void Dispose()
    {
      if (fileStream != null && streamWriter != null)
      {
        Save();
        Close();
      }
    }
  }
}
