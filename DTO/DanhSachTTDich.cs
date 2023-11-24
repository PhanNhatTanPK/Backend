namespace Backend.DTO
{
    public class DanhSachTTDich
    {
        public Guid? Id { get; set; }
        public string? TenTrai { get; set; }
        public decimal LongitudeNumber { get; set; }
        public decimal LatitudeNumber { get; set; }
        public long ThoiDiemPhatBenh { get; set; }
        public long ThoiDiemKetThuc { get; set; }
        public string? LoaiBenh { get; set; }
        public int? SoBenh { get; set; }
        public int? IdLoaiTrangTrai { get; set; }
    }
}
