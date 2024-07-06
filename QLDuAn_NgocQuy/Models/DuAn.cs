using System.ComponentModel.DataAnnotations;

namespace QLDuAn_NgocQuy.Models
{
    public class DuAn
    {
         
        public string MaDuAn { get; set; }
        public string TenDuAn { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        
    }
}
