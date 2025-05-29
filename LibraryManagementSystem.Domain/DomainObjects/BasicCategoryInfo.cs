using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class BasicCategoryInfo
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string CategoryName { get; set; } = null!;

    /// <summary>
    /// 狀態
    /// 0：不可選、1：可選
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
