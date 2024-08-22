using System;
using System.Diagnostics;
using System.Windows;

namespace TheMovies
{
    public class MovieRepository : IRepo<Movie>
    {
        private List<Movie> _movieRepo;

        // Specify the file path to save the CSV on the desktop
        private string desktopPath;
        private string filePath;

        DataHandler moviesDataHandler = new DataHandler();

        public List<Movie> MovieRepo
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
            Movie mov = new Movie(movie.Title, movie.Duration, movie.Genre);
            _movieRepo.Add(mov);

            // Prepare the movie data in CSV format
            string movieData = $"{movie.Title}, {movie.Duration}, {movie.Genre}{Environment.NewLine}";

            moviesDataHandler.Save(movieData, filePath);

            // Bruges midtlertidigt til at kunne se, at filmen blev oprettet korrekt
            MessageBox.Show($"Film oprettet: {mov.ToString()}");
        }

        // Bruges midlertidigt til at kunne se, at filmen findes i _movieRepo
        public List<Movie> GetAll()
        {
            return _movieRepo;
        }
    }
}