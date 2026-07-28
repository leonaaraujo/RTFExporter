namespace RTFExporter
{
  /// <summary>
  /// Represents an RGB color used within an RTF document's color table (<c>\colortbl</c>).
  /// </summary>
  /// <remarks>
  /// RTF colors are defined once in the document color table and referenced throughout the text using 1-based indices (<c>\cfN</c>).
  /// </remarks>
  public class Color
  {
#pragma warning disable SA1401
    /// <summary>Predefined black color (RGB: 0, 0, 0).</summary>
    public static Color Black = new Color(0, 0, 0);

    /// <summary>Predefined white color (RGB: 255, 255, 255).</summary>
    public static Color White = new Color(255, 255, 255);

    /// <summary>Predefined red color (RGB: 255, 0, 0).</summary>
    public static Color Red = new Color(255, 0, 0);

    /// <summary>Predefined green color (RGB: 0, 255, 0).</summary>
    public static Color Green = new Color(0, 255, 0);

    /// <summary>Predefined blue color (RGB: 0, 0, 255).</summary>
    public static Color Blue = new Color(0, 0, 255);

    /// <summary>Predefined yellow color (RGB: 255, 255, 0).</summary>
    public static Color Yellow = new Color(255, 255, 0);

    /// <summary>Predefined purple color (RGB: 255, 0, 255).</summary>
    public static Color Purple = new Color(255, 0, 255);

    /// <summary>Predefined cyan color (RGB: 0, 255, 255).</summary>
    public static Color Cyan = new Color(0, 255, 255);
#pragma warning restore SA1401

    /// <summary>
    /// Initializes a new instance of the <see cref="Color"/> class with specified RGB values.
    /// </summary>
    /// <param name="r">Red color component (0 to 255).</param>
    /// <param name="g">Green color component (0 to 255).</param>
    /// <param name="b">Blue color component (0 to 255).</param>
    public Color(byte r, byte g, byte b)
    {
      this.R = r;
      this.G = g;
      this.B = b;
    }

    /// <summary>Gets or sets the red component (0 to 255).</summary>
    public byte R { get; set; }

    /// <summary>Gets or sets the green component (0 to 255).</summary>
    public byte G { get; set; }

    /// <summary>Gets or sets the blue component (0 to 255).</summary>
    public byte B { get; set; }

    /// <summary>Gets or sets the 1-based index assigned to this color within the generated RTF document's color table.</summary>
    public int Index { get; set; }
  }
}
