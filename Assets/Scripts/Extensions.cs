
public static class Extensions
{
    public static bool IsNull<T>(this T o) where T : UnityEngine.Object
    {
        return ReferenceEquals(o, null);
    }
    public static bool IsNullOrDestroyed<T>(this T o) where T : UnityEngine.Object
    {
        return o == null;
    }
}
