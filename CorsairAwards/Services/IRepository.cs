using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorsairAwards.Services
{
    public interface IRepository
    {
        Task InsertOrUpdateSamples(IEnumerable<Sample> samples);
        Task<IEnumerable<Sample>> GetSamples();
    }
}