namespace TheMovies
{
    public class MovieRepository : IRepo<Movie>
    {
        private static List<Movie> _movieRepo;

        // Specify the file path to save the CSV on the desktop
        private string desktopPath;
        private string filePath;

        DataHandler moviesDataHandler = new DataHandler();

        public static List<Movie> MovieRepo
        {
            get { return _movieRepo; }
            set { _movieRepo = value; }
        }

        public MovieRepository()
        {
            _movieRepo = new List<Movie>();
            
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filePath = System.IO.Path.Combine(desktopPath, "Uge33-TheMovies.csv");  //filen skal gemmes i skrivebordet for at virke
        }

        public void Add(Movie movie)
        {
            Movie mov = new Movie(movie.Title, movie.Duration, movie.Genre, movie.Director, movie.PremiereDate);
            _movieRepo.Add(mov);

            moviesDataHandler.Save(movie.ToString(), filePath);
        }
    }
}