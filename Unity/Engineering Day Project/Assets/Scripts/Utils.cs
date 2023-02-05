using UnityEngine;

class Utils
{

    public static Vector2 ToVector2(Vector3 ref_vec) => new Vector2(ref_vec.x, ref_vec.y);
    public static bool ArrayNotEmpty<T>(T[] array) where T : class
    {
        if (array == null || array.Length == 0)
            return false;
        return true;
    }
}