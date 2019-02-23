using iTextSharp.text;
using iTextSharp.text.pdf;
using SVN.Pdf.Enums;
using System;
using System.Collections.Generic;

namespace SVN.Pdf.Components
{
    public class SvnRow : SvnCell
    {
        private List<SvnCol> Cols { get; } = new List<SvnCol>();

        internal SvnRow(SvnCell parent, float relativeHeight, BaseColor background, BaseColor foreground, SvnBorder border)
        {
            base.Parent = parent;
            base.AbsoluteHeight = () => base.Parent.AbsoluteHeight() * base.RelativeHeight / 100;
            base.RelativeHeight = relativeHeight;
            base.Background = background;
            base.Foreground = foreground;
            base.Border = border;
        }

        public void AddTable(
            float relativeWidth = 100,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null,
            Action<SvnTable> delegator = null)
        {
            this.AddCol(
                relativeWidth: relativeWidth,
                background: background,
                foreground: foreground,
                delegator: col =>
                {
                    col.AddTable(
                        background: background,
                        foreground: foreground,
                        border: border,
                        delegator: delegator);
                });
        }

        public void AddCol(
            float relativeWidth = 100,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null,
            Action<SvnCol> delegator = null)
        {
            var col = new SvnCol(this, relativeWidth, background, foreground, border);
            this.Cols.Add(col);
            delegator(col);
        }

        public void AddText(
            float relativeWidth = 100,
            string text = "",
            float size = 10,
            VAlign vAlign = VAlign.Top,
            HAlign hAlign = HAlign.Left,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddCol(
                relativeWidth: relativeWidth,
                background: background,
                foreground: foreground,
                delegator: col =>
            {
                col.AddText(
                    text: text,
                    size: size,
                    vAlign: vAlign,
                    hAlign: hAlign,
                    background: background,
                    foreground: foreground,
                    border: border);
            });
        }

        public void AddImage(
            float relativeWidth = 100,
            string filePath = null,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddCol(
                relativeWidth: relativeWidth,
                background: background,
                foreground: foreground,
                delegator: col =>
            {
                col.AddImage(
                    filePath: filePath,
                    background: background,
                    foreground: foreground,
                    border: border);
            });
        }

        public void AddEmpty(
            float relativeWidth = 100,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddText(
                relativeWidth: relativeWidth,
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

            foreach (var col in this.Cols)
            {
                table.AddCell(new PdfPCell(col.Build())
                {
                    Colspan = (int)col.RelativeWidth,
                    FixedHeight = base.AbsoluteHeight(),
                    BackgroundColor = base.Background,
                    BorderWidthTop = base.BorderWidthTop,
                    BorderWidthRight = base.BorderWidthRight,
                    BorderWidthBottom = base.BorderWidthBottom,
                    BorderWidthLeft = base.BorderWidthLeft,
                    BorderColor = base.BorderColor,
                });
            }

            return table;
        }
    }
}