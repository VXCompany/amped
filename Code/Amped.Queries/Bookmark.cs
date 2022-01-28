using System;
using System.Text.Json.Serialization;

namespace Amped.Queries
{
    public class Bookmark
    {
        public int Id { get; set; }
        
        public Uri Uri { get; set; }
        
        public bool Read { get; set; }
        
        [JsonIgnore] // muhahaha!
        public Guid Owner { get; set; }
    }
}