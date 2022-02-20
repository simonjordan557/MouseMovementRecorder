using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovementRecorderLibrary
{
    [Serializable]
    public class MovementRecord
    {
        public List<ScreenPosition> course;
        public int recordedHeight;
        public int recordedWidth;
        public MovementRecord(List<ScreenPosition> inCourse, int height, int width)
        {
            this.course = inCourse;
            this.recordedHeight = height;
            this.recordedWidth = width;
        }

        public ScreenPosition this[int index]
        {
            get { return this.course[index]; }
        }
    }
}
