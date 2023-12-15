using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNATDBTTGiaSuc
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoATDB { get; set; }
        public DateTime? NgayATDB { get; set; }
        [ForeignKey(nameof(TrangTraiDaiGiaSuc))]
        public Guid IdTrangTraiDaiGiaSuc { get; set; }
        public TrangTraiDaiGiaSuc? TrangTraiDaiGiaSuc { get; set; }
    }
}
