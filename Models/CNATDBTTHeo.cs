using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNATDBTTHeo
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoATDB { get; set; }
        public DateTime? NgayATDB { get; set; }
        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}
