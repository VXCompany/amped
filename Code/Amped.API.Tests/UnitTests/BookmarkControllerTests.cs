using System;
using System.Threading.Tasks;
using Amped.API.Controllers;
using Amped.Core;
using NSubstitute;
using Xunit;

namespace Amped.API.Tests.UnitTests
{
    public class BookmarkControllerTests
    {
        [Fact]
        public async Task Uses_NewBookmarkUseCase_To_Process_CreateRequest()
        {
            // Arrange
            var request = new CreateBookmarkRequest
            {
                Uri = new Uri("https://app.totallyampednow.com")
            };

            var useCase = Substitute.For<INewBookmarkUseCase>();
            var sut = new BookmarkController();
            
            // Act
            sut.Create(useCase, request);

            // Assert
            await useCase.Received(1).CreateBookmark(Arg.Any<Bookmark>());
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