using System;
using System.Threading.Tasks;
using Amped.API.Controllers;
using Amped.Core.NewBookmark;
using NSubstitute;
using Xunit;

namespace Amped.API.Tests.UnitTests
{
    public class BookmarkControllerTests
    {
        [Fact]
        public async Task Sends_CreateBookmarkRequest_To_Bus()
        {
            // Arrange
            var request = new CreateBookmarkCommand
            {
                Uri = new Uri("https://app.totallyampednow.com")
            };

            var bus = Substitute.For<IBus>();
            
            var sut = new BookmarkController();
            
            // Act
            await sut.Create(bus, request);

            // Assert
            await bus.Received(1).Send(Arg.Is(request));
        }
        
        [Fact]
        public async Task Uses_BookmarkRepository_To_List_Bookmarks()
        {
            // Arrange
            var repository = Substitute.For<Queries.IBookmarkRepository>();
            var sut = new BookmarkController();
            
            // Act
            await sut.Get(repository);

            // Assert
            await repository.Received(1).GetAll();
        }
    }
}