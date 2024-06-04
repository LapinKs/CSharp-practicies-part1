namespace ReadOnlyVector;
public class ReadOnlyVector
{
    public readonly double X;
    public readonly double Y;
    public ReadOnlyVector(double X, double Y)
    {
        this.X = X;
        this.Y = Y;
    }
    public ReadOnlyVector Add(ReadOnlyVector other) { return new ReadOnlyVector(X + other.X, Y + other.Y); }
    public ReadOnlyVector WithX(double x) { return new ReadOnlyVector(x, this.Y); }
    public ReadOnlyVector WithY(double y) { return new ReadOnlyVector(this.X, y); }
}