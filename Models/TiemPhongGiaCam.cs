using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TiemPhongGiaCam
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? NgayTiemPhong { get; set; }
        [ForeignKey(nameof(LoaiGiaCam))]

        public long? IdLoaiGiaCam { get; set; }
        public LoaiGiaCam? LoaiGiaCam { get; set; }
        public string? LoaiGiaCamTiemPhong { get; set; }

        [ForeignKey(nameof(LoaiBenhGiaCam))]
        public long? IdLoaiBenh { get; set; }
        public LoaiBenhGiaCam? LoaiBenhGiaCam { get; set; }
        public string? LoaiBenh { get; set; }
        public int? SoDuocTiemPhong { get; set; }
        public string? TenVaccine { get; set; }
        public string? NoiCungCap { get; set; }
        public string? KetQuaGiamSat { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey(nameof(TrangTraiGiaCam))]
        public Guid IdTrangTraiGiaCam { get; set; }
        public TrangTraiGiaCam? TrangTraiGiaCam { get; set; }
    }
}
