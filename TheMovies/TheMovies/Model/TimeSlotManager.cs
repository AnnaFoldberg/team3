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
        public TimeSlotRepo TimeSlotsAll {  get; set; } // Indeholder alle TimeSlots, inklusiv dem der er booket
        public TimeSlotRepo TimeSlotsAvailable { get; set; } // Indeholder alle TimeSlots for alle måneder, undtagen dem der er booket
        public TimeSlotRepo TimeSlotsMonth {  get; set; } // Indeholder TimeSlots for måneden
        public TimeSlotRepo TimeSlotsMovie { get; set; } // Indeholder TimeSlots for den givne film og dato
        public List<DateOnly> Dates { get; set; }

        public TimeSlotManager()
        {
            AddedTime = new AdditionalTime();
            TimeSlotsAll = new TimeSlotRepo();
            TimeSlotsAvailable = new TimeSlotRepo();
            TimeSlotsMonth = new TimeSlotRepo();
            TimeSlotsMovie = new TimeSlotRepo();
            Dates = new List<DateOnly>();

            for (Month month = Month.Januar; month <= Month.December; month++)
            {
                GenerateNewMonth(2024, month);
            }
        }

        // Remove from TimeSlotsAvailable when booked
        // This Should be used instead of directly calling Remove in the higher layers.
        // It's to ensure that TimeSlots are NOT removed from TimeSlotsAll, so that we can compare against all previously created TimeSlots, when calling GenerateNewMonth.
        // This tries to ensure that we don't remove a TimeSlot when booking it, and then later risk regenerating that timeslot and booking it again.
        public void BookTimeSlot(TimeSlot ts)
        {
            TimeSlotsAvailable.Remove(ts);
            TimeSlotsMonth.Remove(ts);
            TimeSlotsMovie.Remove(ts);
        }

        
        public void GenerateNewMonth(int year, Month month)
        {
            TimeSlotsAvailable.TimeSlots.Clear();

            foreach (Cinema cinema in Enum.GetValues(typeof(Cinema)))
            {
                foreach (Hall hall in Enum.GetValues(typeof(Hall)))
                {
                    for (int day = 1; day <= DateTime.DaysInMonth(year, Convert.ToInt32(month)); day++)
                    {
                        for (int hour = 14; hour <= 20; hour++)
                        {
                            var newTimeSlot = new TimeSlot(cinema, hall, new DateOnly(year, Convert.ToInt32(month), day), new TimeOnly(hour, 0));

                            // Add Only if it doesn't already exist in TimeSlotsAll
                            if (!TimeSlotsAll.TimeSlots.Contains(newTimeSlot))
                            {
                                TimeSlotsAvailable.Add(newTimeSlot);
                                TimeSlotsAll.Add(newTimeSlot);
                            }
                        }
                    }
                }
            }
        }

        // Skal laves til at tage inputs der bruges
        public void GetCinemaMonth(Cinema cinema, Month month)
        {
            Dates.Clear();

            int intMonth = Convert.ToInt32(month);

            // Define the year and month
            int year = 2024;

            // Create a DateOnly object for the first day of the month
            DateOnly startDate = new DateOnly(year, intMonth, 1);

            // Get the number of days in the month
            int daysInMonth = DateTime.DaysInMonth(year, intMonth);

            // Loop through all days in the month
            for (int day = 0; day < daysInMonth; day++)
            {
                // Add 'day' days to the startDate to get the current date
                DateOnly currentDate = startDate.AddDays(day);

                Dates.Add(currentDate);
            }


            //TimeSlotsMonth.TimeSlots.Clear();
            //Dates.Clear();

            //foreach (TimeSlot timeslot in TimeSlotsAvailable.TimeSlots)
            //{
            //    if (timeslot.Cinema == cinema && timeslot.Date.Month == Convert.ToInt32(month))
            //    {
            //        TimeSlotsMonth.Add(timeslot);
            //        if (!Dates.Contains(timeslot.Date))
            //            Dates.Add(timeslot.Date);
            //    }
            //}
        }

        public void GetAvailableMovieTS(DateOnly selectedDate, TimeSpan movieDur)
        {
            // Calculate the total duration of the movie including ads and cleaning
            TimeSpan totalDuration = movieDur + AddedTime.TotalAdditionalTime;
            TimeSlotsMovie.TimeSlots.Clear();

            // Iterate through each TimeSlot in TimeSlotsAvailable
            foreach (var potentialSlot in TimeSlotsAvailable.TimeSlots)
            {
                // We only care about time slots on the selected date
                if (potentialSlot.Date != selectedDate)
                    continue;

                // Calculate the end time for this potential slot
                TimeOnly endTime = potentialSlot.Time.Add(totalDuration);
                bool isAvailable = true;

                // Check all TimeSlots between StartTime and EndTime for overlaps
                for (TimeOnly currentTime = potentialSlot.Time; currentTime < endTime; currentTime = currentTime.AddHours(1))
                {
                    bool timeSlotExistsInTimeSlotsAll = false;
                    bool timeSlotExistsInTimeSlotsAvailable = false;

                    // Iterate through TimeSlotsAll to check for overlaps
                    foreach (var ts in TimeSlotsAll.TimeSlots)
                    {
                        if (ts.Cinema == potentialSlot.Cinema &&
                            ts.Hall == potentialSlot.Hall &&
                            ts.Date == potentialSlot.Date &&
                            ts.Time == currentTime)
                        {
                            timeSlotExistsInTimeSlotsAll = true;
                            break;
                        }
                    }

                    // Iterate through TimeSlotsAvailable to check if the slot exists there
                    foreach (var ts in TimeSlotsAvailable.TimeSlots)
                    {
                        if (ts.Cinema == potentialSlot.Cinema &&
                            ts.Hall == potentialSlot.Hall &&
                            ts.Date == potentialSlot.Date &&
                            ts.Time == currentTime)
                        {
                            timeSlotExistsInTimeSlotsAvailable = true;
                            break;
                        }
                    }

                    // If the TimeSlot exists in TimeSlotsAll but not in TimeSlotsAvailable, it's not available
                    if (timeSlotExistsInTimeSlotsAll && !timeSlotExistsInTimeSlotsAvailable)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                // If all required TimeSlots are available, add the potentialSlot to the available list
                if (isAvailable)
                {
                    TimeSlotsMovie.Add(potentialSlot);
                }
            }
        }
    }
}