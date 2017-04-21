namespace RTFExporter {

    public class Color {
        public byte r;
        public byte g;
        public byte b;
        public int index;
        public static Color black = new Color(0, 0, 0);
        public static Color white = new Color(255, 255, 255);
        public static Color red = new Color(255, 0, 0);
        public static Color green = new Color(0, 255, 0);
        public static Color blue = new Color(0, 0, 255);
        public static Color yellow = new Color(255, 255, 0);
        public static Color purple = new Color(255, 0, 255);
        public static Color cyan = new Color(0, 255, 255);

        public Color(byte r, byte g, byte b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

}
