using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVSTPTTHeo
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVSTP { get; set; }
        public DateTime? NgayVSTP { get; set; }
        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}
