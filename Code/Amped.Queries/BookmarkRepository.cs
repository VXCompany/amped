using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amped.Queries
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<Bookmark>> GetAll();
    }

    public class BookmarkRepository : IBookmarkRepository
    {
        public async Task<IEnumerable<Bookmark>> GetAll()
        {
            // Todo: Implement some form of storage?
            // The following values are for Dimitri, now he can build some sort of a front-end :-)
            var result = new[]
            {
                new Bookmark
                {
                    Id = 1, 
                    Owner = new Guid("00000000-0000-0000-0000-000000000001"), 
                    Read = false, 
                    Uri = new Uri("https://totallyamped.com/why-amped-rulez")
                }
            };

            return result;
        }
    }
}