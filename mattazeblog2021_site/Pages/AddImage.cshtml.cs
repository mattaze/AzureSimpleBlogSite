using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace mattazeblog2021_site.Pages {
    public class AddImageModel : PageModel {
        public void OnPost(IEnumerable<Microsoft.AspNetCore.Http.IFormFile> images) {
            foreach (var image in images) {
                var postImage = new Models.PostImage();
                postImage.Data = BinaryData.FromStream(image.OpenReadStream());
                postImage.Name = image.FileName;
                postImage.ContentType = image.ContentType;
            }
        }
    }
}
