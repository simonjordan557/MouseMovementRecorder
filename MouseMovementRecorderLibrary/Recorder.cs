using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;

namespace MouseMovementRecorderLibrary
{
    public class Recorder
    {
        private InputSimulator inputSimulator = new InputSimulator();

        private double ConvertToX(double rawX, int maxX)
        {
            return (rawX * 65535d / (double)(maxX - 1));
        }

        private double ConvertToY(double rawY, int maxY)
        {
            return (rawY * 65535d / (double)(maxY - 1));
        }

        public void Play(MovementRecord recording)
        {
            // Get playback screen resolution to compare to recorded screen resolution

            Process heightProcess = Process.Start(@"L:\Backup 19.1.18\Gamedev\Visual Studio 2019\My Programs\MouseMovementRecorder\MouseMovementRecorder\bin\Release\net6.0\GetScreenHeight.exe");
            heightProcess.WaitForExit();
            int screenHeight = heightProcess.ExitCode;

            Process widthProcess = Process.Start(@"L:\Backup 19.1.18\Gamedev\Visual Studio 2019\My Programs\MouseMovementRecorder\MouseMovementRecorder\bin\Release\net6.0\GetScreenWidth.exe");
            widthProcess.WaitForExit();
            int screenWidth = widthProcess.ExitCode;

            Console.WriteLine($"Detected PLAYBACK screen resolution {screenWidth} x {screenHeight}.");
            Console.WriteLine($"Detected RECORDED screen resolution {recording.recordedWidth} x {recording.recordedHeight}.");
   
            for (int i = 0; i < recording.course.Count; i++)
            {
                
                inputSimulator.Mouse.MoveMouseTo(ConvertToX(recording[i].xpos, recording.recordedWidth), ConvertToY(recording[i].ypos, recording.recordedHeight));
                if (recording[i].leftButtonClicked) {
                    inputSimulator.Mouse.LeftButtonClick();
                }
                Thread.Sleep(8);
            }
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

            Process heightProcess = Process.Start(@"L:\Backup 19.1.18\Gamedev\Visual Studio 2019\My Programs\MouseMovementRecorder\MouseMovementRecorder\bin\Release\net6.0\GetScreenHeight.exe");
            heightProcess.WaitForExit();
            int screenHeight = heightProcess.ExitCode;

            Process widthProcess = Process.Start(@"L:\Backup 19.1.18\Gamedev\Visual Studio 2019\My Programs\MouseMovementRecorder\MouseMovementRecorder\bin\Release\net6.0\GetScreenWidth.exe");
            widthProcess.WaitForExit();
            int screenWidth = widthProcess.ExitCode;

            Console.WriteLine($"Detected RECORDING screen resolution {screenWidth} x {screenHeight}.");

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
                course.Add(new ScreenPosition(inputSimulator.Mouse.Position.X, inputSimulator.Mouse.Position.Y, inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.LBUTTON)));
                Thread.Sleep(4);
                recordTimer -= 16;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine("Mouse Movement Saved!");
            Thread.Sleep(2500);
            return new MovementRecord(course, screenHeight, screenWidth);
        }
    }
}

