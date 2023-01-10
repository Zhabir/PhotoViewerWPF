using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ВПФ_Фоторедактор
{
    /// <summary>
    /// Логика взаимодействия для Рисование.xaml
    /// </summary>
    public partial class Рисование : Window
    {
        public Рисование()
        {
            InitializeComponent();
            image1.Width = ImageList.bitmaps[ImageList.actualIndex].Width;
            image1.Height = ImageList.bitmaps[ImageList.actualIndex].Height;
            inkcanvas1.Height = image1.Height;
            inkcanvas1.Width = image1.Width;
            image1.Source = Imaging.CreateBitmapSourceFromHBitmap(ImageList.bitmaps[ImageList.actualIndex].GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            image1.Source = BitmapToSource(MainWindow.RotateImage(ImageList.bitmaps[ImageList.actualIndex], ImageList.anglesActual[ImageList.actualIndex]));
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inkcanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            inkcanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "savedimage"; // Default file name
            dlg.DefaultExt = ".jpg"; // Default file extension
            dlg.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                //get the dimensions of the ink control
                int margin = (int)this.inkcanvas1.Margin.Left;
                int width = (int)this.inkcanvas1.ActualWidth - margin;
                int height = (int)this.inkcanvas1.ActualHeight - margin;
                //render ink to bitmap
                RenderTargetBitmap rtb =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(inkcanvas1);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }
            }


        }

        private void RedButton_Checked(object sender, RoutedEventArgs e)
        {
            inkcanvas1.DefaultDrawingAttributes.Color = Colors.Red;

        }

        private void GreenButton_Checked(object sender, RoutedEventArgs e)
        {
            inkcanvas1.DefaultDrawingAttributes.Color = Colors.Green;
        }

        private void BlueButton_Checked(object sender, RoutedEventArgs e)
        {
            inkcanvas1.DefaultDrawingAttributes.Color = Colors.Blue;
        }

        string nameOfDirectory()
        {
            string name = System.AppDomain.CurrentDomain.BaseDirectory;
            string directory = "ВПФ Фоторедактор_checkDraw";
            int indexOfDirectory = name.IndexOf(directory);
            int size = directory.Length;
            name = name.Substring(0, indexOfDirectory + size);
            return name;
        }
    }
}
