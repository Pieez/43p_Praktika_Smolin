using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace practika.ViewModel
{
    public class Registr
    {
        public string service { get; set; }
        public string Barcode { get; set; }
        public decimal price { get; set; }

        public byte[] BarcodeBytes { get; set; }
        public ImageSource BarcodeImage
        {
            get
            {
                if (BarcodeBytes == null || BarcodeBytes.Length == 0) return null;
                var image = new BitmapImage();
                using (var stream = new MemoryStream(BarcodeBytes))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                return image;
            }
        }
    }
}
