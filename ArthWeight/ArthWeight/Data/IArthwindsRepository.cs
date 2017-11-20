using System.Collections.Generic;
using ArthWeight.Data.Entities;

namespace ArthWeight.Data
{
    public interface IArthwindsRepository
    {
        IEnumerable<WeightEntry> GetWeightEntries();
        IEnumerable<WeightEntry> GetWeightEntriesByUser(string username);
        bool SaveAll();
        void AddEntity(object model);
    }
}