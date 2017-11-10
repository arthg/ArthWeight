using ArthWeight.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArthWeight.Data
{
    public class ArthwindsRepository : IArthwindsRepository
    {
        private readonly ArthwindsContext _arthwindsContext;
        private readonly ILogger<ArthwindsRepository> _logger;

        public ArthwindsRepository(ArthwindsContext arthwindsContext, 
            ILogger<ArthwindsRepository> logger)
        {
            _arthwindsContext = arthwindsContext;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _arthwindsContext.Add(model);
        }

        public IEnumerable<WeightEntry> GetWeightEntries()
        {
            try
            {
                return _arthwindsContext.WeightEntries.OrderBy(e => e.CreatedDate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }


        public bool SaveAll()
        {
            return _arthwindsContext.SaveChanges() > 0;
        }
    }
}
