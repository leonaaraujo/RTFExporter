using System;
using System.Collections.Generic;
using System.IO;

namespace RTFExporter
{
  /// <summary>
  /// The document margin
  /// </summary>
  public class Margin
  {
    public float left;
    public float right;
    public float top;
    public float bottom;

    /// <summary>
    /// Margin constructor. All values need to be declared
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="top"></param>
    /// <param name="bottom"></param>
    public Margin(float left, float right, float top, float bottom)
    {
      this.left = left;
      this.right = right;
      this.top = top;
      this.bottom = bottom;
    }
  }

  /// <summary>
  /// Page orientation setup
  /// </summary>
  public enum Orientation
  {
    Landscape,
    Portrait
  }

  /// <summary>
  /// Page measure units
  /// </summary>
  public enum Units
  {
    Inch,
    Millimeters,
    Centimeters
  }

  /// <summary>
  /// The RTF document, it is the main class and use a IDisposable interface
  /// </summary>
  public class RTFDocument : IDisposable
  {
    public List<RTFParagraph> paragraphs = new List<RTFParagraph>();
    public List<Color> colors = new List<Color>();
    public List<string> fonts = new List<string>();
    public string author;
    public float width;
    public float height;
    public Orientation orientation;
    public Margin margin;
    public Units units;
    private FileStream fileStream;
    private StreamWriter streamWriter;
    public int version = 1;
    public List<string> keywords = new List<string>();

    /// <summary>
    /// The simple RTF Document constructor without use streams
    /// </summary>
    public RTFDocument()
    {
      Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// The RTF Document constructor with a file path
    /// </summary>
    /// <param name="path">A path to a folder with file name</param>
    public RTFDocument(string path)
    {
      SetFile(path);
      Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// The RTF Document constructor with a file stream
    /// </summary>
    /// <param name="fileStream">A file stream</param>
    public RTFDocument(FileStream fileStream)
    {
      SetStream(fileStream);
      Init(8, 11, Orientation.Portrait, Units.Inch);
    }

    /// <summary>
    /// RTF document constructor with setup parameters
    /// <seealso cref="RTFExporter.Orientation">
    /// <seealso cref="RTFExporter.Units">
    /// </summary>
    /// <param name="path">A path to a folder with file name</param>
    /// <param name="width">the width of the page</param>
    /// <param name="height">the height of the page</param>
    /// <param name="orientation">the page orientation</param>
    /// <param name="units">the measure units of the page</param>
    public RTFDocument(string path, float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      SetFile(path);
      Init(width, height, orientation, units);
    }

    /// <summary>
    /// RTF document constructor with setup parameters without use streams
    /// <seealso cref="RTFExporter.Orientation">
    /// <seealso cref="RTFExporter.Units">
    /// </summary>
    /// <param name="fileStream">A file stream</param>
    /// <param name="width">the width of the page</param>
    /// <param name="height">the height of the page</param>
    /// <param name="orientation">the page orientation</param>
    /// <param name="units">the measure units of the page</param>
    public RTFDocument(FileStream fileStream, float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      SetStream(fileStream);
      Init(width, height, orientation, units);
    }

    /// <summary>
    /// RTF document constructor with setup parameters
    /// <seealso cref="RTFExporter.Orientation">
    /// <seealso cref="RTFExporter.Units">
    /// </summary>
    /// <param name="width">the width of the page</param>
    /// <param name="height">the height of the page</param>
    /// <param name="orientation">the page orientation</param>
    /// <param name="units">the measure units of the page</param>
    public RTFDocument(float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch)
    {
      Init(width, height, orientation, units);
    }

    /// <summary>
    /// Set a file by path and allocate it stream
    /// </summary>
    /// <param name="path">A path to a folder with file name</param>
    public void SetFile(string path)
    {
      fileStream = new FileStream(path, FileMode.Create);
      streamWriter = new StreamWriter(fileStream);
    }

    /// <summary>
    /// Use a file stream directly
    /// </summary>
    /// <param name="fileStream">A file stream</param>
    public void SetStream(FileStream fileStream)
    {
      this.fileStream = fileStream;
      streamWriter = new StreamWriter(fileStream);
    }

    /// <summary>
    /// RTF document init method to use after a simple constructor
    /// <seealso cref="RTFExporter.Orientation">
    /// <seealso cref="RTFExporter.Units">
    /// </summary>
    /// <param name="width">the width of the page</param>
    /// <param name="height">the height of the page</param>
    /// <param name="orientation">the page orientation</param>
    /// <param name="units">the measure units of the page</param>
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
    /// A method to set the margin of the document
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="top"></param>
    /// <param name="bottom"></param>
    public void SetMargin(float left, float right, float top, float bottom)
    {
      margin.left = left;
      margin.right = right;
      margin.top = top;
      margin.bottom = bottom;
    }

    /// <summary>
    /// Append a new paragraph to the document
    /// <seealso cref="RTFExporter.RTFParagraph">
    /// </summary>
    /// <returns>The appended paragraph</returns>
    public RTFParagraph AppendParagraph()
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      return paragraph;
    }

    /// <summary>
    /// Append a new paragraph to the document
    /// <seealso cref="RTFExporter.RTFParagraph">
    /// <seealso cref="RTFExporter.RTFParagraphStyle">
    /// </summary>
    /// <param name="style">A paragraph style object</param>
    /// <returns>The appended paragraph</returns>
    public RTFParagraph AppendParagraph(RTFParagraphStyle style)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.style = style;
      return paragraph;
    }

