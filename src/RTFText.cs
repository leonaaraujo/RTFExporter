namespace RTFExporter {
    
    public class RTFText {
        public RTFTextStyle style;
        public string content;

        public RTFText(RTFParagraph paragraph, string content) {
            style = new RTFTextStyle(false, false, 12, "Calibri", new Color(0, 0, 0));
            this.content = content;
            paragraph.text.Add(this);
        }

        public RTFText(RTFParagraph paragraph, string content, RTFTextStyle style) {
            this.style = style;
            this.content = content;
            paragraph.text.Add(this);
        }

        public RTFText SetStyle() {
            style = new RTFTextStyle(false, false, false, false, false, false, 12, "Calibri", Color.black, Underline.None);
            return this;
        }

        public RTFText SetStyle(Color color, int fontSize = 12, string fontFamily = "Calibri") {
            style = new RTFTextStyle(false, false, fontSize, fontFamily, color);
            return this;
        }

        public RTFText SetStyle(Color color, bool italic = false, bool bold = false, int fontSize = 12, string fontFamily = "Calibri") {
            style = new RTFTextStyle(italic, bold, fontSize, fontFamily, color);
            return this;
        }

        public RTFText SetStyle(bool italic, bool bold, Underline underline = Underline.None,
            bool smallCaps = false, bool strikeThrough = false, bool allCaps = false, bool outline = false) {
            style = new RTFTextStyle(italic, bold, smallCaps, strikeThrough, allCaps, outline, 12, "Calibri", Color.black, underline);
            return this;
        }
    }
    
}
