using LibraryManagementSystem.Contract;
using LibraryManagementSystem.Domain.UnitTest.Fake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.UnitTest.Repositories
{
    public class BasicBookInfoRepository_Tests
    {
        private readonly LibraryDBContextFakeBuilder _contextBuilderFake = new();

        /// <summary>
        /// 檢查ISBN是否重複
        /// </summary>
        [Fact]
        public void CheckISBNIsExist_isbnIsDuplicate_ReturnTrue()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var repository = new BasicBookInfoRepository(context);

            // Act
            bool result = repository.CheckISBNIsExist("9789863714101");

            // Assert
            Assert.True(result);
        }
    }
}
