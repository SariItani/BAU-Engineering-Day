using UnityEngine;

class Utils
{

    public static Vector2 ToVector2(Vector3 ref_vec) => new(ref_vec.x, ref_vec.y);
    public static bool ArrayNotEmpty<T>(T[] array) where T : class
    {
        if (array == null || array.Length == 0)
            return false;
        return true;
    }
    public static Vector3 ToVector3(Vector2 ref_vec) => new(ref_vec.x, ref_vec.y, 0);
    public static Vector3 ToVector3(float x_coord = 0, float y_coord = 0) => new(x_coord, y_coord, 0);

    public static Vector2 ToVector2(float x_coord = 0, float y_coord = 0)
    {
        return new(x_coord, y_coord);
    }

    public static Vector3 OffsetVectorX(Vector3 base_vec, float offset, float direction) => base_vec + ToVector3(x_coord: direction * offset);
}