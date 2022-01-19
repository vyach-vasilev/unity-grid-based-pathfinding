using UnityEngine;

public static class MathExtension
{
    public static void ToInt(this Transform transform)
    {
        transform.position =transform.position.ToInt();
        transform.eulerAngles = transform.eulerAngles.ToInt();
        transform.localScale =transform.localScale.ToInt();
    }
    
    public static Vector3Int ToInt(this Vector3 vector)
    {
        return new Vector3Int(
            Mathf.CeilToInt(vector.x),
            Mathf.CeilToInt(vector.y),
            Mathf.CeilToInt(vector.z)
            );
    }
    
    public static int ToInt(this float value)
    {
        return Mathf.CeilToInt(value);
    }
}