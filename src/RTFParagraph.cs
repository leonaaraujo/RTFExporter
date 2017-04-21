using System.Collections.Generic;

namespace RTFExporter {
    
    public class RTFParagraph {
        public List<RTFText> text = new List<RTFText>();
        public RTFParagraphStyle style;

        public RTFParagraph(RTFDocument document) {
            style = new RTFParagraphStyle(document);
            document.paragraphs.Add(this);
        }

        public RTFText AppendText(string content) {
            RTFText text = new RTFText(this, content);
            return text;
        }

        public RTFText AppendText(string content, RTFTextStyle style) {
            RTFText text = new RTFText(this, content, style);
            return text;
        }
    }

}
