using LibraryManagementSystem.Contract;
using LibraryManagementSystem.Domain.UnitTest.Fake;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibraryManagementSystem.Domain.UnitTest.Services
{
    public class BasicBookInfoService_Tests
    {
        private readonly LibraryDBContextFakeBuilder _contextBuilderFake = new();
        private readonly BasicBookInfoRepository _repository;


        //新增書籍：書籍名稱、ISBN、出版日期、摘要、書籍語言、作者、出版社必填，且ISBN不能重複

        /// <summary>
        /// 新增書籍：沒填寫必填欄位，會跳出錯誤
        /// </summary>
        [Theory]
        [InlineData("title")]
        [InlineData("isbn")]
        [InlineData("publishedDate")]
        [InlineData("language")]
        [InlineData("description")]
        public void InsertBook_RequiredFieldEqualEmpty_ArgumentNullException(string fieldType)
        {
            // Arrange
            string fieldValue = string.Empty;
            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context);
            var service = new BasicBookInfoService(_repository);
            ArgumentNullException exception = new ArgumentNullException();

            // Act

            // Assert
            switch (fieldType)
            {
                case "title":
                    exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook(title: fieldValue, "a", "a", "a", "a", null));
                    break;
                case "isbn":
                    exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", isbn: fieldValue, "a", "a", "a", null));
                    break;
                case "publishedDate":
                    exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", "a", publishedDate: fieldValue, "a", "a", null));
                    break;
                case "language":
                    exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", "a", "a", language: fieldValue, "a", null));
                    break;
                case "description":
                    exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", "a", "a", "a", description: fieldValue, null));
                    break;
                default:
                    break;
            }

            Assert.Equal(fieldType, exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫作者、出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public void InsertBook_relationDataEqualNull_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> relationData = null;

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context);
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", "a", "a", "a", "a", relationData: relationData));
            Assert.Equal("relationData", exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫作者，會跳出錯誤
        /// </summary>
        [Fact]
        public void InsertBook_AuthorListEqualEmpty_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> authorList = new List<BasicBookInfoDtos.relationOfInsertBook>{
            new BasicBookInfoDtos.relationOfInsertBook{
             Type=2,
             Name="a",
            } };

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context);
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", "a", "a", "a", "a", relationData: authorList));
            Assert.Equal("authorList", exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public void InsertBook_PublisherListEqualEmpty_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> publisherList = new List<BasicBookInfoDtos.relationOfInsertBook> {
            new BasicBookInfoDtos.relationOfInsertBook{
             Type=0,
             Name="a",
            } };

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context);
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.InsertBook("a", "a", "a", "a", "a", relationData: publisherList));
            Assert.Equal("publisherList", exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public void InsertBook_PisbnIsDuplicate_ArgumentOutOfRangeException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> publisherList = new List<BasicBookInfoDtos.relationOfInsertBook> {
            new BasicBookInfoDtos.relationOfInsertBook{
             Type=0,
             Name="a",
            },
            new BasicBookInfoDtos.relationOfInsertBook{
             Type=2,
             Name="a",
            }};

            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context);
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.InsertBook("a", "9789863714101", "a", "a", "a",  publisherList));
            Assert.Equal("isbn", exception.ParamName);
        }
    }
}