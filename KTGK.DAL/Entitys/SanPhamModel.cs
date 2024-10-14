using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KTGK.DAL.Entitys
{
    public partial class SanPhamModel : DbContext
    {
        public SanPhamModel()
            : base("name=SanPhamModel")
        {
        }

        public virtual DbSet<LOAISP> LOAISP { get; set; }
        public virtual DbSet<SANPHAM> SANPHAM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LOAISP>()
                .Property(e => e.MALOAI)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MASANPHAM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MALOAI)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
