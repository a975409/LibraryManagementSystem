using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class BasicUserInfo
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string Loginid { get; set; } = null!;

    public string Passwd { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    /// <summary>
    /// 使用者權限
    /// 0：一般使用者、1：管理者
    /// </summary>
    public int Role { get; set; }

    /// <summary>
    /// 使用者狀態
    /// 0：未驗證、1：已驗證、2：已被鎖定無法登入
    /// </summary>
    public int Status { get; set; }

    public int Sequence { get; set; }

    public bool Alive { get; set; }

    public string CreateTime { get; set; } = null!;

    public decimal CreateTimeUnix { get; set; }

    public string UpdateTime { get; set; } = null!;

    public decimal UpdateTimeUnix { get; set; }

    public Guid UpdateUserCode { get; set; }
}
