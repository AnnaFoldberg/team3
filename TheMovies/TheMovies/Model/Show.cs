using TheMovies.Model;

namespace TheMovies
{
    public class Show
        {
            public Cinema Cinema { get; set; }
            public Hall Hall { get; set; }
            public DateOnly Date { get; set; }
            public TimeOnly Time { get; set;}
            public Movie Mov { get; set; }
            public AdditionalTime AdditionalTime { get; set; }
            public TimeSpan TotalDuration { get; set; }

            public Show(TimeSlot timeslot, Movie movie, AdditionalTime additionalTime) //Constructor med 5 parametere Cinema, Hall, timeslot, Movie, AdditionalTime 
            {
                TimeSlot Ts = timeslot;
                Cinema = Ts.Cinema;
                Hall = Ts.Hall;
                Date = Ts.Date;
                Time = Ts.Time;
                Mov = movie;
                AdditionalTime = additionalTime;
                TotalDuration = additionalTime.TotalAdditionalTime + Mov.Duration;
            }

            public override string ToString()
            {
                return $"{Mov}, {Cinema}, {Hall}, {Date}, {Time}, {TotalDuration}";  // Returnerer en beskrivelse af forestillingen
            }
        }
}
