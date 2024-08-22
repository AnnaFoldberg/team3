using System;

namespace TheMovies
{
    public class DataHandler
    {
        public void Save(List<string> data, string filepath)
        {
            // Append the movie data to the CSV file
            System.IO.File.AppendAllText(filePath, data);
        }
    }
}