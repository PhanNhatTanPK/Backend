using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO
{
    public class JoinDanhSachTrangTrai
    {
       public string Id { get; set; }
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
        public bool IsDich { get; set; }
    }
}
