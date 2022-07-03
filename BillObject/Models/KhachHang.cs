using System;
using System.Collections.Generic;

#nullable disable

namespace BillObject.Models
{
    public partial class KhachHang
    {
        public int? MaKh { get; set; }
        public string HoTenKh { get; set; }
        public string DiaChiKh { get; set; }
        public string QuocTich { get; set; }
        public string DoiTuongKh { get; set; }
        public double? SoLuongTieuThu { get; set; }
        public double? DinhMucTieuThu { get; set; }
    }
}
