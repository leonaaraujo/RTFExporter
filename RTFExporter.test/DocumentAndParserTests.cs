using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using RTFExporter;

namespace RTFExporter.test
{
    public class DocumentAndParserTests
    {
        [Fact]
        public void RTFDocument_Constructors_WorkCorrectly()
        {
            var d1 = new RTFDocument();
            Assert.Equal(8f, d1.Width);
            Assert.Equal(11f, d1.Height);
            Assert.Equal(Orientation.Portrait, d1.Orientation);
            Assert.Equal(Units.Inch, d1.Units);

            var d2 = new RTFDocument(297f, 210f, Orientation.Landscape, Units.Millimeters);
            Assert.Equal(297f, d2.Width);
            Assert.Equal(210f, d2.Height);
            Assert.Equal(Orientation.Landscape, d2.Orientation);
            Assert.Equal(Units.Millimeters, d2.Units);

            var d3 = new RTFDocument(20f, 30f, Orientation.Portrait, Units.Centimeters);
            Assert.Equal(Units.Centimeters, d3.Units);

            string tempPath = Path.GetTempFileName();
            try
            {
                var d4 = new RTFDocument(tempPath);
                d4.Dispose();

                var d5 = new RTFDocument(tempPath, 10f, 12f, Orientation.Landscape, Units.Inch);
                d5.Dispose();

                using (var fs = new FileStream(tempPath, FileMode.Create))
                {
                    var d6 = new RTFDocument(fs);
                    d6.AppendParagraph().AppendText("Stream test");
                    d6.Dispose();
                }

                using (var fs = new FileStream(tempPath, FileMode.Create))
                {
                    var d7 = new RTFDocument(fs, 9f, 13f, Orientation.Portrait, Units.Inch);
                    d7.Dispose();
                }
            }
            finally
            {
                if (File.Exists(tempPath)) File.Delete(tempPath);
            }
        }

        [Fact]
        public void RTFDocument_SetMargin_WorksCorrectly()
        {
            var doc = new RTFDocument();
            doc.SetMargin(1.5f, 1.5f, 2.0f, 2.0f);
            Assert.Equal(1.5f, doc.Margin.Left);
            Assert.Equal(1.5f, doc.Margin.Right);
            Assert.Equal(2.0f, doc.Margin.Top);
            Assert.Equal(2.0f, doc.Margin.Bottom);
        }

        [Fact]
        public void RTFDocument_AppendParagraph_Overloads()
        {
            var doc = new RTFDocument();

            var p1 = doc.AppendParagraph();
            Assert.NotNull(p1);

            var customStyle = new RTFParagraphStyle(Alignment.Center);
            var p2 = doc.AppendParagraph(customStyle);
            Assert.Same(customStyle, p2.Style);

            var p3 = doc.AppendParagraph(Alignment.Right);
            Assert.Equal(Alignment.Right, p3.Style.Alignment);

            var indent = new Indent(0.5f, 1f, 1f);
            var p4 = doc.AppendParagraph(indent);
            Assert.Equal(Alignment.Left, p4.Style.Alignment);
            Assert.Equal(0.5f, p4.Style.Indent.FirstLine);

            var p5 = doc.AppendParagraph(Alignment.Justified, indent);
            Assert.Equal(Alignment.Justified, p5.Style.Alignment);

            var p6 = doc.AppendParagraph(Alignment.Center, indent, 100, 150);
            Assert.Equal(100, p6.Style.SpaceBefore);
            Assert.Equal(150, p6.Style.SpaceAfter);

            Assert.Equal(6, doc.Paragraphs.Count);
        }

