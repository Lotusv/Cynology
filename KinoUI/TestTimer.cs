using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinoUI
{
    class TestTimer
    {

        #region timewatch
        private System.Windows.Forms.Timer _timer;

        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;

        // Time between now and when the timer was started last
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time timer was started after a reset
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        // Whether or not the timer is currently running
        private bool _timerRunning = false;

        #endregion

        public TestTimer()
        {
            _timer = new System.Windows.Forms.Timer();

            _startTime = DateTime.Now;
            _timer.Start();
        }

        public void TimerTick(String _from)
        {
            // We do this to chop off any stray milliseconds resulting from 
            // the Timer's inherent inaccuracy, with the bonus that the 
            // TimeSpan.ToString() method will now show correct HH:MM:SS format
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);

            // The current elapsed time is the time since the start button was
            // clicked, plus the total time elapsed since the last reset

            _currentElapsedTime = timeSinceStartTime - _currentElapsedTime;

            System.Diagnostics.Debug.WriteLine(_from + " _currentElapsedTime: " + _currentElapsedTime.ToString());
            System.Diagnostics.Debug.WriteLine(_from + "timeSinceStartTime: " + timeSinceStartTime.ToString());
        }
    }
}
