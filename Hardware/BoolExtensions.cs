namespace Hardware;

public static class BoolExtensions
{
    public static byte ToByte(this bool value)
    {
        return (byte)(value ? 1 : 0);
    }
}
