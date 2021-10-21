using System;

namespace Mob
{
    public class ContentItem
    {
        public Uri Uri { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public int Rating { get; private set; }

        public void SetRating(int rating)
        {
            if (rating > 5)
            {
                throw new ArgumentException();
            }

            Rating = rating;
        }
    }
}