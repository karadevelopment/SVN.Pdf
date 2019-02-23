using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;

namespace SVN.Pdf.Components
{
    public class SvnCell
    {
        private static Random RNG { get; } = new Random();
        private static List<BaseColor> Colors { get; } = new List<BaseColor>
        {
            //BaseColor.BLACK,
            BaseColor.BLUE,
            BaseColor.CYAN,
            BaseColor.DARK_GRAY,
            BaseColor.GRAY,
            BaseColor.GREEN,
            BaseColor.LIGHT_GRAY,
            BaseColor.MAGENTA,
            BaseColor.ORANGE,
            BaseColor.PINK,
            BaseColor.RED,
            //BaseColor.WHITE,
            BaseColor.YELLOW,
        };

        protected bool IsDebugMode { get; set; }
        protected SvnCell Parent { get; set; }
        internal Func<float> AbsoluteWidth { get; set; }
        internal Func<float> AbsoluteHeight { get; set; }
        internal float RelativeWidth { get; set; }
        internal float RelativeHeight { get; set; }
        private BaseColor _Background { get; set; }
        private BaseColor _Foreground { get; set; }
        protected SvnBorder Border { get; set; }

        protected BaseColor Background
        {
            get => this.Parent?.Background ?? this._Background ?? (this.IsDebugMode ? SvnCell.Colors[SvnCell.RNG.Next(1, SvnCell.Colors.Count) - 1] : BaseColor.WHITE);
            set => this._Background = value;
        }

        protected BaseColor Foreground
        {
            get => this.Parent?.Foreground ?? this._Foreground ?? BaseColor.BLACK;
            set => this._Foreground = value;
        }

        protected float BorderWidthTop
        {
            get => this.Border?.BorderWidthTop ?? this.Border?.BorderWidth ?? default(int);
        }

        protected float BorderWidthRight
        {
            get => this.Border?.BorderWidthRight ?? this.Border?.BorderWidth ?? default(int);
        }

        protected float BorderWidthBottom
        {
            get => this.Border?.BorderWidthBottom ?? this.Border?.BorderWidth ?? default(int);
        }

        protected float BorderWidthLeft
        {
            get => this.Border?.BorderWidthLeft ?? this.Border?.BorderWidth ?? default(int);
        }

        protected BaseColor BorderColor
        {
            get => this.Border?.Color ?? BaseColor.BLACK;
        }

        protected SvnCell()
        {
            this.AbsoluteWidth = () => this.Parent?.AbsoluteWidth() ?? default(float);
            this.AbsoluteHeight = () => this.Parent?.AbsoluteHeight() ?? default(float);
        }

        public virtual PdfPTable Build()
        {
            return null;
        }
    }
}