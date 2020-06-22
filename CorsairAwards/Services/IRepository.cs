﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CorsairAwards.Pages;

namespace CorsairAwards.Services
{
    public interface IRepository
    {
        Task InsertOrUpdateSamples(List<Sample> samples);
        Task<IEnumerable<Sample>> GetSamples();
        Task<IEnumerable<Sample>> GetSamples(AwardsQuery query);
        Task<Dictionary<int, string>> GetCategories();
    }
}