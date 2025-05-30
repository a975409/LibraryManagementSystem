

using LibraryManagementSystem.Contract;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Domain
{
    public class BasicBookInfoService
    {
        private readonly BasicBookInfoRepository _repository;
        public BasicBookInfoService(BasicBookInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> InsertBook(BasicBookInfoDtos.InsertData insertData)
        {
            if (string.IsNullOrEmpty(insertData.Title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(insertData.ISBN))
                throw new ArgumentNullException("isbn");
            if (string.IsNullOrEmpty(insertData.PublishedDate))
                throw new ArgumentNullException("publishedDate");
            if (string.IsNullOrEmpty(insertData.Language))
                throw new ArgumentNullException("language");
            if (string.IsNullOrEmpty(insertData.Description))
                throw new ArgumentNullException("description");
            
            if (insertData.RelationData == null)
                throw new ArgumentNullException("relationData");
            else
            {
                if (insertData.RelationData.Any(m => m.Type == 0 && string.IsNullOrEmpty(m.Name) == false) == false)
                    throw new ArgumentNullException("authorList");

                if (insertData.RelationData.Any(m => m.Type == 2 && string.IsNullOrEmpty(m.Name) == false) == false)
                    throw new ArgumentNullException("publisherList");
            }

            if (await _repository.CheckISBNIsExist(insertData.ISBN))
                throw new ArgumentOutOfRangeException("isbn");

            return await _repository.InsertBasicBookInfo(insertData);
        }
    }
}