using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TrangTraiDaiGiaSuc
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TenTrai { get; set; }
        public string? ChuTrangTrai { get; set; }
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
        [ForeignKey(nameof(LoaiGiaSuc))]
        public long? IdLoaiGiaSuc { get; set; }
        public LoaiGiaSuc? LoaiGiaSuc { get; set; }
        public int? DucGiong { get; set; }
        public int? Sua { get; set; }
        public int? SinhSan { get; set; }
        public int? Giong { get; set; }
        public int? TongDan { get; set; }
        public string? HinhThucChanNuoi { get; set; }
        public string? HinhThucNuoi { get; set; }
        public bool? IsTuSanXuat { get; set; }
        public bool? IsNhapDiaPhuong { get; set; }
        public bool? IsNhapNgoaiTinh { get; set; }
        public bool? NguonNuocQuaXuLy { get; set; }
        public bool? IsNguonNuocKhongQuaXuLy { get; set; }
        public bool? IsGiengKhoan { get; set; }
        public bool? IsGiengDao { get; set; }
        public bool? IsChon { get; set; }
        public bool? IsDot { get; set; }
        public bool? IsLuocChinChoCa { get; set; }
        public bool? IsHamTuHuy { get; set; }
        public string? IsHeThongBiogas { get; set; }
        public string? IsThuGom { get; set; }
        public string? IsThaiTrucTiep { get; set; }
        public string? IsUPhanViSinh { get; set; }
        public string? IsKhaiBaoNhap { get; set; }
        public string? IsKhaiBaoXuat { get; set; }
        public bool? IsDanhGiaTacDongMoiTruong { get; set; }
        public bool? IsKeHoachBaoVeMoiTruong { get; set; }
        public string? IsXacNhanDKCN { get; set; }
        public string? SoChungNhanDKCN { get; set; }
        public DateTime? NgayXacNhanDKCN { get; set; }
        public string? IsTruyXuatNguonGoc { get; set; }
        public string? IsSoTheoDoiChanNuoi { get; set; }
        public string? IsQuyHoachChanNuoi { get; set; }
        public bool? IsADTB { get; set; }
        public string? ATDBBenhKhac { get; set; }
        public string? SoADTB { get; set; }
        public DateTime? NgayADTB { get; set; }
        public string? SoVIETGAHP { get; set; }
        public DateTime? NgayVIETGAHP { get; set; }
        public string? IsBanTuoi { get; set; }
        public string? IsThaiXuongHo { get; set; }
        public bool? IsQuyHoachTrong { get; set; }
        public bool? IsQuyHoachNgoai { get; set; }
        public bool? IsSanXuatGiong { get; set; }
        public bool? IsSanXuatThit { get; set; }
        public bool? IsSanXuatSua { get; set; }
        public bool? IsHangRaoKin { get; set; }
        public int? SoDayChuongKin { get; set; }

        public bool? IsHangRaoHo { get; set; }
        public int? SoDayChuongHo { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IsDich { get; set; }
        [ForeignKey(nameof(LoaiTrangTrai))]
        public long? IdLoaiTrangTrai { get; set; }
        public LoaiTrangTrai? LoaiTrangTrai { get; set; }

    }
}