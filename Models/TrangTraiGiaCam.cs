using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TrangTraiGiaCam
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TenTrai { get; set; }
        public string? ChuTrangTrai { get; set; }
        public string? DiaChi { get; set; }
        public string? TenCongTyDauTu { get; set; }
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
       public string? HinhThucChanNuoi { get; set; }
        public string? CongTyThue { get; set; }
        public string? CoCauGiong { get; set; }
        public long? QuyMo { get; set; }
        public bool? IsGaLongMau { get; set; }
        public bool? IsGaLongTrang { get; set; }
        [ForeignKey(nameof(LoaiGiaCam))]
        public long? IdLoaiGiaCam { get; set; }
        public LoaiGiaCam? LoaiGiaCam { get; set; }
        public string? GiaCam { get; set; }
        public long? DienTichTrangTrai { get; set; }
        public long? DienTichXayDungChuong { get; set; }
        public bool? IsTuongXay { get; set; }
     public bool? IsTuongLuoiB40 { get; set; }
     public bool? IsKhongCoTuong { get; set; }
        public bool? IsHangRaoKin { get; set; }
        public int? SoDayChuongKin { get; set; }
        public bool? IsHangRaoHo { get; set; }
        public int? SoDayChuongHo { get; set; }
        public string? IsQuyetDinhChuTruongDauTu { get; set; }
        public string? QuyetDinhChuTruongDauTu { get; set; }
        public bool? QuyetDinhPheDuyetMoiTruong { get; set; }
        public bool? KeHoachBaoVeMoiTruong { get; set; }
        public string? IsGiayPhepXayDungTrai { get; set; }
        public string? IsSoTheoDoiChanNuoi { get; set; }
        public string? IsChungNhanDieuKienChanNuoi { get; set; }
        public string? SoChungNhanDKCN { get; set; }
        public DateTime? NgayChungNhanDKCN { get; set; }
        public string? SoChungNhanVSTP { get; set; }
        public DateTime? NgayChungNhanVSTP { get; set; }
        public string? SoChungNhanATDB { get; set; }
        public DateTime? NgayChungNhanATDB { get; set; }
        public string? IsChuoiGaATBDXuatKhau { get; set; }
        public string? IsThuocVungATDB { get; set; }
        public bool? IsVietGAHP { get; set; }
        public string? SoVietGAHP { get; set; }
        public DateTime? NgayVietGAHP { get; set; }
        public string? IsNewcastle { get; set; }
        public string? IsH5N1 { get; set; }
        public bool? IsHoSatTrung { get; set; }
        public bool? IsPhongKhuTrung { get; set; }
        public bool? IsMayPhunThuocTrung { get; set; }
        public bool? IsKhongCoHeThongTieuDoc { get; set; }
        public string? IsThucAnChanNuoi { get; set; }
        public bool? NguonNuocQuaXuLy { get; set; }
        public bool? IsNguonNuocKhongQuaXuLy { get; set; }
        public bool? IsGiengKhoan { get; set; }
        public bool? IsGiengDao { get; set; }
        public string? IsKiemTraDinhKy { get; set; }
        public bool? IsChon { get; set; }
        public bool? IsDot { get; set; }
        public bool? IsLuocChinChoCa { get; set; }
        public bool? Khac { get; set; }
        public string? BienPhapKhac { get; set; }

        public string? IsHopDongXuLyRac { get; set; }
        public string? IsBanTuoi {get; set; }
        public string? IsThaiXuongHo { get; set; }
        public bool? IsQuyHoachTrong { get; set; }
        public bool? IsQuyHoachNgoai { get; set; }
        public bool? IsSanXuatGiong { get; set; }
        public bool? IsSanXuatThit { get; set; }
        public bool? IsSanXuatTrung { get; set; }
        public string? IsBiogas { get; set; }
        public double? DienTichKhuXuLy { get; set; }
        public double? HamBiogas { get; set; }
        public double? HoSinhHoc { get; set; }
        public string? IsNhaChuaPhan { get; set; }
        public string? IsBaoCaoSoLieuChanNuoi { get; set; }
        public string? H5N1 { get; set; }
        public string? Newcastle { get; set; }
        public string? BenhKhac { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IsDich { get; set; }
        [ForeignKey(nameof(LoaiTrangTrai))]
        public long? IdLoaiTrangTrai { get; set; }
        public LoaiTrangTrai? LoaiTrangTrai { get; set; }
    }
}