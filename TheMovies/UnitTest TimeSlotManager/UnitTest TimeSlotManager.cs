//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using TheMovies.Model;
//using System;
//using System.Linq;

//namespace TheMovies.Tests
//{
//    [TestClass]
//    public class TimeSlotManagerTests
//    {
//        private TimeSlotManager _timeSlotManager;
//        private TimeSlotRepo _timeSlotsAll;
//        private TimeSlotRepo _timeSlotsMonth;
//        private TimeSlotRepo _timeSlotsMovie;

//        [TestInitialize]
//        public void Setup()
//        {
//            // Initialize the TimeSlotManager and repositories
//            _timeSlotsAll = new TimeSlotRepo();
//            _timeSlotsMonth = new TimeSlotRepo();
//            _timeSlotsMovie = new TimeSlotRepo();

//            _timeSlotManager = new TimeSlotManager
//            {
//                TimeSlotsAll = _timeSlotsAll,
//                TimeSlotsMonth = _timeSlotsMonth,
//                TimeSlotsMovie = _timeSlotsMovie
//            };
//        }
//        [TestMethod]
//        public void GenerateNewMonth_ShouldAddSpecificTimeSlots()
//        {
//            // Arrange
//            int year = 2024;
//            int month = 8; // August

//            // Define specific days and hours to check
//            var specificTimeSlots = new List<TimeSlot>
//    {
//        new TimeSlot(Cinema.Ræhr, Hall.One, new DateOnly(year, month, 1), new TimeOnly(14, 0)), // 1st day, 14:00
//        new TimeSlot(Cinema.Thorsminde, Hall.Two, new DateOnly(year, month, 10), new TimeOnly(15, 0)), // 10th day, 15:00
//        new TimeSlot(Cinema.Hjerm, Hall.Three, new DateOnly(year, month, 31), new TimeOnly(20, 0))  // Last day, 20:00
//    };

//            // Act
//            _timeSlotManager.GenerateNewMonth(year, month);

//            // Assert: Validate that each specific TimeSlot is present in both TimeSlotsMonth and TimeSlotsAll
//            foreach (var expectedTimeSlot in specificTimeSlots)
//            {
//                // Find the existing TimeSlot in the list (if you don't override Equals, you must find the exact instance)
//                bool foundInMonth = _timeSlotsMonth.TimeSlots.Any(ts =>
//                    ts.Cinema == expectedTimeSlot.Cinema &&
//                    ts.Hall == expectedTimeSlot.Hall &&
//                    ts.Date == expectedTimeSlot.Date &&
//                    ts.Time == expectedTimeSlot.Time);

//                bool foundInAll = _timeSlotsAll.TimeSlots.Any(ts =>
//                    ts.Cinema == expectedTimeSlot.Cinema &&
//                    ts.Hall == expectedTimeSlot.Hall &&
//                    ts.Date == expectedTimeSlot.Date &&
//                    ts.Time == expectedTimeSlot.Time);

//                Assert.IsTrue(foundInMonth,
//                    $"Expected TimeSlot not found in TimeSlotsMonth: {expectedTimeSlot.Cinema}, {expectedTimeSlot.Hall}, {expectedTimeSlot.Date} at {expectedTimeSlot.Time}.");
//                Assert.IsTrue(foundInAll,
//                    $"Expected TimeSlot not found in TimeSlotsAll: {expectedTimeSlot.Cinema}, {expectedTimeSlot.Hall}, {expectedTimeSlot.Date} at {expectedTimeSlot.Time}.");
//            }
//        }
//        [TestMethod]
//        // Testing if AddedTime is Correctly intialized and set to 2* 15 min
//        public void AddedTime_IsInitializedCorrectly()
//        {
//            // Arrange
//            TimeSlotManager timeSlotManager = new TimeSlotManager();

//            // Act
//            AdditionalTime addedTime = timeSlotManager.AddedTime;

//            // Assert
//            Assert.IsNotNull(addedTime);
//            Assert.AreEqual(new TimeSpan(0, 15, 0), addedTime.Ads);
//            Assert.AreEqual(new TimeSpan(0, 15, 0), addedTime.Cleaning);
//        }

//        [TestMethod]
//        public void GetCinemaMonth_ReturnsCorrectTimeSlots()
//        {
//            // Arrange
//            int year = 2024;
//            int month = 8; // August
//            Cinema cinema = Cinema.Ræhr;

//            // Generate the time slots for August
//            _timeSlotManager.GenerateNewMonth(year, month);

