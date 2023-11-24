using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TinhHinhDichBenhDaiGiaSuc
    {
        public Guid Id { get; set; }
        public DateTime? ThoiDiemPhatBenh { get; set; }
        public DateTime? ThoiDiemKetThuc { get; set; }
        public int? SoBenh { get; set; }
        [ForeignKey(nameof(LoaiBenhGiaSuc))]
        public long? IdLoaiBenh { get; set; }
        public LoaiBenhGiaSuc? LoaiBenhGiaSuc { get; set; }
        public string? LoaiBenh { get; set; }
        public string? KetQuaXetNghiem { get; set; }
        public string? LoaiVaccine { get; set; }
        public int? SoKhoiBenh { get; set; }
        public int? SoChet { get; set; }
        public string? BienPhapXuLy { get; set; }
        [ForeignKey(nameof(TrangTraiDaiGiaSuc))]
        public Guid IdTrangTraiDaiGiaSuc { get; set; }
        public TrangTraiDaiGiaSuc? TrangTraiDaiGiaSuc { get; set; }
    }
}