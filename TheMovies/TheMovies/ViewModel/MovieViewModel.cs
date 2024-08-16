using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace TheMovies
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _title;
        private TimeSpan _duration;
        private string _genre;
        
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

        private MovieRepository _movieRepo = new MovieRepository();

        // public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        
        public ICommand AddMovieCommand { get; }

        public MovieViewModel()
        {
            AddMovieCommand = new RelayCommand(AddMovie);
        }

        public void AddMovie()
        {
            Movie movie = new Movie(Title, Duration, Genre);
            _movieRepo.Add(movie);
            // Movies.Add(movie);
            Title = string.Empty;
            Duration = TimeSpan.Zero;
            Genre = string.Empty;
        }
    }
}