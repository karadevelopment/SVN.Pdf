using iTextSharp.text;
using iTextSharp.text.pdf;
using SVN.Pdf.Enums;
using System;

namespace SVN.Pdf.Components
{
    public class SvnCol : SvnCell
    {
        private SvnCell Content { get; set; }

        internal SvnCol(SvnCell parent, float relativeWidth, BaseColor background, BaseColor foreground, SvnBorder border)
        {
            base.Parent = parent;
            base.AbsoluteWidth = () => base.Parent.AbsoluteWidth() * base.RelativeWidth / 100;
            base.RelativeWidth = relativeWidth;
            base.Background = background;
            base.Foreground = foreground;
            base.Border = border;
        }

        public void AddTable(
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null,
            Action<SvnTable> delegator = null)
        {
            var table = new SvnTable(this, base.AbsoluteWidth(), base.AbsoluteHeight(), background, foreground, border);
            this.Content = table;
            delegator?.Invoke(table);
        }

        public void AddText(
            string text = "",
            float size = 10,
            VAlign vAlign = VAlign.Top,
            HAlign hAlign = HAlign.Left,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.Content = new SvnText(this, text, size, vAlign, hAlign, background, foreground, border);
        }

        public void AddImage(
            string filePath = null,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.Content = new SvnImage(this, filePath, background, foreground, border);
        }

        public void AddEmpty(
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddText(
                background: background,
                foreground: foreground,
                border: border);
        }

        public override PdfPTable Build()
        {
            var table = new PdfPTable(100)
            {
                WidthPercentage = 100,
            };

            SvnCell cell;
            if (this.Content != null)
            {
                cell = this.Content;
            }
            else
            {
                cell = new SvnText(this, string.Empty, 10, VAlign.Top, HAlign.Left, BaseColor.WHITE, BaseColor.BLACK, null);
            }

            table.AddCell(new PdfPCell(cell.Build())
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