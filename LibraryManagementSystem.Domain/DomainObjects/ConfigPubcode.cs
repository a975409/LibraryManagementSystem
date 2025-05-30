using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class ConfigPubcode
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string CodeKind { get; set; } = null!;

    public string CodeName { get; set; } = null!;

    public string CodeNameEn { get; set; } = null!;

    public string CodeValue { get; set; } = null!;

    public int Sequence { get; set; }

    public int Enable { get; set; }

    public int Editable { get; set; }
}
