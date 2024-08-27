namespace TheMovies
{
    public class Movie
    {
        private string _title;
        private TimeSpan _duration;
        private string _genre;
        private string _director;
        private DateOnly _premiereDate;
        private string v;  

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

        public DateOnly PremiereDate
        {
            get { return _premiereDate; }
            set { _premiereDate = value; }
        }     

        public string Director
        {
            get { return _director; }
            set { _director = value; }
        }

        public Movie(string title, TimeSpan duration, string genre, string director,DateOnly premiereDate)
        {
            _title = title;
            _duration = duration;
            _genre = genre;
            _director=director;
            _premiereDate=premiereDate;
        }

        public Movie(string v)
        {
            this.v = v;
        }

        public string ToString()
        {
            return $"{_title}, { _duration}, { _genre}, { _director}, { _premiereDate}{Environment.NewLine}";
        }
    }
}