    /// <summary>
    /// Append a new paragraph to the document
    /// <seealso cref="RTFExporter.RTFParagraph">
    /// <seealso cref="RTFExporter.Alignment">
    /// </summary>
    /// <param name="alignment">A paragraph alignment object</param>
    /// <returns>The appended paragraph</returns>
    public RTFParagraph AppendParagraph(Alignment alignment)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.style = new RTFParagraphStyle(alignment);
      return paragraph;
    }

    /// <summary>
    /// Append a new paragraph to the document
    /// <seealso cref="RTFExporter.RTFParagraph">
    /// <seealso cref="RTFExporter.Indent">
    /// </summary>
    /// <param name="indent">A paragraph indent object</param>
    /// <returns>The appended paragraph</returns>
    public RTFParagraph AppendParagraph(Indent indent)
    {
      return AppendParagraph(Alignment.Left, indent);
    }

    /// <summary>
    /// 
    /// <seealso cref="RTFExporter.RTFParagraph">
    /// <seealso cref="RTFExporter.Alignment">
    /// <seealso cref="RTFExporter.Indent">
    /// </summary>
    /// <param name="alignment">A paragraph alignment object</param>
    /// <param name="indent">A paragraph indent object</param>
    /// <returns>The appended paragraph</returns>
    public RTFParagraph AppendParagraph(Alignment alignment, Indent indent)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.style = new RTFParagraphStyle(alignment, indent);
      return paragraph;
    }

    /// <summary>
    /// Append a new paragraph to the document
    /// <seealso cref="RTFExporter.RTFParagraph">
    /// <seealso cref="RTFExporter.Alignment">
    /// <seealso cref="RTFExporter.Indent">
    /// </summary>
    /// <param name="alignment">A paragraph alignment object</param>
    /// <param name="indent">A paragraph indent object</param>
    /// <param name="spaceBefore">The value of the vertical space before the paragraph</param>
    /// <param name="spaceAfter">The value of the vertical space after the paragraph</param>
    /// <returns>The appended paragraph</returns>
    public RTFParagraph AppendParagraph(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter)
    {
      RTFParagraph paragraph = new RTFParagraph(this);
      paragraph.style = new RTFParagraphStyle(alignment, indent, spaceBefore, spaceAfter);
      return paragraph;
    }

    /// <summary>
    /// Close the streams (StreamWriter, FileStream)
    /// </summary>
    public void Close()
    {
      streamWriter.Close();
      fileStream.Close();
    }

    /// <summary>
    /// Save the values to the file
    /// </summary>
    public void Save()
    {
      streamWriter.Write(RTFParser.ToString(this));
    }

    /// <summary>
    /// The dispose routine. It save and close the streams (StreamWriter, FileStream)
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
