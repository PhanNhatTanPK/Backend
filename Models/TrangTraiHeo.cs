using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;


namespace Backend.Models
{

    public class TrangTraiHeo
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
        public string? TenCongTyDauTu { get; set; }
         public string? HinhThucChanNuoi { get; set; }
        public string? CongTyThue { get; set; }
        public string? CoCauGiong { get; set; }
        public int? QuyMo { get; set; }
        public int? HeoNai { get; set; }
        public int? HeoThit { get; set; }
        public int? DucGiong { get; set; }
        public int? HeoConTheoMe { get; set; }
        public int? HeoCaiSua { get; set; }
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
        public bool? IsATDBLMLM { get; set; }
        public bool? IsATDBDichTa { get; set; }
        public bool? IsATDBDichTaChauPhi { get; set; }
        public bool? IsVietGAHP { get; set; }
        public string? SoVietGAHP { get; set; }
        public DateTime? NgayVietGAHP { get; set; }
        public double? DienTichTrangTrai { get; set; }
        public double? DienTichChuong { get; set; }

        public bool? IsTuongXay { get; set; }
        public bool? IsTuongLuoiB40 { get; set; }
        public bool? IsKhongCoTuong { get; set; }

        public bool? IsHangRaoKin { get; set; }
        public int? SoDayChuongKin { get; set; }

        public bool? IsHangRaoHo { get; set; }
        public int? SoDayChuongHo { get; set; }
        public bool? IsHoSatTrung { get; set; }
        public bool? IsPhongKhuTrung { get; set; }
        public bool? IsMayPhunThuocTrung { get; set; }
        public bool? IsKhongCoHeThongTieuDoc { get; set; }
        public bool? IsSanXuatGiong { get; set; }
        public bool? IsSanXuatThit { get; set; }
        public bool? IsSanXuatHonHop { get; set; }
        public bool? IsTuSanXuat { get; set; }
        public bool? IsNhapDiaPhuong { get; set; }
        public bool? IsNhapNgoaiTinh { get; set; }
        public string? IsKhaiBaoNhap { get; set; }
        public string? IsKhaiBaoXuat { get; set; }
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
        public string? IsBiogas { get; set; }
        public string? IsThaiXuongHo { get; set; }
        public string? IsBanTuoi { get; set; }
        public bool? IsQuyHoachTrong { get; set; }
        public bool? IsQuyHoachNgoai { get; set; }
        public double? DienTichKhuXuLy { get; set; }
        public double? HamBiogas { get; set; }
        public double? HoSinhHoc { get; set; }
        public string? IsBeXuLyHoa { get; set; }
        public string? IsNhaChuaPhan { get; set; }
        public bool? IsBaoCaoSoLieu { get; set; }
        public bool? IsDeleted { get; set; }
        public string? IsDich { get; set; }
        [ForeignKey(nameof(LoaiTrangTrai))]
        public long? IdLoaiTrangTrai { get; set; }
        public LoaiTrangTrai? LoaiTrangTrai { get; set; }

    }
}