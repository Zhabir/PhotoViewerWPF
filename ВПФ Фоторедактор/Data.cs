using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВПФ_Фоторедактор
{
    internal class Data
    {
       public double RgbValue { get; set; }
        public double ContrastValue { get; set; }
        public double BrightnessValue { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RGBRadio { get; set; }
        public int actualindex { get; set; }
        public List<int> angles { get; set; }
        public int bitmaps_count { get; set; }
        public int index_name { get; set; }
    }
}
