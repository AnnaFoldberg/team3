using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheMovies.Model;
using System;
using System.Linq;

namespace TheMovies.Tests
{
    [TestClass]
    public class TimeSlotManagerTests
    {
        private TimeSlotManager _timeSlotManager;
        private TimeSlotRepo _timeSlotsAll;
        private TimeSlotRepo _timeSlotsMonth;
        private TimeSlotRepo _timeSlotsMovie;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the TimeSlotManager and repositories
            _timeSlotsAll = new TimeSlotRepo();
            _timeSlotsMonth = new TimeSlotRepo();
            _timeSlotsMovie = new TimeSlotRepo();

            _timeSlotManager = new TimeSlotManager
            {
                TimeSlotsAll = _timeSlotsAll,
                TimeSlotsMonth = _timeSlotsMonth,
                TimeSlotsMovie = _timeSlotsMovie
            };
        }
        [TestMethod]
        public void GenerateNewMonth_ShouldAddSpecificTimeSlots()
        {
            // Arrange
            int year = 2024;
            int month = 8; // August

            // Define specific days and hours to check
            var specificTimeSlots = new List<TimeSlot>
    {
        new TimeSlot(Cinema.Ræhr, Hall.One, new DateOnly(year, month, 1), new TimeOnly(14, 0)), // 1st day, 14:00
        new TimeSlot(Cinema.Thorsminde, Hall.Two, new DateOnly(year, month, 10), new TimeOnly(15, 0)), // 10th day, 15:00
        new TimeSlot(Cinema.Hjerm, Hall.Three, new DateOnly(year, month, 31), new TimeOnly(20, 0))  // Last day, 20:00
    };

            // Act
            _timeSlotManager.GenerateNewMonth(year, month);

            // Assert: Validate that each specific TimeSlot is present in both TimeSlotsMonth and TimeSlotsAll
            foreach (var expectedTimeSlot in specificTimeSlots)
            {
                // Find the existing TimeSlot in the list (if you don't override Equals, you must find the exact instance)
                bool foundInMonth = _timeSlotsMonth.TimeSlots.Any(ts =>
                    ts.Cinema == expectedTimeSlot.Cinema &&
                    ts.Hall == expectedTimeSlot.Hall &&
                    ts.Date == expectedTimeSlot.Date &&
                    ts.Time == expectedTimeSlot.Time);

                bool foundInAll = _timeSlotsAll.TimeSlots.Any(ts =>
                    ts.Cinema == expectedTimeSlot.Cinema &&
                    ts.Hall == expectedTimeSlot.Hall &&
                    ts.Date == expectedTimeSlot.Date &&
                    ts.Time == expectedTimeSlot.Time);

                Assert.IsTrue(foundInMonth,
                    $"Expected TimeSlot not found in TimeSlotsMonth: {expectedTimeSlot.Cinema}, {expectedTimeSlot.Hall}, {expectedTimeSlot.Date} at {expectedTimeSlot.Time}.");
                Assert.IsTrue(foundInAll,
                    $"Expected TimeSlot not found in TimeSlotsAll: {expectedTimeSlot.Cinema}, {expectedTimeSlot.Hall}, {expectedTimeSlot.Date} at {expectedTimeSlot.Time}.");
            }
        }
        [TestMethod]
        // Testing if AddedTime is Correctly intialized and set to 2* 15 min
        public void AddedTime_IsInitializedCorrectly()
        {
            // Arrange
            TimeSlotManager timeSlotManager = new TimeSlotManager();

            // Act
            AdditionalTime addedTime = timeSlotManager.AddedTime;

            // Assert
            Assert.IsNotNull(addedTime);
            Assert.AreEqual(new TimeSpan(0, 15, 0), addedTime.Ads);
            Assert.AreEqual(new TimeSpan(0, 15, 0), addedTime.Cleaning);
        }
    }
}
