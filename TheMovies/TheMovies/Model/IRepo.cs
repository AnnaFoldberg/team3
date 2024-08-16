using System;

namespace TheMovies
{
    public interface IRepo<T>
    {
        void Add(T obj);
    }
}