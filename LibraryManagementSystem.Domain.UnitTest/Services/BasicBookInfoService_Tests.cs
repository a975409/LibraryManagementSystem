using LibraryManagementSystem.Common;
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
        public async Task InsertBook_RequiredFieldEqualEmpty_ArgumentNullException(string fieldType)
        {
            // Arrange
            string fieldValue = string.Empty;
            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository);
            ArgumentNullException exception = new ArgumentNullException();

            // Act

            // Assert
            switch (fieldType)
            {
                case "title":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
                    {
                        Title = fieldValue,
                        Description = "a",
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = null,
                        ImgFile = null,
                    }));
                    break;
                case "isbn":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
                    {
                        Title = "a",
                        Description = "a",
                        ISBN = fieldValue,
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = null,
                        ImgFile = null,
                    }));
                    break;
                case "publishedDate":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
                    {
                        Title = "a",
                        Description = "a",
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = fieldValue,
                        RelationData = null,
                        ImgFile = null,
                    }));
                    break;
                case "language":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
                    {
                        Title = "a",
                        Description = "a",
                        ISBN = "a",
                        Language = fieldValue,
                        PublishedDate = "a",
                        RelationData = null,
                        ImgFile = null,
                    }));
                    break;
                case "description":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
                    {
                        Title = "a",
                        Description = fieldValue,
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = null,
                        ImgFile = null,
                    }));
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
        public async Task InsertBook_relationDataEqualNull_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> relationData = null;

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
            {
                Description = "a",
                ImgFile = null,
                ISBN = "a",
                Language = "a",
                PublishedDate = "a",
                RelationData = relationData,
                Title = "a"
            }));
            Assert.Equal("relationData", exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫作者，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task InsertBook_AuthorListEqualEmpty_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> authorList = new List<BasicBookInfoDtos.relationOfInsertBook>{
            new BasicBookInfoDtos.relationOfInsertBook{
             Type=2,
             Name="a",
            } };

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
            {
                Description = "a",
                ImgFile = null,
                ISBN = "a",
                Language = "a",
                PublishedDate = "a",
                RelationData = authorList,
                Title = "a"
            }));
            Assert.Equal("authorList", exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task InsertBook_PublisherListEqualEmpty_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfInsertBook> publisherList = new List<BasicBookInfoDtos.relationOfInsertBook> {
            new BasicBookInfoDtos.relationOfInsertBook{
             Type=0,
             Name="a",
            } };

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository);

            // Act


            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
            {
                Description = "a",
                ImgFile = null,
                ISBN = "a",
                Language = "a",
                PublishedDate = "a",
                RelationData = publisherList,
                Title = "a"
            }));
            Assert.Equal("publisherList", exception.ParamName);
        }

        /// <summary>
        /// 新增書籍：沒填寫出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task InsertBook_PisbnIsDuplicate_ArgumentOutOfRangeException()
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
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository);

            // Act

            // Assert
            ArgumentOutOfRangeException exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
            {
                Description = "a",
                ImgFile = null,
                ISBN = "9789863714101",
                Language = "a",
                PublishedDate = "a",
                RelationData = publisherList,
                Title = "a"
            }));
            Assert.Equal("isbn", exception.ParamName);
        }
    }
}