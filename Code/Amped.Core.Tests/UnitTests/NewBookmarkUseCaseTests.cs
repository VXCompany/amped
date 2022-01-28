using System;
using System.Threading.Tasks;
using Amped.Core.NewBookmark;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Amped.Core.Tests.UnitTests
{
    public class NewBookmarkUseCaseTests
    {
        private readonly IBookmarkRepository _repository;
        
        public NewBookmarkUseCaseTests()
        {
            _repository = Substitute.For<IBookmarkRepository>();
        }
        
        [Fact]
        public async Task Can_Add_New_Bookmark_To_Datasource()
        {
            // arrange
            var payload = new CreateBookmarkCommand
            {
                Uri = new Uri("https://totallyamped.com")
            };
            
            var sut = new CreateBookmarkCommandHandler(_repository);

            // act
            var command = new TestConsumeContextBuilder<CreateBookmarkCommand>()
                .WithValue(payload)
                .Build();
                
            await sut.Consume(command);
            
            // assert
            await _repository.Received(1).Add(Arg.Is<Bookmark>(x => x.Uri == payload.Uri));
        }
        
        [Fact]
        public void Constructor_Requires_BookmarkRepository()
        {
            Action actual = () => new CreateBookmarkCommandHandler(null);

            actual.Should().Throw<ArgumentNullException>().WithParameterName("bookmarkRepository");
        }
    }
}