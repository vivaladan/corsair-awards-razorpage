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
        private readonly CsvParser _csvParser;

        public UploadModel(
            ILogger<IndexModel> logger,
            CsvParser csvParser)
        {
            this.logger = logger;
            this._csvParser = csvParser;
        }

        public void OnGet()
        {
        }
        
        [BindProperty]
        public IFormFile Upload { get; set; }

        public JsonResult OnPostAsync()
        {
            var samples = _csvParser.ParseCsv(Upload.OpenReadStream());
            return new JsonResult(new
            {
                Count = samples.Count
            });
        }
    }
}
