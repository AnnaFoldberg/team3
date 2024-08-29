using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TheMovies.Model;


namespace TheMovies
{
    public class ShowRepository //klasse til at h�ndtere alle forestillinger, organiseret efter m�ned 
    {
        // // En liste af biografer
        // private List<Cinema> cinemas = new List<Cinema>
        // {
        //     new Cinema ("Hjerm"),
        //     new Cinema ("R�hr"),
        //     new Cinema ("Videb�k"),
        //     new Cinema ("Thorsminde")
        // };

        // //En liste af biografsale 
        // private List<Hall> halls = new List<Hall>
        // {
        //     new Hall(1),
        //     new Hall(2),
        //     new Hall(3),
        // };

        // // En liste af film
        // private List<Movie> movies = new List<Movie>
        // {
        //     new Movie("Ayka"),
        //     new Movie("The Wife"),
        //     new Movie("1917")
        // };

        // Dictionary/ordbod der gemmer lister af forestillinger (show) for hver m�ned 
        public Dictionary<Month, List<Show>> ShowsByMonth { get; set; }

        // Constructor til ShowRepo
        public ShowRepository() 
        {
            ShowsByMonth = new Dictionary<Month, List<Show>>(); // Initialisering af dictionary'en

            
            foreach (Month month in Enum.GetValues(typeof(Month))) // Initialisering af dictionary med tomme lister for hver m�ned
            {
                ShowsByMonth[month] = new List<Show>();
            }

            InitializeShows(); // Kalder en metode som tilf�jer et par eksempler p� forestillinger. I dete tilf�lde Ayka og The Wife 
        }

        // Metode til at tilf�je en forestilling til en bestemt m�ned
        public void AddShow(Month month, Show show)
        {
            if (ShowsByMonth.ContainsKey(month))
            {
                ShowsByMonth[month].Add(show); // Tilf�jer forestillingen til den eksisterende liste for m�neden
            }
            else
            {
                ShowsByMonth[month] = new List<Show> { show }; // Opretter en ny liste, hvis m�neden ikke findes
            }
        }

        public void InitializeShows()
        {
            TimeSpan duration1 = new TimeSpan(1, 30, 0);
            TimeOnly time1 = new TimeOnly(14, 30);
            DateOnly date1 = new DateOnly(2024, 12, 14);
            TimeSlot timeSlot1 = new TimeSlot(Cinema.Videbaek, Hall.Two, date1, time1);
            Movie movie1 = new Movie("Titel", duration1, "Genre", "Director", date1);
            AdditionalTime addedTime1 = new();
            Show show1 = new Show(timeSlot1, movie1, addedTime1);
            AddShow(Month.December, show1);

            TimeSpan duration2 = new TimeSpan(2, 30, 0);
            TimeOnly time2 = new TimeOnly(15, 30);
            DateOnly date2 = new DateOnly(2024, 12, 15);
            TimeSlot timeSlot2 = new TimeSlot(Cinema.Videbaek, Hall.Two, date2, time2);
            Movie movie2 = new Movie("Titel", duration2, "Genre", "Director", date2);
            AdditionalTime addedTime2 = new();
            Show show2 = new Show(timeSlot2, movie2, addedTime2);
            AddShow(Month.December, show2);
        }

        // Metode til at hente alle forestillinger for en given m�ned
        // public List<Show> GetShows(Month month, Cinema cin)
        // {
        // if (ShowsByMonth.TryGetValue(month, out List<Show> shows))
        // {
        //     return shows; // Returnerer listen af forestillinger for m�neden
        // }
        // return new List<Show>(); // Returnerer en tom liste, hvis m�neden ikke findes

        // }

        //private void InitializeShows()
        //{
        //    // Tilf�j eksempler p� forestillinger i August (fra Objektmodel). Ayka vises den 10/08/2024 kl 13 i sal i Hjerm. The Wife vises samme dag med kl 16 i sal 2 i R�hr
        //    ShowsByMonth[Month.August].Add(new Show(
        //        cinemas[0],  // Hjerm
        //        halls[0],    // Sal 1
        //        new Ts(new DateTime(2024, 10, 08, 13, 0, 0), new DateTime(2024, 10, 08, 13, 0, 0)),  //Vises i denne format DateTime(�r, m�ned, dag, time, minut, sekund).
        //        new Mov("Ayka"),   // Ayka
        //        new AdditionalTime(TimeSpan.FromMinutes(15))
        //    ));

        //    ShowsByMonth[Month.August].Add(new Show(
        //        cinemas[1],  // R�hr
        //        halls[1],    // Sal 2
        //        new Ts(new DateTime(2024, 10, 08, 16, 0, 0), new DateTime(2024, 10, 08, 16, 0, 0)), //Vises i denne format DateTime(�r, m�ned, dag, time, minut, sekund).
        //        new Mov("The Wife"),   // The Wife
        //        new AdditionalTime(TimeSpan.FromMinutes(10))
        //    ));
        //}

        // void Main()
        // {
        //     ShowRepo repo = new ShowRepo(); // Opretter en instans af ShowRepo

        //     // Tilf�j en ny forestilling i april i Videb�k
        //     repo.AddShow(Month.April, new Show(
        //         repo.cinemas[2],  // Videb�k
        //         repo.halls[2],    // Hall 3
        //         new Ts(new DateTime(2024, 4, 1, 16, 0, 0), new DateTime(2024, 4, 1, 18, 45, 0)), // DateTime(�r, m�ned, dag, time, minut, sekund).
        //         new Mov ("1917"),   // 1917
        //         new AdditionalTime(TimeSpan.FromMinutes(20))
        //     ));

        //     // Hent og vis forestillinger for januar
        //     Month selectedMonth = Month.Januar;
        //     List<Show> januaryShows = repo.GetShows(selectedMonth);

        //     Console.WriteLine($"Shows in {selectedMonth}:");
        //     foreach (var show in januaryShows)
        //     {
        //         Console.WriteLine(show);
        //     }

        //     // Viser alle forestillinger for hver m�ned
        //     Console.WriteLine("\nAll Shows:");
        //     foreach (Month month in Enum.GetValues(typeof(Month)))
        //     {
        //         List<Show> shows = repo.GetShows(month);
        //         if (shows.Count > 0)
        //         {
        //             Console.WriteLine($"{month}:");
        //             foreach (var show in shows)
        //             {
        //                 Console.WriteLine($"  - {show}");
        //             }
        //         }
        //     }
        // }
    }
}




        



    





