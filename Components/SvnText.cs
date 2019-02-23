using iTextSharp.text;
using iTextSharp.text.pdf;
using SVN.Pdf.Enums;

namespace SVN.Pdf.Components
{
    public class SvnText : SvnCell
    {
        private string Text { get; }
        private float Size { get; }
        private VAlign VerticalAlignment { get; }
        private HAlign HorizontalAlignment { get; }

        private int ElementAlignmentV
        {
            get
            {
                switch (this.VerticalAlignment)
                {
                    case VAlign.Bottom:
                        return Element.ALIGN_BOTTOM;
                    case VAlign.Middle:
                        return Element.ALIGN_MIDDLE;
                    case VAlign.Top:
                    default:
                        return Element.ALIGN_TOP;
                }
            }
        }

        private int ElementAlignmentH
        {
            get
            {
                switch (this.HorizontalAlignment)
                {
                    case HAlign.Right:
                        return Element.ALIGN_RIGHT;
                    case HAlign.Center:
                        return Element.ALIGN_CENTER;
                    case HAlign.Left:
                    default:
                        return Element.ALIGN_LEFT;
                }
            }
        }

        private BaseFont BaseFont
        {
            get => BaseFont.CreateFont(@"C:\Windows\Fonts\Calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        }

        internal SvnText(SvnCell parent, string text, float size, VAlign vAlign, HAlign hAlign, BaseColor background, BaseColor foreground, SvnBorder border)
        {
            base.Parent = parent;
            base.Background = background;
            base.Foreground = foreground;
            base.Border = border;

            this.Text = text;
            this.Size = size;
            this.VerticalAlignment = vAlign;
            this.HorizontalAlignment = hAlign;
        }

        public override PdfPTable Build()
        {
            var table = new PdfPTable(100)
            {
                WidthPercentage = 100,
            };

            table.AddCell(new PdfPCell(new Paragraph(this.Text, new Font(this.BaseFont, this.Size, default(int), base.Foreground)))
            {
                Colspan = 100,
                FixedHeight = base.AbsoluteHeight(),
                VerticalAlignment = this.ElementAlignmentV,
                HorizontalAlignment = this.ElementAlignmentH,
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