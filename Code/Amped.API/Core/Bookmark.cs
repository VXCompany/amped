using System;

namespace Amped.API.Core
{
    public class Bookmark
    {
        public Uri Uri { get; }
        public bool Read { get; private set; }
        public object Owner { get; }

        public static Bookmark CreateUnreadBookmark(Uri uri, string owner) => new(uri, owner, false); 
        
        internal Bookmark(Uri uri, string owner, bool read)
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