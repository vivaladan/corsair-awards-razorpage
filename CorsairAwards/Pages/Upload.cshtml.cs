using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CorsairAwards.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public UploadModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        
        [BindProperty]
        public IFormFile Upload { get; set; }

        public Task OnPostAsync()
        {
            return Task.CompletedTask;
        }
    }
}
