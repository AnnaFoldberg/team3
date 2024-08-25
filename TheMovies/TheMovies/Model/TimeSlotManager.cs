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
        public TimeSlotRepo TimeSlotsAll = new TimeSlotRepo();
        public TimeSlotRepo TimeSlotsMonth = new TimeSlotRepo();
        public TimeSlotRepo TimeSlotsMovie = new TimeSlotRepo();


        public TimeSlotManager() {} // Jeg ved ikke lige hvad +ShowTimeSlotManager() i DCD'et er og skal gøre, så har lige den her som Placeholder

        // Remove from TimeSlotsMonth when booked
        // This Should be used instead of directly calling Remove in the higher layers.
        // It's to ensure that TimeSlots are not removed from TimeSlotsAll, so that we can compare against all previously created TimeSlots, when calling GenerateNewMonth.
        // This tries to ensure that we don't remove a TimeSlot when booking it, and then later risk regenerating that timeslot and booking it again.
        public void BookTimeSlot(TimeSlot ts)
        {
            TimeSlotsMonth.Remove(ts);
        }

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

                            // Add Only if it doesn't already exist in TimeSlotsAll
                            if (!TimeSlotsAll.TimeSlots.Contains(newTimeSlot))
                            {
                                TimeSlotsMonth.Add(newTimeSlot);
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

        public List<TimeSlot> GetAvailableMovieTS(DateOnly selectedDate, TimeSpan movieDur)
        {
            // Calculate the total duration of the movie including ads and cleaning
            TimeSpan totalDuration = movieDur + AddedTime.TotalAdditionalTime;
            List<TimeSlot> availableTimeSlots = new List<TimeSlot>();

            // Iterate through each TimeSlot in TimeSlotsMonth
            foreach (var potentialSlot in TimeSlotsMonth.TimeSlots)
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
                    bool timeSlotExistsInTimeSlotsMonth = false;

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

                    // Iterate through TimeSlotsMonth to check if the slot exists there
                    foreach (var ts in TimeSlotsMonth.TimeSlots)
                    {
                        if (ts.Cinema == potentialSlot.Cinema &&
                            ts.Hall == potentialSlot.Hall &&
                            ts.Date == potentialSlot.Date &&
                            ts.Time == currentTime)
                        {
                            timeSlotExistsInTimeSlotsMonth = true;
                            break;
                        }
                    }

                    // If the TimeSlot exists in TimeSlotsAll but not in TimeSlotsMonth, it's not available
                    if (timeSlotExistsInTimeSlotsAll && !timeSlotExistsInTimeSlotsMonth)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                // If all required TimeSlots are available, add the potentialSlot to the available list
                if (isAvailable)
                {
                    availableTimeSlots.Add(potentialSlot);
                }
            }

            return availableTimeSlots;
        }
    }
}