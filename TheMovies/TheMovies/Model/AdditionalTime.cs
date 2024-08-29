namespace TheMovies.Model
{
    public class AdditionalTime
    {
        private TimeSpan _ads;
        private TimeSpan _cleaning;

        public TimeSpan Ads { get { return _ads; }}
        public TimeSpan Cleaning { get { return _cleaning; }}

        public TimeSpan TotalAdditionalTime
        {
            get { return _ads + _cleaning; }
        }
        
        public AdditionalTime()
        {
            _ads = new TimeSpan(0, 15, 0);
            _cleaning = new TimeSpan(0, 15, 0);
        }
    }
}