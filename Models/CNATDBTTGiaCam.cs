using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNATDBTTGiaCam
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoATDB { get; set; }
        public DateTime? NgayATDB { get; set; }
        [ForeignKey(nameof(TrangTraiGiaCam))]
        public Guid IdTrangTraiGiaCam { get; set; }
        public TrangTraiGiaCam? TrangTraiGiaCam { get; set; }
    }
}
