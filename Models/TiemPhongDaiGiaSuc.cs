using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TiemPhongDaiGiaSuc
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? NgayTiemPhong { get; set; }
        [ForeignKey(nameof(LoaiGiaSuc))]

        public long? IdLoaiGiaSuc { get; set; }
        public LoaiGiaSuc? LoaiGiaSuc { get; set; }
        public string? LoaiGiaSucTiemPhong { get; set; }

        [ForeignKey(nameof(LoaiBenhGiaSuc))]
        public long? IdLoaiBenh { get; set; }
        public LoaiBenhGiaSuc? LoaiBenhGiaSuc { get; set; }
        public string? LoaiBenh { get; set; }
        public int? SoDuocTiemPhong { get; set; }
        public string? TenVaccine { get; set; }
        public string? NoiCungCap { get; set; }
        public string? KetQuaGiamSat { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey(nameof(TrangTraiDaiGiaSuc))]
        public Guid IdTrangTraiDaiGiaSuc { get; set; }
        public TrangTraiDaiGiaSuc? TrangTraiDaiGiaSuc { get; set; }

    }
}