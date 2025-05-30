

using LibraryManagementSystem.Contract;

namespace LibraryManagementSystem.Domain
{
    public class BasicBookInfoService
    {
        private readonly BasicBookInfoRepository _repository;
        public BasicBookInfoService(BasicBookInfoRepository repository)
        {
            _repository = repository;
        }

        public void InsertBook(string title, string isbn, string publishedDate, string language, string description, List<BasicBookInfoDtos.relationOfInsertBook>? relationData)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(isbn))
                throw new ArgumentNullException("isbn");
            if (string.IsNullOrEmpty(publishedDate))
                throw new ArgumentNullException("publishedDate");
            if (string.IsNullOrEmpty(language))
                throw new ArgumentNullException("language");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            
            if (relationData == null)
                throw new ArgumentNullException("relationData");
            else
            {
                if (relationData.Any(m => m.Type == 0 && string.IsNullOrEmpty(m.Name) == false) == false)
                    throw new ArgumentNullException("authorList");

                if (relationData.Any(m => m.Type == 2 && string.IsNullOrEmpty(m.Name) == false) == false)
                    throw new ArgumentNullException("publisherList");
            }

            if (_repository.CheckISBNIsExist(isbn))
                throw new ArgumentOutOfRangeException("isbn");
        }
    }
}