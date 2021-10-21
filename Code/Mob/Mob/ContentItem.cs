using System;
using System.Collections.Generic;

namespace Mob
{
    public class ContentItem
    {
        private List<Rating> _ratings = new();


        public Uri Uri { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Rating Rating
        {
            get
            {
                //
            }
        }

        public void AddRating(Rating rating)
        {
            _ratings.Add(rating);
        }
    }
}