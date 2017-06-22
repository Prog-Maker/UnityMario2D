using UnityEngine;


namespace MarioWorldForAll
{

    public static class HelpsClass
    {
        /// <summary>
        /// Метод задает localScale по оси X.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scale"></param>
        public static void SetLocalScaleX (this Transform transform, float scale)
        {
            var vector = new Vector3 (scale, transform.localScale.y, transform.localScale.z);

            transform.localScale = vector;
        }


        /// <summary>
        /// Метод задает localScale по оси Y.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scale"></param>
        public static void SetLocalScaleY (this Transform transform, float scale)
        {
            var vector = new Vector3 (transform.localScale.x, scale, transform.localScale.z);

            transform.localScale = vector;
        }

        /// <summary>
        /// Метод задает localScale по оси Z.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scale"></param>
        public static void SetLocalScaleZ (this Transform transform, float scale)
        {
            var vector = new Vector3 (transform.localScale.x, transform.localScale.y, scale);

            transform.localScale = vector;
        }


        public static void deBug (this object obj)
        {
            Debug.Log (obj);
        }
    }
}