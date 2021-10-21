using System;

namespace Mob
{
    public class ContentItem
    {
        public Uri Uri { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Rating Rating { get; private set; }

        public void SetRating(Rating rating)
        {
            Rating = rating;
        }
    }

    public record Rating(int Value);
}