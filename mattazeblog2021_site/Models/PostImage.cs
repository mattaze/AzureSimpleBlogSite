using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mattazeblog2021_site.Models {
    public class PostImage {
        public BinaryData Data { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }

        public string GetDataURL() {
            if (Data == null) {
                return "";
            }

            return $"data:{ContentType};base64,{Convert.ToBase64String(Data)}";
        }
    }
}
