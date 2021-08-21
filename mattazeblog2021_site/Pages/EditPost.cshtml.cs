using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mattazeblog2021_site.Pages {
    public class EditPostModel : PageModel {
        public Models.PostItem PostItem { get; set; }
        public Models.PostImage MainImage { get; set; }

        public void OnGet() {
            PostItem = new Models.PostItem() {
                Title = "title preview"
            };
            MainImage = new Models.PostImage();
        }

        public void OnPost(Models.PostItem post, Microsoft.AspNetCore.Http.IFormFile mainimage) {
            PostItem = post;

            var postImage = new Models.PostImage();
            postImage.Data = BinaryData.FromStream(mainimage.OpenReadStream());
            postImage.Name = mainimage.FileName;
            postImage.ContentType = mainimage.ContentType;

            MainImage = postImage;
        }

    }
}
