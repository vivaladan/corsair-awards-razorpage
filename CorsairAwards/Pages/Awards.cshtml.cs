using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsairAwards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsairAwards.Pages
{
    public class AwardsModel : PageModel
    {
        private readonly IRepository repository;

        public AwardsModel(IRepository repository)
        {
            this.repository = repository;
        }
        
        public void OnGet()
        {
            
        }

        [BindProperty]
        public AwardsQuery Query { get; set; }
        
        public IEnumerable<Sample> Awards { get; set; } = Enumerable.Empty<Sample>();
        
        
        public async Task OnPost()
        {
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            Awards = await repository.GetSamples(Query);
        }
    }

    public class AwardsQuery
    {
        public string Category { get; set; }

        public string SKU { get; set; }

        public string Product { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string Year { get; set; }
    }
}