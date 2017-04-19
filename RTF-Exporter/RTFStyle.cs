namespace RTFExporter {
    
    public class Color {
        public byte r;
        public byte g;
        public byte b;
        public int index;
        public static Color black = new Color(0, 0, 0);
        public static Color white = new Color(255, 255, 255);

        public Color(byte r, byte g, byte b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

    public class RTFStyle {
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

        public RTFStyle(bool italic, bool bold, int fontSize, string fontFamily, Color color) {
            this.italic = italic;
            this.bold = bold;
            this.fontSize = fontSize;
            this.fontFamily = fontFamily;
            this.color = color;
        }

        public RTFStyle(bool italic, bool bold, bool smallCaps, bool strikeThrough, bool allCaps,
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
