using System;

namespace Amped.API
{
    public class Bookmark
    {
        public Uri Uri { get; }
        public bool Read { get; private set; }
        public object Owner { get; }
        
        public Bookmark(Uri uri, string owner, bool read = false)
        {
            Uri = uri ?? throw new ArgumentNullException();
            Owner = owner;
            Read = read;
        }

        public void MarkRead()
        {
            Read = true;
        }
    }
}