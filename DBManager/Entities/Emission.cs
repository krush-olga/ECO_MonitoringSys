using System;
using System.ComponentModel;

namespace Data.Entity
{
    public class Emission : INotifyPropertyChanged
    {
        private int year;
        private int month;
        private int day;
        private double maxValue;
        private double avgValue;

        private Environment environment;
        private Element element;

        public Emission()
        {
            Month = 1;
            Day = 1;
        }

        public int Id { get; set; }
        public double AvgValue
        {
            get { return avgValue; }
            set
            {
                avgValue = value;
                OnPropertyChanged();
            }
        }
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                OnPropertyChanged();
            }
        }
        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged();
            }
        }
        public int Month
        {
            get { return month; }
            set
            {
                month = value;
                OnPropertyChanged();
            }
        }
        public int Day
        {
            get { return day; }
            set
            {
                day = value;
                OnPropertyChanged();
            }
        }

        public Element Element
        {
            get { return element; }
            set
            {
                element = value;
                OnPropertyChanged();
            }
        }
        public Environment Environment
        {
            get { return environment; }
            set
            {
                environment = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"Max value = {MaxValue}; Average value = {AvgValue}";
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
