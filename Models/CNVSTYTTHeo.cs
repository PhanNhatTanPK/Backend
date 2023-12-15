using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVSTYTTHeo
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVSTY { get; set; }
        public DateTime? NgayVSTY { get; set; }
        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}
