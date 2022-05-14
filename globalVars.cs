using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace V4._0
{
    public class globalVars
    {
        public static int statuss; //хто я?
        public static string nameG;
        public static string surnameG;
        public static int id_usr;
        public static int leng;

        public static ImageSource NewOpenPic(string path) //подгрузщик картинок
        {
            var bitmap = new BitmapImage();
            var stream = File.OpenRead(path);
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.StreamSource = stream;
            bitmap.EndInit();
            stream.Close();
            return bitmap;
        }
    }
}
