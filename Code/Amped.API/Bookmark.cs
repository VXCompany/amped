using System;

namespace Amped.API.Tests
{
    public class Bookmark
    {
        public Uri Uri { get; }
        public bool Read { get; }

        public Bookmark(Uri uri, bool read = false)
        {
            Uri = uri ?? throw new ArgumentNullException();
            Read = read;
        }
    }
}