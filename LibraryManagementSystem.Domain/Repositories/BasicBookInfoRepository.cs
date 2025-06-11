using LibraryManagementSystem.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Dapper;
using LibraryManagementSystem.Contract;
using LibraryManagementSystem.Common;
using Microsoft.Data.SqlClient;
using static LibraryManagementSystem.Contract.BasicBookInfoDtos;
using System;

namespace LibraryManagementSystem.Domain
{
    public class BasicBookInfoRepository
    {
        private readonly LibraryDBContext _context;
        private readonly FileManager _fileManager;

        public BasicBookInfoRepository(LibraryDBContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<bool> CanUpdateBookInfo(int id, Guid code)
        {
            var result = await _context.BasicBookInfos.FirstOrDefaultAsync(m => m.Id == id && m.Code == code && m.Alive == true);

            if (result == null)
                throw new ArgumentNullException("notFound");

            return result.Status != 1;
        }

        public async Task<bool> CheckISBNIsExist(string isbn)
        {
            return await _context.BasicBookInfos.AsNoTracking().AnyAsync(m => m.Isbn == isbn && m.Alive == true);
        }

        public async Task<BasicBookInfoDtos.DetailData> GetDetailByCode(string code)
        {
            var basicBookInfo = await _context.BasicBookInfos.AsNoTracking().FirstOrDefaultAsync(m => m.Code.ToString() == code && m.Alive == true);

            if (basicBookInfo == null)
                throw new ArgumentNullException(nameof(basicBookInfo));

            var dto = new BasicBookInfoDtos.DetailData
            {
                Alive = basicBookInfo.Alive,
                Code = basicBookInfo.Code,
                CreateTime = basicBookInfo.CreateTime,
                CreateTimeUnix = basicBookInfo.CreateTimeUnix,
                Description = basicBookInfo.Description,
                Id = basicBookInfo.Id,
                ImgDataUri = basicBookInfo.ImgDataUri,
                Isbn = basicBookInfo.Isbn,
                Language = basicBookInfo.Language,
                PublishedDate = basicBookInfo.PublishedDate,
                PublishedDateUnix = basicBookInfo.PublishedDateUnix,
                Sequence = basicBookInfo.Sequence,
                Status = basicBookInfo.Status,
                Title = basicBookInfo.Title,
                UpdateTime = basicBookInfo.UpdateTime,
                UpdateTimeUnix = basicBookInfo.UpdateTimeUnix,
                UpdateUserCode = basicBookInfo.UpdateUserCode,
                RelationBookInfos = new List<BasicBookInfoDtos.DetailDataOfRelation>()
            };

            string connectionString = _context.Database.GetConnectionString() ?? "";

            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = @"select relation_bookInfo.*,basic_authorInfo.authorName,basic_categoryInfo.categoryName,basic_publisherInfo.publisherName from relation_bookInfo
                                  left join basic_authorInfo on relation_bookInfo.basic_type=0 and basic_authorInfo.code=relation_bookInfo.code
                                  left join basic_categoryInfo on relation_bookInfo.basic_type=1 and basic_categoryInfo.code=relation_bookInfo.code
                                  left join basic_publisherInfo on relation_bookInfo.basic_type=2 and basic_publisherInfo.code=relation_bookInfo.code
                                  where relation_bookInfo.alive=1 and relation_bookInfo.basic_bookInfo_code=@basic_bookInfo_code";

                var parameters = new DynamicParameters();
                parameters.Add("@basic_bookInfo_code", basicBookInfo.Code);

                var resultRelation = await conn.QueryAsync<BasicBookInfoDtos.DetailDataOfRelation>(query, parameters);

                dto.RelationBookInfos = resultRelation.ToList();
            }

            return dto;
        }

