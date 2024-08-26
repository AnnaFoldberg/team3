using System;


namespace TheMovies
{
    public enum Month
    {
        January = 1,
        February = 2,
        Marts = 3,
        April = 4,
        Maj = 5,
        Juni = 6,
        Juli = 7,
        August = 8,
        September = 9,
        Oktober = 10,
        November = 11,
        December = 12,
    }




    public class Cinema //klasse der repræsenterer biografer 
    {
        public string Name { get; set; }

        public Cinema(string name)
        {
            Name = name; //navn på biograf 

        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Hall
    {
        public int HallNumber { get; set; }

        public Hall(int hallNumber)
        {
            HallNumber = hallNumber; 
        }

        public override string ToString()
        {
            return $"Hall {HallNumber}"; // Returnerer salens nummer 
        }
    }

    public class Ts //Klasse der repræsenterer et tidsrum for en forestilling
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Ts (DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        public override string ToString()
        {
            return $"{StartTime.ToShortTimeString()} - {EndTime.ToShortTimeString()}";  // Returnerer start- og sluttidspunkt
        }


        public class Mov //klasse der repræsenterer film
        {
            public string Title { get; set; }

            public Mov(string title)
            {
                Title = title;
            }

            public override string ToString()
            {

                return Title; //Returnerer filmens titel

            }
        }

        public class AdditionalTime
        {
            public TimeSpan Duration { get; set; }

            public AdditionalTime(TimeSpan duration)
            {
                Duration = duration;
            }

            public override string ToString()
            {
                return $"Extra {Duration.TotalMinutes} minutes";  // Returnerer længden af den ekstra tid
            }
        }
             
        public class Show  // Klasse til at repræsentere en forestilling
        {
            public Cinema Cinema { get; set; }
            public Hall Hall { get; set; }
            public Ts Timeslot { get; set; }
            public Mov Movie { get; set; }
            public AdditionalTime AdditionalTime { get; set; }

            public Show(Cinema cinema, Hall hall, Ts timeslot, Mov movie, AdditionalTime additionalTime) //Constructor med 5 parametere Cinema, Hall, timeslot, Movie, AdditionalTime 
            {
                Cinema = cinema;
                Hall = hall;
                Timeslot = timeslot;
                Movie = movie;
                AdditionalTime = additionalTime;
            }

            public override string ToString()
            {
                return $"{Movie} at {Cinema}, {Hall}, {Timeslot}, {AdditionalTime}";  // Returnerer en beskrivelse af forestillingen
            }
        }








    }
}


               




            

        



