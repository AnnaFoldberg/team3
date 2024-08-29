namespace TheMovies.Model
{
    public class TimeSlotRepo
    {
        public List<TimeSlot> TimeSlots;

        public TimeSlotRepo() 
        {
            TimeSlots = new List<TimeSlot>();
        }

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
