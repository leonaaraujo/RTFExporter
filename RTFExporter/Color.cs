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
    /// <summary>The red component (0 to 255).</summary>
    public byte r;

    /// <summary>The green component (0 to 255).</summary>
    public byte g;

    /// <summary>The blue component (0 to 255).</summary>
    public byte b;

    /// <summary>The 1-based index assigned to this color within the generated RTF document's color table.</summary>
    public int index;

    /// <summary>Predefined black color (RGB: 0, 0, 0).</summary>
    public static Color black = new Color(0, 0, 0);

    /// <summary>Predefined white color (RGB: 255, 255, 255).</summary>
    public static Color white = new Color(255, 255, 255);

    /// <summary>Predefined red color (RGB: 255, 0, 0).</summary>
    public static Color red = new Color(255, 0, 0);

    /// <summary>Predefined green color (RGB: 0, 255, 0).</summary>
    public static Color green = new Color(0, 255, 0);

    /// <summary>Predefined blue color (RGB: 0, 0, 255).</summary>
    public static Color blue = new Color(0, 0, 255);

    /// <summary>Predefined yellow color (RGB: 255, 255, 0).</summary>
    public static Color yellow = new Color(255, 255, 0);

    /// <summary>Predefined purple color (RGB: 255, 0, 255).</summary>
    public static Color purple = new Color(255, 0, 255);

    /// <summary>Predefined cyan color (RGB: 0, 255, 255).</summary>
    public static Color cyan = new Color(0, 255, 255);

    /// <summary>
    /// Initializes a new instance of the <see cref="Color"/> class with specified RGB values.
    /// </summary>
    /// <param name="r">Red color component (0 to 255).</param>
    /// <param name="g">Green color component (0 to 255).</param>
    /// <param name="b">Blue color component (0 to 255).</param>
    public Color(byte r, byte g, byte b)
    {
      this.r = r;
      this.g = g;
      this.b = b;
    }
  }
}
