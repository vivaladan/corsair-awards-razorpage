using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CorsairAwards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CorsairAwards.Pages
{
    public class AwardsModel : PageModel
    {
        private readonly IRepository repository;

        public AwardsModel(IRepository repository)
        {
            this.repository = repository;
        }

        private async Task LoadOptions()
        {
            var categories = await repository.GetCategories();
            Categories = categories.Select(c => new SelectListItem(c.Value, c.Value));
            
            var partNumbers = await repository.GetPartNumbers();
            PartNumbers = partNumbers.Select(c => new SelectListItem(c.Value, c.Value));

            var partDescriptions = await repository.GetPartDescriptions();
            PartDescriptions = partDescriptions.Select(c => new SelectListItem(c.Value, c.Value));

            var regions = await repository.GetRegions();
            Regions = regions.Select(c => new SelectListItem(c.Value, c.Value));

            var countries = await repository.GetCountries();
            Countries = countries.Select(c => new SelectListItem(c.Value, c.Value));
            
            var years = await repository.GetYears();
            Years = years.Select(c => new SelectListItem(c.Value, c.Value));
        }

        public async Task OnGet()
        {
            await LoadOptions();
        }

        [BindProperty]
        public AwardsQuery Query { get; set; }
        
        public IEnumerable<SelectListItem> Categories { get; set; }
        
        public IEnumerable<SelectListItem> PartNumbers { get; set; }

        public IEnumerable<SelectListItem> PartDescriptions { get; set; }
        
        public IEnumerable<SelectListItem> Regions { get; set; }
        
        public IEnumerable<SelectListItem> Countries { get; set; }
        
        public IEnumerable<SelectListItem> Years { get; set; }

        [BindProperty] 
        public int CurrentPage { get; set; } = 1;
        
        public int PageSize { get; set; } = 50;

        public int Total { get; set; }

        public int Showing => Awards.Count() + (CurrentPage - 1) * PageSize;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Total, PageSize));

        public IEnumerable<Sample> Awards { get; set; } = Enumerable.Empty<Sample>();
        
        
        public async Task OnPost()
        {
            await LoadOptions();

            Query.Skip = (CurrentPage * PageSize) - PageSize;
            Query.Take = PageSize;
            
            var sampleResults = await repository.GetSamples(Query);
            
            Awards = sampleResults.Samples;
            Total = sampleResults.Count;
        }
    }

    public class AwardsQuery
    {
        public string Category { get; set; }

        public string PartNumber { get; set; }

        public string PartDescription { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        public string Year { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}