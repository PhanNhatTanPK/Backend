using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class LoaiGiaSuc
    {
        [Key]
        public long Id { get; set; }
        public string? TenLoaiGiaSuc { get; set; }
    }
}
