using iTextSharp.text;

namespace SVN.Pdf.Components
{
    public class SvnBorder
    {
        public int? BorderWidth { get; }
        public int? BorderWidthTop { get; }
        public int? BorderWidthRight { get; }
        public int? BorderWidthBottom { get; }
        public int? BorderWidthLeft { get; }
        public BaseColor Color { get; }

        public SvnBorder(int borderWidth)
            : this(borderWidth, BaseColor.BLACK)
        {
        }

        public SvnBorder(int borderWidth, BaseColor color)
        {
            this.BorderWidth = borderWidth;
            this.Color = color;
        }

        public SvnBorder(int borderWidthVertical, int borderHorizontal)
            : this(borderWidthVertical, borderHorizontal, BaseColor.BLACK)
        {
        }

        public SvnBorder(int borderWidthVertical, int borderHorizontal, BaseColor color)
        {
            this.BorderWidthTop = borderWidthVertical;
            this.BorderWidthRight = borderHorizontal;
            this.BorderWidthBottom = borderWidthVertical;
            this.BorderWidthLeft = borderHorizontal;
            this.Color = color;
        }

        public SvnBorder(int borderWidthTop, int borderWidthRight, int borderWidthBottom, int borderWidthLeft)
            : this(borderWidthTop, borderWidthRight, borderWidthBottom, borderWidthLeft, BaseColor.BLACK)
        {
        }

        public SvnBorder(int borderWidthTop, int borderWidthRight, int borderWidthBottom, int borderWidthLeft, BaseColor color)
        {
            this.BorderWidthTop = borderWidthTop;
            this.BorderWidthRight = borderWidthRight;
            this.BorderWidthBottom = borderWidthBottom;
            this.BorderWidthLeft = borderWidthLeft;
            this.Color = color;
        }
    }
}