using LibraryManagementSystem.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Repositories
{
    
    public class BasicUserInfoRepository
    {
        private readonly LibraryDBContext _context;

        public BasicUserInfoRepository(LibraryDBContext context) {
            _context = context;
        }

        public async Task<BasicUserInfo?> GetDetailByCode(string code)
        {
            return await _context.BasicUserInfos.FirstOrDefaultAsync(m => m.Code.ToString() == code);
        }
    }
}
