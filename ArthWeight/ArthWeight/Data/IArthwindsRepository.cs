using System.Collections.Generic;
using ArthWeight.Data.Entities;

namespace ArthWeight.Data
{
    public interface IArthwindsRepository
    {
        IEnumerable<WeightEntry> GetWeightEntries();
        bool SaveAll();
    }
}