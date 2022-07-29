namespace DrawingLib.Util
{
    public static class MathMetrics
    {
        public static Pi PI => new(1);

        public static Pi Deg => new (1);
    }

    public record Pi(float amount)
    {
        public float ToDegree() =>
            MathUtil.RadToDeg(this);

        public static implicit operator float(Pi pi) => pi.amount * MathF.PI;

        public static Pi operator *(float amout, Pi pi) =>
            new(amout * pi.amount);

        public static Pi operator *(Pi a, Pi b) =>
            new(a.amount * b.amount);

        public static Pi operator +(Pi a, Pi b) =>
            new(a.amount + b.amount);

        public static Pi operator -(Pi a, Pi b) =>
            new(a.amount - b.amount);

        public static Pi operator /(Pi a, Pi b) =>
            new(a.amount / b.amount);
    }

    public record Deg(float amount)
    {
        public static Deg operator *(float amout, Deg deg) =>
            new(amout * deg.amount);

        public static Deg operator *(Deg a, Deg b) =>
            new(a.amount * b.amount);

        public static Deg operator +(Deg a, Deg b) =>
            new(a.amount + b.amount);

        public static Deg operator -(Deg a, Deg b) =>
            new(a.amount - b.amount);

        public static Deg operator /(Deg a, Deg b) =>
            new(a.amount / b.amount);
    }
}
