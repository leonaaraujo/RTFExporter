using Xunit;
using RTFExporter;

namespace RTFExporter.test
{
    public class ColorTests
    {
        [Fact]
        public void Constructor_SetsRgbValues()
        {
            var color = new Color(10, 20, 30);
            Assert.Equal(10, color.r);
            Assert.Equal(20, color.g);
            Assert.Equal(30, color.b);
            Assert.Equal(0, color.index);
        }

        [Fact]
        public void PredefinedColors_HaveCorrectRgbValues()
        {
            Assert.Equal(0, Color.black.r);
            Assert.Equal(0, Color.black.g);
            Assert.Equal(0, Color.black.b);

            Assert.Equal(255, Color.white.r);
            Assert.Equal(255, Color.white.g);
            Assert.Equal(255, Color.white.b);

            Assert.Equal(255, Color.red.r);
            Assert.Equal(0, Color.red.g);
            Assert.Equal(0, Color.red.b);

            Assert.Equal(0, Color.green.r);
            Assert.Equal(255, Color.green.g);
            Assert.Equal(0, Color.green.b);

            Assert.Equal(0, Color.blue.r);
            Assert.Equal(0, Color.blue.g);
            Assert.Equal(255, Color.blue.b);

            Assert.Equal(255, Color.yellow.r);
            Assert.Equal(255, Color.yellow.g);
            Assert.Equal(0, Color.yellow.b);

            Assert.Equal(255, Color.purple.r);
            Assert.Equal(0, Color.purple.g);
            Assert.Equal(255, Color.purple.b);

            Assert.Equal(0, Color.cyan.r);
            Assert.Equal(255, Color.cyan.g);
            Assert.Equal(255, Color.cyan.b);
        }
    }
}
