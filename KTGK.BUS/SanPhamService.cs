using KTGK.DAL.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGK.BUS
{
    public class SanPhamService
    {
        public List<SANPHAM> GetAll()
        {
            SanPhamModel context = new SanPhamModel();
            return context.SANPHAM.ToList();
        }
        private readonly SanPhamRepository sapphamRepository;

        public SanPhamService()
        {
            sapphamRepository = new SanPhamRepository();
        }

        public void Add(SANPHAM sanpham)
        {
            sapphamRepository.Add(sanpham);
        }

        public void Update(SANPHAM sanpham)
        {
            sapphamRepository.Update(sanpham);
        }

        public void Delete(string maSanPham)
        {
            sapphamRepository.Delete(maSanPham);
        }


        
        public SANPHAM FindByTenSanPham(string tenSanPham)
        {
            return sapphamRepository.FindByTen(tenSanPham);
        }


    }
}
