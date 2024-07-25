using System.ComponentModel.DataAnnotations.Schema;

namespace BrainBoost_API.Models
{
    public class WhatToLearn
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string? Content { get; set; }
        [ForeignKey("course")]
        public int CrsId { get; set; }
        public Course course { get; set; }
    }
}
