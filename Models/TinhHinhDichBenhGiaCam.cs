using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TinhHinhDichBenhGiaCam
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? ThoiDiemPhatBenh { get; set; }
        public DateTime? ThoiDiemKetThuc { get; set; }
        public int? SoBenh { get; set; }
        [ForeignKey(nameof(LoaiBenhGiaCam))]
        public long? IdLoaiBenh { get; set; }
        public LoaiBenhGiaCam? LoaiBenhGiaCam { get; set; }
        public string? LoaiBenh { get; set; }
        public string? KetQuaXetNghiem { get; set; }
        public string? LoaiVaccine { get; set; }
        public int? SoKhoiBenh { get; set; }
        public int? SoChet { get; set; }
        public string? BienPhapXuLy { get; set; }
        [ForeignKey(nameof(TrangTraiGiaCam))]
        public Guid IdTrangTraiGiaCam { get; set; }
        public TrangTraiGiaCam? TrangTraiGiaCam { get; set; }
    }
}
