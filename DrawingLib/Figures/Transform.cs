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

        public Matrix3x2 GetTransformationMatrixRelativeTo(Transform? transform)
        {
            if(transform == null)
            {
                return AbsoluteTransformationMatrix;
            }

            var finalMatrix = TransformationMatrix;
            var current = this.Parent;
            while(current != null && current != transform)
            {
                finalMatrix *= current.TransformationMatrix;
                current = current.Parent;
            }

            if(current == null)
            {
                return AbsoluteTransformationMatrix;
            }
            else
            {
                return finalMatrix;
            }
        }

        public Vector2 GetPositionRelativeTo(Transform? transform)
        {
            var matrix = GetTransformationMatrixRelativeTo(transform);
            return Vector2.Transform(this.Position, matrix);
        }

        public void SetRadRotation(float rad)
        {
            Rotation = MathUtil.RadToDeg(rad);
        }

        public void SetPosition(PointF point)
        {
            Position = new Vector2(point.X, point.Y);
        }

        public void SetPosition(float x, float y) => this.Position = new Vector2(x, y);

        public void SetPositionX(float x) => this.Position = new Vector2(x, Position.Y);

        public void SetPositionY(float y) => this.Position = new Vector2(Position.X, y);

        public void Translate(Vector2 p) => this.Position += p;
    }
}
