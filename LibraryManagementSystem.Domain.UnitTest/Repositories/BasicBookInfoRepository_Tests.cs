using LibraryManagementSystem.Common;
using LibraryManagementSystem.Contract;
using LibraryManagementSystem.Domain.UnitTest.Fake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagementSystem.Domain.UnitTest.Repositories
{
    public class BasicBookInfoRepository_Tests
    {
        private readonly LibraryDBContextFakeBuilder _contextBuilderFake = new();

        /// <summary>
        /// 檢查ISBN是否重複
        /// </summary>
        [Fact]
        public async Task CheckISBNIsExist_isbnIsDuplicate_ReturnTrue()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var repository = new BasicBookInfoRepository(context, new FileManager());

            // Act
            bool result = await repository.CheckISBNIsExist("9789863714101");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckISBNIsExist_isbnIsDuplicate_ReturnFalse()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var repository = new BasicBookInfoRepository(context, new FileManager());

            // Act
            bool result = await repository.CheckISBNIsExist("9789863714100");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanUpdateBookInfo_StatusNotFound_ArgumentNullException()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var repository = new BasicBookInfoRepository(context, new FileManager());

            // Act
            bool result = false;

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                result = await repository.CanUpdateBookInfo(-1, Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b9"));
            });

            Assert.Equal("notFound", exception.ParamName);
        }

        [Fact]
        public async Task CanUpdateBookInfo_StatusNotEqualOne_ReturnTrue()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var repository = new BasicBookInfoRepository(context, new FileManager());

            // Act
            bool result = await repository.CanUpdateBookInfo(1, Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CanUpdateBookInfo_StatusNotEqualOne_ReturnFalse()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var repository = new BasicBookInfoRepository(context, new FileManager());

            // Act
            bool result = await repository.CanUpdateBookInfo(2, Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b9"));

            // Assert
            Assert.False(result);
        }
    }
}
