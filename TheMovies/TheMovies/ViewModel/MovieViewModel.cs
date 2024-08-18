using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;

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

        // public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        
        // Den første => bruges som en expression-bodied property, hvilket er en kortere måde
        // at definere en property, som returnerer en enkelt expression.
        // I dette tilfælde returneres en ny RelayCommand instans.
        // Lambda udtrykket () => AddMovie() skaber en Action delegate, som RelayCommand bruger som
        // Execute metode.
        public RelayCommand AddMovieCommand { get; }

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
            Message = "Oprettet film: " + movie.ToString();

            // Bruges midlertidigt til at kunne se, at filmen findes i _movieRepo
            foreach (var mov in _movieRepo.GetAll())
            {
                MessageBox.Show(mov.ToString());
            }
        }
    }
}