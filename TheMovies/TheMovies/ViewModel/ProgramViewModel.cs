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
                }
            }
        }


        //private TimeSlot _selectedTimeSlot;
        //public TimeSlot SelectedTimeSlot
        //{
        //    get { return _selectedTimeSlot; }
        //    set
        //    {
        //        OnPropertyChanged();
        //    }
        //}

        private MovieRepository _movieRepo;
        private ShowRepository _showRepo;
        private TimeSlotManager _tsm;
        public ObservableCollection<Show> Shows;
        public ObservableCollection<Month> Months { get; }
        public ObservableCollection<Movie> Movies { get; }
        public ObservableCollection<DateOnly> Dates { get; set; }
        public RelayCommandT<string> SelectCinemaCommand { get; set; }
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
            Months = new ObservableCollection<Month>(Enum.GetValues(typeof(Month)) as Month[]);
            SelectedMonth = Months[0];
            SelectedCinema = Cinema.Videbaek;
            Movies = new ObservableCollection<Movie>(MovieRepository.MovieRepo);
            _tsm = new TimeSlotManager();
            _showRepo = new ShowRepository();
            Shows = new ObservableCollection<Show>();
            Dates = new ObservableCollection<DateOnly>();
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
            PopulateDates();
            //SelectedDate = Dates[0];
        }


        public void GetCinemaMonth() // Metoden crasher - find fejl
        {
            //Shows.Clear();
            //foreach (Show show in _showRepo.ShowsByMonth[SelectedMonth])
            //{
            //    Shows.Add(show);
            //}
            //_tsm.GetCinemaMonth(SelectedCinema, SelectedMonth);
        }

        // Method to populate Dates
        private void PopulateDates()
        {
            if (SelectedCinema != null && SelectedMonth != null)
            {
                _tsm.GetCinemaMonth(SelectedCinema, SelectedMonth);
                Dates = new ObservableCollection<DateOnly>(_tsm.Dates);

                //    int test = SelectedMonthIndex;

                //switch (test)
                //{ 
                //        case 1:
                //            SelectedMonth = Months[0];
                //            break;
                //        case Month.Februar:
                //            SelectedMonth = Months[1];
                //            break;
                //        //case 3:
                //        //    SelectedMonth = Months[2];
                //    //SelectedMonth = Months[];
                //}
                //_tsm.GetCinemaMonth();

                //Dates = new ObservableCollection<DateOnly>(_tsm.Dates);


                // For example, adding some sample dates (you should replace this with your actual logic)
                //Dates.Add(new DateOnly(2024, 1, 1));
                //Dates.Add(new DateOnly(2024, 2, 1));
                //Dates.Add(new DateOnly(2024, 3, 1));
            }
        }
    }
}

// Gør så GetCinemaMonth ikke crasher -> Dates vises i dropdown + Shows vises til højre
// Få TimeSlots til at vises (tidspunkt og sal, der matcher dato)