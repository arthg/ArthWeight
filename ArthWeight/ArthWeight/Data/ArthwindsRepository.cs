using ArthWeight.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
                return _arthwindsContext.WeightEntries
                    .Include(e => e.User)
                    .OrderBy(e => e.CreatedDate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all weights: {ex}");
                return null;
            }
        }

        public IEnumerable<WeightEntry> GetWeightEntriesByUser(string username)
        {
            //TODO: reduce repeated code with GetWeightEntries
            //TODO: limit the information returned on the user - don't need all of it.  any of it??!!
            try
            {
                return _arthwindsContext.WeightEntries
                    .Where(e => e.User.UserName == username)
                    .Include(e => e.User)
                    .OrderBy(e => e.CreatedDate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get weight entries: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _arthwindsContext.SaveChanges() > 0;
        }
    }
}
