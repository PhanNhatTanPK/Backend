using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class TrangTraiTheoHuyen
    {
        public Guid Id { get; set; }
        public string? Xa { get; set; }
        [ForeignKey(nameof(District))]
        public long? DistrictId { get; set; }
        public District? District { get; set; }
        public string? TenTrangTrai { get; set; }
        public string? DiaChi { get; set; }
        public int? LonThit { get; set; }
        public int? LonNai { get; set; }
        public int? LonDucGiong { get; set; }
        public int? Bo { get; set; }
        public int? BoCaiSinhSan { get; set; }
        public int? BoDuc { get; set; }
        public int? Trau { get; set; }
        public int? Ngua { get; set; }
        public int? De { get; set; }
        public int? Cuu { get; set; }
        public int? Tho { get; set; }
        public int? GaThit { get; set; }
        public int? GaTrung { get; set; }
        public int? VitThit { get; set; }
        public int? VitTrung { get;set; }
        public int? Ngan { get; set; }
        public int? Ngong { get; set; }
        public int? DaDieu { get; set; }
        public int? ChimCut { get; set; }
        public int? BoCau { get; set; }
        public int? HuouSao { get; set; }
        public int? Yen { get; set; }
        public int? Cho { get; set; }
        public int? Meo { get; set; }
        public int? VitTroi { get; set; }
        public int? OngMat { get; set; }

    }
}
