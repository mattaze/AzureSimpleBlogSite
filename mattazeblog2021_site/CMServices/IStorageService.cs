using mattazeblog2021_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mattazeblog2021_site.CMServices {
    public interface IStorageService {
        public PostItem GetPost(string partitionKey, string rowKey);

        //PutPost

        //GetImage
        //PutImage

        //GetImages
    }
}
