using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillObject.Models;

namespace DataAccess.Reponsity
{
    public class KhachHangReponsity : IKhachHangReponsity
    {
        void IKhachHangReponsity.Add(ChiTietHoaDon cthd) => KhachHangDAO.Instance.New(cthd);

        void IKhachHangReponsity.Delete(ChiTietHoaDon cthd) => KhachHangDAO.Instance.Delete(cthd);

        IEnumerable<ChiTietHoaDon> IKhachHangReponsity.GetAll() => KhachHangDAO.Instance.GetAllInfor();


        IEnumerable<ChiTietHoaDon> IKhachHangReponsity.SearchByID(int id) => KhachHangDAO.Instance.TimKiemTheoID(id);

        void IKhachHangReponsity.Update(ChiTietHoaDon cthd) => KhachHangDAO.Instance.Update(cthd);

    }
}
