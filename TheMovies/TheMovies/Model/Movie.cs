namespace TheMovies
{
    public class Movie
    {
        private string _title;
        private TimeSpan _duration;
        private string _genre;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public TimeSpan Duration 
        { 
        
            get { return _duration; }
            set { _duration = value;  }
        }

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        public Movie(string title, TimeSpan duration, string genre)
        {
            _title = title;
            _duration = duration;
            _genre = genre;
        }

        public string ToString()
        {
            return $"Titel: {_title}, $Varighed: { _duration}, $Genre: { _genre}";
        }
    }
}