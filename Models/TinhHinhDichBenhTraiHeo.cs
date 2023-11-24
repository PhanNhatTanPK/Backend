using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TinhHinhDichBenhTraiHeo
    {
        public Guid Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")] // specify the display format as "dd-MM-yyyy"
        public DateTime? ThoiDiemPhatBenh { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")] // specify the display format as "dd-MM-yyyy"
        public DateTime? ThoiDiemKetThuc { get; set; }
        public int? SoBenh { get; set; }
        [ForeignKey(nameof(LoaiBenhHeo))]
        public long? IdLoaiBenh { get; set; }
        public LoaiBenhHeo? LoaiBenhHeo { get; set; }
        public string? LoaiBenh { get; set; }
        public string? KetQuaXetNghiem { get; set; }
        public string? LoaiVaccine { get; set; }
        public int? SoKhoiBenh { get; set; }
        public int? SoChet { get; set; }
        public string? BienPhapXuLy { get; set; }
        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}