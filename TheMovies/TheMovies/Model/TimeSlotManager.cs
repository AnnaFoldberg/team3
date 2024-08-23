using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Model
{
    public class TimeSlotManager
    {
        public AdditionalTime AddedTime { get; set; }
        public TimeSlotRepo TimeSlotsAll;
        public TimeSlotRepo TimeSlotsMonth;
        public TimeSlotRepo TimeSlotsMovie;

        public ShowTimeSlotManager()
        {

        }

        public GenerateNewMonth(Month month)
        {

        }

        public GetCinemaMonth(Cinema cin, Month month)
        {

        }

        public GetAvailableMovieTS(ShowRepository showRepo, TimeSpan movDur)
        {

        }
    }
}