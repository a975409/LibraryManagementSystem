using LibraryManagementSystem.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Dapper;
using LibraryManagementSystem.Contract;
using LibraryManagementSystem.Common;

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

        public Task<bool> CheckISBNIsExist(string isbn)
        {
            return _context.BasicBookInfos.AsNoTracking().AnyAsync(m => m.Isbn == isbn && m.Alive == true);
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

            var relationList = await _context.RelationBookInfos.AsNoTracking().Where(m => m.BasicBookInfoCode.ToString() == code && m.Alive == true).ToListAsync();

            foreach (var relation in relationList)
            {
                var resultItem = new BasicBookInfoDtos.DetailDataOfRelation
                {
                    Alive = relation.Alive,
                    BasicType = relation.BasicType,
                    BasicBookInfoCode = relation.BasicBookInfoCode,
                    BasicCode = relation.BasicCode,
                    Code = relation.Code,
                    CreateTime = relation.CreateTime,
                    CreateTimeUnix = relation.CreateTimeUnix,
                    Id = relation.Id,
                    Sequence = relation.Sequence,
                    UpdateTime = relation.UpdateTime,
                    UpdateTimeUnix = relation.UpdateTimeUnix,
                    UpdateUserCode = relation.UpdateUserCode,
                };

                if (relation.BasicType == 0)
                {
                    var result = await _context.BasicAuthorInfos.AsNoTracking().FirstOrDefaultAsync(m => m.Alive == true && m.Code == relation.BasicCode);
                    resultItem.Name = result?.AuthorName ?? "";
                }
                else if (relation.BasicType == 1)
                {
                    var result = await _context.BasicCategoryInfos.AsNoTracking().FirstOrDefaultAsync(m => m.Alive == true && m.Code == relation.BasicCode);
                    resultItem.Name = result?.CategoryName ?? "";
                }
                else if (relation.BasicType == 2)
                {
                    var result = await _context.BasicPublisherInfos.AsNoTracking().FirstOrDefaultAsync(m => m.Alive == true && m.Code == relation.BasicCode);
                    resultItem.Name = result?.PublisherName ?? "";
                }

                dto.RelationBookInfos.Add(resultItem);
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
                Status = insertData.Status,
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
    }
}