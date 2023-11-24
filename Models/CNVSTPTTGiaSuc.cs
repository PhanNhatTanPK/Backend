using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNVSTPTTGiaSuc
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoVSTP { get; set; }
        public DateTime? NgayVSTP { get; set; }
        [ForeignKey(nameof(TrangTraiDaiGiaSuc))]
        public Guid IdTrangTraiDaiGiaSuc { get; set; }
        public TrangTraiDaiGiaSuc? TrangTraiDaiGiaSuc { get; set; }
    }
}
