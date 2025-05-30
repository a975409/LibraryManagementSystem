using LibraryManagementSystem.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace LibraryManagementSystem.Domain
{
    public class BasicBookInfoRepository
    {
        private LibraryDBContext _context;

        public BasicBookInfoRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public bool CheckISBNIsExist(string isbn)
        {
            return _context.BasicBookInfos.AsNoTracking().Any(m => m.Isbn == isbn && m.Alive == true);
        }
    }
}