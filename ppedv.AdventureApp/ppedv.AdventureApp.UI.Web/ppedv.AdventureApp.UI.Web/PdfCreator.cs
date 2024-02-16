using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using ppedv.AdventureApp.Model.DomainModel;

namespace ppedv.AdventureApp.UI.Web
{
    public class PdfCreator
    {
        static PdfCreator()
        {
            GlobalFontSettings.FontResolver = new FileFontResolver();
        }

        public MemoryStream CreateBirthDayCardPdf(EmployeeBirthdayCard card)
        {
            var document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            document.Info.Subject = "Just a simple Hello-World program.";

            // Create an empty page in this document.
            var page = document.AddPage();

            // Get an XGraphics object for drawing on this page.
            var gfx = XGraphics.FromPdfPage(page);

            //// Create a font.
            var font = new XFont("Verdana", 20, XFontStyleEx.BoldItalic);


            //docu / samples: https://www.pdfsharp.net/wiki/PDFsharpSamples.ashx
            // Draw the text.
            gfx.DrawString(card.Title, font, XBrushes.Black,
                new XRect(0, 0, page.Width-50, page.Height), XStringFormats.TopCenter);

            XRect rect = new XRect(40, 100, page.Width - 50, page.Height);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.DrawString(card.Text, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            
            var memStream = new MemoryStream();
            document.Save(memStream, false);
            memStream.Position = 0;
            return memStream;
        }

        public class FileFontResolver : IFontResolver // FontResolverBase
        {
            public string DefaultFontName => throw new NotImplementedException();

            public byte[] GetFont(string faceName)
            {
                using (var ms = new MemoryStream())
                {
                    using (var fs = File.Open(faceName, FileMode.Open))
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        return ms.ToArray();
                    }
                }
            }

            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                if (familyName.Equals("Verdana", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new FontResolverInfo("Fonts/Verdana.ttf");
                    if (isBold && isItalic)
                    {
                        return new FontResolverInfo("Fonts/Verdana-BoldItalic.ttf");
                    }
                    else if (isBold)
                    {
                        return new FontResolverInfo("Fonts/Verdana-Bold.ttf");
                    }
                    else if (isItalic)
                    {
                        return new FontResolverInfo("Fonts/Verdana-Italic.ttf");
                    }
                    else
                    {
                        return new FontResolverInfo("Fonts/Verdana-Regular.ttf");
                    }
                }
                return null;
            }
        }
    }
}
