using System;
using System.Diagnostics;
using System.Windows;

namespace TheMovies
{
    public class MovieRepository : IRepo<Movie>
    {
        private List<Movie> _movieRepo;

        private string _filePath;

        public List<Movie> MovieRepo
        {
            get { return _movieRepo; }
            set { _movieRepo = value; }
        }

        public MovieRepository()
        {
            _movieRepo = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            Movie mov = new Movie(movie.Title, movie.Duration, movie.Genre);
            _movieRepo.Add(mov);

            // Specify the file path to save the CSV on the desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = System.IO.Path.Combine(desktopPath, "Uge33-TheMovies.csv");  //filen skal gemmes i skrivebordet for at virke

            // Prepare the movie data in CSV format
            string movieData = $"{movie.Title}, {movie.Duration}, {movie.Genre}{Environment.NewLine}";

            // Append the movie data to the CSV file
            System.IO.File.AppendAllText(filePath, movieData);

            // Bruges midtlertidigt til at kunne se, at filmen blev oprettet korrekt
            MessageBox.Show($"Film oprettet: {mov.ToString()}");

            // Bruges midlertidigt til at kunne se, at filmen findes i _movieRepo
        }

        // Bruges midlertidigt til at kunne se, at filmen findes i _movieRepo
        public List<Movie> GetAll()
        {
            return _movieRepo;
        }
    }
}