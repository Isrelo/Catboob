using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Threading;

namespace CatboobGGStream
{
    public class EventTimer
    {
        private DispatcherTimer event_timer;

        public EventTimer()
        {
            event_timer = new DispatcherTimer();
        }

        public void StartTimer(TimeSpan time_interval, EventHandler elapsed_event)
        {
            event_timer.Interval = time_interval;
            event_timer.Tick += elapsed_event;
            event_timer.Start();
        }

        public void StopTimer()
        {
            event_timer.Stop();
        }   
    }
}
