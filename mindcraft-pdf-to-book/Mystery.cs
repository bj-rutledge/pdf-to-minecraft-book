using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PdfToMineCraftBook
{
    class Test
    {
        public Test()
        {
            Console.WriteLine("w = {0} h= {1}", Console.WindowWidth, Console.WindowHeight);
            Mystery mystery = new Mystery(@"D:\OneDrive\Pictures\nina-nino\nino.png");
            if (mystery.GetAsciiImage())
            {
                Console.WriteLine(mystery.AsciiImage);
            }
        }
    }
    internal class Mystery
    {


        private string path; 
        public string? AsciiImage;
        private string[] _AsciiChars = { "#", "#", "@", "%", "=", "+", "*", ":", "-", ".", " " };
        public Mystery(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(); 
            this.path = path;
        }

        public bool GetAsciiImage()
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using(Bitmap image = new Bitmap(fs))
                {
                    int w = Console.WindowWidth > 0 ? Console.WindowWidth * 3: 10; 
                    int h = Console.WindowHeight > 0 ? Console.WindowHeight * 3  : 10;
                    Size size = new Size(w, h);
                    //Not using resize. If we go too small, the image won't render well 
                    //in ascii 
                    this.AsciiImage = ConvertToAscii(image);//resizeBitmap(image, size));
                }
                return true; 
            }catch(Exception e)
            {
                Console.WriteLine("Failed...", e.Message);
                return false;
            }
        }

        private string ConvertToAscii(Bitmap image)
        {
            Boolean toggle = false;
            StringBuilder sb = new StringBuilder();
            for (int h = 0; h < image.Height; h++)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    Color pixelColor = image.GetPixel(w, h);
                    //Average out the RGB components to find the Gray Color
                    int red = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int green = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color grayColor = Color.FromArgb(red, green, blue);
                    //Use the toggle flag to minimize height-wise stretch
                    if (!toggle)
                    {
                        int index = (grayColor.R * 10) / 255;
                        sb.Append(_AsciiChars[index]);
                    }
                }
                if (!toggle)
                {
                    sb.Append("\n");
                    toggle = true;
                }
                else
                {
                    toggle = false;
                }
            }
            return sb.ToString();
        }

        private Size ScaleSize(Size size)
        {
            int maxWidth = Console.WindowWidth- 1;
            int maxHeight = Console.WindowHeight- 1;
            double ratio = maxWidth / maxHeight;

            if(size.Width < maxWidth && size.Height < maxHeight)
            {
                return size;
            }
            int width; 
            int height;
            if(size.Height > size.Width)
            {
                height = maxHeight;
                width = maxHeight / size.Width;
            }
            else
            {
                height = maxHeight / size.Height;
                width = maxWidth;
            }

            return new Size(width, height);
        }

        private Bitmap ResizeBitmap(Bitmap imgToResize, Size size)
        {
            if(imgToResize.Width == size.Width && imgToResize.Height == size.Height)
            {
                return imgToResize;
            }

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
            {
                nPercent = nPercentH;
            }
            else
            {
                nPercent = nPercentW;
            }
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap bm = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)bm);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return bm;
        }

    }


}


