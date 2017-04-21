namespace RTFExporter {

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

    public class RTFParagraphStyle {

        public Indent indent;
        public Alignment alignment;
        public int spaceBefore;
        public int spaceAfter = 100;
        
        public RTFParagraphStyle(Alignment alignment) {
            this.alignment = alignment;
        }

        public RTFParagraphStyle(Alignment alignment, Indent indent) {
            this.alignment = alignment;
            this.indent = indent;
        }

        public RTFParagraphStyle(Alignment alignment, Indent indent, int spaceBefore, int spaceAfter) {
            this.alignment = alignment;
            this.indent = indent;
            this.spaceBefore = spaceBefore;
            this.spaceAfter = spaceAfter;
        }

        public RTFParagraphStyle(RTFDocument document) {
            switch (document.units) {
                case Units.Inch:
                    indent = new Indent(1, 0, 0);
                    break;
                case Units.Millimeters:
                    indent = new Indent(25.4f, 0, 0);
                    break;
                case Units.Centimeters:
                    indent = new Indent(2.54f, 0, 0);
                    break;
            }
        }
    }

}