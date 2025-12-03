using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{
    public class PdfPrintDocument : PrintDocument
    {
        private readonly PdfiumViewer.PdfDocument _pdf;

        public PdfPrintDocument(PdfiumViewer.PdfDocument pdf)
        {
            _pdf = pdf;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            // Get PDF page as bitmap
            using (var img = _pdf.Render(0, 300, 300, true)) // 300 DPI
            {
                // Print at exact size with no scaling
                e.Graphics.DrawImage(img, new Rectangle(0, 0,
                    e.PageBounds.Width,
                    e.PageBounds.Height));
            }

            e.HasMorePages = false;
        }
    }
}
