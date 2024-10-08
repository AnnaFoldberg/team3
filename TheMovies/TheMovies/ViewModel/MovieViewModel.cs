using System.ComponentModel;
namespace TheMovies.ViewModel
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;
        private TimeSpan _duration;
        private string _genre;
        private string _director;
        private DateOnly _premiereDate;

        private string _message;
        
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public string Director
        {
            get { return _director; }
            set
            {
                _director = value;
                OnPropertyChanged(nameof(Director));
            }
        }

        public DateOnly PremiereDate
        {
            get { return _premiereDate; }
            set
            {
                _premiereDate = value;
                OnPropertyChanged(nameof(PremiereDate));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private MovieRepository _movieRepo = new MovieRepository();

        public RelayCommand AddMovieCommand { get; }

        public MovieViewModel()
        {
            AddMovieCommand = new RelayCommand(AddMovie);
            PremiereDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public void AddMovie()
        {
            Movie movie = new Movie(Title, Duration, Genre, Director, PremiereDate);
            MovieRepository.MovieRepo.Add(movie);
            Title = string.Empty;
            Duration = TimeSpan.Zero;
            Genre = string.Empty;
            Director = string.Empty;
            PremiereDate = DateOnly.FromDateTime(DateTime.Now);
            Message = "Oprettet film: " + movie.ToString();
        }

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}