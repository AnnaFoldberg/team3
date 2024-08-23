using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using static TheMovies.Ts;


namespace TheMovies
{
    public class ShowRepo //klasse til at håndtere alle forestillinger, organiseret efter måned 
    {

      

        // En liste af biografer
        private List<Cinema> cinemas = new List<Cinema>
        {
            new Cinema ("Hjerm"),
            new Cinema ("Ræhr"),
            new Cinema ("Videbæk"),
            new Cinema ("Thorsminde")



        };

        //En liste af biografsale 
        private List<Hall> halls = new List<Hall>
        {
            new Hall(1),
            new Hall(2),
            new Hall(3),





        };

        // En liste af film
        private List<Movie> movies = new List<Movie>
        {
            new Movie("Ayka"),
            new Movie("The Wife"),
            new Movie("1917")



        };

        // Dictionary/ordbod der gemmer lister af forestillinger (show) for hver måned 
        private Dictionary<Month, List<Show>> showsByMonth;


        // Constructor til ShowRepo
        public ShowRepo() 
        {
            showsByMonth = new Dictionary<Month, List<Show>>(); // Initialisering af dictionary'en

            
            foreach (Month month in Enum.GetValues(typeof(Month))) // Initialisering af dictionary med tomme lister for hver måned
            {
                showsByMonth[month] = new List<Show>();
            }

            InitializeShows(); // Kalder en metode som tilføjer et par eksempler på forestillinger. I dete tilfælde Ayka og The Wife 
        }
        private void InitializeShows()
        {
            // Tilføj eksempler på forestillinger i januar og februar
            showsByMonth[Month.January].Add(new Show(
                cinemas[0],  // Hjerm
                halls[0],    // Hall 1
                new Ts(new DateTime(2024, 1, 1, 19, 0, 0), new DateTime(2024, 1, 1, 21, 30, 0)),
                new Mov ("Ayka"),   // Ayka
                new AdditionalTime(TimeSpan.FromMinutes(15))
            ));

            showsByMonth[Month.February].Add(new Show(
                cinemas[1],  // Ræhr
                halls[1],    // Hall 2
                new Ts(new DateTime(2024, 2, 14, 17, 0, 0), new DateTime(2024, 2, 14, 19, 45, 0)),
                new Mov ("The Wife"),   // The Wife
                new AdditionalTime(TimeSpan.FromMinutes(10))
            ));
        }

        // Metode til at tilføje en forestilling til en bestemt måned
        public void AddShow(Month month, Show show)
        {
            if (showsByMonth.ContainsKey(month))
            {
                showsByMonth[month].Add(show); // Tilføjer forestillingen til den eksisterende liste for måneden
            }
            else
            {
                showsByMonth[month] = new List<Show> { show }; // Opretter en ny liste, hvis måneden ikke findes
            }
        }

        // Metode til at hente alle forestillinger for en given måned
        public List<Show> GetShows(Month month)
        {
            if (showsByMonth.TryGetValue(month, out List<Show> shows))
            {
                return shows; // Returnerer listen af forestillinger for måneden
            }
            return new List<Show>(); // Returnerer en tom liste, hvis måneden ikke findes
        }

        void Main()
        {
            ShowRepo repo = new ShowRepo(); // Opretter en instans af ShowRepo

            // Tilføj en ny forestilling i april
            repo.AddShow(Month.April, new Show(
                repo.cinemas[2],  // Videbæk
                repo.halls[2],    // Hall 3
                new Ts(new DateTime(2024, 4, 1, 16, 0, 0), new DateTime(2024, 4, 1, 18, 45, 0)),
                new Mov ("1917"),   // 1917
                new AdditionalTime(TimeSpan.FromMinutes(20))
            ));

            // Hent og vis forestillinger for januar
            Month selectedMonth = Month.January;
            List<Show> januaryShows = repo.GetShows(selectedMonth);

            Console.WriteLine($"Shows in {selectedMonth}:");
            foreach (var show in januaryShows)
            {
                Console.WriteLine(show);
            }

            // Vis alle forestillinger for hver måned
            Console.WriteLine("\nAll Shows:");
            foreach (Month month in Enum.GetValues(typeof(Month)))
            {
                List<Show> shows = repo.GetShows(month);
                if (shows.Count > 0)
                {
                    Console.WriteLine($"{month}:");
                    foreach (var show in shows)
                    {
                        Console.WriteLine($"  - {show}");
                    }
                }
            }
        }







    }

}




        



    





