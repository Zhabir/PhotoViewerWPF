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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace ВПФ_Фоторедактор
{
    /// <summary>
    /// Логика взаимодействия для Collages.xaml
    /// </summary>
    public partial class Collages : Window
    {
        int count = 0;
        public Collages()
        {
            InitializeComponent();
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
        private void buttonSamples_Click(object sender, RoutedEventArgs e)
        {
            if(count == 0 || count == 4)
            {
                count = 2;
                Border2.Visibility = Visibility.Hidden;
                Border3.Visibility = Visibility.Hidden;
                Border5.Visibility = Visibility.Hidden;
                Border6.Visibility = Visibility.Hidden;
                Border7.Visibility = Visibility.Visible;
                Border8.Visibility = Visibility.Visible;
            }
            else if(count == 2)
            {
                count = 3;
                Border7.Visibility = Visibility.Hidden;
                Border8.Visibility = Visibility.Hidden;
                Border2.Visibility=Visibility.Visible;
                Border3.Visibility = Visibility.Visible;
                Border4.Visibility = Visibility.Visible;
            }
            else if(count == 3)
            {
                count = 4;
                Border4.Visibility = Visibility.Hidden;
                Border5.Visibility = Visibility.Visible;
                Border6.Visibility = Visibility.Visible;
            }
        }

        
        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            string ImageFileName=null;
            List<Bitmap> images = new List<Bitmap>();
            if (count == 0) return;
            if (count ==2)
            {
                for (int j = 0; j < 2; j++)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "JPEG|*.jpg";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        ImageFileName = openFileDialog.FileName;
                        Bitmap a = new Bitmap(ImageFileName);
                        images.Add(a);
                    }
                }

                image7.Source = BitmapToSource(images[0]);
                image8.Source = BitmapToSource(images[1]);
            }
            else if (count == 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "JPEG|*.jpg";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        ImageFileName = openFileDialog.FileName;
                        Bitmap a = new Bitmap(ImageFileName);
                        images.Add(a);
                    }
                }

                image2.Source = BitmapToSource(images[0]);
                image3.Source = BitmapToSource(images[1]);
                image4.Source = BitmapToSource(images[2]);
            }
            else if (count == 4)
            {
                
                for (int j = 0; j < 4; j++)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "JPEG|*.jpg";
                    if (openFileDialog.ShowDialog() == true )
                    {
                        ImageFileName = openFileDialog.FileName;
                        Bitmap a = new Bitmap(ImageFileName);
                        images.Add(a);
                    }
                }

                image2.Source = BitmapToSource(images[0]);
                image3.Source = BitmapToSource(images[1]);
                image5.Source = BitmapToSource(images[2]);
                image6.Source = BitmapToSource(images[3]);
            }
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

        private void save_ScreenShot_as_File()

        {

            String filename = "ScreenCapture-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";


            int screenLeft = (int)SystemParameters.VirtualScreenLeft;

            int screenTop = (int)SystemParameters.VirtualScreenTop;

            int screenWidth = (int)SystemParameters.VirtualScreenWidth;

            int screenHeight = (int)SystemParameters.VirtualScreenHeight;

            //</ size >



            Bitmap bitmap_Screen = new Bitmap(screenWidth, screenHeight);

            Graphics g = Graphics.FromImage(bitmap_Screen);



            g.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap_Screen.Size);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image files(*.jpg) | *.jpg";
            if(sfd.ShowDialog()==true)
                 bitmap_Screen.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //</ save bitmap >



            //------------</ save_ScreenShot_as_File() >------------

        }

      
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            save_ScreenShot_as_File();
        }


        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
}
