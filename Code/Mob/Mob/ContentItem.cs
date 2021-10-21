using System;

namespace Mob
{
    public class ContentItem
    {
        public Uri Uri { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();
        
        public int Rating { get; set; }
    }
}