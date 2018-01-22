using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace tr.mustaliscl.ekran
{
    public class Ekran
    {
        public static Bitmap EkranResmi()
        {
            Bitmap BMP = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height,
                                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Graphics GFX = System.Drawing.Graphics.FromImage(BMP);
            GFX.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X,
                                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y,
                                0, 0,
                                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size,
                                System.Drawing.CopyPixelOperation.SourceCopy);

            return BMP;
        } 
    }
}
