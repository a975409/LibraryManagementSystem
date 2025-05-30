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
    }
}
