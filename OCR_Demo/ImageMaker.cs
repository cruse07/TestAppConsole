using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_Demo
{
    public class ImageTag
    {
        public string ImgTag { get; set; }
    }
    public class ImageMaker
    {
        public char[] Alphabets { get; set; }
        public List<Image> ImageList { get; set; }
        public ImageMaker()
        {
            ImageList = new List<Image>();
            Alphabets = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();
        }
        // Ok, assuming you want to draw a string on an image in C#, you are going to need to use the System.Drawing namespace here:
        public List<Image> TextImage()
        {
            foreach (var item in Alphabets)
            {
                var img = DrawText(item.ToString().ToUpper(), new Font("Arial Black", 18), Color.Black, Color.White);
                var filename = @"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/OutPut/";
                filename = filename + item.ToString() + ".jpeg";
                img.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                ImageList.Add(img);
            }
            return ImageList;
        }
        private Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(30, 30);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            ImageTag tag = new ImageTag();
            tag.ImgTag = text;
            img.Tag = tag;
            return img;

        }
    }
}
