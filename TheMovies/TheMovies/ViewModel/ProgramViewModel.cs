using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Model;


namespace TheMovies.ViewModel
{
    internal class ProgramViewModel : ViewModelBase
    {
        //public ObservableCollection<ShowCurrentMonth> ShowCurrentMonth { get; set; }

        //private Show _selectedShow;

        //public Show SelectedShow
        //{
        //    get { return _selectedShow; }
        //    set
        //    {
        //        OnPropertyChanged();
        //    }
        //}

        public ObservableCollection<Month> Months { get; }
        public ObservableCollection<Movie> Movies { get; }
        public ObservableCollection<DateOnly> Dates { get; set; }

        private Month _selectedMonth;

        public Month SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    OnPropertyChanged(nameof(SelectedMonth));
                }
            }
        }

        private Cinema _selectedCinema;

        public Cinema SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                OnPropertyChanged();
            }
        }

        private Movie _selectedMovie;

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                if (_selectedMovie != value)
                {
                    _selectedMovie = value;
                    OnPropertyChanged(nameof(SelectedMovie));
                }
            }
        }

        private DateOnly _selectedDate;
        public DateOnly SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        private TimeSlot _selectedTimeSlot;

        public TimeSlot SelectedTimeSlot
        {
            get { return _selectedTimeSlot; }
            set
            {
                OnPropertyChanged();
            }
        }

        private MovieRepository _movieRepo;
        private TimeSlotManager _tsm;
        private ShowRepo _showRepo;

        public ObservableCollection<Show> Shows;

        public ProgramViewModel()
        {
            Months = new ObservableCollection<Month>(Enum.GetValues(typeof(Month)) as Month[]);
            SelectedMonth = Months[0];

            Movies = new ObservableCollection<Movie>(MovieRepository.MovieRepo);

            _tsm = new TimeSlotManager();

            Shows = new ObservableCollection<Show>();

            // SelectedDate = Dates[0];
        }

        public void RegisterCinemaMonth()
        {
            // public List<Show> GetShows(Month month, Cinema cin) -> Listen til højre
            // ShowRepository.GetShows(SelectedMonth, SelectedCinema);

            foreach (Show show in _showRepo.ShowsByMonth[SelectedMonth])
            {
                Shows.Add(show);
            }

            // public void GetCinemaMonth(Cinema cinema, Month month) -> Tider available på den valgte måned og biograf hentes og dates oprettes
            _tsm.GetCinemaMonth(SelectedCinema, SelectedMonth);

            Dates = new ObservableCollection<DateOnly>(_tsm.Dates);

        }
        //        // Assuming Shows is an ObservableCollection<Show>
        //        ObservableCollection<Show> Shows = new ObservableCollection<Show>();

        //        // Assuming _showRepo.ShowsByMonth[SelectedMonth] returns a List<Show>
        //        List<Show> selectedMonthShows = _showRepo.ShowsByMonth[SelectedMonth];

        //// Adding each Show from the list to the ObservableCollection
        //foreach (var show in selectedMonthShows)
        //{
        //    Shows.Add(show);
        //}
    }

}
