using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillObject.Models;

namespace DataAccess.Reponsity
{
    public interface IKhachHangReponsity
    {
        IEnumerable<ChiTietHoaDon> GetAll();
        IEnumerable<ChiTietHoaDon> SearchByID(int id);

        void Add(ChiTietHoaDon cthd);
        void Update(ChiTietHoaDon cthd);
        void Delete(ChiTietHoaDon cthd);

        
    }
}
