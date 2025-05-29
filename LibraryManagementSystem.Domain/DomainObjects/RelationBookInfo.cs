using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class RelationBookInfo
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public Guid BasicBookInfoCode { get; set; }

    /// <summary>
    /// 0：basic_authorInfo、1：basic_categoryInfo、2：basic_publisherInfo
    /// </summary>
    public int? BasicType { get; set; }

    public Guid BasicCode { get; set; }

    public int? Sequence { get; set; }

    public bool Alive { get; set; }

    public decimal? CreateUnixTime { get; set; }

    public string CreateTime { get; set; } = null!;

    public decimal? UpdateUnixTime { get; set; }

    public string UpdateTime { get; set; } = null!;

    public Guid? UpdateUserInfoCode { get; set; }
}
