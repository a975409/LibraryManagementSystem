using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.UnitTest.Fake
{
    public class LibraryDBContextFakeBuilder : IDisposable
    {
        private readonly LibraryDBContextFake _context = new();

        public LibraryDBContextFakeBuilder AddBasicBookInfo()
        {
            _context.BasicBookInfos.Add(new DomainObjects.BasicBookInfo
            {
                Alive = true,
                Code = Guid.NewGuid(),
                Description = "給孩子一個擁抱，用愛安撫壞情緒。",
                ImgDataUri = "",
                Isbn = "9789863714101",
                Language = "繁體中文",
                PublishedDate = "2023/01/01",
                PublishedDateUnix = 1672502400,
                CreateTimeUnix = 1748604107,
                CreateTime = "2025/05/30 19:21:47",
                Sequence = 1,
                Status = 0,
                Title = "魔法抱抱【情緒管理】（理解並安撫孩子的壞情緒)",
                UpdateTime = "2025/05/30 19:21:47",
                UpdateTimeUnix = 1748604107,
                UpdateUserCode = Guid.NewGuid()
            });

            return this;
        }

        public LibraryDBContextFake Build()
        {
            _context.SaveChanges();
            return _context;
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
