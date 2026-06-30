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
            Assert.Equal("Hello World", text.content);
            Assert.Contains(text, par.text);

            var customStyle = new RTFTextStyle(true, false, 12, "Arial", Color.red);
            var textWithStyle = new RTFText(par, "With Style", customStyle);
            Assert.Same(customStyle, textWithStyle.style);

            // Test SetStyle default overload
            var t1 = par.AppendText("Test").SetStyle();
            Assert.Equal("Calibri", t1.style.fontFamily);
            Assert.Equal(12, t1.style.fontSize);
            Assert.Equal(Color.black, t1.style.color);
            Assert.Equal(Underline.None, t1.style.underline);

            // Test SetStyle overload 2
            var t2 = par.AppendText("Test").SetStyle(Color.blue, 16, "Verdana");
            Assert.Equal(Color.blue, t2.style.color);
            Assert.Equal(16, t2.style.fontSize);
            Assert.Equal("Verdana", t2.style.fontFamily);

            // Test SetStyle overload 3
            var t3 = par.AppendText("Test").SetStyle(Color.green, true, true, 20, "Courier");
            Assert.Equal(Color.green, t3.style.color);
            Assert.True(t3.style.italic);
            Assert.True(t3.style.bold);
            Assert.Equal(20, t3.style.fontSize);
            Assert.Equal("Courier", t3.style.fontFamily);

            // Test SetStyle overload 4
            var t4 = par.AppendText("Test").SetStyle(true, true, Underline.Wave, true, true, false, true);
            Assert.True(t4.style.italic);
            Assert.True(t4.style.bold);
            Assert.Equal(Underline.Wave, t4.style.underline);
            Assert.True(t4.style.smallCaps);
            Assert.True(t4.style.strikeThrough);
            Assert.False(t4.style.allCaps);
            Assert.True(t4.style.outline);
        }

        [Fact]
        public void RTFParagraph_ConstructorsAndAppendText()
        {
            var doc = new RTFDocument();
            var par = new RTFParagraph(doc);
            Assert.Contains(par, doc.paragraphs);

            var t1 = par.AppendText("First run");
            Assert.Equal("First run", t1.content);
            Assert.Contains(t1, par.text);

            var style = new RTFTextStyle(false, true, 14, "Arial", Color.blue);
            var t2 = par.AppendText("Second run", style);
            Assert.Equal("Second run", t2.content);
            Assert.Same(style, t2.style);
            Assert.Equal(2, par.text.Count);
        }
    }
}
