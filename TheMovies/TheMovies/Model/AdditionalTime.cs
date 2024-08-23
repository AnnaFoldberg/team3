using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Model
{
    public class AdditionalTime
    {
        private TimeSpan _ads = new TimeSpan(0, 15, 0);
        private TimeSpan _cleaning = new TimeSpan(0, 15, 0);

        public TimeSpan Ads { get { return _ads; }}
        public TimeSpan Cleaning { get { return _cleaning; }}

        public AdditionalTime(TimeSpan ads, TimeSpan cleaning)
        {
            _ads = ads;
            _cleaning = cleaning;
        }

    }
}