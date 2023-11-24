using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVietGAHPTTGiaCam
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVietGAHP { get; set; }
        public DateTime? NgayVietGAHP { get; set; }
        [ForeignKey(nameof(TrangTraiGiaCam))]
        public Guid IdTrangTraiGiaCam { get; set; }
        public TrangTraiGiaCam? TrangTraiGiaCam { get; set; }
    }
}
