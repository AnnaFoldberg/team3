using TheMovies.Model;

namespace TheMovies
{
    public class Show  // Klasse til at repræsentere en forestilling
        {
            public Cinema Cinema { get; set; }
            public Hall Hall { get; set; }
            public TimeSlot Ts { get; set; }
            public Movie Mov { get; set; }
            public AdditionalTime AdditionalTime { get; set; }
            public TimeSpan TotalDuration { get; set; }

            public Show(TimeSlot timeslot, Movie movie, AdditionalTime additionalTime) //Constructor med 5 parametere Cinema, Hall, timeslot, Movie, AdditionalTime 
            {
                Ts = timeslot;
                Cinema = Ts.Cinema;
                Hall = Ts.Hall;
                Mov = movie;
                AdditionalTime = additionalTime;
                TotalDuration = additionalTime.TotalAdditionalTime + Mov.Duration;
            }

            public override string ToString()
            {
                return $"{Mov}, {Cinema}, {Hall}, {Ts}, {TotalDuration}";  // Returnerer en beskrivelse af forestillingen
            }
        }
}
