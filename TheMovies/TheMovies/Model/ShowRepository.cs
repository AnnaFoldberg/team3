using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TheMovies.Model;


namespace TheMovies
{
    public class ShowRepo //klasse til at håndtere alle forestillinger, organiseret efter måned 
    {
        // // En liste af biografer
        // private List<Cinema> cinemas = new List<Cinema>
        // {
        //     new Cinema ("Hjerm"),
        //     new Cinema ("Ræhr"),
        //     new Cinema ("Videbæk"),
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

        // Dictionary/ordbod der gemmer lister af forestillinger (show) for hver måned 
        public Dictionary<Month, List<Show>> ShowsByMonth { get; set; }

        // Constructor til ShowRepo
        public ShowRepo() 
        {
            ShowsByMonth = new Dictionary<Month, List<Show>>(); // Initialisering af dictionary'en

            
            foreach (Month month in Enum.GetValues(typeof(Month))) // Initialisering af dictionary med tomme lister for hver måned
            {
                ShowsByMonth[month] = new List<Show>();
            }

            // InitializeShows(); // Kalder en metode som tilføjer et par eksempler på forestillinger. I dete tilfælde Ayka og The Wife 
        }

        // Metode til at tilføje en forestilling til en bestemt måned
        public void AddShow(Month month, Show show)
        {
            if (ShowsByMonth.ContainsKey(month))
            {
                ShowsByMonth[month].Add(show); // Tilføjer forestillingen til den eksisterende liste for måneden
            }
            else
            {
                ShowsByMonth[month] = new List<Show> { show }; // Opretter en ny liste, hvis måneden ikke findes
            }
        }

        // Metode til at hente alle forestillinger for en given måned
        // public List<Show> GetShows(Month month, Cinema cin)
        // {
            // if (ShowsByMonth.TryGetValue(month, out List<Show> shows))
            // {
            //     return shows; // Returnerer listen af forestillinger for måneden
            // }
            // return new List<Show>(); // Returnerer en tom liste, hvis måneden ikke findes

        // }

        // private void InitializeShows()
        // {
        //     // Tilføj eksempler på forestillinger i August (fra Objektmodel). Ayka vises den 10/08/2024 kl 13 i sal i Hjerm. The Wife vises samme dag med kl 16 i sal 2 i Ræhr
        //     ShowsByMonth[Month.August].Add(new Show(
        //         cinemas[0],  // Hjerm
        //         halls[0],    // Sal 1
        //         new Ts(new DateTime(2024, 10, 08, 13, 0, 0), new DateTime(2024, 10, 08, 13, 0, 0)),  //Vises i denne format DateTime(år, måned, dag, time, minut, sekund).
        //         new Mov ("Ayka"),   // Ayka
        //         new AdditionalTime(TimeSpan.FromMinutes(15))
        //     ));

        //     ShowsByMonth[Month.August].Add(new Show(
        //         cinemas[1],  // Ræhr
        //         halls[1],    // Sal 2
        //         new Ts(new DateTime(2024, 10, 08, 16, 0, 0), new DateTime(2024, 10, 08, 16, 0, 0)), //Vises i denne format DateTime(år, måned, dag, time, minut, sekund).
        //         new Mov ("The Wife"),   // The Wife
        //         new AdditionalTime(TimeSpan.FromMinutes(10))
        //     ));
        // }

        // void Main()
        // {
        //     ShowRepo repo = new ShowRepo(); // Opretter en instans af ShowRepo

        //     // Tilføj en ny forestilling i april i Videbæk
        //     repo.AddShow(Month.April, new Show(
        //         repo.cinemas[2],  // Videbæk
        //         repo.halls[2],    // Hall 3
        //         new Ts(new DateTime(2024, 4, 1, 16, 0, 0), new DateTime(2024, 4, 1, 18, 45, 0)), // DateTime(år, måned, dag, time, minut, sekund).
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

        //     // Viser alle forestillinger for hver måned
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




        



    





