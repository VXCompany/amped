using System;
using System.Collections.Generic;

namespace Mob
{
    public class ContentItem
    {
        public Uri Uri { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Rating Rating { get; private set; }

        public void AddRating(Rating rating)
        {
            Rating = rating;
        }
    }

    public record Rating(int Value);
}