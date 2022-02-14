using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovementRecorderLibrary
{
    public static class ImportExport
    {
        public static bool CheckForExistingFiles()
        {
            string movementDirectory = Directory.GetCurrentDirectory() + @"\movements\";
            if (!Directory.Exists(movementDirectory)) return false;

            return Directory.GetFiles(movementDirectory).Length > 0;
        }

        public static Dictionary<string, List<MovementRecord>> ImportAllFiles()
        {
            string movementDirectory = Directory.GetCurrentDirectory() + @"\movements\";
            Dictionary<string, List<MovementRecord>> result = new Dictionary<string, List<MovementRecord>>();
            foreach (string entry in Directory.GetFiles(movementDirectory))
            {
                string key = entry.Remove(0, 130);
                key = key.Remove(key.Length - 5, 5);
                List<MovementRecord> value = ImportFromFile(key);
                result[key] = value;
                Console.WriteLine($"Imported {key}...");
            }
            return result;

        }
        public static void ExportToFile(string fileName, List<MovementRecord> data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string targetDirectory = Directory.GetCurrentDirectory() + @"\movements\";
            string file = targetDirectory + fileName.ToUpper() + ".mvmt";
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            FileStream fs = File.Create(file);
            bf.Serialize(fs, data);
            fs.Close();

            Console.WriteLine($"Saving movements to {file}...");
            Thread.Sleep(2000);
        }

        public static List<MovementRecord> ImportFromFile(string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string file = Directory.GetCurrentDirectory() + @"\movements\" + fileName + ".mvmt";
            if (File.Exists(file))
            {
                FileStream fs = File.Open(file, FileMode.Open);
                List<MovementRecord> result = (List<MovementRecord>)bf.Deserialize(fs);
                fs.Close();
                return result;
            }
            else
            {
                Console.WriteLine("File Not Found.");
                return null;
            }
        }
    }
}
