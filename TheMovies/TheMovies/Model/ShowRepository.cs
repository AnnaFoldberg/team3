using TheMovies.Model;


namespace TheMovies
{
    public class ShowRepository //klasse til at h�ndtere alle forestillinger, organiseret efter m�ned 
    {

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
            DateOnly date2 = new DateOnly(2024, 11, 15);
            TimeSlot timeSlot2 = new TimeSlot(Cinema.Videbaek, Hall.Two, date2, time2);
            Movie movie2 = new Movie("Titel", duration2, "Genre", "Director", date2);
            AdditionalTime addedTime2 = new();
            Show show2 = new Show(timeSlot2, movie2, addedTime2);
            AddShow(Month.November, show2);
        }
    }
}




        



    





