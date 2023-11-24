using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class DanhSachTrangTrai
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string TenTrai { get; set; }
        public string ChuTrangTrai { get; set; }
        public string? DiaChi { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? DienThoai { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal LongitudeNumber { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal LatitudeNumber { get; set; }
        public long? IdLoaiTrangTrai { get; set; }
        public string? IsDich { get; set; }
    }
}
