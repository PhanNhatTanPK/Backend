using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Dich
    {
        public Guid? Id { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public int? SoBenh { get; set; }
        public string? TenBenhNghiNgo { get; set; }
        public bool? IsDeleted { get; set; }
        public Guid? IdTrangTrai { get; set; }
    }
}
