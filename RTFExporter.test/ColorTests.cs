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
            Assert.Equal(10, color.R);
            Assert.Equal(20, color.G);
            Assert.Equal(30, color.B);
            Assert.Equal(0, color.Index);
        }

        [Fact]
        public void PredefinedColors_HaveCorrectRgbValues()
        {
            Assert.Equal(0, Color.Black.R);
            Assert.Equal(0, Color.Black.G);
            Assert.Equal(0, Color.Black.B);

            Assert.Equal(255, Color.White.R);
            Assert.Equal(255, Color.White.G);
            Assert.Equal(255, Color.White.B);

            Assert.Equal(255, Color.Red.R);
            Assert.Equal(0, Color.Red.G);
            Assert.Equal(0, Color.Red.B);

            Assert.Equal(0, Color.Green.R);
            Assert.Equal(255, Color.Green.G);
            Assert.Equal(0, Color.Green.B);

            Assert.Equal(0, Color.Blue.R);
            Assert.Equal(0, Color.Blue.G);
            Assert.Equal(255, Color.Blue.B);

            Assert.Equal(255, Color.Yellow.R);
            Assert.Equal(255, Color.Yellow.G);
            Assert.Equal(0, Color.Yellow.B);

            Assert.Equal(255, Color.Purple.R);
            Assert.Equal(0, Color.Purple.G);
            Assert.Equal(255, Color.Purple.B);

            Assert.Equal(0, Color.Cyan.R);
            Assert.Equal(255, Color.Cyan.G);
            Assert.Equal(255, Color.Cyan.B);
        }
    }
}
