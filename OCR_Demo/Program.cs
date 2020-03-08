using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IronOcr;

namespace OCR_Demo
{
    public class ComparisionResult
    {
        public string Source { get; set; }
        public string Input { get; set; }
        public int Score { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ImageMaker imgMkr = new ImageMaker();
            var ocr = new AdvancedOcr();
            List<ComparisionResult> compList = new List<ComparisionResult>();
            string pathInput = @"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/input/abcd.png";
            var characters =ocr.Read(pathInput);
            Console.WriteLine(characters.Text);
            Image img = Image.FromFile(pathInput);
            //GetHash(new Bitmap(img));
            // source
            var sourceTextImages = imgMkr.TextImage();
            string[] filePaths = Directory.GetFiles(@"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/Source/");
            foreach (var srcImg in sourceTextImages)
            {
                ComparisionResult cr = new ComparisionResult();

                //Image sourceImg = Image.FromFile(path);
                var score = CompareImages(new Bitmap(img), new Bitmap(srcImg));
                cr.Input = pathInput.Replace("C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/input/", "");
                //cr.Source = path.Replace("C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/Source/", "");
                cr.Source = ((ImageTag)srcImg.Tag).ImgTag;
                cr.Score = score;
                compList.Add(cr);
            }
            var res = compList.OrderByDescending(o => o.Score).ToList();
            foreach (var item in compList)
            {
                Console.WriteLine(item.Input + " " + item.Source + " " + item.Score);
            }

            Console.ReadKey();
         }
        static void ReadSource()
        { 
        
        }
        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }
        static int CompareImages(Bitmap input, Bitmap source)
        {
            List<bool> iHash1 = GetHash(input);
            List<bool> iHash2 = GetHash(source);

            //determine the number of equal pixel (x of 256)
            return iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
        }
    }
}
