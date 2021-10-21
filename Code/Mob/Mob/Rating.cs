using System;

namespace Mob
{
    public record Rating
    {
        public Rating(int value)
        {
            if (value < 1)
            {
                throw new ArgumentException();
            }

            if (value > 5)
            {
                throw new ArgumentException();
            }

            Value = value;
        }


        public int Value { get; }
    }
}