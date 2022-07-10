using DrawingLib.Util;
using System.Numerics;

namespace DrawingLib.Figures
{
    public class Transform
    {
        public Transform? Parent { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Rotation { get; set; } = 0;
        public Vector2 Scale { get; set; } = Vector2.One;

        // L = T * R * S
        public Matrix3x2 TransformationMatrix =>
            Matrix3x2.CreateRotation(MathUtil.DegToRad(Rotation)) *
            Matrix3x2.CreateTranslation(Position) *
            Matrix3x2.CreateScale(Scale);

        public Vector2 AbsolutePosition =>
            Vector2.Transform(Vector2.Zero, AbsoluteTransformationMatrix);

        public float AbsoluteRotation => (Parent?.AbsoluteRotation ?? 0) + Rotation;

        public Vector2 AbsoluteScale => (Parent?.AbsoluteScale ?? Vector2.One) * Scale;

        public Matrix3x2 AbsoluteTransformationMatrix =>
            TransformationMatrix *
            (Parent?.AbsoluteTransformationMatrix ?? Matrix3x2.Identity);

        //public Matrix3x2 AbsoluteTransformationMatrix =>
        //    (Parent?.AbsoluteTransformationMatrix ?? Matrix3x2.Identity) *
        //    TransformationMatrix;


        //Matrix3x2.CreateScale(AbsoluteScale) *
        //Matrix3x2.CreateRotation(MathUtil.DegToRad(AbsoluteRotation)) *
        //Matrix3x2.CreateTranslation(AbsolutePosition);

        //public Matrix3x2 AbsoluteTransformationMatrix =>
        //    Matrix3x2.Identity *
        //    Matrix3x2.CreateTranslation(AbsolutePosition) *
        //    Matrix3x2.CreateRotation(MathUtil.DegToRad(AbsoluteRotation)) *
        //    Matrix3x2.CreateScale(AbsoluteScale);

        public void SetRadRotation(float rad)
        {
            Rotation = MathUtil.RadToDeg(rad);
        }

        public void SetPosition(PointF point)
        {
            Position = new Vector2(point.X, point.Y);
        }
    }
}
