using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;

namespace MouseMovementRecorderLibrary
{
    public class Recorder
    {
        private InputSimulator inputSimulator = new InputSimulator();

        private double ConvertToX(double rawX)
        {
            return (rawX * 65535d / 1919d);
        }

        private double ConvertToY(double rawY)
        {
            return (rawY * 65535d / 1079d);
        }

        /// <summary>
        /// Record mouse movement for a given number of seconds
        /// </summary>
        /// <param name="timeToRecord">How many seconds to record for</param>
        /// <param name="delay">How many seconds countdown before recording begins</param>
        /// <returns></returns>
        public MovementRecord Record(int timeToRecord, int delay)
        {
            List<ScreenPosition> course = new List<ScreenPosition>();

            for (int i = delay; i >= 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Recording starts in: {i}...");
                if (i == 0)
                {
                    break;
                }
                Thread.Sleep(1000);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            int recordTimer = timeToRecord * 1000;
            while (recordTimer > 0)
            {
                Console.Clear();
                Console.WriteLine($"RECORDING! {Math.Ceiling(recordTimer / 1000d)} SECONDS REMAINING");
                course.Add(new ScreenPosition(inputSimulator.Mouse.Position.X, inputSimulator.Mouse.Position.Y));
                Thread.Sleep(8);
                recordTimer -= 16;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine("Mouse Movement Saved!");
            Thread.Sleep(2500);
            return new MovementRecord(course);
        }

        public void Play(MovementRecord recording)
        {
            for (int i = 0; i < recording.course.Count; i++)
            {
                Console.WriteLine($"X: {recording[i].xpos}, Y: {recording[i].ypos}");
                inputSimulator.Mouse.MoveMouseTo(ConvertToX(recording[i].xpos), ConvertToY(recording[i].ypos));
                Thread.Sleep(8);
            }
        }
    }
}

