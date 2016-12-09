using NGif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CreateGıfApplication
{
    class Program
    {

        //[STAThread]
        static void Main(string[] args)
        {
            /* create Gif */
            //you should replace filepath
            string main_filepath = AppDomain.CurrentDomain.BaseDirectory + @"SamplePictures\";
            String[] imageFilePaths = new String[] { main_filepath + "batman.jpg", main_filepath + "superman.jpg", main_filepath + "ironman.jpg", main_filepath + "spiderman.jpg" };
            String outputFilePath = main_filepath+"test.gif";
            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.Start(outputFilePath);
            e.SetDelay(500);
            //-1:no repeat,0:always repeat
            e.SetRepeat(0);
            for (int i = 0, count = imageFilePaths.Length; i < count; i++)
            {
                e.AddFrame(Image.FromFile(imageFilePaths[i]));
            }
            e.Finish();

            /* extract Gif */
            string outputPath = main_filepath;
            GifDecoder gifDecoder = new GifDecoder();
            gifDecoder.Read(outputFilePath);

            List<string> temp_imageFilePaths = new List<string>();
            for (int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++)
            {
                Image frame = gifDecoder.GetFrame(i);  // frame i

                string framepath = outputPath + Guid.NewGuid().ToString() + ".png";
                frame.Save(framepath, ImageFormat.Png);
                temp_imageFilePaths.Add(framepath);

            }

            //delete temporary files
            for (int i = 0, count = temp_imageFilePaths.Count; i < count; i++)
            {
                FileInfo file = new FileInfo(temp_imageFilePaths[i]);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

            Console.WriteLine("gif is created.");
            Console.ReadLine();

        }
    }
}
