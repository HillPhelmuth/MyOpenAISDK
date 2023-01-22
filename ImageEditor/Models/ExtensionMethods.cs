using ImageEditor.Services;
using SkiaSharp;

namespace ImageEditor.Models
{
    public static class ExtensionMethods
    {
        //public static DrawnRectangleI ToRectI(this DrawnRectangle drawnRectangle)
        //{
        //    var x = Convert.ToInt32(drawnRectangle.X);
        //    var y = Convert.ToInt32(drawnRectangle.Y);
        //    var w = Convert.ToInt32(drawnRectangle.Width);
        //    var h = Convert.ToInt32(drawnRectangle.Height);
        //    return new DrawnRectangleI(x, y, w, h);
        //}

        public static SKRectI ToSkRectI(this DrawnRectangle drawnRectangle)
        {
            var x = Convert.ToInt32(drawnRectangle.X);
            var y = Convert.ToInt32(drawnRectangle.Y);
            var w = Convert.ToInt32(drawnRectangle.Width);
            var h = Convert.ToInt32(drawnRectangle.Height);
            return SKRectI.Create(x, y, w, h);
        }

        public static SKRectI[] ToSkRectIArray(this DrawnRectangle[] drawnRectangles)
        {
            return drawnRectangles.Select(x => x.ToSkRectI()).ToArray();
        }
    }
}
