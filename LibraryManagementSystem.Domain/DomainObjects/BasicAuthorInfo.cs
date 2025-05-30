using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class BasicAuthorInfo
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string AuthorName { get; set; } = null!;

    public int Sequence { get; set; }

    public bool Alive { get; set; }

    public string CreateTime { get; set; } = null!;

    public decimal CreateTimeUnix { get; set; }

    public string UpdateTime { get; set; } = null!;

    public decimal UpdateTimeUnix { get; set; }

    public Guid UpdateUserCode { get; set; }
}
