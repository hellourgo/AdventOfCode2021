namespace Day01_SonarSweep
{
    internal class Differential
    {
        public int CurrentValue { get; set; }
        public int PreviousValue { get; set; }
        public int Delta => CurrentValue - PreviousValue;

        public override string ToString()
        {
            return $"{PreviousValue} -> {CurrentValue} (Delta: {Delta})";
        }
    }
}