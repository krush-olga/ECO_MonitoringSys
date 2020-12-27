using System;

namespace Data.Entity
{
    public class Emission
    {
        public int Id { get; set; }
        public int ElementId { get; set; }
        public int EnvironmentId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public double MaxValue { get; set; }
        public double AvgValue { get; set; }

        public override string ToString()
        {
            return $"Max value = {MaxValue}; Average value = {AvgValue}";
        }
    }
}
