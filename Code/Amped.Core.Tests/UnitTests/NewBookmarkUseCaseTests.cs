using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Amped.Core.Tests.UnitTests
{
    public class NewBookmarkUseCaseTests
    {
        private IBookmarkRepository repository;
        
        public NewBookmarkUseCaseTests()
        {
            repository = Substitute.For<IBookmarkRepository>();
        }
        
        [Fact]
        public async Task Can_Add_New_Bookmark_To_Datasource()
        {
            // arrange
            var bookmark = new Bookmark(new Uri("https://totallyamped.com"), "albert", true);
            var sut = new NewBookmarkUseCase(repository);

            // act
            await sut.CreateBookmark(bookmark);
            
            // assert
            await repository.Received(1).Add(Arg.Is(bookmark));
        }
        
        [Fact]
        public void Constructor_Requires_BookmarkRepository()
        {
            Action actual = () => new NewBookmarkUseCase(null);

            actual.Should().Throw<ArgumentNullException>().WithParameterName("bookmarkRepository");
        }

        [Fact]
        public async Task Throws_ArgumentNullException_When_Bookmark_Is_Null()
        {
            var sut = new NewBookmarkUseCase(repository);

            await sut.CreateBookmark(null);
            
            repository.Add(Arg.Any<Bookmark>()).Throws<ArgumentException>();
        }
    }
}