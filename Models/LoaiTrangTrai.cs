using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class LoaiTrangTrai
    {
        [Key]
        public long Id { get; set; }
        public string? LoaiTrangTraiChanNuoi { get; set; }
    }
}
