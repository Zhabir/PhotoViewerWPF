using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Image = System.Drawing.Image;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;
using SystemColors = System.Drawing.SystemColors;

namespace ВПФ_Фоторедактор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int RGBflag = 0;
        int RGBflagDraw = 0;
        bool cropButton = false;
        bool drawButton = false;
        bool Drow = false;
        int index_Name=0;
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("data.json"))           //deserialization
            {
                string data = File.ReadAllText("data.json");
                Data a = JsonSerializer.Deserialize<Data>(data);
                // ImageList.bitmaps = a.bitmaps;
                ImageList.actualIndex = a.actualindex;
                RGBSlider.Value = a.RgbValue;
                BrightnessSlider.Value = a.BrightnessValue;
                ContrastSlider.Value = a.ContrastValue;
                RGBflag = a.RGBRadio;
                index_Name = a.index_name;
                if (RGBflag == 0 || RGBflag == 1)
                {
                    RedRadio.IsChecked = true;
                    GreenRadio.IsChecked = false;
                    BlueRadio.IsChecked = false;
                }
                else if (RGBflag == 2)
                {
                    RedRadio.IsChecked = false;
                    GreenRadio.IsChecked = true;
                    BlueRadio.IsChecked = false;
                }
                else if (RGBflag == 3)
                {
                    RedRadio.IsChecked = false;
                    GreenRadio.IsChecked = false;
                    BlueRadio.IsChecked = true;
                }

                textBoxWidth.Text = Convert.ToString(a.Width);
                textBoxHeight.Text = Convert.ToString(a.Height);
                ImageList.anglesActual = a.angles;
                for (int i = 0; i < a.bitmaps_count; i++)
                {
                    Bitmap bmp;
                    if (File.Exists($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{index_Name - 1}{i}" + ".jpeg"))
                    {
                        bmp = new Bitmap($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{index_Name - 1}{i}" + ".jpeg");
                        Image img = bmp;
                        ImageList.bitmaps.Add((Bitmap)img);
                    }
                }
                if (ImageList.actualIndex < ImageList.bitmaps.Count)
                {
                    FromBitmapToScreen();
                    ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                }
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

        void deleteExtrabitmaps()
        {
            int j = 0;
            for (int i = 0; i < index_Name - 1; i++)
            {
                while (true)
                {
                    if (!File.Exists($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{i}{j}" + ".jpeg")) break;
                    System.IO.File.Delete($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{i}{j}" + ".jpeg");
                    j++;
                }
            }

            if (index_Name > 9) index_Name = 0;
        }
        public void FromBitmapToScreen()
        {
            EditImage.Source = BitmapToSource(ImageList.bitmaps[ImageList.actualIndex]);

            EditImage.Source = BitmapToSource(RotateImage(ImageList.bitmaps[ImageList.actualIndex], ImageList.anglesActual[ImageList.actualIndex]));
        }
        public static void FromOnePixelToBitmap(int x, int y, UInt32 pixel)
        {
            ImageList.bitmap.SetPixel(y, x, System.Drawing.Color.FromArgb((int)pixel));
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
        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a picture";
            ofd.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.bpe|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "Bstu Photo Editor (*.bpe)|*.bpe";

            if (ofd.ShowDialog() == true)
            {
                ImageList.nameImage = ofd.FileName;
                if (ofd.FileName == nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png") ImageList.indeSave++;
                ImageList.bitmap = new Bitmap(ofd.FileName);
                //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                EditImage.Source = BitmapToSource(ImageList.bitmap);
                //pictureBox1.Invalidate();
                //получение матрицы с пикселями
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                ImageList.bitmaps.Add(ImageList.bitmap);
                ImageList.actualIndex = ImageList.bitmaps.Count - 1;
                ImageList.anglesActual.Add(0);
                ContrastSlider.Value = 0;
                BrightnessSlider.Value = 0;
                RGBSlider.Value = 0;
            }
            
        }

        public static Bitmap RotateImage(Bitmap img, int rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new System.Drawing.Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();
            //ImageList.bitmaps[ImageList.actualIndex] = bmp;
            //ImageList.bitmap = bmp;
            //return the image
            return bmp;
        }

        private void RotateBtn_Click(object sender, RoutedEventArgs e)
        {
            //int angle = 90;
            //int actualAngle = ImageList.anglesActual[ImageList.actualIndex];
            if (ImageList.bitmaps.Count == 0) return;
            ImageList.anglesActual[ImageList.actualIndex] += 90;
            //angle += actualAngle;
            if (ImageList.anglesActual[ImageList.actualIndex] >= 360)
                ImageList.anglesActual[ImageList.actualIndex] = 0;
            EditImage.Source = BitmapToSource(RotateImage(ImageList.bitmaps[ImageList.actualIndex], ImageList.anglesActual[ImageList.actualIndex]));
            //FromBitmapToScreen();
        }
        void createPixels()
        {
            ImageList.pixel = null;
            for (int y = 0; y < ImageList.bitmap.Height; y++)
                for (int x1 = 0; x1 < ImageList.bitmap.Width; x1++)
                    ImageList.pixel[y, x1] = new int();
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.bitmaps.Count == 0 || ImageList.bitmaps.Count == 1) return;
            if (ImageList.actualIndex == ImageList.bitmaps.Count - 1) { EditImage.Source = BitmapToSource(ImageList.bitmaps[0]); ImageList.actualIndex = 0; }
            else { ++ImageList.actualIndex; EditImage.Source = BitmapToSource(ImageList.bitmaps[ImageList.actualIndex]); }
            // createPixels();
            ContrastSlider.Value = 0;
            BrightnessSlider.Value = 0;
            RGBSlider.Value = 0;
            ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
            ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
            for (int y = 0; y < ImageList.bitmap.Height; y++)
                for (int x = 0; x < ImageList.bitmap.Width; x++)
                    ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
            FromBitmapToScreen();
        }

       
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.bitmaps.Count == 0 || ImageList.bitmaps.Count == 1) return;
            if (ImageList.actualIndex == 0)
            {
                EditImage.Source = BitmapToSource(ImageList.bitmaps[ImageList.bitmaps.Count - 1]);
                ImageList.actualIndex = ImageList.bitmaps.Count - 1;
            }
            else
            {
                --ImageList.actualIndex;
                EditImage.Source = BitmapToSource(ImageList.bitmaps[ImageList.actualIndex]);

            }
            ContrastSlider.Value = 0;
            BrightnessSlider.Value = 0;
            RGBSlider.Value = 0;
            ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
            ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
            for (int y = 0; y < ImageList.bitmap.Height; y++)
                for (int x = 0; x < ImageList.bitmap.Width; x++)
                    ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
            FromBitmapToScreen();
        }

        private void RGBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageList.bitmaps.Count != 0)
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        if (RGBflag == 0 || RGBflag == 1)
                            p = ColorBalance.ColorBalance_R(ImageList.pixel[i, j], RGBSlider.Value, RGBSlider.Maximum);
                        else if (RGBflag == 2)
                            p = ColorBalance.ColorBalance_G(ImageList.pixel[i, j], RGBSlider.Value, RGBSlider.Maximum);
                        else
                            p = ColorBalance.ColorBalance_B(ImageList.pixel[i, j], RGBSlider.Value, RGBSlider.Maximum);
                        FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void BrightnessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageList.bitmaps.Count != 0)
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = BrightnessContrast.Brightness(ImageList.pixel[i, j], BrightnessSlider.Value, BrightnessSlider.Maximum);
                        FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void ContrastSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageList.bitmaps.Count != 0)
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = BrightnessContrast.Contrast(ImageList.pixel[i, j], ContrastSlider.Value, ContrastSlider.Maximum);
                        FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void RedRadio_Checked(object sender, RoutedEventArgs e)
        {
            RGBflag = 1;
        }

        private void GreenRadio_Checked(object sender, RoutedEventArgs e)
        {
            RGBflag = 2;
        }

        private void BlueRadio_Checked(object sender, RoutedEventArgs e)
        {
            RGBflag = 3;
        }

        private void RedRadioDraw_Checked(object sender, RoutedEventArgs e)
        {
            RGBflagDraw = 1;
        }

        private void GreenRadioDraw_Checked(object sender, RoutedEventArgs e)
        {
            RGBflagDraw = 2;
        }

        private void BlueRadioDraw_Checked(object sender, RoutedEventArgs e)
        {
            RGBflagDraw = 3;
        }


        public static System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
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
                    // Clone the bitmap so that we can dispose it and
                    // release the unmanaged memory at ptr
                    return new System.Drawing.Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }

        
        private void CropButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (textBoxWidth.Text=="" || textBoxHeight.Text == "")
            {
                MessageBox.Show("Plesae, fill fields");
            }

            int width;
            int height;
            try
            {
                width = ImageList.exceptionWidth(Convert.ToInt32(textBoxWidth.Text));
            }
            catch(Exception)
            {
                MessageBox.Show("Please, enter allowed values");
                return;
            }

            try
            {
                height = ImageList.exceptionHeight(Convert.ToInt32(textBoxHeight.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Please, enter allowed values");
                return;
            }
            CroppedBitmap cb = new CroppedBitmap((BitmapSource)this.EditImage.Source, new Int32Rect(0, 0, width,height));
            EditImage.Source = cb;

            Grid r = new Grid();
            r.Background = new ImageBrush(EditImage.Source);


            System.Windows.Size sz = new System.Windows.Size(EditImage.Source.Width, EditImage.Source.Height);
            r.Measure(sz);
            r.Arrange(new Rect(sz));
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)EditImage.Source.Width, (int)EditImage.Source.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(r);

            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            FileStream fs = File.Open(nameOfDirectory() + @"\crop add\lol"+$"{ImageList.indeSave}.png", FileMode.Create);
            encoder.Save(fs);
            fs.Close();

            ImageList.bitmaps[ImageList.actualIndex] = new Bitmap(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png");

            ImageList.bitmap = new Bitmap(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png");

            ImageList.indeSave++;
            //ImageList.bitmaps[ImageList.actualIndex] = (Bitmap)cb;

            //         CroppedBitmap cb = new CroppedBitmap(
            //(BitmapSource)this.Resources["masterImage"],
            //new Int32Rect(30, 20, 105, 50));       //select region rect
            //         croppedImage.Source = cb;
            //         Image chainImage = new Image();
            //         chainImage.Width = 200;
            //         chainImage.Margin = new Thickness(5);

            //         // Create the cropped image based on previous CroppedBitmap.
            //         CroppedBitmap chained = new CroppedBitmap(cb,
            //            new Int32Rect(30, 0, (int)cb.Width - 30, (int)cb.Height));
            //         // Set the image's source.
            //         chainImage.Source = chained;
            //EditImage.Width = Convert.ToDouble(textBoxWidth.Text);
            //EditImage.Height = Convert.ToDouble(textBoxHeight.Text);
        }

        

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            Рисование a = new Рисование();
            a.ShowDialog();
        }

        private void EditImage_MouseUp(object sender, MouseEventArgs e)
        {
            Drow = false;
        }
  

        private void pictureBox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Drow = true;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drow == true && drawButton == true)
            {

                Graphics graf = Graphics.FromImage(ImageList.bitmap);

                var point = e.GetPosition(this.EditImage);

                if (RGBflagDraw == 0 || RGBflagDraw == 1)
                    graf.FillEllipse(System.Drawing.Brushes.Red, (float)point.X, (float)point.Y, 10, 10); // толщина кисти
                else if (RGBflagDraw == 2)
                    graf.FillEllipse(System.Drawing.Brushes.Green, (float)point.X, (float)point.Y, 20, 20); // толщина кисти
                else if (RGBflagDraw == 3)
                    graf.FillEllipse(System.Drawing.Brushes.Blue, (float)point.X, (float)point.Y, 20, 20); // толщина кисти
                EditImage.Source = BitmapToSource(ImageList.bitmap);
            }
        }

        private void AddText_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            Crop a = new Crop();
            a.ShowDialog();

            FromBitmapToScreen();
        }
      
        
        private void buttonCollage_Click(object sender, RoutedEventArgs e)
        {
            Collages a = new Collages();
            a.ShowDialog();
        }
        void save_bitmaps()
        {
            for (int i = 0; i < ImageList.bitmaps.Count; i++)
            {
                Bitmap bmp = new Bitmap(480, 495);
                bmp = ImageList.bitmaps[i];

                string Name = $"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{index_Name}{i}" + ".jpeg";
                bmp.Save(Name, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            deleteExtrabitmaps();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image files(*.jpg) | *.jpg";
            Grid r = new Grid();
            r.Background = new ImageBrush(EditImage.Source);


            System.Windows.Size sz = new System.Windows.Size(EditImage.Source.Width, EditImage.Source.Height);
            r.Measure(sz);
            r.Arrange(new Rect(sz));
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)EditImage.Source.Width, (int)EditImage.Source.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(r);

            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            FileStream fs = File.Open(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png", FileMode.Create);
            encoder.Save(fs);
            fs.Close();
            Bitmap bmp = new Bitmap(nameOfDirectory() + @"\crop add\lol" + $"{ImageList.indeSave}.png");
            if (sfd.ShowDialog() == true)
                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            Рисование a = new Рисование();
            a.ShowDialog();
        }
        private void buttonData_Click(object sender, RoutedEventArgs e)
        {
            Data b = new Data()
            {
                // bitmaps = ImageList.bitmaps,
                actualindex = ImageList.actualIndex,
                angles = ImageList.anglesActual,
                RgbValue = RGBSlider.Value,
                BrightnessValue = BrightnessSlider.Value,
                ContrastValue = ContrastSlider.Value,
                RGBRadio = RGBflag,
                bitmaps_count = ImageList.bitmaps.Count,
                index_name = index_Name + 1,
                Width = Convert.ToInt32(textBoxWidth.Text),
                Height = Convert.ToInt32(textBoxHeight.Text)
            };
            string personJson = JsonSerializer.Serialize(b, typeof(Data));
            StreamWriter file = File.CreateText("data.json");
            file.WriteLine(personJson);
            file.Close();

            save_bitmaps();

            MessageBox.Show("Data is saved");
        }
    }
    
}

