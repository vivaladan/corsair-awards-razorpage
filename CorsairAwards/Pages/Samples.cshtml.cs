using System.Collections.Generic;
using System.Threading.Tasks;
using CorsairAwards.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsairAwards.Pages
{
    public class SamplesModel : PageModel
    {
        private readonly IRepository repository;

        public SamplesModel(IRepository repository)
        {
            this.repository = repository;
        }
        
        public async Task OnGet()
        {
            Samples = await repository.GetSamples();
        }

        public IEnumerable<Sample> Samples { get; set; }
    }
}