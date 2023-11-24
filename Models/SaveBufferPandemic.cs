using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class SaveBufferPandemic
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Distance { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal? LatitudeNumber { get; set; }
        [Column(TypeName = "decimal(18,10)")]
        public decimal? LongitudeNumber { get; set; }
    }
}
