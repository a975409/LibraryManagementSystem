using LibraryManagementSystem.Common;
using LibraryManagementSystem.Contract;
using LibraryManagementSystem.Domain.UnitTest.Fake;
using LibraryManagementSystem.Domain.UnitTest.Mock;
using Microsoft.AspNetCore.Http;
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
        private readonly FileManager _fileManager = new();


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
            var service = new BasicBookInfoService(_repository, _fileManager);
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
                        RelationData = new List<BasicBookInfoDtos.relationOfInsertBook>(),
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
                        RelationData = new List<BasicBookInfoDtos.relationOfInsertBook>(),
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
                        RelationData = new List<BasicBookInfoDtos.relationOfInsertBook>(),
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
                        RelationData = new List<BasicBookInfoDtos.relationOfInsertBook>(),
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
                        RelationData = new List<BasicBookInfoDtos.relationOfInsertBook>(),
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
            List<BasicBookInfoDtos.relationOfInsertBook> relationData = new List<BasicBookInfoDtos.relationOfInsertBook>();

            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

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
            var service = new BasicBookInfoService(_repository, _fileManager);

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
            var service = new BasicBookInfoService(_repository, _fileManager);

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
        /// 新增書籍：填寫的isbn值重複，會跳出錯誤
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
            var service = new BasicBookInfoService(_repository, _fileManager);

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


        /// <summary>
        /// 新增書籍：如果上傳非圖檔，則會跳出錯誤
        /// </summary>
        [Fact]
        public async Task InsertBook_UploadFileIsNotImg_ArgumentException()
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
            var service = new BasicBookInfoService(_repository, _fileManager);
            var fileContent = Encoding.UTF8.GetBytes("This is a test file content");
            var formFile = new FormFileMock(fileContent, "test.txt")
            {
                ContentType = "text/plain"
            };

            // Act


            // Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => service.InsertBook(new BasicBookInfoDtos.InsertData
            {
                Description = "a",
                ImgFile = formFile,
                ISBN = "4717211037911",
                Language = "a",
                PublishedDate = "a",
                RelationData = publisherList,
                Title = "a"
            }));

            Assert.Equal("ImgFile", exception.ParamName);
        }

        //編輯書籍：書籍名稱、ISBN、出版日期、摘要、書籍語言、作者、出版社必填，且ISBN不能重複

        /// <summary>
        /// 更新書籍：如果更新已上架的書籍，會跳出錯誤
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateBook_StatusEqualOne_ArgumentOutOfRangeException()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

            // Act

            // Assert
            ArgumentOutOfRangeException exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b9"),
                Id = 2,
                Title = "a",
                Description = "a",
                ISBN = "a",
                Language = "a",
                PublishedDate = "a",
                RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                ImgFile = null,
            }));

            Assert.Equal("Status", exception.ParamName);
        }

        /// <summary>
        /// 更新書籍：沒找到指定書籍，會跳出錯誤
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateBook_NotFound_ArgumentNullException()
        {
            // Arrange
            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

            // Act

            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"),
                Id = 5,
                Title = "a",
                Description = "a",
                ISBN = "a",
                Language = "a",
                PublishedDate = "a",
                RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                ImgFile = null,
            }));

            Assert.Equal("notFound", exception.ParamName);
        }

        /// <summary>
        /// 更新書籍：沒填寫必填欄位，會跳出錯誤
        /// </summary>
        /// <param name="fieldType"></param>
        [Theory]
        [InlineData("IdOrCode")]
        [InlineData("title")]
        [InlineData("isbn")]
        [InlineData("publishedDate")]
        [InlineData("language")]
        [InlineData("description")]
        public async Task UpdateBook_RequiredFieldEqualEmpty_ArgumentNullException(string fieldType)
        {
            // Arrange
            string fieldValue = string.Empty;
            var context = _contextBuilderFake.Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);
            ArgumentNullException exception = new ArgumentNullException();

            // Act

            // Assert
            switch (fieldType)
            {
                case "IdOrCode":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
                    {
                        Code = Guid.Empty,
                        Id = 1,
                        Title = fieldValue,
                        Description = "a",
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                        ImgFile = null,
                    }));
                    break;
                case "title":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
                    {
                        Code = Guid.NewGuid(),
                        Id = 1,
                        Title = fieldValue,
                        Description = "a",
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                        ImgFile = null,
                    }));
                    break;
                case "isbn":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
                    {
                        Code = Guid.NewGuid(),
                        Id = 1,
                        Title = "a",
                        Description = "a",
                        ISBN = fieldValue,
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                        ImgFile = null,
                    }));
                    break;
                case "publishedDate":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
                    {
                        Code = Guid.NewGuid(),
                        Id = 1,
                        Title = "a",
                        Description = "a",
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = fieldValue,
                        RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                        ImgFile = null,
                    }));
                    break;
                case "language":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
                    {
                        Code = Guid.NewGuid(),
                        Id = 1,
                        Title = "a",
                        Description = "a",
                        ISBN = "a",
                        Language = fieldValue,
                        PublishedDate = "a",
                        RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                        ImgFile = null,
                    }));
                    break;
                case "description":
                    exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
                    {
                        Code = Guid.NewGuid(),
                        Id = 1,
                        Title = "a",
                        Description = fieldValue,
                        ISBN = "a",
                        Language = "a",
                        PublishedDate = "a",
                        RelationData = new List<BasicBookInfoDtos.relationOfUpdateBook>(),
                        ImgFile = null,
                    }));
                    break;
                default:
                    break;
            }

            Assert.Equal(fieldType, exception.ParamName);
        }

        /// <summary>
        /// 更新書籍：沒填寫作者、出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task UpdateBook_relationDataEqualNull_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfUpdateBook> relationData = new List<BasicBookInfoDtos.relationOfUpdateBook>();

            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

            // Act


            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"),
                Id = 1,
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
        /// 更新書籍：沒填寫作者，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task UpdateBook_AuthorListEqualEmpty_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfUpdateBook> authorList = new List<BasicBookInfoDtos.relationOfUpdateBook>{
            new BasicBookInfoDtos.relationOfUpdateBook{
             Type=2,
             Name="a",
            } };

            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

            // Act


            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"),
                Id = 1,
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
        /// 更新書籍：沒填寫出版社，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task UpdateBook_PublisherListEqualEmpty_ArgumentNullException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfUpdateBook> publisherList = new List<BasicBookInfoDtos.relationOfUpdateBook> {
            new BasicBookInfoDtos.relationOfUpdateBook{
             Type=0,
             Name="a",
            } };

            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

            // Act


            // Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"),
                Id = 1,
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
        /// 更新書籍：填寫的isbn值重複，會跳出錯誤
        /// </summary>
        [Fact]
        public async Task UpdateBook_PisbnIsDuplicate_ArgumentOutOfRangeException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfUpdateBook> publisherList = new List<BasicBookInfoDtos.relationOfUpdateBook> {
            new BasicBookInfoDtos.relationOfUpdateBook{
             Type=0,
             Name="a",
            },
            new BasicBookInfoDtos.relationOfUpdateBook{
             Type=2,
             Name="a",
            }};

            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);

            // Act

            // Assert
            ArgumentOutOfRangeException exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Id = 1,
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"),
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


        /// <summary>
        /// 更新書籍：如果上傳非圖檔，則會跳出錯誤
        /// </summary>
        [Fact]
        public async Task UpdateBook_UploadFileIsNotImg_ArgumentException()
        {
            // Arrange
            List<BasicBookInfoDtos.relationOfUpdateBook> publisherList = new List<BasicBookInfoDtos.relationOfUpdateBook> {
            new BasicBookInfoDtos.relationOfUpdateBook{
             Type=0,
             Name="a",
            },
            new BasicBookInfoDtos.relationOfUpdateBook{
             Type=2,
             Name="a",
            }};

            var context = _contextBuilderFake.AddBasicBookInfo().Build();
            var _repository = new BasicBookInfoRepository(context, new FileManager());
            var service = new BasicBookInfoService(_repository, _fileManager);
            var fileContent = Encoding.UTF8.GetBytes("This is a test file content");
            var formFile = new FormFileMock(fileContent, "test.txt")
            {
                ContentType = "text/plain"
            };

            // Act


            // Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateBook(new BasicBookInfoDtos.UpdateData
            {
                Code = Guid.Parse("c6ec7f91-3bf0-47d9-a224-71b3ac5019b8"),
                Id = 1,
                Description = "a",
                ImgFile = formFile,
                ISBN = "4717211037911",
                Language = "a",
                PublishedDate = "a",
                RelationData = publisherList,
                Title = "a"
            }));

            Assert.Equal("ImgFile", exception.ParamName);
        }
    }
}