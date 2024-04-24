using UnityEngine;

public class Converter
{ 
    public static Protocol.Vector3 Convert( UnityEngine.Vector3 vector )
    {
        return new Protocol.Vector3
        {
            X = vector.x,
            Y = vector.y,
            Z = vector.z
        };
    }

    public static UnityEngine.Vector3 Convert( Protocol.Vector3 vector )
    {
        return new UnityEngine.Vector3(vector.X, vector.Y, vector.Z);
    }

    public static Quaternion Convert (float z)
    {
        return Quaternion.Euler(0, 0, z);
    }

    public static float Convert (Quaternion quaternion)
    {
        return quaternion.eulerAngles.z;
    }
}