using System;

namespace SomToday.NET.Exceptions
{
    public class SomTodayUnauthorizedException : Exception
    {
        public SomTodayUnauthorizedException(int retriesRemaining, 
            string message) : base(message)
        {
            RetriesRemaining = retriesRemaining;
        }
        public int RetriesRemaining { get; }
    }
}