//            // Define the expected time slots for the given cinema and month
//            var expectedTimeSlots = new List<TimeSlot>
//    {
//        new TimeSlot(cinema, Hall.One, new DateOnly(year, month, 1), new TimeOnly(14, 0)),
//        // Add other expected time slots if applicable
//    };

//            // Act
//            var result = _timeSlotManager.GetCinemaMonth(cinema, month);

//            // Assert: Check that the returned time slots match the expected results
//            foreach (var expectedTimeSlot in expectedTimeSlots)
//            {
//                bool found = result.Any(ts =>
//                    ts.Cinema == expectedTimeSlot.Cinema &&
//                    ts.Hall == expectedTimeSlot.Hall &&
//                    ts.Date == expectedTimeSlot.Date &&
//                    ts.Time == expectedTimeSlot.Time);

//                Assert.IsTrue(found,
//                    $"Expected TimeSlot not found for Cinema {cinema} in month {month}: {expectedTimeSlot.Cinema}, {expectedTimeSlot.Hall}, {expectedTimeSlot.Date} at {expectedTimeSlot.Time}.");
//            }
//        }

//        [TestMethod]
//        public void GetCinemaMonth_ReturnsEmptyListForMonthWithNoTimeSlots()
//        {
//            // Arrange
//            int year = 2024;
//            int month = 2; // February
//            Cinema cinema = Cinema.Hjerm;

//            // Generate time slots for a different month
//            int otherMonth = 7; // July
//            _timeSlotManager.GenerateNewMonth(year, otherMonth);

//            // Act
//            var result = _timeSlotManager.GetCinemaMonth(cinema, month);

//            // Assert
//            Assert.IsNotNull(result, "The result should not be null.");
//            Assert.AreEqual(0, result.Count, "The result should be an empty list as there are no time slots for this cinema in the specified month.");
//        }

//        [TestMethod]
//        public void GetAvailableMovieTS_ReturnsCorrectAvailableTimeSlots()
//        {
//            // Arrange
//            // Set up the TimeSlotManager and the repositories
//            var timeSlotManager = new TimeSlotManager
//            {
//                TimeSlotsAll = new TimeSlotRepo(),
//                TimeSlotsMonth = new TimeSlotRepo()
//            };

//            // Define the test date and movie duration
//            var selectedDate = new DateOnly(2024, 8, 15);
//            var movieDuration = new TimeSpan(2, 0, 0); // 2 hours

//            // Set up TimeSlotsAll with some test data
//            timeSlotManager.TimeSlotsAll.Add(new TimeSlot(Cinema.Ræhr, Hall.One, selectedDate, new TimeOnly(13, 0)));
//            timeSlotManager.TimeSlotsAll.Add(new TimeSlot(Cinema.Ræhr, Hall.One, selectedDate, new TimeOnly(15, 0))); // Overlaps with the movie
//            timeSlotManager.TimeSlotsAll.Add(new TimeSlot(Cinema.Ræhr, Hall.One, selectedDate, new TimeOnly(17, 0)));

//            // Set up TimeSlotsMonth with some test data
//            timeSlotManager.TimeSlotsMonth.Add(new TimeSlot(Cinema.Ræhr, Hall.One, selectedDate, new TimeOnly(13, 0)));
//            timeSlotManager.TimeSlotsMonth.Add(new TimeSlot(Cinema.Ræhr, Hall.One, selectedDate, new TimeOnly(17, 0)));

//            // Act
//            var availableTimeSlots = timeSlotManager.GetAvailableMovieTS(selectedDate, movieDuration);

//            // Assert
//            // Define the expected available time slots
//            var expectedTimeSlots = new List<TimeSlot>
//            {
//                new TimeSlot(Cinema.Ræhr, Hall.One, selectedDate, new TimeOnly(17, 0))
//            };

//            Assert.AreEqual(expectedTimeSlots.Count, availableTimeSlots.Count, "The count of available time slots is incorrect.");

//            foreach (var expectedSlot in expectedTimeSlots)
//            {
//                bool found = availableTimeSlots.Any(ts =>
//                    ts.Cinema == expectedSlot.Cinema &&
//                    ts.Hall == expectedSlot.Hall &&
//                    ts.Date == expectedSlot.Date &&
//                    ts.Time == expectedSlot.Time);

//                Assert.IsTrue(found,
//                    $"Expected TimeSlot not found in available time slots: {expectedSlot.Cinema}, {expectedSlot.Hall}, {expectedSlot.Date} at {expectedSlot.Time}.");
//            }
//        }




//    }
//}
