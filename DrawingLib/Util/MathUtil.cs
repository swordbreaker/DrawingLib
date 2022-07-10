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

        public static RectF CalculateBoundingBox(params PointF[] points) => points.CalculateBoundingBox();

        public static RectF CalculateBoundingBox(this IEnumerable<PointF> points) =>
            Rect.FromLTRB(
                points.Select(p => p.X).Min(),
                points.Select(p => p.Y).Min(),
                points.Select(p => p.X).Max(),
                points.Select(p => p.Y).Max());

        public static IEnumerable<PointF> GetCorners(this RectF self)
        {
            yield return new PointF(self.Left, self.Top);
            yield return new PointF(self.Right, self.Top);
            yield return new PointF(self.Right, self.Bottom);
            yield return new PointF(self.Left, self.Bottom);
        }

        public static IEnumerable<PointF> GetRectAnchorPoints(this RectF self) =>
            new[]
            {
                new PointF(self.Left, self.Center.Y),
                new PointF(self.Right, self.Center.Y),
                new PointF(self.Center.X, self.Top),
                new PointF(self.Center.X, self.Bottom),
            };
    }
}
