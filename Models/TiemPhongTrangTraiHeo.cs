using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TiemPhongTrangTraiHeo
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")] // specify the display format as "dd-MM-yyyy"
        public DateTime? NgayTiemPhong { get; set; }
        [ForeignKey(nameof(LoaiBenhHeo))]
        public long? IdLoaiBenh { get; set; }
        public LoaiBenhHeo? LoaiBenhHeo { get; set; }
        public string? LoaiBenh { get; set; }
        public int? SoDuocTiemPhong { get; set; }
        public string? TenVaccine { get; set; }
        public string? NoiCungCap { get; set; }
        public string? KetQuaGiamSat { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}