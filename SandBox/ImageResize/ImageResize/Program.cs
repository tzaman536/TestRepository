using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResize
{
    class Program
    {
        static void Main(string[] args)
        {

            string sourceFile = @"C:\delete\ImageOriginal\SoftwareConsulting.jpg";
            string destinationPath = @"C:\delete\ImageResized\SoftwareConsultingConverted.jpg";


            Bitmap bmOriginal = new Bitmap(sourceFile);
            ImageHandler ih = new ImageHandler();
            ih.Save(bmOriginal, 100, 100, 100, destinationPath);
            



        }
    }
}
