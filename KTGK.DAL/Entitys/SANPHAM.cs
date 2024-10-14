namespace KTGK.DAL.Entitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [Key]
        [StringLength(5)]
        public string MASANPHAM { get; set; }

        [Required]
        [StringLength(100)]
        public string TENSANPHAM { get; set; }

        public DateTime? NGAYNHAP { get; set; }

        [StringLength(5)]
        public string MALOAI { get; set; }

        public virtual LOAISP LOAISP { get; set; }
    }
    public class SanPhamRepository
    {


        public void Add(SANPHAM sanPham)
        {
            using (var context = new SanPhamModel())
            {
                context.SANPHAM.Add(sanPham);
                context.SaveChanges();
            }
        }

        public void Update(SANPHAM sanPham)
        {
            using (var context = new SanPhamModel())
            {
                context.SANPHAM.AddOrUpdate(sanPham);
                context.SaveChanges();
            }
        }

        public void Delete(string maSanPham)
        {
            using (var context = new SanPhamModel())
            {
                var sanPham = context.SANPHAM.Find(maSanPham);
                if (sanPham != null)
                {
                    context.SANPHAM.Remove(sanPham);
                    context.SaveChanges();
                }
            }
        }
        public SANPHAM FindByTen(string tenSanPham)
        {
            using (var context = new SanPhamModel())
            {
                return context.SANPHAM
                              .FirstOrDefault(sp => sp.TENSANPHAM.Equals(tenSanPham, StringComparison.OrdinalIgnoreCase));
            }
        }

    }
}