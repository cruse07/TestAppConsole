using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR_Demo
{
    class ByteApproach
    {
        static void ImageReader()
        {
            string path = @"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/input/numbers.jpg";
            Image img = Image.FromFile(path); // a.png has 312X312 width and height
            var imgArr = ImageToByteArray(img);
            var bytList = SplitByteArray(imgArr);
            var combo = from a in imgArr
                        from b in imgArr
                        from c in imgArr
                        select string.Concat(a + ",", b + ",", c);
            int i = 0;
            foreach (var v in bytList)
            {
                try
                {
                    i++;
                    var newImg = byteArrayToImage(v);
                    var filename = @"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/OutPut/";
                    filename = filename + i.ToString() + ".jpeg";
                    newImg.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Console.WriteLine(v);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                //var splImg = v.Split(',');
                //var byt = splImg.Select(byte.Parse).ToArray();
                //Image x = (Bitmap)((new ImageConverter()).ConvertFrom(v));

            }
            //SplitImages();
            // var combi = GetPermutationsWithRept<byte>(imgArr, imgArr.Length-1);
        }

        static Image byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }
        static List<byte[]> SplitByteArray(byte[] arr)
        {
            //byte[] arr = { 0x1E, 0x23, 0x1E, 0x33, 0x44, 0x1E };
            byte split = arr[0];
            List<byte[]> result = new List<byte[]>();
            int start = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == split && i != 0)
                {
                    byte[] _in = new byte[i - start];
                    Array.Copy(arr, start, _in, 0, i - start);
                    result.Add(_in);
                    start = i + 1;
                }
                else if (arr[i] == split && i == 0)
                {
                    start = i + 1;
                }
                else if (arr.Length - 1 == i && i != start)
                {
                    byte[] _in = new byte[i - start + 1];
                    Array.Copy(arr, start, _in, 0, i - start + 1);
                    result.Add(_in);
                }

            }
            return result;
        }
        static IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        static void SplitImages()
        {
            string path = @"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/input/numbers.jpg";
            Image img = Image.FromFile(path); // a.png has 312X312 width and height
            int widthThird = (int)((double)img.Width);
            int heightThird = (int)((double)img.Height);
            Bitmap[,] bmps = new Bitmap[img.Width, img.Height];
            for (int i = 0; i < img.Width; i++)
                for (int j = 0; j < img.Height; j++)
                {
                    bmps[i, j] = new Bitmap(i * widthThird, j * heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();
                }
            List<Bitmap> bmpList = new List<Bitmap>();
            var img1 = bmps[0, 0];
            bmpList.Add(img1);
            var img2 = bmps[0, 1];
            bmpList.Add(img2);
            var img3 = bmps[0, 2];
            bmpList.Add(img3);
            var img4 = bmps[1, 0];
            bmpList.Add(img4);
            var img5 = bmps[1, 1];
            bmpList.Add(img5);
            var img6 = bmps[1, 2];
            bmpList.Add(img6);
            var img7 = bmps[2, 0];
            bmpList.Add(img7);
            var img8 = bmps[2, 1];
            bmpList.Add(img8);
            var img9 = bmps[2, 2];
            bmpList.Add(img9);

            for (int i = 0; i < bmpList.Count; i++)
            {
                var filename = @"C:/Users/pranav/source/repos/TestAppConsole/OCR_Demo/OutPut/";
                filename = filename + i.ToString() + ".jpeg";
                bmpList[i].Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
