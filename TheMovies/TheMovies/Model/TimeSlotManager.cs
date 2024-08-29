namespace TheMovies.Model
{
    public class TimeSlotManager
    {
        public AdditionalTime AddedTime { get; set; }
        public TimeSlotRepo TimeSlotsAll {  get; set; } // Indeholder alle TimeSlots, inklusiv dem der er booket
        public TimeSlotRepo TimeSlotsAvailable { get; set; } // Indeholder alle TimeSlots for alle m�neder, undtagen dem der er booket
        public TimeSlotRepo TimeSlotsMonth {  get; set; } // Indeholder TimeSlots for m�neden
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

        public void BookTimeSlot(TimeSlot ts, TimeSpan showDur)
        {
            int roundedDur = (int)Math.Ceiling(showDur.TotalHours);
            for (int i = 1; i < roundedDur + 1; i++)
            {
                TimeSlot overlapTs = ts;
                overlapTs.Time = overlapTs.Time.AddHours(i);
                TimeSlotsAvailable.Remove(overlapTs);
            }
            TimeSlotsAvailable.Remove(ts);
            TimeSlotsMonth.Remove(ts);
            TimeSlotsMovie.Remove(ts);
        }
        
        public void GenerateNewMonth(int year, Month month)
        {
            foreach (Cinema cinema in Enum.GetValues(typeof(Cinema)))
            {
                foreach (Hall hall in Enum.GetValues(typeof(Hall)))
                {
                    for (int day = 1; day <= DateTime.DaysInMonth(year, Convert.ToInt32(month)); day++)
                    {
                        for (int hour = 14; hour <= 20; hour++)
                        {
                            TimeSlot newTimeSlot = new TimeSlot(cinema, hall, new DateOnly(year, Convert.ToInt32(month), day), new TimeOnly(hour, 0));

                            // // Add Only if it doesn't already exist in TimeSlotsAll
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
        }

        public void GetAvailableMovieTS(Cinema selectedCinema, DateOnly selectedDate, TimeSpan movieDur)
        {
            // Calculate the total duration of the movie including ads and cleaning
            TimeSpan totalDuration = movieDur + AddedTime.TotalAdditionalTime; // 1 t 30 min
            TimeSlotsMovie.TimeSlots.Clear();

            int roundedDur = (int)Math.Ceiling(totalDuration.TotalHours); // 2
            foreach (TimeSlot ts in TimeSlotsAvailable.TimeSlots) // 14.00, 16.00
            {
                if (ts.Date == selectedDate)
                {
                    if (ts.Cinema == selectedCinema)
                    {
                        bool availableForMovie = true;
                        for (int i = 0; i < roundedDur; i++)
                        {
                            TimeSlot tempTs = ts; // tempTs = 14.00
                            tempTs.Time = tempTs.Time.AddHours(i); // 0: tempTs = 14.00, 1: 15.00
                            if (TimeSlotsAll.TimeSlots.Contains(tempTs) && !TimeSlotsAvailable.TimeSlots.Contains(tempTs)) // 0: TsAll: 14.00, 15.00, 16.00 contains 14.00 = true, TsAv: 14.00, 16.00 contains 14.00 = true, 1: TsAll = true, TsAv = false
                            {
                                availableForMovie = false;
                            }
                        }
                        if (availableForMovie)
                        {
                            TimeSlotsMovie.Add(ts);
                        }
                    }
                }
            }
        }
    }
}