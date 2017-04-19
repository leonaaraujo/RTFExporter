using System;
using System.IO;
using System.Collections.Generic;

namespace RTFExporter {

    public class RTFDocument : IDisposable {
        public List<RTFParagraph> paragraphs = new List<RTFParagraph>();
        public List<Color> colors = new List<Color>();
        public List<string> fonts = new List<string>();
        public string author;
        public float width;
        public float height;
        public Orientation orientation;
        public Margin margin;
        public Units units;
        private FileStream fileStream;
        private StreamWriter streamWriter;

        public struct Margin {
            public float left;
            public float right;
            public float top;
            public float bottom;

            public Margin(float left, float right, float top, float bottom) {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;
            }
        }

        public enum Orientation {
            Landscape,
            Portrait
        }

        public enum Units {
            Inch,
            Millimeters,
            Centimeters
        }

        public RTFDocument() {
            Init(8, 11, Orientation.Portrait, Units.Inch);
        }

        public RTFDocument(string path) {
            fileStream = new FileStream(path, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
            Init(8, 11, Orientation.Portrait, Units.Inch);
        }

        public RTFDocument(float width = 8, float height = 11, Orientation orientation = Orientation.Portrait, Units units = Units.Inch) {
            Init(width, height, orientation, units);
        }

        public void Init(float width, float height, Orientation orientation, Units units) {
            this.width = width;
            this.height = height;
            this.orientation = orientation;
            this.units = units;

            switch (units) {
                case Units.Inch:
                    margin = new Margin(1, 1, 1, 1);
                    break;
                case Units.Millimeters:
                    margin = new Margin(25.4f, 25.4f, 25.4f, 25.4f);
                    break;
                case Units.Centimeters:
                    margin = new Margin(2.54f, 2.54f, 2.54f, 2.54f);
                    break;
            }
        }

        public void SetMargin(float left, float right, float top, float bottom) {
            margin.left = left;
            margin.right = right;
            margin.top = top;
            margin.bottom = bottom;
        }

        public RTFParagraph AppendParagraph() {
            RTFParagraph paragraph = new RTFParagraph(this);
            return paragraph;
        }

        public RTFParagraph AppendParagraph(RTFParagraph.Alignment alignment) {
            RTFParagraph paragraph = new RTFParagraph(this, alignment);
            return paragraph;
        }

        public RTFParagraph AppendParagraph(RTFParagraph.Indent indent) {
            return AppendParagraph(RTFParagraph.Alignment.Left, indent);
        }

        public RTFParagraph AppendParagraph(RTFParagraph.Alignment alignment, RTFParagraph.Indent indent) {
            RTFParagraph paragraph = new RTFParagraph(this, alignment, indent);
            return paragraph;
        }

        public void Close() {
            streamWriter.Close();
            fileStream.Close();
        }

        public void Dispose() {
            streamWriter.Write(RTFParser.ToString(this));
            Close();
        }
    }
    
}
