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
        public MovementRecord(List<ScreenPosition> inCourse)
        {
            this.course = inCourse;
        }

        public ScreenPosition this[int index]
        {
            get { return this.course[index]; }
            
        }
    }
}
