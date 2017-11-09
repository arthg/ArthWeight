using ArthWeight.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ArthWeight.Data
{
    public class ArthwindsRepository : IArthwindsRepository
    {
        private readonly ArthwindsContext _arthwindsContext;

        public ArthwindsRepository(ArthwindsContext arthwindsContext)
        {
            _arthwindsContext = arthwindsContext;
        }

        public IEnumerable<WeightEntry> WeightEntries()
        {
            return _arthwindsContext.WeightEntries.OrderBy(e => e.CreatedDate);
        }


        public bool SaveAll()
        {
            return _arthwindsContext.SaveChanges() > 0;
        }
    }
}
