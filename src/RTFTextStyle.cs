namespace RTFExporter {

    public enum Underline {
        None,
        Basic,
        Double,
        Thick,
        WordsOnly,
        Wave,
        Dotted,
        Dash,
        DotDash
    }

    public class RTFTextStyle {
        public bool italic;
        public bool bold;
        public bool smallCaps;
        public bool strikeThrough;
        public bool allCaps;
        public bool outline;
        public int fontSize;
        public string fontFamily;
        public Color color;
        public Underline underline;
        
        public RTFTextStyle(bool italic, bool bold, int fontSize, string fontFamily, Color color) {
            this.italic = italic;
            this.bold = bold;
            this.fontSize = fontSize;
            this.fontFamily = fontFamily;
            this.color = color;
        }

        public RTFTextStyle(bool italic, bool bold, bool smallCaps, bool strikeThrough, bool allCaps,
            bool outline, int fontSize, string fontFamily, Color color, Underline underline) {
            this.italic = italic;
            this.bold = bold;
            this.smallCaps = smallCaps;
            this.strikeThrough = strikeThrough;
            this.allCaps = allCaps;
            this.outline = outline;
            this.fontSize = fontSize;
            this.fontFamily = fontFamily;
            this.color = color;
            this.underline = underline;
        }
    }

}
