using System;
using System.Windows.Media;

namespace PomodoroWP
{
    public class Pomodoro
    {
        public int PomodoroInterval { get; set; }
        public int ShortRestInterval { get; set; }
        public int LongRestInterval { get; set; }
        public int RedInterval { get; set; }
        private DateTime startTime;
        private bool isPomodoro = true; // false = break


        // todo - get configuration values from isolated storage
        public Pomodoro()
        {
            PomodoroInterval = 25;
            ShortRestInterval = 5;
            LongRestInterval = 20;
            RedInterval = 5;
            startTime = DateTime.Now; // -TimeSpan.FromSeconds( 55 );
            isPomodoro = true;
        }




        public string GetDisplayString(out Color newColor, out bool isCompleted)
        {
            TimeSpan timePassed = DateTime.Now - startTime;
            double totalMinutesPassed = timePassed.TotalMinutes;
            int minutesPassed = timePassed.Minutes;
            int secondsPassed = timePassed.Seconds;
            isCompleted = false;

            if (secondsPassed > 0)
            {
                minutesPassed++;
            }

            if (totalMinutesPassed >= PomodoroInterval)
            {
                minutesPassed = 0;
                secondsPassed = 0;
                isCompleted = true;
            }

            if (totalMinutesPassed > PomodoroInterval - RedInterval)
            {
                newColor = Colors.Red;
            }
            else
            {
                newColor = Colors.White;
            }

            string displayString = string.Format( "{0:D2}", PomodoroInterval - minutesPassed ) + ":";

            if (secondsPassed == 0)
            {
                displayString += "00";
            }
            else
            {
                displayString += string.Format( "{0:D2}", 60 - secondsPassed );
            }

           return displayString;
        }
    }
}
