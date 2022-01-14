

using UnityEngine;

namespace Bird_VS_Boar
{
    public struct CustomVector3
    {
        public float x;
        public float y;
        public float z;
        public CustomVector3(float x,float y,float z)
        {
            this.x = x; this.y = y; this.z = z;
        }
        public Vector3 ToVector3()
        {
            return new Vector3(x,y,z);
        }
    }
}