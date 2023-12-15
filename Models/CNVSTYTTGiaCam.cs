using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVSTYTTGiaCam
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVSTY { get; set; }
        public DateTime? NgayVSTY { get; set; }
        [ForeignKey(nameof(TrangTraiGiaCam))]
        public Guid IdTrangTraiGiaCam { get; set; }
        public TrangTraiGiaCam? TrangTraiGiaCam { get; set; }
    }
}
