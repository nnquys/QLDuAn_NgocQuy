using Microsoft.EntityFrameworkCore;
using QLDuAn_NgocQuy.Models;
namespace QLDuAn_NgocQuy.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DuAn> DuAn { get; set; }
        public DbSet<DangNhap> DangNhap { get; set; }
        public DbSet<ThanhVien> thanhVien { get; set; }
        public DbSet<PhanCongCV> phanCongCV { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DangNhap>()
                .HasKey(d => d.TaiKhoan); // Xác định khóa chính là TaiKhoan
            modelBuilder.Entity<DuAn>()
                .HasKey(d => d.MaDuAn); // Xác định khóa chính là MaDuAn
            modelBuilder.Entity<ThanhVien>()
                .HasKey(d => d.MaThanhVien); // Xác định khóa chính là MaThanhVien
            modelBuilder.Entity<PhanCongCV>()
                .HasKey(d => d.MaPhanCong); // Xác định khóa chính là MaThanhVien
        }
    }

}
