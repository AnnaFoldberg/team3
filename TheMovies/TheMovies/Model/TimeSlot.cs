namespace TheMovies.Model
{
    public class TimeSlot
    {
        private Cinema _cinema;
        private Hall _hall;
        private DateOnly _dateOnly;
        private TimeOnly _timeOnly;

        public Cinema Cinema { get { return _cinema; } set { _cinema = value; } }
        public Hall Hall { get { return _hall; } set { _hall = value; } }
        public DateOnly Date { get { return _dateOnly; } set { _dateOnly = value; } }
        public TimeOnly Time { get { return _timeOnly; } set { _timeOnly = value; } }

        public TimeSlot(Cinema cinema, Hall hall, DateOnly date, TimeOnly time)
        {
            Cinema = cinema;
            Hall = hall;
            Date = date;
            Time = time;
        }
    }
}
