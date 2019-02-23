using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Hosting;

namespace SVN.Pdf.Components
{
    public class SvnImage : SvnCell
    {
        private string FilePath { get; }

        internal SvnImage(SvnCell parent, string filePath, BaseColor background, BaseColor foreground, SvnBorder border)
        {
            base.Parent = parent;
            base.Background = background;
            base.Foreground = foreground;
            base.Border = border;

            this.FilePath = filePath;
        }

        public override PdfPTable Build()
        {
            var table = new PdfPTable(100)
            {
                WidthPercentage = 100,
            };

            if (this.FilePath is null)
            {
                return table;
            }

            var filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, this.FilePath);
            var image = Image.GetInstance(filePath);
            image.ScaleAbsolute(base.AbsoluteWidth(), base.AbsoluteHeight());

            table.AddCell(new PdfPCell(image)
            {
                Colspan = 100,
                FixedHeight = base.AbsoluteHeight(),
                BackgroundColor = base.Background,
                BorderWidthTop = base.BorderWidthTop,
                BorderWidthRight = base.BorderWidthRight,
                BorderWidthBottom = base.BorderWidthBottom,
                BorderWidthLeft = base.BorderWidthLeft,
                BorderColor = base.BorderColor,
            });

            return table;
        }
    }
}