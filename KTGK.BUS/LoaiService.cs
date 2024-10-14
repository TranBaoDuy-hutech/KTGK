using KTGK.DAL.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTGK.BUS
{
    public class LoaiService
    {
        public List<LOAISP> GetAll()
        {
            SanPhamModel context = new SanPhamModel();
            return context.LOAISP.ToList();
        }
    }
}
