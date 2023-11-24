using Backend.Models;

namespace Backend.DTO
{
    public class AllTrangTrai
    {
        public IEnumerable<TrangTraiHeo> TrangTraiHeo { get; set; }
        public IEnumerable<TrangTraiDaiGiaSuc> TrangTraiDaiGiaSuc { get; set; }
        public IEnumerable<CoSoGietMo> CoSoGietMo { get; set; }
    }
}
