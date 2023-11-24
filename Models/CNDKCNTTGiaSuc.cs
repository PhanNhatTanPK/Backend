﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CNDKCNTTGiaSuc
    {
        [Key]
        public Guid Id { get; set; }
        public string? SoDKCN { get; set; }
        public DateTime? NgayDKCN { get; set; }
        [ForeignKey(nameof(TrangTraiDaiGiaSuc))]
        public Guid IdTrangTraiDaiGiaSuc { get; set; }
        public TrangTraiDaiGiaSuc? TrangTraiDaiGiaSuc { get; set; }
    }
}
