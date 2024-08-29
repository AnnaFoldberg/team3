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
            TimeSpan totalDuration = movieDur + AddedTime.TotalAdditionalTime;
            TimeSlotsMovie.TimeSlots.Clear();

            /////////////

            int roundedDur = (int)Math.Ceiling(totalDuration.TotalHours);
            foreach (TimeSlot ts in TimeSlotsAvailable.TimeSlots)
            {
                if (ts.Date == selectedDate)
                {
                    if (ts.Cinema == selectedCinema)
                    {
                        bool availableForMovie = true;
                        for (int i = 0; i < roundedDur; i++)
                        {
                            TimeSlot tempTs = ts;
                            tempTs.Time = tempTs.Time.AddHours(i);
                            if (TimeSlotsAll.TimeSlots.Contains(tempTs) && !TimeSlotsAvailable.TimeSlots.Contains(tempTs))
                            {
                                availableForMovie = false;
                            }
                        }
                        if (availableForMovie)
                        {
                            TimeSlotsMovie.Add(ts);
                        }
                    }
                // Find om TimeSlotsAll tid findes, som ikke findes i TimeSlotsAvailable
                // 
                }
            }

            // TimeSlotsAll: 14.00, 15.00, 16.00, 17.00
            // TimeSlotsAvailable: 14.00, 16.00, 17.00
            // totalDuration: 1 t 30 min
            // Andet show: 15.00-16.30
            // Eftersom et andet show kører 15.00, kan en ny 1 t 30 min lang film ikke køre klokken 14.00. Vi skal fange de forrige tider - efterfølgende tider fanges i Book...
            // Altså findes 14.00 i TimeSlotsAvailable (for den er available til en film på <1 time), men den skal ikke være i TimeSlotsMovie for filen på 1 t 30 min.


            // for (int i = 1; i < roundedDur + 1; i++)
            // {
            //     TimeSlot overlapTs = ts;
            //     overlapTs.Time = ts.Time.AddHours(i);
            //     TimeSlotsAvailable.Remove(overlapTs);
            // }



            /////////////

            // // Iterate through each TimeSlot in TimeSlotsAvailable
            // foreach (TimeSlot potentialSlot in TimeSlotsAvailable.TimeSlots)
            // {
            //     // We only care about time slots on the selected date
            //     if (potentialSlot.Date != selectedDate)
            //         continue;

            //     // Calculate the end time for this potential slot
            //     TimeOnly endTime = potentialSlot.Time.Add(totalDuration);
            //     bool isAvailable = true;

            //     // Check all TimeSlots between StartTime and EndTime for overlaps
            //     for (TimeOnly currentTime = potentialSlot.Time; currentTime < endTime; currentTime = currentTime.AddHours(1))
            //     {
            //         bool timeSlotExistsInTimeSlotsAll = false;
            //         bool timeSlotExistsInTimeSlotsAvailable = false;

            //         // Iterate through TimeSlotsAll to check for overlaps
            //         foreach (TimeSlot ts in TimeSlotsAll.TimeSlots)
            //         {
            //             if (ts.Cinema == potentialSlot.Cinema &&
            //                 ts.Hall == potentialSlot.Hall &&
            //                 ts.Date == potentialSlot.Date &&
            //                 ts.Time == currentTime)
            //             {
            //                 timeSlotExistsInTimeSlotsAll = true;
            //                 break;
            //             }
            //         }

            //         // Iterate through TimeSlotsAvailable to check if the slot exists there
            //         foreach (TimeSlot ts in TimeSlotsAvailable.TimeSlots)
            //         {
            //             if (ts.Cinema == potentialSlot.Cinema &&
            //                 ts.Hall == potentialSlot.Hall &&
            //                 ts.Date == potentialSlot.Date &&
            //                 ts.Time == currentTime)
            //             {
            //                 timeSlotExistsInTimeSlotsAvailable = true;
            //                 break;
            //             }
            //         }

            //         // If the TimeSlot exists in TimeSlotsAll but not in TimeSlotsAvailable, it's not available
            //         if (timeSlotExistsInTimeSlotsAll && !timeSlotExistsInTimeSlotsAvailable)
            //         {
            //             isAvailable = false;
            //             break;
            //         }
            //     }

            //     // If all required TimeSlots are available, add the potentialSlot to the available list
            //     if (isAvailable)
            //     {
            //         TimeSlotsMovie.Add(potentialSlot);
            //     }
            // }
        }
    }
}