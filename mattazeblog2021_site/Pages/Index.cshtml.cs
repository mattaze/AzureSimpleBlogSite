using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace mattazeblog2021_site.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CMServices.AzureStorage CMS;

        public Models.PostItem LatestPost;
        public string ConStr;

        public IndexModel(ILogger<IndexModel> logger, CMServices.AzureStorage cms)
        {
            _logger = logger;
            CMS = cms;
        }

        public void OnGet()
        {
            LatestPost = new Models.PostItem()
            {
                ImageURL = Url.Content("~/images/matrix-3109378_640.jpg"),
                PostBody = "Her be some <strong>interesting</strong> text about the image or just rambling",
                PostTime = DateTime.Now
            };
        }
    }
}
