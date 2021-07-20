using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mattazeblog2021_site.API {
    [Route("[controller]")]
    [ApiController]
    public class PostImagesController : ControllerBase {

        private readonly CMServices.AzureStorage CMS;

        public PostImagesController(CMServices.AzureStorage cms) {
            CMS = cms;
        }

        [HttpGet]
        [Route("{blobName}")]
        public IActionResult Get(string blobName) {
            var postImage = CMS.GetImage(blobName);

            return File(postImage.Data.ToStream(), postImage.ContentType, blobName);
        }
    }
}
