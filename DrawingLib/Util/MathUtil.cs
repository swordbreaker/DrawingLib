using System.Numerics;

namespace DrawingLib.Util
{
    public static class MathUtil
    {
        public static float RadToDeg(float radians) => 
            (180 / MathF.PI) * radians;
        public static float DegToRad(float degrees) =>
            degrees * MathF.PI / 180;

        /// <summary>
        /// Gets the rotation between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The amount of rotation in rad from the first vector a to the second vector b. 
        /// If the amount of rotation is greater than a half-rotation, then the equivalent negative angle is returned.</returns>
        public static float Rotation(this Vector2 a, Vector2 b) =>
            MathF.Acos(Vector2.Dot(Vector2.Normalize(a), Vector2.Normalize(b)));

        public static RectF CalculateBoundingBox(params Vector2[] points) => points.CalculateBoundingBox();

        public static RectF CalculateBoundingBox(this IEnumerable<Vector2> points) =>
            Rect.FromLTRB(
                points.Select(p => p.X).Min(),
                points.Select(p => p.Y).Min(),
                points.Select(p => p.X).Max(),
                points.Select(p => p.Y).Max());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <returns>
        /// TopLeft, TopRight, BottomRight, BottomLeft 
        /// </returns>
        public static Vector2[] GetCorners(this RectF self) =>
            new[]
            {
                new Vector2(self.Left, self.Top),
                new Vector2(self.Right, self.Top),
                new Vector2(self.Right, self.Bottom),
                new Vector2(self.Left, self.Bottom),
            };

        public static IEnumerable<PointF> GetRectAnchorPoints(this RectF self) =>
            new[]
            {
                new PointF(self.Left, self.Center.Y),
                new PointF(self.Center.X, self.Top),
                new PointF(self.Right, self.Center.Y),
                new PointF(self.Center.X, self.Bottom),
            };

        public static SizeF Max(this SizeF a, SizeF b) =>
            new(MathF.Max(a.Width, b.Width), MathF.Max(a.Height, b.Height));

        public static PointF ToPoint(this SizeF size) => new PointF(size.Width, size.Height);
        public static Vector2 ToVec2(this SizeF size) => new Vector2(size.Width, size.Height);

        public static RectF Transform(this RectF rect, Matrix3x2 tranfomationMatrix) =>
            rect.GetCorners().Select(p => Vector2.Transform(p, tranfomationMatrix)).CalculateBoundingBox();

        public static RectF Union(this IEnumerable<RectF> rects)
        {
            var left = rects.Select(r => r.Left).Min();
            var top = rects.Select(r => r.Top).Min();
            var right = rects.Select(r => r.Right).Max();
            var bottom = rects.Select(r => r.Bottom).Max();

            return RectF.FromLTRB(left, top, right, bottom);
        }

        public static RectF Union(params RectF[] rects) => rects.Union();
    }
}
