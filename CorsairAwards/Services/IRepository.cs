using System.Collections.Generic;
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
        Task<Dictionary<int, string>> GetPartDescriptions();
        Task<Dictionary<int, string>> GetPartNumbers();
        Task<Dictionary<int, string>> GetRegions();
        Task<Dictionary<int, string>> GetCountries();
        Task<Dictionary<int, string>> GetYears();
    }
}