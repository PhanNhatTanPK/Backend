using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVSTPTTGiaCam
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVSTP { get; set; }
        public DateTime? NgayVSTP { get; set; }
        [ForeignKey(nameof(TrangTraiGiaCam))]
        public Guid IdTrangTraiGiaCam { get; set; }
        public TrangTraiGiaCam? TrangTraiGiaCam { get; set; }
    }
}
