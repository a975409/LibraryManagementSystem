using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Common
{
    public class FileManager
    {
        /// <summary>
        /// 將 IFormFile 轉換為 Data URI 字串
        /// </summary>
        /// <param name="file">要轉換的檔案</param>
        /// <returns>Data URI 格式的字串</returns>
        public async Task<string> ConvertToDataUri(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                //throw new ArgumentException("檔案為空或不存在", nameof(file));
                return string.Empty;
            }

            // 讀取檔案內容
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            // 將檔案內容轉換為 Base64 字串
            string base64String = Convert.ToBase64String(fileBytes);

            // 取得 MIME 類型
            string contentType = file.ContentType;

            // 構建 Data URI
            string dataUri = $"data:{contentType};base64,{base64String}";

            return dataUri;
        }
    }
}
