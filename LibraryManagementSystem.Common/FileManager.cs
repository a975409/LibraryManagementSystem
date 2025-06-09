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
        private Dictionary<string, List<byte[]>> fileSignature = new Dictionary<string, List<byte[]>>
                    {
                    { ".DOC", new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } } },
                    { ".DOCX", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
                    { ".PDF", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
                    { ".ZIP", new List<byte[]>
                                            {
                                              new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                                              new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x55 },
                                              new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                                              new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                                              new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                                              new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 }
                                                }
                                            },
                    { ".PNG", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
                    { ".JPG", new List<byte[]>
                                    {
                                              new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                              new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                                              new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
                                    }
                                    },
                    { ".JPEG", new List<byte[]>
                                        {
                                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                                            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
                                        }
                                        },
                    { ".XLS", new List<byte[]>
                                            {
                                              new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                                              new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                                              new byte[] { 0xFD, 0xFF, 0xFF, 0xFF }
                                            }
                                            },
                    { ".XLSX", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
                    { ".GIF", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } }
                };

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

        /// <summary>
        /// 驗證檔案內容與副檔名是否符合
        /// </summary>
        /// <param name="file"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        private bool IsValidFileExtension(IFormFile file, byte[] allowedChars)
        {
            if (file == null)
                return false;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileName = file.FileName;
                var fileBytes = ms.ToArray();

                if (string.IsNullOrEmpty(fileName) || fileBytes == null || fileBytes.Length == 0)
                {
                    return false;
                }

                bool flag = false;
                string ext = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(ext))
                {
                    return false;
                }

                ext = ext.ToUpperInvariant();

                if (ext.Equals(".TXT") || ext.Equals(".CSV") || ext.Equals(".PRN"))
                {
                    foreach (byte b in fileBytes)
                    {
                        if (b > 0x7F)
                        {
                            if (allowedChars != null)
                            {
                                if (!allowedChars.Contains(b))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }

                if (!fileSignature.ContainsKey(ext))
                {
                    return false;
                }

                List<byte[]> sig = fileSignature[ext];
                foreach (byte[] b in sig)
                {
                    var curFileSig = new byte[b.Length];
                    Array.Copy(fileBytes, curFileSig, b.Length);
                    if (curFileSig.SequenceEqual(b))
                    {
                        flag = true;
                        break;
                    }
                }

                return flag;
            }
        }

        /// <summary>
        /// 比對副檔名
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public bool FileExtensionComparison(IFormFile file,string fileExtension)
        {
            if (IsValidFileExtension(file, new byte[] { }) == false)
                return false;

            var fileName = file.FileName;
            string ext = Path.GetExtension(fileName).ToUpperInvariant();

            return ext == fileExtension.ToUpperInvariant();
        }
    }
}
