using Xunit;
using RTFExporter;

namespace RTFExporter.test
{
    public class TextAndParagraphTests
    {
        [Fact]
        public void RTFText_Constructors_AndFluentSetStyle()
        {
            var doc = new RTFDocument();
            var par = new RTFParagraph(doc);

            var text = new RTFText(par, "Hello World");
            Assert.Equal("Hello World", text.Content);
            Assert.Contains(text, par.Text);

            var customStyle = new RTFTextStyle(true, false, 12, "Arial", Color.Red);
            var textWithStyle = new RTFText(par, "With Style", customStyle);
            Assert.Same(customStyle, textWithStyle.Style);

            // Test SetStyle default overload
            var t1 = par.AppendText("Test").SetStyle();
            Assert.Equal("Calibri", t1.Style.FontFamily);
            Assert.Equal(12, t1.Style.FontSize);
            Assert.Equal(Color.Black, t1.Style.Color);
            Assert.Equal(Underline.None, t1.Style.Underline);

            // Test SetStyle overload 2
            var t2 = par.AppendText("Test").SetStyle(Color.Blue, 16, "Verdana");
            Assert.Equal(Color.Blue, t2.Style.Color);
            Assert.Equal(16, t2.Style.FontSize);
            Assert.Equal("Verdana", t2.Style.FontFamily);

            // Test SetStyle overload 3
            var t3 = par.AppendText("Test").SetStyle(Color.Green, true, true, 20, "Courier");
            Assert.Equal(Color.Green, t3.Style.Color);
            Assert.True(t3.Style.Italic);
            Assert.True(t3.Style.Bold);
            Assert.Equal(20, t3.Style.FontSize);
            Assert.Equal("Courier", t3.Style.FontFamily);

            // Test SetStyle overload 4
            var t4 = par.AppendText("Test").SetStyle(true, true, Underline.Wave, true, true, false, true);
            Assert.True(t4.Style.Italic);
            Assert.True(t4.Style.Bold);
            Assert.Equal(Underline.Wave, t4.Style.Underline);
            Assert.True(t4.Style.SmallCaps);
            Assert.True(t4.Style.StrikeThrough);
            Assert.False(t4.Style.AllCaps);
            Assert.True(t4.Style.Outline);
        }

        [Fact]
        public void RTFParagraph_ConstructorsAndAppendText()
        {
            var doc = new RTFDocument();
            var par = new RTFParagraph(doc);
            Assert.Contains(par, doc.Paragraphs);

            var t1 = par.AppendText("First run");
            Assert.Equal("First run", t1.Content);
            Assert.Contains(t1, par.Text);

            var style = new RTFTextStyle(false, true, 14, "Arial", Color.Blue);
            var t2 = par.AppendText("Second run", style);
            Assert.Equal("Second run", t2.Content);
            Assert.Same(style, t2.Style);
            Assert.Equal(2, par.Text.Count);
        }
    }
}
