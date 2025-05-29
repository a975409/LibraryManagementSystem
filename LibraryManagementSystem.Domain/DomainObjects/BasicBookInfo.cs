using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class BasicBookInfo
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
    public decimal? PublishedUnixTime { get; set; }

    public string? Language { get; set; }

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

    public int? Sequence { get; set; }

    public bool Alive { get; set; }

    public decimal? CreateUnixTime { get; set; }

    public string CreateTime { get; set; } = null!;

    public decimal? UpdateUnixTime { get; set; }

    public string UpdateTime { get; set; } = null!;

    public Guid? UpdateUserInfoCode { get; set; }
}
