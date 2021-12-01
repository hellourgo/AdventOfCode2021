using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day01_SonarSweep
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string inputFileName = "input.txt";
            var inputValues = ReadInput(inputFileName).Select(int.Parse).ToList();
            var diffs = inputValues.Skip(1).Zip(inputValues, GetDifferential).ToList();
            // diffs.ForEach(Console.WriteLine);
            
            var increases = diffs.Count(d => d.Delta > 0);
            Console.WriteLine($"{increases} measurements larger than the previous measurement.");
        }

        private static IEnumerable<string> ReadInput(string fileName)
        {
            var resourceName = GetResourceName(fileName);

            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            using var streamReader = new StreamReader(stream);
            while (!streamReader.EndOfStream)
            {
                yield return streamReader.ReadLine();
            }
        }

        private static string GetResourceName(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .SingleOrDefault(n => n.EndsWith(fileName));
        }

        private static Differential GetDifferential(int current, int previous) =>
            new Differential
            {
                CurrentValue = current,
                PreviousValue = previous
            };
    }
}