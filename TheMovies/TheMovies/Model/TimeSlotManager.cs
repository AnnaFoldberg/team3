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
        public AdditionalTime AddedTime { get; } = new AdditionalTime();
        public TimeSlotRepo TimeSlotsAll;
        public TimeSlotRepo TimeSlotsMonth;
        public TimeSlotRepo TimeSlotsMovie;

        
        public TimeSlotManager() {} // Jeg ved ikke lige hvad +ShowTimeSlotManager() i DCD'et er og skal gøre, så har lige den her som Placeholder


        // Using 'int' for month 
        // Might change later
        public void GenerateNewMonth(int year, int month)
        {
            TimeSlotsMonth.TimeSlots.Clear();

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
        public List<TimeSlot> GetCinemaMonth(Cinema cinema, int month)
        {
            List<TimeSlot> filteredtimeSlots = new List<TimeSlot>();

            foreach (TimeSlot timeslot in TimeSlotsMonth.TimeSlots)
            {
                if (timeslot.Cinema == cinema && timeslot.Date.Month == month)
                {
                    filteredtimeSlots.Add(timeslot);
                }
            }
            return filteredtimeSlots;
        }

        public void GetAvailableMovieTS(ShowRepository showRepo, TimeSpan movieDur)
        {
            TimeSlotsMovie.TimeSlots.Clear();

            // Method 1
            // use the potential TimeSlot as a StartTime for the movie. Then add the movies duration and the AdditionalTime to get a TotalDuration. Now Add TotalDuration to StartTime to get EndTime.
            // Now check if all TimeSlots, that falls fully or partially between StartTime and EndTime, are available. Only show Timeslots that has a StartTime that satisfies the previous.
            //
            // Method 2
            // When selecting a timeslot for a Show, remove all the timeslots where TotalDuration for that show overlaps with even slightly.
            // Exampel: Terminator 2 hours showing. TotalDuration 2,5 hours. Set to show at 14. Now remove TimeSlot, for that cinema, hall and date, of the hours 14, 15 and 16.
        }
    }
}