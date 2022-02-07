using System;
using MouseMovementRecorderLibrary;

Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.Clear();
Console.WriteLine("Welcome To Mouse Movement Recorder!");
Recorder recorder = new Recorder();
List<MovementRecord> recordedMovements = new List<MovementRecord>();
Dictionary<string, List<MovementRecord>> movementDictionary = new Dictionary<string, List<MovementRecord>>();
bool playIsOn;
bool recordIsOn;

if (ImportExport.CheckForExistingFiles())
{
    Console.WriteLine("Existing recordings found. Importing...");
    movementDictionary = ImportExport.ImportAllFiles();
}

while (true)
{
    recordIsOn = false;
    playIsOn = false;
    Console.WriteLine("(P)lay existing movements, (R)ecord new ones, or e(X)it?");

    bool gotPlayOrRecord = false;
    string playOrRecordInput = "";
    while (!gotPlayOrRecord)
    {
        playOrRecordInput = Console.ReadLine();
        (gotPlayOrRecord, playOrRecordInput) = InputValidator.ValidateStringContainsAcceptableInput(playOrRecordInput, new List<string>() { "P", "R", "X" });
    }

    if (playOrRecordInput == "R")
    {
        recordIsOn = true;
    }

    else if (playOrRecordInput == "P")
    {
        playIsOn = true;
    }

    else if (playOrRecordInput == "X")
    {
        Console.WriteLine("\nThanks for using Mouse Movement Recorder.");
        Thread.Sleep(2500);
        Environment.Exit(0);
    }


    while (playIsOn)
    {
        string recordingToPlayInput = "";
        bool gotRecordingToPlay = false;
        Console.WriteLine("Play back which set of recordings? ");
        foreach (string record in movementDictionary.Keys)
        {
            Console.WriteLine(record);
        }

        Console.WriteLine();

        while (!gotRecordingToPlay)
        {
            recordingToPlayInput = Console.ReadLine();
            (gotRecordingToPlay, recordingToPlayInput) = InputValidator.ValidateStringContainsAcceptableInput(recordingToPlayInput, movementDictionary.Keys.ToList());
        }

        List<MovementRecord> recordingToPlay = movementDictionary[recordingToPlayInput];
        foreach (MovementRecord m in recordingToPlay)
        {
            for (int i = 3; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"Playing movement {recordingToPlay.IndexOf(m) + 1} of {recordingToPlay.Count} in {i}...");
                Thread.Sleep(1000);
            }

            recorder.Play(m);
        }
        playIsOn = false;
        Console.Clear();
    }

    while (recordIsOn)
    {
        recordedMovements.Clear();

        bool gotRecordingLength = false;
        bool gotDelayLength = false;
        bool gotNumberOfRecordings = false;
        bool gotHappyInput;
        bool gotIsOn;

        int recordingLength = 0;
        int delayLength = 0;
        int numberOfRecordings = 0;
        string happyInput = "";
        string isOnInput = "";



        Console.WriteLine("How many seconds do you want to record movement for?  ");

        while (!gotRecordingLength)
        {
            string recordingLengthInput = Console.ReadLine();
            (gotRecordingLength, recordingLength) = InputValidator.ValidatePositiveInteger(recordingLengthInput);
        }


        Console.WriteLine("How many seconds countdown do you need before recording?  ");

        while (!gotDelayLength)
        {
            string delayLengthInput = Console.ReadLine();
            (gotDelayLength, delayLength) = InputValidator.ValidatePositiveInteger(delayLengthInput);
        }

        Console.WriteLine("How many recordings do you want to make?  ");

        while (!gotNumberOfRecordings)
        {
            string numberOfRecordingsInput = Console.ReadLine();
            (gotNumberOfRecordings, numberOfRecordings) = InputValidator.ValidatePositiveInteger(numberOfRecordingsInput);
        }

        for (int j = 0; j < numberOfRecordings; j++)
        {
            MovementRecord m = recorder.Record(recordingLength, delayLength);
            Console.ForegroundColor = ConsoleColor.White;
            gotHappyInput = false;

            for (int i = 5; i >= 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"PLAYING BACK MOVEMENT IN {i}...");
                Thread.Sleep(1000);
            }
            recorder.Play(m);
            Console.WriteLine("Are you happy with this recording (Y/N)? ");

            while (!gotHappyInput)
            {
                happyInput = Console.ReadLine();
                (gotHappyInput, happyInput) = InputValidator.ValidateStringContainsAcceptableInput(happyInput, new List<string> { "Y", "N" });
            }
            if (happyInput == "Y")
            {
                Console.WriteLine($"Recording {j + 1} of {numberOfRecordings} saved.");
                Thread.Sleep(2500);
                recordedMovements.Add(m);
            }
            else if (happyInput == "N")
            {
                j--;
            }
        }

        Console.WriteLine("Please enter a name for this set of movements: ");
        string fileNameInput = Console.ReadLine().ToUpper();
        ImportExport.ExportToFile(fileNameInput, recordedMovements);
        movementDictionary[fileNameInput] = recordedMovements;

        gotIsOn = false;
        Console.WriteLine("Record more movements (Y/N)? ");
        while (!gotIsOn)
        {
            isOnInput = Console.ReadLine();
            (gotIsOn, isOnInput) = InputValidator.ValidateStringContainsAcceptableInput(isOnInput, new List<string>() { "Y", "N" });
        }

        if (isOnInput == "N")
        {
            recordIsOn = false;
            Console.Clear();
        }
    }
}
