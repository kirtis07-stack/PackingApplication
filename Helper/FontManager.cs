using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{
    using System.Drawing;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;

    public static class FontManager
    {
        private static PrivateFontCollection pfc = new PrivateFontCollection();
        public static FontFamily CustomFontFamily { get; private set; }

        static FontManager()
        {
            byte[] fontData = Properties.Resources.PublicSans;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            pfc.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);

            CustomFontFamily = pfc.Families[0];
        }

        public static Font GetFont(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font(CustomFontFamily, size, style);
        }
    }
}
