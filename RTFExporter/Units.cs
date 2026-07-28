namespace RTFExporter
{
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
    Centimeters,
  }
}