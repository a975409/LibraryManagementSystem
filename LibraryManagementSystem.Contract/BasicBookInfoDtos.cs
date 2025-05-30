using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Contract
{
    public static class BasicBookInfoDtos
    {
        public class InsertData
        {

            /// <summary>
            /// 書籍標題
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 國際標準書號 (ISBN)
            /// </summary>
            public string ISBN { get; set; }

            /// <summary>
            /// 出版日期
            /// </summary>
            public string PublishedDate { get; set; }

            /// <summary>
            /// 書籍語言
            /// </summary>
            public string Language { get; set; }

            /// <summary>
            /// 書籍摘要
            /// </summary>
            public string Description { get; set; }

            public IFormFile? ImgFile { get; set; } = null;

            /// <summary>
            /// 書籍相關資料列表
            /// </summary>
            public List<relationOfInsertBook>? RelationData { get; set; }
        }

        public class relationOfInsertBook
        {
            public Guid Code { get; set; }
            public int Type { get;set; } 
            public string Name { get; set; }
        }
    }
}