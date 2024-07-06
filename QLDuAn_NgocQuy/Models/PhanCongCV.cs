
using System.ComponentModel.DataAnnotations;
namespace QLDuAn_NgocQuy.Models
{
    public class PhanCongCV
    {
        public string MaPhanCong { get; set; }
        public string MaDuAn { get; set; }
        public string MaThanhVien { get; set; }
        public string CongViec { get; set; }
        public DateTime NgayGiao  { get; set; }
        public DateTime HanCuoi { get; set; }
        public string TrangThai { get; set; }

    }
}
