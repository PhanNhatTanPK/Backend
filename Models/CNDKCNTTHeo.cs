using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNDKCNTTHeo
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoDKCN { get; set; }
        public DateTime? NgayDKCN { get; set; }
        [ForeignKey(nameof(TrangTraiHeo))]
        public Guid IdTrangTraiHeo { get; set; }
        public TrangTraiHeo? TrangTraiHeo { get; set; }
    }
}
