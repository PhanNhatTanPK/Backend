using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVSTYTTGiaSuc
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVSTY { get; set; }
        public DateTime? NgayVSTY { get; set; }
        [ForeignKey(nameof(TrangTraiDaiGiaSuc))]
        public Guid IdTrangTraiDaiGiaSuc { get; set; }
        public TrangTraiDaiGiaSuc? TrangTraiDaiGiaSuc { get; set; }
    }
}
