using System.Collections.Generic;

namespace RTFExporter {
    
    public class RTFParagraph {
        public List<RTFText> text = new List<RTFText>();
        public Indent indent;
        public Alignment alignment;
        public int spaceBefore;
        public int spaceAfter = 100;

        public struct Indent {
            public float firstLine;
            public float left;
            public float right;

            public Indent(float firstLine, float left, float right) {
                this.firstLine = firstLine;
                this.left = left;
                this.right = right;
            }
        }

        public enum Alignment {
            Left,
            Right,
            Center,
            Justified
        }

        public RTFParagraph(RTFDocument document) {
            switch (document.units) {
                case RTFDocument.Units.Inch:
                    indent = new Indent(1, 0, 0);
                    break;
                case RTFDocument.Units.Millimeters:
                    indent = new Indent(25.4f, 0, 0);
                    break;
                case RTFDocument.Units.Centimeters:
                    indent = new Indent(2.54f, 0, 0);
                    break;
            }

            document.paragraphs.Add(this);
        }

        public RTFParagraph(RTFDocument document, Alignment alignment) {
            this.alignment = alignment;
            document.paragraphs.Add(this);
        }

        public RTFParagraph(RTFDocument document, Alignment alignment, Indent indent) {
            this.alignment = alignment;
            this.indent = indent;
            document.paragraphs.Add(this);
        }

        public RTFText AppendText(string content) {
            RTFText text = new RTFText(this, content);
            return text;
        }

        public RTFText AppendText(string content, RTFStyle style) {
            RTFText text = new RTFText(this, content, style);
            return text;
        }
    }

}
