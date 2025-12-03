using Acrobat;
using System;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{

    public class PdfSilentPrinter
    {
        public static bool PrintPdf(string pdfPath, string printerName = null)
        {
            try
            {
                Acrobat.AcroApp app = new Acrobat.AcroApp();
                Acrobat.AcroAVDoc avDoc = new Acrobat.AcroAVDoc();

                if (!avDoc.Open(pdfPath, ""))
                    return false;

                // Acrobat COM cannot silently select printer anymore
                // Use PrinterSettings instead (fallback)
                if (!string.IsNullOrWhiteSpace(printerName))
                {
                    var ps = new PrinterSettings();
                    ps.PrinterName = printerName;
                    if (!ps.IsValid)
                        throw new Exception("Invalid printer: " + printerName);
                }

                // This shows a dialog unless Acrobat is hacked to be hidden
                avDoc.PrintPages(0, avDoc.GetPDDoc().GetNumPages() - 1, 1, 0,0);

                avDoc.Close(1);
                app.Exit();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
