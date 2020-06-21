using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsairAwards.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CorsairAwards.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly CsvParser csvParser;
        private readonly IRepository repository;

        public UploadModel(
            ILogger<IndexModel> logger,
            CsvParser csvParser,
            IRepository repository)
        {
            this.logger = logger;
            this.csvParser = csvParser;
            this.repository = repository;
        }

        public void OnGet()
        {
        }
        
        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {
            var samples = csvParser.ParseCsv(Upload.FileName, Upload.OpenReadStream());
            await repository.InsertOrUpdateSamples(samples);
            
            return new JsonResult(new
            {
                Count = samples.Count
            });
        }
    }
}
