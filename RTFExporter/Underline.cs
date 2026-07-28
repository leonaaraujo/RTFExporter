namespace RTFExporter
{
  /// <summary>
  /// Specifies the style of underline applied to an RTF text run.
  /// </summary>
  public enum Underline
  {
    /// <summary>No underline (<c>\ul0</c>).</summary>
    None,

    /// <summary>Standard single line underline (<c>\ul</c>).</summary>
    Basic,

    /// <summary>Double line underline (<c>\uldb</c>).</summary>
    Double,

    /// <summary>Thick line underline (<c>\ulth</c>).</summary>
    Thick,

    /// <summary>Underline applied to words only, skipping spaces (<c>\ulw</c>).</summary>
    WordsOnly,

    /// <summary>Wavy underline (<c>\ulwave</c>).</summary>
    Wave,

    /// <summary>Dotted underline (<c>\uld</c>).</summary>
    Dotted,

    /// <summary>Dashed underline (<c>\uldash</c>).</summary>
    Dash,

    /// <summary>Dot-dash underline (<c>\uldashd</c>).</summary>
    DotDash,
  }
}