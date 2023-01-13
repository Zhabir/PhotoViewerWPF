using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ВПФ_Фоторедактор
{
    /// <summary>
    /// Логика взаимодействия для Crop.xaml
    /// </summary>
    public partial class Crop : Window
    {
        Border pic = new Border();
        int begin_x, begin_y;
        bool resize = false;
        int size_crop_x, size_crop_y;
        public Crop()
        {
            InitializeComponent();

            EditImage.Source = BitmapToSource(ImageList.bitmaps[ImageList.actualIndex]);

            EditImage.Source = BitmapToSource(MainWindow.RotateImage(ImageList.bitmaps[ImageList.actualIndex], ImageList.anglesActual[ImageList.actualIndex]));
            //CroppedBitmap cb = new CroppedBitmap((BitmapSource)BitmapToSource(ImageList.bitmaps[ImageList.actualIndex]), new Int32Rect(0, 0, ImageList.bitmaps[ImageList.actualIndex].Width, ImageList.bitmaps[ImageList.actualIndex].Height));
            //EditImage.Source = cb;
            //double del = ImageList.bitmaps[ImageList.actualIndex].Width / ImageList.bitmaps[ImageList.actualIndex].Height;
            //double delControl = EditImage.Width/ EditImage.Height;
            //if(del!=delControl)
            //{
            //    double factor = del / delControl;
            //    if (del > 1) EditImage.Width /= factor;
            //    else EditImage.Height /= factor;
            //}
            //EditImage.Width = ;
            //EditImage.Height = ImageList.bitmaps[ImageList.actualIndex].Height / 2;

        }
        private BitmapImage BitmapToSource(Bitmap src)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            src.Save(ms, ImageFormat.Jpeg);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
            private void EditImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //begin_x = e.X;
            //begin_y = e.Y;
            //pic.Left = e.X;
            //pic.Top = e.Y;
            //pic.Width = 0;
            //pic.Height = 0;
            //pic.Visible = true;
            //timer1.Start();
            //resize = true;
        }
        int RGBflag = 0;

        public static Bitmap BitmapSourceToBitmap2(BitmapSource srs)
        {
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
                {
                    return new Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }


        string nameOfDirectory()
        {
            string name = System.AppDomain.CurrentDomain.BaseDirectory;
            string directory = "PhotoViewerWPF-main";
            int indexOfDirectory = name.IndexOf(directory);
            int size = directory.Length;
            name = name.Substring(0, indexOfDirectory + size);
            return name;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text=="")
            {
                MessageBox.Show("Enter text!");
                return;
            }
            if (textBoxX.Text == "" || textBoxY.Text == "")
            {
                MessageBox.Show("Enter coordinates!");
                return;
            }
            if(Convert.ToInt32(textBoxX.Text) > ImageList.bitmaps[ImageList.actualIndex].Width || Convert.ToInt32(textBoxY.Text) > ImageList.bitmaps[ImageList.actualIndex].Height)
            {
                MessageBox.Show("Please, enter allowed coordinates");
                return;
            }
            var visual = new DrawingVisual();
            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                drawingContext.DrawImage(BitmapToSource(ImageList.bitmap), new Rect(0, 0, ImageList.bitmap.Width, ImageList.bitmap.Height));
                if (RGBflag == 0 || RGBflag == 1)
                    drawingContext.DrawText(new FormattedText(textBox.Text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"), 32, System.Windows.Media.Brushes.Red), new System.Windows.Point(Convert.ToInt32(textBoxX.Text), Convert.ToInt32(textBoxY.Text)));
                else if (RGBflag == 2)
                    drawingContext.DrawText(new FormattedText(textBox.Text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"), 32, System.Windows.Media.Brushes.Green), new System.Windows.Point(0, 0));
                else
                    drawingContext.DrawText(new FormattedText(textBox.Text, CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"), 32, System.Windows.Media.Brushes.Blue), new System.Windows.Point(0, 0));

            }
            var image = new DrawingImage(visual.Drawing);
            EditImage.Source = image;

            Grid r = new Grid();
            r.Background = new ImageBrush(EditImage.Source);


            System.Windows.Size sz = new System.Windows.Size(EditImage.Source.Width, EditImage.Source.Height);
            r.Measure(sz);
            r.Arrange(new Rect(sz));
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)EditImage.Source.Width, (int)EditImage.Source.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(r);

            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb)); ImageList.indeSave++;
            FileStream fs = File.Open(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png", FileMode.Create);
            encoder.Save(fs);
            fs.Close();

            ImageList.bitmaps[ImageList.actualIndex] = new Bitmap(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png");

            ImageList.bitmap = new Bitmap(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave++}.png");

            EditImage.Source = BitmapToSource(MainWindow.RotateImage(ImageList.bitmaps[ImageList.actualIndex], ImageList.anglesActual[ImageList.actualIndex]));
        }
    }
}
