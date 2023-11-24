using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVietGAHPTTHeo
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVietGAHP { get; set; }
        public DateTime? NgayVietGAHP { get; set; }
        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}
