using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace TheMovies.Model
{
    public class TimeSlotRepo
    {
        public List<TimeSlot> TimeSlots = new List<TimeSlot>();

        public TimeSlotRepo() {}

        public void Add(TimeSlot ts)
        {
            TimeSlots.Add(ts);
        }

        public void Remove(TimeSlot ts)
        {
            TimeSlots.Remove(ts);
        }
    }
}
