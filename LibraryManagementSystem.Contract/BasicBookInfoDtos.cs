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
            public List<relationOfInsertBook> RelationData { get; set; }
        }

        public class relationOfInsertBook
        {
            public Guid Code { get; set; }
            public int Type { get; set; }
            public string Name { get; set; }
        }

        public class DetailData
        {
            public int Id { get; set; }

            public Guid Code { get; set; }

            public string Title { get; set; } = null!;

            public string Isbn { get; set; } = null!;

            /// <summary>
            /// 出版日期
            /// </summary>
            public string PublishedDate { get; set; } = null!;

            /// <summary>
            /// 出版日期
            /// </summary>
            public decimal PublishedDateUnix { get; set; }

            public string Language { get; set; } = null!;

            /// <summary>
            /// 摘要
            /// </summary>
            public string Description { get; set; } = null!;

            /// <summary>
            /// 書籍封面圖
            /// </summary>
            public string ImgDataUri { get; set; } = null!;

            /// <summary>
            /// 書籍狀態
            /// 0：尚未上架、1：已上架、2：已下架
            /// </summary>
            public int Status { get; set; }

            public int Sequence { get; set; }

            public bool Alive { get; set; }

            public string CreateTime { get; set; } = null!;

            public decimal CreateTimeUnix { get; set; }

            public string UpdateTime { get; set; } = null!;

            public decimal UpdateTimeUnix { get; set; }

            public Guid UpdateUserCode { get; set; }

            public List<DetailDataOfRelation> RelationBookInfos { get; set; }
        }

        public class DetailDataOfRelation
        {
            public int id { get; set; }

            public Guid code { get; set; }

            public Guid basicBookInfoCode { get; set; }

            /// <summary>
            /// 0：basic_authorInfo、1：basic_categoryInfo、2：basic_publisherInfo
            /// </summary>
            public int basicType { get; set; }

            public Guid basicCode { get; set; }
            public string authorName { get; set; }
            public string categoryName { get; set; }
            public string publisherName { get; set; }
            public int sequence { get; set; }

            public bool alive { get; set; }

            public string createTime { get; set; } = null!;

            public decimal createTimeUnix { get; set; }

            public string updateTime { get; set; } = null!;

            public decimal updateTimeUnix { get; set; }

            public Guid updateUser_Code { get; set; }
        }

        public class UpdateData
        {
            public int Id { get; set; }

            public Guid Code { get; set; }

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

            /// <summary>
            /// 書籍狀態
            /// 0：尚未上架、1：已上架、2：已下架
            /// </summary>
            public int Status { get; set; } = 0;

            public IFormFile? ImgFile { get; set; } = null;

            /// <summary>
            /// 書籍相關資料列表
            /// </summary>
            public List<relationOfUpdateBook> RelationData { get; set; }
        }

        public class relationOfUpdateBook
        {
            public Guid Code { get; set; }
            public int Type { get; set; }
            public string Name { get; set; }
        }
    }
}