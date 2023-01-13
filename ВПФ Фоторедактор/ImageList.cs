using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВПФ_Фоторедактор
{
    public class ImageList
    {
        static public Bitmap bitmap;
        public static UInt32[,] pixel;
        static public string nameImage = "\0";
        static public List<Bitmap> bitmaps = new List<Bitmap>();
        static public List<int> angles = new List<int>();
        static public int angleActual;
        static public List <int> anglesActual = new List<int>();
        static public int count = 0;
        static public int actualIndex = -1;
        static public int indeSave = 0;

        public ImageList(int value)
        {
            int Value = value;
        }
        public static void FromPixelToBitmap()
        {
            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                    bitmap.SetPixel(x, y, Color.FromArgb((int)pixel[y, x]));
        }


        //преобразование из UINT32 to Bitmap по одному пикселю
        public static void FromOnePixelToBitmap(int x, int y, UInt32 pixel)
        {
            bitmap.SetPixel(y, x, Color.FromArgb((int)pixel));
        }

        static public int exceptionWidth(int a)
        {
            if(ImageList.bitmap==null) throw new Exception("Not allowed values");
            if (a > ImageList.bitmap.Width)
            {
                throw new Exception("Not allowed values");
            }

            return a;
        }
        static public int exceptionHeight(int a)
        {
            if (ImageList.bitmap == null) throw new Exception("Not allowed values");
            if (a > ImageList.bitmap.Height)
            {
                throw new Exception("Not allowed values");
            }

            return a;
        }
    }
}
