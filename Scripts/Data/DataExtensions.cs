using UnityEngine;

namespace TowerDefence.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) =>
             new Vector3Data(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector(this Vector3Data Vector3Data) =>
            new Vector3(Vector3Data.X, Vector3Data.Y, Vector3Data.Z);

        public static string ToJson(this object obj) => 
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}
