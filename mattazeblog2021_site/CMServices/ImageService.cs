using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mattazeblog2021_site.CMServices {
    public static class ImageService {
        //thumbnail = 100x100
        //article = 300x300
        //focus = 1000x1000
        //raw = original - 2k+ in size

        static System.Drawing.Size SizeThumbnail = new System.Drawing.Size(100, 100);
        static System.Drawing.Size SizeMain = new System.Drawing.Size(800, 300);
        static System.Drawing.Size SizeLarge = new System.Drawing.Size(1000, 1000);

        public static IEnumerable<System.Drawing.Image> MakeStorageSizes(Models.PostImage raw) {
            //var formats = new List<Models.PostImage>();
            var formats = new List<System.Drawing.Image>();

            System.Drawing.Image image;

            using (var stream = raw.Data.ToStream()) {
                image = System.Drawing.Image.FromStream(stream);
            }

            formats.Add(resizeImage(image, SizeThumbnail));

            return formats;
        }

        /// <summary>
        /// Using System Drawing Image to make a new image that fits inside the size.
        /// Maintains aspect ratio.
        /// 
        /// found via web search -
        /// Ish Bandhu
        /// https://www.c-sharpcorner.com/UploadFile/ishbandhu2009/resize-an-image-in-C-Sharp/
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size">maxium width heigh that fits the aspect ratio</param>
        /// <returns></returns>
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size) {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            System.Drawing.Bitmap b = new System.Drawing.Bitmap(destWidth, destHeight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private static System.Drawing.Image resizeImage2(System.Drawing.Image imgToResize, System.Drawing.Size maxSize) {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //Calculate  width with new desired size  
            nPercentW = ((float)maxSize.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            System.Drawing.Bitmap b = new System.Drawing.Bitmap(destWidth, destHeight);

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage((System.Drawing.Image)b)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // Draw image with new width and height  
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            }

            return (System.Drawing.Image)b;
        }
    }
}