        public async Task<string> InsertBasicBookInfo(BasicBookInfoDtos.InsertData insertData)
        {
            DateTime now = DateTime.Now;
            var updateUserCode = Guid.NewGuid();

            var data = new BasicBookInfo
            {
                Alive = true,
                Code = Guid.NewGuid(),
                CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                CreateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                Description = insertData.Description,
                Isbn = insertData.ISBN,
                Language = insertData.Language,
                PublishedDate = insertData.PublishedDate,
                PublishedDateUnix = ConvertUnixTime.DateTimeStringToUnixTimeMilliseconds(insertData.PublishedDate),
                Sequence = 1,
                Status =0,
                Title = insertData.Title,
                UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                UpdateUserCode = updateUserCode,
                ImgDataUri = await _fileManager.ConvertToDataUri(insertData.ImgFile)
            };

            _context.BasicBookInfos.Add(data);

            if (insertData.RelationData != null)
            {
                int i = 1;

                foreach (var item in insertData.RelationData)
                {
                    Guid BasicCode = Guid.Empty;

                    if (item.Type == 0)
                    {
                        var itemResult = await _context.BasicAuthorInfos.AsNoTracking().FirstOrDefaultAsync(m => m.AuthorName == item.Name && m.Alive == true);

                        if (itemResult == null)
                        {
                            itemResult = new BasicAuthorInfo
                            {
                                Alive = true,
                                AuthorName = item.Name,
                                Code = Guid.NewGuid(),
                                CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                                CreateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                                UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                                UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                                Sequence = 1,
                                UpdateUserCode = updateUserCode,
                            };

                            _context.BasicAuthorInfos.Add(itemResult);
                        }

                        BasicCode = itemResult.Code;
                    }
                    else if (item.Type == 1)
                    {
                        var itemResult = await _context.BasicCategoryInfos.AsNoTracking().FirstOrDefaultAsync(m => m.CategoryName == item.Name && m.Alive == true);

                        if (itemResult == null)
                        {
                            itemResult = new BasicCategoryInfo
                            {
                                Alive = true,
                                CategoryName = item.Name,
                                Code = Guid.NewGuid(),
                                CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                                CreateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                                UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                                UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                                Sequence = 1,
                                UpdateUserCode = updateUserCode,
                            };

                            _context.BasicCategoryInfos.Add(itemResult);
                        }

                        BasicCode = itemResult.Code;
                    }
                    else if (item.Type == 2)
                    {
                        var itemResult = await _context.BasicPublisherInfos.AsNoTracking().FirstOrDefaultAsync(m => m.PublisherName == item.Name && m.Alive == true);

                        if (itemResult == null)
                        {
                            itemResult = new BasicPublisherInfo
                            {
                                Alive = true,
                                PublisherName = item.Name,
                                Code = Guid.NewGuid(),
                                CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                                CreateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                                UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                                UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                                Sequence = 1,
                                UpdateUserCode = updateUserCode,
                            };

                            _context.BasicPublisherInfos.Add(itemResult);
                        }

                        BasicCode = itemResult.Code;
                    }
                    else { continue; }

                    _context.RelationBookInfos.Add(new RelationBookInfo
                    {
                        BasicType = item.Type,
                        BasicCode = BasicCode,
                        Alive = true,
                        BasicBookInfoCode = data.Code,
                        Code = Guid.NewGuid(),
                        CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                        CreateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                        UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                        UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                        Sequence = i,
                        UpdateUserCode = updateUserCode,
                    });

                    i++;
                }
            }


            await _context.SaveChangesAsync();

            return data.Code.ToString();
        }

        public async Task UpdateBasicBookInfo(BasicBookInfoDtos.UpdateData updateData)
        {
            var result = await _context.BasicBookInfos.FirstOrDefaultAsync(m => m.Id == updateData.Id && m.Code == updateData.Code && m.Alive == true);

            if (result == null)
                throw new ArgumentNullException(nameof(result), "查無書籍資料");

            DateTime now = DateTime.Now;

            result.Isbn = updateData.ISBN;
            result.Language = updateData.Language;
            result.Title = updateData.Title;
            result.Description = updateData.Description;
            result.PublishedDate = updateData.PublishedDate;
            result.PublishedDateUnix = ConvertUnixTime.DateTimeStringToUnixTimeMilliseconds(updateData.PublishedDate);
            result.ImgDataUri = await _fileManager.ConvertToDataUri(updateData.ImgFile);
            result.UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss");
            result.UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now);

            var resultOfRelationData = await _context.RelationBookInfos.Where(m => m.BasicBookInfoCode == result.Code && m.Alive == true).ToListAsync();

            var updateRelationDataCode = updateData.RelationData.Select(m => m.Code);

            int i = 1;
            foreach (var relationData in resultOfRelationData)
            {
                if (updateRelationDataCode.Contains(relationData.Code))
                {
                    relationData.Sequence = i;
                    i++;
                    continue;
                }

                relationData.Sequence = -1;
                relationData.Alive = false;
            }

            var resultOfRelationDataCode = resultOfRelationData.Select(m => m.Code);

            foreach (var item in updateData.RelationData)
            {
                if (resultOfRelationDataCode.Contains(item.Code))
                    continue;

                _context.RelationBookInfos.Add(new RelationBookInfo
                {
                    Alive = true,
                    BasicBookInfoCode = result.Code,
                    BasicCode = item.Code,
                    Code = Guid.NewGuid(),
                    BasicType = item.Type,
                    CreateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                    CreateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                    UpdateTime = now.ToString("yyyy/MM/dd HH:mm:ss"),
                    UpdateTimeUnix = ConvertUnixTime.DateTimeToUnixTimeMilliseconds(now),
                    Sequence = i
                });

                i++;
            }

            _context.SaveChanges();
        }
    }
}