        [Fact]
        public void RTFParser_ToString_GeneratesValidRtfSyntax()
        {
            var doc = new RTFDocument(8.5f, 11f, Orientation.Landscape, Units.Inch);
            doc.Author = "Test Author";
            doc.Version = 2;
            doc.Keywords = new List<string> { "rtf", "test" };

            var p1 = doc.AppendParagraph(Alignment.Center, new Indent(0f, 0.5f, 0.5f), 120, 240);
            p1.AppendText("Title\nWith Line\tAnd Tab", new RTFTextStyle(
                italic: true,
                bold: true,
                smallCaps: true,
                strikeThrough: true,
                allCaps: true,
                outline: true,
                fontSize: 16,
                fontFamily: "Arial",
                color: Color.Red,
                underline: Underline.Double
            ));

            var p2 = doc.AppendParagraph(Alignment.Right);
            p2.AppendText("Normal Text", new RTFTextStyle(
                italic: false,
                bold: false,
                fontSize: 12,
                fontFamily: "Calibri",
                color: Color.Blue
            ));

            // Test all underline styles
            var p3 = doc.AppendParagraph();
            p3.AppendText("Basic").Style.Underline = Underline.Basic;
            p3.AppendText("Thick").Style.Underline = Underline.Thick;
            p3.AppendText("Words").Style.Underline = Underline.WordsOnly;
            p3.AppendText("Wave").Style.Underline = Underline.Wave;
            p3.AppendText("Dotted").Style.Underline = Underline.Dotted;
            p3.AppendText("Dash").Style.Underline = Underline.Dash;
            p3.AppendText("DotDash").Style.Underline = Underline.DotDash;

            string rtf = RTFParser.ToString(doc);

            Assert.StartsWith("{\\rtf1\\ansi\\deff0", rtf);
            Assert.Contains("{\\fonttbl", rtf);
            Assert.Contains("{\\colortbl", rtf);
            Assert.Contains("\\red255\\green0\\blue0", rtf);
            Assert.Contains("\\red0\\green0\\blue255", rtf);
            Assert.Contains("{\\info", rtf);
            Assert.Contains("{\\author Test Author}", rtf);
            Assert.Contains("{\\version2}", rtf);
            Assert.Contains("{\\keywords rtf test }", rtf);
            Assert.Contains("\\landscape", rtf);
            Assert.Contains("\\paperw12240\\paperh15840", rtf);
            Assert.Contains("\\margl1440\\margr1440\\margt1440\\margb1440", rtf);
            Assert.Contains("\\qc", rtf);
            Assert.Contains("\\qr", rtf);
            Assert.Contains("\\li720\\ri720", rtf);
            Assert.Contains("\\sb120\\sa240", rtf);
            Assert.Contains("\\b ", rtf);
            Assert.Contains("\\i ", rtf);
            Assert.Contains("\\scaps ", rtf);
            Assert.Contains("\\strike ", rtf);
            Assert.Contains("\\caps ", rtf);
            Assert.Contains("\\outl ", rtf);
            Assert.Contains("\\uldb ", rtf);
            Assert.Contains("\\ul ", rtf);
            Assert.Contains("\\ulth ", rtf);
            Assert.Contains("\\ulw ", rtf);
            Assert.Contains("\\ulwave ", rtf);
            Assert.Contains("\\uld ", rtf);
            Assert.Contains("\\uldash ", rtf);
            Assert.Contains("\\uldashd ", rtf);
            Assert.Contains("\\line ", rtf);
            Assert.Contains("\\tab ", rtf);
            Assert.EndsWith("}", rtf.TrimEnd());
        }

        [Fact]
        public void RTFParser_ToFile_Overloads_SaveFileToDisk()
        {
            string tempFile1 = Path.GetTempFileName();
            string tempFile2 = Path.GetTempFileName();
            try
            {
                var doc = new RTFDocument();
                doc.AppendParagraph().AppendText("Hello ToFile");
                RTFParser.ToFile(tempFile1, doc);
                Assert.True(File.Exists(tempFile1));
                string content1 = File.ReadAllText(tempFile1);
                Assert.Contains("Hello ToFile", content1);

                RTFParser.ToFile(tempFile2, "Raw Content Test");
                Assert.True(File.Exists(tempFile2));
                string content2 = File.ReadAllText(tempFile2);
                Assert.Equal("Raw Content Test", content2);
            }
            finally
            {
                if (File.Exists(tempFile1)) File.Delete(tempFile1);
                if (File.Exists(tempFile2)) File.Delete(tempFile2);
            }
        }
    }
}
