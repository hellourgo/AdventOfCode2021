using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day01_SonarSweep
{
    internal class SlidingWindow
    {
        public int FirstWindow { get; set; }
        public int SecondWindow { get; set; }
        public int ThirdWindow { get; set; }
        public int Total => FirstWindow + SecondWindow + ThirdWindow;

        public override string ToString()
        {
            return $"{FirstWindow} + {SecondWindow} + {ThirdWindow} = {Total}";
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            const string inputFileName = "input.txt";
            var inputValues = ReadInput(inputFileName).Select(int.Parse).ToList();
            var sliders = inputValues
                .Zip(inputValues.Skip(1), GetSlidingWindow)
                .Zip(inputValues.Skip(2), AddToSlidingWindow)
                .ToList();
            // sliders.ForEach(Console.WriteLine);
            var sliderTotals = sliders.Select(s => s.Total).ToList();
            var diffs = GetDifferentials(sliderTotals);
            // diffs.ForEach(Console.WriteLine);
            
            var increases = diffs.Count(d => d.Delta > 0);
            Console.WriteLine($"{increases} measurements larger than the previous measurement.");
        }

        private static List<Differential> GetDifferentials(List<int> inputValues)
        {
            return inputValues.Skip(1).Zip(inputValues, GetDifferential).ToList();
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

        private static SlidingWindow GetSlidingWindow(int first, int second) =>
            new SlidingWindow
            {
                FirstWindow = first,
                SecondWindow = second
            };

        private static SlidingWindow AddToSlidingWindow(SlidingWindow slidingWindow, int third)
        {
            slidingWindow.ThirdWindow = third;
            return slidingWindow;
        }
    }
}