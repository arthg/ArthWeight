using System;

namespace ArthWeight.Data.Entities
{
    public sealed class WeightEntry
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Weight { get; set; }
    }
}
