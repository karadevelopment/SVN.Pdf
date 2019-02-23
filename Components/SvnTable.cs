using iTextSharp.text;
using iTextSharp.text.pdf;
using SVN.Pdf.Enums;
using System;
using System.Collections.Generic;

namespace SVN.Pdf.Components
{
    public class SvnTable : SvnCell
    {
        private List<SvnRow> Rows { get; } = new List<SvnRow>();

        public SvnTable(Document document, bool isDebugMode = false)
        {
            base.IsDebugMode = isDebugMode;
            base.AbsoluteWidth = () => document.Right - document.Left;
            base.AbsoluteHeight = () => document.Top - document.Bottom;
        }

        public SvnTable(SvnCell parent, float absoluteWidth, float absoluteHeight, BaseColor background, BaseColor foreground, SvnBorder border)
        {
            base.Parent = parent;
            base.AbsoluteWidth = () => absoluteWidth;
            base.AbsoluteHeight = () => absoluteHeight;
            base.Background = background;
            base.Foreground = foreground;
            base.Border = border;
        }

        public void AddRow(
            float relativeHeight = 100,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null,
            Action<SvnRow> delegator = null)
        {
            var row = new SvnRow(this, relativeHeight, background, foreground, border);
            this.Rows.Add(row);
            delegator?.Invoke(row);
        }

        public void AddText(
            float relativeHeight = 100,
            string text = "",
            float size = 10,
            VAlign vAlign = VAlign.Top,
            HAlign hAlign = HAlign.Left,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddRow(
                relativeHeight: relativeHeight,
                background: background,
                foreground: foreground,
                delegator: row =>
            {
                row.AddCol(
                    relativeWidth: 100,
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
            });
        }

        public void AddImage(
            float relativeHeight = 100,
            string filePath = null,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddRow(
                relativeHeight: relativeHeight,
                background: background,
                foreground: foreground,
                delegator: row =>
            {
                row.AddCol(
                    relativeWidth: 100,
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
            });
        }

        public void AddEmpty(
            float relativeHeight = 100,
            BaseColor background = null,
            BaseColor foreground = null,
            SvnBorder border = null)
        {
            this.AddText(
                relativeHeight: relativeHeight,
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

            foreach (var row in this.Rows)
            {
                table.AddCell(new PdfPCell(row.Build())
                {
                    Colspan = 100,
                    FixedHeight = row.AbsoluteHeight(),
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