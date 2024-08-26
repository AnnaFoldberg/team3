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
        
        
        public ProgramViewModel()
        {
            Months = new ObservableCollection<Month>(Enum.GetValues(typeof(Month)) as Month[]);
            SelectedMonth = Months[0];
        }

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
            get { return _selectedMovie; }
            set
            {
                OnPropertyChanged();
            }
        }

        private DateTime _selectedDateTime;

        public DateTime SelectedDateTime
        {
            get { return _selectedDateTime; }
            set
            {
                OnPropertyChanged();
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
        


    }
}
