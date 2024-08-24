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
        public AdditionalTime AddedTime;
        public TimeSlotRepo TimeSlotsAll;
        public TimeSlotRepo TimeSlotsMonth;
        public TimeSlotRepo TimeSlotsMovie;

        public void ShowTimeSlotManager()
        {

        }
        // Using 'int' for month 
        // Might change later
        public void GenerateNewMonth(int year, int month)
        {
            foreach (Cinema cinema in Enum.GetValues(typeof(Cinema)))
            {
                foreach (Hall hall in Enum.GetValues(typeof(Hall)))
                {
                    for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                    {
                        for (int hour = 14; hour <= 20; hour++)
                        {
                            var newTimeSlot = new TimeSlot(cinema, hall, new DateOnly(year, month, day), new TimeOnly(hour, 0));
                            TimeSlotsMonth.Add(newTimeSlot);

                            if (!TimeSlotsAll.TimeSlots.Contains(newTimeSlot))
                            {
                                TimeSlotsAll.Add(newTimeSlot);
                            }
                        }
                    }
                }
            }
        }

        // Using 'int' for month 
        // Might change later
        public void GetCinemaMonth(Cinema cin, int month)
        {

        }

        public void GetAvailableMovieTS(ShowRepository showRepo, TimeSpan movDur)
        {

        }
    }
}