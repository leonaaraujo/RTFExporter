using Xunit;
using RTFExporter;

namespace RTFExporter.test
{
    public class StyleTests
    {
        [Fact]
        public void Indent_Constructor_SetsFields()
        {
            var indent = new Indent(0.5f, 1.2f, 0.8f);
            Assert.Equal(0.5f, indent.firstLine);
            Assert.Equal(1.2f, indent.left);
            Assert.Equal(0.8f, indent.right);
        }

        [Fact]
        public void Margin_Constructor_SetsFields()
        {
            var margin = new Margin(1f, 2f, 3f, 4f);
            Assert.Equal(1f, margin.left);
            Assert.Equal(2f, margin.right);
            Assert.Equal(3f, margin.top);
            Assert.Equal(4f, margin.bottom);
        }

        [Fact]
        public void RTFParagraphStyle_Constructors_WorkCorrectly()
        {
            var s1 = new RTFParagraphStyle(Alignment.Center);
            Assert.Equal(Alignment.Center, s1.alignment);

            var indent = new Indent(1f, 2f, 3f);
            var s2 = new RTFParagraphStyle(Alignment.Right, indent);
            Assert.Equal(Alignment.Right, s2.alignment);
            Assert.Equal(1f, s2.indent.firstLine);

            var s3 = new RTFParagraphStyle(Alignment.Justified, indent, 100, 200);
            Assert.Equal(Alignment.Justified, s3.alignment);
            Assert.Equal(100, s3.spaceBefore);
            Assert.Equal(200, s3.spaceAfter);
        }

        [Fact]
        public void RTFParagraphStyle_DocumentConstructor_SetsDefaultIndentBasedOnUnits()
        {
            var docInch = new RTFDocument(units: Units.Inch);
            var styleInch = new RTFParagraphStyle(docInch);
            Assert.Equal(1f, styleInch.indent.firstLine);
            Assert.Equal(0f, styleInch.indent.left);
            Assert.Equal(0f, styleInch.indent.right);

            var docMm = new RTFDocument(units: Units.Millimeters);
            var styleMm = new RTFParagraphStyle(docMm);
            Assert.Equal(25.4f, styleMm.indent.firstLine);

            var docCm = new RTFDocument(units: Units.Centimeters);
            var styleCm = new RTFParagraphStyle(docCm);
            Assert.Equal(2.54f, styleCm.indent.firstLine);
        }

        [Fact]
        public void RTFTextStyle_SimpleConstructor_SetsFields()
        {
            var color = new Color(1, 2, 3);
            var style = new RTFTextStyle(true, false, 14, "Arial", color);
            Assert.True(style.italic);
            Assert.False(style.bold);
            Assert.Equal(14, style.fontSize);
            Assert.Equal("Arial", style.fontFamily);
            Assert.Same(color, style.color);
        }

        [Fact]
        public void RTFTextStyle_CompleteConstructor_SetsAllFields()
        {
            var color = new Color(10, 20, 30);
            var style = new RTFTextStyle(
                italic: true,
                bold: true,
                smallCaps: true,
                strikeThrough: true,
                allCaps: true,
                outline: true,
                fontSize: 18,
                fontFamily: "Times New Roman",
                color: color,
                underline: Underline.Double
            );

            Assert.True(style.italic);
            Assert.True(style.bold);
            Assert.True(style.smallCaps);
            Assert.True(style.strikeThrough);
            Assert.True(style.allCaps);
            Assert.True(style.outline);
            Assert.Equal(18, style.fontSize);
            Assert.Equal("Times New Roman", style.fontFamily);
            Assert.Same(color, style.color);
            Assert.Equal(Underline.Double, style.underline);
        }
    }
}
