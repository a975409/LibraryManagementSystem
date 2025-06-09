using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.UnitTest.Mock
{
    // 建立 IFormFile 的測試替身
    public class FormFileMock : IFormFile
    {
        private readonly byte[] _content;

        public FormFileMock(byte[] content, string fileName)
        {
            _content = content;
            FileName = fileName;
            Length = content.Length;
        }

        public string ContentType { get; set; }
        public string ContentDisposition { get; set; }
        public IHeaderDictionary Headers { get; set; } = new HeaderDictionary();
        public long Length { get; }
        public string Name { get; set; }
        public string FileName { get; }

        public Stream OpenReadStream()
        {
            return new MemoryStream(_content);
        }

        public void CopyTo(Stream target)
        {
            using (var stream = new MemoryStream(_content))
            {
                stream.CopyTo(target);
            }
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            using (var stream = new MemoryStream(_content))
            {
                await stream.CopyToAsync(target, cancellationToken);
            }
        }
    }
}
