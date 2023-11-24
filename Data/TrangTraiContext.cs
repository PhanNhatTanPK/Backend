using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data;

public class TrangTraiContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public TrangTraiContext(DbContextOptions<TrangTraiContext> options)
        : base(options)
    {
    }

    public DbSet<CoSoGietMo> CoSoGietMo { get; set; }
    public DbSet<LoaiTrangTrai> LoaiTrangTrai { get; set; }
    public DbSet<TiemPhongDaiGiaSuc> TiemPhongDaiGiaSuc { get; set; }
    public DbSet<TiemPhongTrangTraiHeo> TiemPhongTrangTraiHeo { get; set; }
    public DbSet<TinhHinhDichBenhDaiGiaSuc> TinhHinhDichBenhDaiGiaSuc { get; set; }
    public DbSet<TinhHinhDichBenhTraiHeo> TinhHinhDichBenhTraiHeo { get; set; }
    public DbSet<TrangTraiDaiGiaSuc> TrangTraiDaiGiaSuc { get; set; }
    public DbSet<TrangTraiHeo> TrangTraiHeo { get; set; }
    public DbSet<LoaiGiaSuc> LoaiGiaSuc { get; set; }
    public DbSet<TrangTraiGiaCam> TrangTraiGiaCam { get; set; }
    public DbSet<LoaiBenhHeo> LoaiBenhHeo { get; set; }
    public DbSet<LoaiBenhGiaSuc> LoaiBenhGiaSuc { get; set; }
    public DbSet<LoaiBenhGiaCam> LoaiBenhGiaCam { get; set; }
    public DbSet<LoaiGiaCam> LoaiGiaCam {get; set;}
    public DbSet<District> District {get; set;}
    public DbSet<TiemPhongGiaCam> TiemPhongGiaCam { get; set; }
    public DbSet<TinhHinhDichBenhGiaCam> TinhHinhDichBenhGiaCam {get; set;}
    public DbSet<CNATDBTTGiaCam> CNATDBTTGiaCam {get; set;}
    public DbSet<CNATDBTTGiaSuc> CNATDBTTGiaSuc { get; set; }
    public DbSet<CNATDBTTHeo> CNATDBTTHeo { get; set; }
    public DbSet<CNDKCNTTGiaCam> CNDKCNTTGiaCam { get; set; }
    public DbSet<CNDKCNTTGiaSuc> CNDKCNTTGiaSuc { get; set; }
    public DbSet<CNDKCNTTHeo> CNDKCNTTHeo { get; set; }
    public DbSet<CNVietGAHPTTGiaCam> CNVietGAHPTTGiaCam { get; set; }
    public DbSet<CNVietGAHPTTGiaSuc> CNVietGAHPTTGiaSuc { get; set; }
    public DbSet<CNVietGAHPTTHeo> CNVietGAHPTTHeo { get; set; }
    public DbSet<CNVSTPTTGiaCam> CNVSTPTTGiaCam { get; set; }
    public DbSet<CNVSTPTTHeo> CNVSTPTTHeo { get; set; }
    public DbSet<SaveBufferPandemic> SaveBufferPandemic { get; set; }
    public DbSet<CNVSTPTTGiaSuc> CNVSTPTTGiaSuc { get; set; }
    public DbSet<TrangTraiTheoHuyen> TrangTraiTheoHuyen { get; set; }
    public DbSet<Dich> Dich { get; set; }
public static object Id { get; internal set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
