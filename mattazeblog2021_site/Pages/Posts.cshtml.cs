using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mattazeblog2021_site.Pages
{
    public class PostsModel : PageModel
    {
        private readonly CMServices.AzureStorage CMS;

        public Models.PostItem PostItem;

        public PostsModel(CMServices.AzureStorage azureStorage) {
            CMS = azureStorage;
        }

        public void OnGet(string title) {
            var post = CMS.GetPost("thissite", "2021-07-04_1");
            PostItem = post;
        }
    }
}
