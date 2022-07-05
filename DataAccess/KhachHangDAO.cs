using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillObject.Models;

namespace DataAccess
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance = null;
        private static readonly object instanceLock = new object();
        private KhachHangDAO() { }

        public static KhachHangDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new KhachHangDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<ChiTietHoaDon> GetAllInfor()
        {
            CSDL_QLHDTDKLContext qlkh = new CSDL_QLHDTDKLContext();
            var hoaDonCt = from khachhang in qlkh.ChiTietHoaDons select khachhang;
            return hoaDonCt.ToList();
        }

        public void New(ChiTietHoaDon cthd)
        {
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    CSDL_QLHDTDKLContext.ChiTietHoaDons.Add(cthd);
                    CSDL_QLHDTDKLContext.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(ChiTietHoaDon cthds)
        {
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    CSDL_QLHDTDKLContext.ChiTietHoaDons.Update(cthds);
                    CSDL_QLHDTDKLContext.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(ChiTietHoaDon cthd)
        {
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    var hoadons = CSDL_QLHDTDKLContext.ChiTietHoaDons.SingleOrDefault(x => x.MaKh == cthd.MaKh);
                    CSDL_QLHDTDKLContext.ChiTietHoaDons.Remove(hoadons);
                    CSDL_QLHDTDKLContext.SaveChanges();
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ChiTietHoaDon> TimKiemTheoID(int id)
        {
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    CSDL_QLHDTDKLContext cSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext();
                    var hd = cSDL_QLHDTDKLContext.ChiTietHoaDons.Where(x => x.MaKh == id);
                    return hd;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ChiTietHoaDon> TimKiemTheoDiaChi(string diaChi)
        {
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    CSDL_QLHDTDKLContext cSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext();
                    var hd = cSDL_QLHDTDKLContext.ChiTietHoaDons.Where(x => x.DiaChiKh.Contains(diaChi));
                    return hd;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ChiTietHoaDon> LocKhachHangTheoQuocTich(string quocTich)
        {
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    CSDL_QLHDTDKLContext cSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext();   
                    var hd = cSDL_QLHDTDKLContext.ChiTietHoaDons.Where(x => x.QuocTich.Contains(quocTich));
                    return hd;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public float TinhTienThanhToanKHVN(int id)
        {
            float kq = 0;
            try
            {
                using (var CSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext())
                {
                    CSDL_QLHDTDKLContext cSDL_QLHDTDKLContext = new CSDL_QLHDTDKLContext();
                    var hd = cSDL_QLHDTDKLContext.ChiTietHoaDons.Where(x => x.MaKh == id);
                    foreach (var item in hd)
                    {
                        if (string.Compare(item.QuocTich, "Vietnames", true) == 0)
                        {
                            if (item.SoLuongTieuThu <= item.DinhMucTieuThu)
                            {
                                kq = (float)(item.SoLuongTieuThu * item.DonGia);
                            }
                            else
                            {
                                kq = (float)(item.DinhMucTieuThu * item.DonGia) +
                                    (float)((item.SoLuongTieuThu - item.DinhMucTieuThu) * item.DonGia * 2.5);
                            }
                        }
                        else
                        {
                            kq = (float)(item.SoLuongTieuThu * item.DonGia);
                        }
                    }
                    return kq;
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
