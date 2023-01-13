using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВПФ_Фоторедактор
{
    internal class ColorBalance
    {
        public static UInt32 ColorBalance_R(UInt32 point, double poz, double lenght)
        {
            double R;
            double G;
            double B;

            double N = (100 / lenght) * poz; //кол-во процентов

            R = (((point & 0x00FF0000) >> 16) + N * 128 / 100);
            G = ((point & 0x0000FF00) >> 8);
            B = (point & 0x000000FF);

            //контролируем переполнение переменных
            if (R < 0) R = 0;
            if (R > 255) R = 255;

            point = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

            return point;
        }

        //цветовой баланс G
        public static UInt32 ColorBalance_G(UInt32 point, double poz, double lenght)
        {
            double R;
            double G;
            double B;

            double N = (100 / lenght) * poz; //кол-во процентов

            R = ((point & 0x00FF0000) >> 16);
            G = (((point & 0x0000FF00) >> 8) + N * 128 / 100);
            B = (point & 0x000000FF);

            //контролируем переполнение переменных
            if (G < 0) G = 0;
            if (G > 255) G = 255;

            point = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

            return point;
        }

        //цветовой баланс B
        public static UInt32 ColorBalance_B(UInt32 point, double poz, double lenght)
        {
            double R;
            double G;
            double B;

            double N = (100 / lenght) * poz; //кол-во процентов

            R = ((point & 0x00FF0000) >> 16);
            G = ((point & 0x0000FF00) >> 8);
            B = ((point & 0x000000FF) + N * 128 / 100);

            //контролируем переполнение переменных
            if (B < 0) B = 0;
            if (B > 255) B = 255;

            point = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

            return point;
        }
    }
}
