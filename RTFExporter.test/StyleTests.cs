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
            Assert.Equal(0.5f, indent.FirstLine);
            Assert.Equal(1.2f, indent.Left);
            Assert.Equal(0.8f, indent.Right);
        }

        [Fact]
        public void Margin_Constructor_SetsFields()
        {
            var margin = new Margin(1f, 2f, 3f, 4f);
            Assert.Equal(1f, margin.Left);
            Assert.Equal(2f, margin.Right);
            Assert.Equal(3f, margin.Top);
            Assert.Equal(4f, margin.Bottom);
        }

        [Fact]
        public void RTFParagraphStyle_Constructors_WorkCorrectly()
        {
            var s1 = new RTFParagraphStyle(Alignment.Center);
            Assert.Equal(Alignment.Center, s1.Alignment);

            var indent = new Indent(1f, 2f, 3f);
            var s2 = new RTFParagraphStyle(Alignment.Right, indent);
            Assert.Equal(Alignment.Right, s2.Alignment);
            Assert.Equal(1f, s2.Indent.FirstLine);

            var s3 = new RTFParagraphStyle(Alignment.Justified, indent, 100, 200);
            Assert.Equal(Alignment.Justified, s3.Alignment);
            Assert.Equal(100, s3.SpaceBefore);
            Assert.Equal(200, s3.SpaceAfter);
        }

        [Fact]
        public void RTFParagraphStyle_DocumentConstructor_SetsDefaultIndentBasedOnUnits()
        {
            var docInch = new RTFDocument(units: Units.Inch);
            var styleInch = new RTFParagraphStyle(docInch);
            Assert.Equal(1f, styleInch.Indent.FirstLine);
            Assert.Equal(0f, styleInch.Indent.Left);
            Assert.Equal(0f, styleInch.Indent.Right);

            var docMm = new RTFDocument(units: Units.Millimeters);
            var styleMm = new RTFParagraphStyle(docMm);
            Assert.Equal(25.4f, styleMm.Indent.FirstLine);

            var docCm = new RTFDocument(units: Units.Centimeters);
            var styleCm = new RTFParagraphStyle(docCm);
            Assert.Equal(2.54f, styleCm.Indent.FirstLine);
        }

        [Fact]
        public void RTFTextStyle_SimpleConstructor_SetsFields()
        {
            var color = new Color(1, 2, 3);
            var style = new RTFTextStyle(true, false, 14, "Arial", color);
            Assert.True(style.Italic);
            Assert.False(style.Bold);
            Assert.Equal(14, style.FontSize);
            Assert.Equal("Arial", style.FontFamily);
            Assert.Same(color, style.Color);
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

            Assert.True(style.Italic);
            Assert.True(style.Bold);
            Assert.True(style.SmallCaps);
            Assert.True(style.StrikeThrough);
            Assert.True(style.AllCaps);
            Assert.True(style.Outline);
            Assert.Equal(18, style.FontSize);
            Assert.Equal("Times New Roman", style.FontFamily);
            Assert.Same(color, style.Color);
            Assert.Equal(Underline.Double, style.Underline);
        }
    }
}
