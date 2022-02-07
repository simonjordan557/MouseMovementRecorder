namespace MouseMovementRecorderLibrary
{
    [Serializable]
    public class ScreenPosition
    {
        public double xpos { get; set; }
        public double ypos { get; set; }

        public ScreenPosition(double x, double y)
        {
            this.xpos = x;
            this.ypos = y;
        }
    }
}