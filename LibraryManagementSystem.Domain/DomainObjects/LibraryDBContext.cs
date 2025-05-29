using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Domain.DomainObjects;

public partial class LibraryDBContext : DbContext
{
    public LibraryDBContext()
    {
    }

    public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BasicAuthorInfo> BasicAuthorInfos { get; set; }

    public virtual DbSet<BasicBookInfo> BasicBookInfos { get; set; }

    public virtual DbSet<BasicCategoryInfo> BasicCategoryInfos { get; set; }

    public virtual DbSet<BasicPublisherInfo> BasicPublisherInfos { get; set; }

    public virtual DbSet<BasicUserInfo> BasicUserInfos { get; set; }

    public virtual DbSet<RelationBookInfo> RelationBookInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LibraryDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BasicAuthorInfo>(entity =>
        {
            entity.HasKey(e => e.Code)
                .HasName("PK_authorInfo")
                .IsClustered(false);

            entity.ToTable("basic_authorInfo");

            entity.HasIndex(e => e.Id, "IX_authorInfo").IsClustered();

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Alive)
                .HasDefaultValue(true)
                .HasColumnName("alive");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasComment("")
                .HasColumnName("authorName");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(50)
                .HasColumnName("createTime");
            entity.Property(e => e.CreateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("createUnixTime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.UpdateTime)
                .HasMaxLength(50)
                .HasColumnName("updateTime");
            entity.Property(e => e.UpdateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("updateUnixTime");
            entity.Property(e => e.UpdateUserInfoCode).HasColumnName("updateUserInfo_Code");
        });

        modelBuilder.Entity<BasicBookInfo>(entity =>
        {
            entity.HasKey(e => e.Code)
                .HasName("PK_bookInfo")
                .IsClustered(false);

            entity.ToTable("basic_bookInfo");

            entity.HasIndex(e => e.Id, "IX_bookInfo").IsClustered();

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Alive)
                .HasDefaultValue(true)
                .HasColumnName("alive");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(50)
                .HasColumnName("createTime");
            entity.Property(e => e.CreateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("createUnixTime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasComment("摘要")
                .HasColumnName("description");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.ImgDataUri)
                .HasComment("書籍封面圖")
                .HasColumnName("imgDataURI");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("isbn");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasColumnName("language");
            entity.Property(e => e.PublishedDate)
                .HasMaxLength(50)
                .HasComment("出版日期")
                .HasColumnName("published_date");
            entity.Property(e => e.PublishedUnixTime)
                .HasComment("出版日期")
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("published_UnixTime");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.Status)
                .HasComment("書籍狀態\r\n0：尚未上架、1：已上架、2：已下架")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasComment("")
                .HasColumnName("title");
            entity.Property(e => e.UpdateTime)
                .HasMaxLength(50)
                .HasColumnName("updateTime");
            entity.Property(e => e.UpdateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("updateUnixTime");
            entity.Property(e => e.UpdateUserInfoCode).HasColumnName("updateUserInfo_Code");
        });

        modelBuilder.Entity<BasicCategoryInfo>(entity =>
        {
            entity.HasKey(e => e.Code)
                .HasName("PK_categoryInfo")
                .IsClustered(false);

            entity.ToTable("basic_categoryInfo");

            entity.HasIndex(e => e.Id, "IX_categoryInfo").IsClustered();

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Alive)
                .HasDefaultValue(true)
                .HasColumnName("alive");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasComment("")
                .HasColumnName("categoryName");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(50)
                .HasColumnName("createTime");
            entity.Property(e => e.CreateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("createUnixTime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.Status)
                .HasComment("狀態\r\n0：不可選、1：可選")
                .HasColumnName("status");
            entity.Property(e => e.UpdateTime)
                .HasMaxLength(50)
                .HasColumnName("updateTime");
            entity.Property(e => e.UpdateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("updateUnixTime");
            entity.Property(e => e.UpdateUserInfoCode).HasColumnName("updateUserInfo_Code");
        });

        modelBuilder.Entity<BasicPublisherInfo>(entity =>
        {
            entity.HasKey(e => e.Code)
                .HasName("PK_publisherInfo")
                .IsClustered(false);

            entity.ToTable("basic_publisherInfo");

            entity.HasIndex(e => e.Id, "IX_publisherInfo").IsClustered();

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Alive)
                .HasDefaultValue(true)
                .HasColumnName("alive");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(50)
                .HasColumnName("createTime");
            entity.Property(e => e.CreateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("createUnixTime");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(50)
                .HasComment("")
                .HasColumnName("publisherName");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.UpdateTime)
                .HasMaxLength(50)
                .HasColumnName("updateTime");
            entity.Property(e => e.UpdateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("updateUnixTime");
            entity.Property(e => e.UpdateUserInfoCode).HasColumnName("updateUserInfo_Code");
        });

        modelBuilder.Entity<BasicUserInfo>(entity =>
        {
            entity.HasKey(e => e.Code)
                .HasName("PK_userInfo")
                .IsClustered(false);

            entity.ToTable("basic_userInfo");

            entity.HasIndex(e => e.Id, "IX_userInfo").IsClustered();

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Alive)
                .HasDefaultValue(true)
                .HasColumnName("alive");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(50)
                .HasColumnName("createTime");
            entity.Property(e => e.CreateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("createUnixTime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Loginid)
                .HasMaxLength(50)
                .HasComment("")
                .HasColumnName("loginid");
            entity.Property(e => e.Passwd)
                .HasMaxLength(50)
                .HasColumnName("passwd");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasComment("使用者權限\r\n0：一般使用者、1：管理者")
                .HasColumnName("role");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.Status)
                .HasComment("使用者狀態\r\n0：未驗證、1：已驗證、2：已被鎖定無法登入")
                .HasColumnName("status");
            entity.Property(e => e.UpdateTime)
                .HasMaxLength(50)
                .HasColumnName("updateTime");
            entity.Property(e => e.UpdateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("updateUnixTime");
        });

        modelBuilder.Entity<RelationBookInfo>(entity =>
        {
            entity.HasKey(e => e.Code)
                .HasName("PK_relation_bookInfo_categoryInfo")
                .IsClustered(false);

            entity.ToTable("relation_bookInfo");

            entity.HasIndex(e => e.Id, "IX_relation_bookInfo_categoryInfo").IsClustered();

            entity.HasIndex(e => e.BasicBookInfoCode, "IX_relation_bookInfo_categoryInfo_basic_bookInfo_code");

            entity.HasIndex(e => e.BasicCode, "IX_relation_bookInfo_categoryInfo_basic_code");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasColumnName("code");
            entity.Property(e => e.Alive)
                .HasDefaultValue(true)
                .HasColumnName("alive");
            entity.Property(e => e.BasicBookInfoCode)
                .HasComment("")
                .HasColumnName("basic_bookInfo_code");
            entity.Property(e => e.BasicCode).HasColumnName("basic_code");
            entity.Property(e => e.BasicType)
                .HasComment("0：basic_authorInfo、1：basic_categoryInfo、2：basic_publisherInfo")
                .HasColumnName("basic_type");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(50)
                .HasColumnName("createTime");
            entity.Property(e => e.CreateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("createUnixTime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.UpdateTime)
                .HasMaxLength(50)
                .HasColumnName("updateTime");
            entity.Property(e => e.UpdateUnixTime)
                .HasColumnType("decimal(20, 0)")
                .HasColumnName("updateUnixTime");
            entity.Property(e => e.UpdateUserInfoCode).HasColumnName("updateUserInfo_Code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
