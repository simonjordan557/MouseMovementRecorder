using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovementRecorderLibrary
{
    [Serializable]
    public class ScreenPosition
    {
        public double xpos { get; set; }
        public double ypos { get; set; }
        public bool leftButtonClicked { get; set; }

        public ScreenPosition(double x, double y, bool click)
        {
            this.xpos = x;
            this.ypos = y;
            this.leftButtonClicked = click;
        }
    }
}
