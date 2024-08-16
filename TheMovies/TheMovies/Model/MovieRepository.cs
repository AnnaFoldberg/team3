using System;

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

        public void Add(Movie movie)
        {
            Movie mov = new Movie(movie.Title, movie.Duration, movie.Genre);
            MovieRepo.Add(mov);
            Console.WriteLine("Film oprettet {0}", mov.ToString());
        }
    }
}