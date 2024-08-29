using System.Collections.ObjectModel;
using TheMovies.Model;

namespace TheMovies.ViewModel
{
    internal class ProgramViewModel : ViewModelBase
    {
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
                    if (Dates != null)
                    {
                        Dates.Clear();
                        PopulateDates();
                    }
                }
            }
        }

        private Cinema _selectedCinema;
        public Cinema SelectedCinema
        {
            get { return _selectedCinema; }
            set
            {
                _selectedCinema = value;
                OnPropertyChanged(nameof(SelectedCinema));
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
                    GetAvailableMovieTS();
                }
            }
        }


        private TimeSlot _selectedTimeSlot;
        public TimeSlot SelectedTimeSlot
        {
            get => _selectedTimeSlot;
            set
            {
                if (_selectedTimeSlot != value)
                {
                    _selectedTimeSlot = value;
                    OnPropertyChanged(nameof(SelectedTimeSlot));
                }
            }
        }

        private MovieRepository _movieRepo;
        private ShowRepository _showRepo;
        private TimeSlotManager _tsm;
        public ObservableCollection<Show> Shows { get; set; }
        public ObservableCollection<Month> Months { get; }
        public ObservableCollection<Movie> Movies { get; }
        public ObservableCollection<DateOnly> Dates { get; set; }
        public ObservableCollection<TimeSlot> TimeSlots { get; set; }
        public RelayCommandT<string> SelectCinemaCommand { get; set; }
        public RelayCommand AddShowCommand { get; set; }
        public AdditionalTime AddedTime { get; set; }
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ProgramViewModel()
        {
            SelectCinemaCommand = new RelayCommandT<string>(ChooseCinema);
            AddShowCommand = new RelayCommand(AddShow);
            Months = new ObservableCollection<Month>(Enum.GetValues(typeof(Month)) as Month[]);
            SelectedMonth = Months[11];
            Movies = new ObservableCollection<Movie>(MovieRepository.MovieRepo);
            _tsm = new TimeSlotManager();
            _showRepo = new ShowRepository();
            Shows = new ObservableCollection<Show>();
            Dates = new ObservableCollection<DateOnly>();
            TimeSlots = new ObservableCollection<TimeSlot>();
            AddedTime = new AdditionalTime();
            PopulateDates();
        }

        public void ChooseCinema(string parameter)
        {
            if (Enum.TryParse(parameter, out Cinema cinema))
            {
                SelectedCinema = cinema;
                Message = $"SelectedCinema: {parameter}";
            }
            else
            {
                Message = "Ugyldig biograf";
            }
            GetCinemaMonth();
        }

        public void AddShow()
        {
            Show show = new Show(SelectedTimeSlot, SelectedMovie, AddedTime);
            Shows.Add(show);
            _showRepo.AddShow(SelectedMonth, show);
        }


        public void GetCinemaMonth() // Metoden crasher - find fejl
        {
            GetShowsMonth();
            PopulateDates();
        }

        // public void GetShowsMonth()
        // {
        //     if (Shows != null)
        //     {
        //         Shows.Clear();
        //         foreach (Show show in _showRepo.ShowsByMonth[SelectedMonth])
        //         {
        //             Shows.Add(show);
        //         }
        //     }
        // }

        public void GetShowsMonth()
        {
            if (Shows != null)
            {
                Shows.Clear();
                if (_showRepo.ShowsByMonth.TryGetValue(SelectedMonth, out var shows))
                {
                    foreach (Show show in shows)
                    {
                        if (show.Cinema == SelectedCinema)
                        {
                            Shows.Add(show);
                        }
                    }
                }
            }
        }

        public void GetAvailableMovieTS()
        {
            if (TimeSlots != null)
            {
                TimeSlots.Clear();
                _tsm.GetAvailableMovieTS(SelectedDate, SelectedMovie.Duration);
                foreach (TimeSlot timeslot in _tsm.TimeSlotsMovie.TimeSlots)
                {
                    TimeSlots.Add(timeslot);
                }
            }
        }

        // Method to populate Dates
        private void PopulateDates()
        {
            if (Dates != null)
            {
                Dates.Clear();
                _tsm.GetCinemaMonth(SelectedCinema, SelectedMonth);
                foreach (DateOnly date in _tsm.Dates)
                {
                    Dates.Add(date);
                }
            }
        }
    }
}

// Gør så GetCinemaMonth ikke crasher -> Dates vises i dropdown + Shows vises til højre
// Få TimeSlots til at vises (tidspunkt og sal, der matcher dato)