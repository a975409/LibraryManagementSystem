

using LibraryManagementSystem.Common;
using LibraryManagementSystem.Contract;
using Microsoft.IdentityModel.Tokens;
using static LibraryManagementSystem.Contract.BasicBookInfoDtos;

namespace LibraryManagementSystem.Domain
{
    public class BasicBookInfoService
    {
        private readonly BasicBookInfoRepository _repository;
        private readonly FileManager _fileManager;

        public BasicBookInfoService(BasicBookInfoRepository repository, FileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
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

            if (insertData.RelationData == null || insertData.RelationData.Any() == false)
                throw new ArgumentNullException("relationData");

            if (insertData.RelationData.Any(m => m.Type == 0 && string.IsNullOrEmpty(m.Name) == false) == false)
                throw new ArgumentNullException("authorList");

            if (insertData.RelationData.Any(m => m.Type == 2 && string.IsNullOrEmpty(m.Name) == false) == false)
                throw new ArgumentNullException("publisherList");

            if (await _repository.CheckISBNIsExist(insertData.ISBN))
                throw new ArgumentOutOfRangeException("isbn");

            if (insertData.ImgFile != null && _fileManager.FileExtensionComparison(insertData.ImgFile, "jpg") == false)
                throw new ArgumentException("上傳非圖檔", "ImgFile");

            return await _repository.InsertBasicBookInfo(insertData);
        }

        public async Task UpdateBook(BasicBookInfoDtos.UpdateData updateData)
        {
            if (updateData.Id <= 0 || updateData.Code == Guid.Empty)
                throw new ArgumentNullException("IdOrCode");
            if (string.IsNullOrEmpty(updateData.Title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(updateData.ISBN))
                throw new ArgumentNullException("isbn");
            if (string.IsNullOrEmpty(updateData.PublishedDate))
                throw new ArgumentNullException("publishedDate");
            if (string.IsNullOrEmpty(updateData.Language))
                throw new ArgumentNullException("language");
            if (string.IsNullOrEmpty(updateData.Description))
                throw new ArgumentNullException("description");

            try
            {
                if (await _repository.CanUpdateBookInfo(updateData.Id, updateData.Code) == false)
                    throw new ArgumentOutOfRangeException("Status");
            }
            catch (Exception ex) {
                throw;
            }
            

            if (updateData.RelationData == null || updateData.RelationData.Any() == false)
                throw new ArgumentNullException("relationData");
            
            if (updateData.RelationData.Any(m => m.Type == 0 && string.IsNullOrEmpty(m.Name) == false) == false)
                throw new ArgumentNullException("authorList");

            if (updateData.RelationData.Any(m => m.Type == 2 && string.IsNullOrEmpty(m.Name) == false) == false)
                throw new ArgumentNullException("publisherList");

            if (await _repository.CheckISBNIsExist(updateData.ISBN))
                throw new ArgumentOutOfRangeException("isbn");

            if (updateData.ImgFile != null && _fileManager.FileExtensionComparison(updateData.ImgFile, "jpg") == false)
                throw new ArgumentException("上傳非圖檔", "ImgFile");

            await _repository.UpdateBasicBookInfo(updateData);
        }
    }
}