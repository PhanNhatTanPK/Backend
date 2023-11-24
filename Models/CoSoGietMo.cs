using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CoSoGietMo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TenTrai { get; set; }
        public string ChuTrangTrai { get; set; }
        public string? DiaChi { get; set; }
        [ForeignKey(nameof(District))]
        public long? DistrictId { get; set; }
        public District? District { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? DienThoai { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal LongitudeNumber { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal LatitudeNumber { get; set; }
        public string? CanBoDieuTra { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        public string? SoBaoCao { get; set; }
        public string? LoaiDongVat { get; set; }
        public string? CongSuatGietMo { get; set; }
        public string? SoHoThamGia { get; set; }
        public string? SoNguoiThamGia { get; set; }
        public string? IsGiayPhepGietMo { get; set; }
        public string? IsGiayChungNhanGiamSat { get; set; }
        public string? IsGiayKiemDich { get; set; }
        public string? IsTuongRaoBaoQuanh { get; set; }
        public string? IsLamLongTachBiet { get; set; }
        public string? IsTuongLat { get; set; }
        public string? IsDoBaoHo { get; set; }
        public string? IsViTriGietMo { get; set; }
        public string? IsHinhThucSoHuu { get; set; }
        public string? IsNguonGocDongVat { get; set; }
        public string? IsMuaGiaSuc { get; set; }
        public string? IsNoiTieuThu { get; set; }
        public string? IsCungCapSanPham { get; set; }
        public string? IsLoaiHinhGietMo { get; set; }
        public string? IsPhuongThucGietMo { get; set; }
        //public string? BienPhapXuLy { get; set; }
        public bool? IsChon { get; set; }
        public bool? IsDot { get; set; }
        public bool? IsLuocChinChoCa { get; set; }
        public bool? IsHamTuHuy { get; set; }

        //public string? XuLyChatThai { get; set; }
        public bool? IsBanPhan { get; set; }
        public bool? IsThaiTrucTiepRaVuon { get; set; }
        public bool? IsThaiRaSuoi { get; set; }
        public bool? IsThaiRaHoCa { get; set; }
        public bool? IsBiogas { get; set; }
        public bool? IsThaiXuongHoThu { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IsDich { get; set; }
        [ForeignKey(nameof(LoaiTrangTrai))]
        public long? IdLoaiTrangTrai { get; set; }
        public LoaiTrangTrai? LoaiTrangTrai { get; set; }
    }
}
