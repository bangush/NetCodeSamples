using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace tr.mustaliscl.frames
{
    public class Frame
    {
        [DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(
        IntPtr hdcDest,
        int nXDest,
        int nYDest,
        int nWidth,
        int nHeight,
        IntPtr hdcSrc,
        int nXSrc,
        int nYSrc,
        int dwRop);

        /// <summary>
        /// Verilen Control Nesnesinin resmini bitmap olarak döndürür.
        /// .NET 4.0 da System.Drawing ve System.Windows.Forms 
        /// kütüphanelerinin eklenmesi gerekir.
        /// </summary>
        /// <param name="control">Control nesnesi</param>
        /// <returns>Verilen Control Nesnesinin resmini bitmap olarak döndürür.</returns>
        public System.Drawing.Bitmap ControlResmi(System.Windows.Forms.Control control)
        {
            System.Drawing.Bitmap controlBmp;
            using (System.Drawing.Graphics g1 = control.CreateGraphics())
            {
                controlBmp = new System.Drawing.Bitmap(control.Width, control.Height, g1);
                using (System.Drawing.Graphics g2 = System.Drawing.Graphics.FromImage(controlBmp))
                {
                    System.IntPtr dc1 = g1.GetHdc();
                    System.IntPtr dc2 = g2.GetHdc();
                    BitBlt(dc2, 0, 0, control.Width, control.Height, dc1, 0, 0, 13369376);
                    g1.ReleaseHdc(dc1);
                    g2.ReleaseHdc(dc2);
                }
            }
            return controlBmp;
        }


        public static void Temizle(System.Windows.Forms.Control kontrol)
        {
            foreach (System.Windows.Forms.Control ctrControl in kontrol.Controls)
            {
                //Loop through all controls
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox
                    ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;
                    //If it is then set the text to String.Empty (empty textbox)
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    //If its a RichTextBox clear the text
                    ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    //Next check if it's a dropdown list
                    ((System.Windows.Forms.ComboBox)ctrControl).SelectedIndex = -1;
                    //If it is then set its SelectedIndex to 0
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    //Next uncheck all checkboxes
                    ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    //Unselect all RadioButtons
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
                if (ctrControl.Controls.Count > 0)
                {
                    //Call itself to get all other controls in other containers
                    Temizle(ctrControl);
                }
            }
        }

        public static Control kontrolBul(Control container, string name)
        {
            if (container.Name.Equals(name))
                return container;

            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = kontrolBul(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }

        System.Drawing.Point GetCaretCoordinates(System.Windows.Forms.RichTextBox rtb)
        { // note, get rid of the "+1" if you want the coordinates to be zero based
            System.Drawing.Point p = new System.Drawing.Point();
            p.Y = (rtb.GetLineFromCharIndex(rtb.SelectionStart)) + 1;
            p.X = (rtb.SelectionStart - rtb.GetFirstCharIndexOfCurrentLine()) + 1;
            return p;
        }
    }
}
