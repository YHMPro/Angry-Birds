

using UnityEngine;
using System;
namespace Bird_VS_Boar
{
    [Serializable]
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
        public void SetValue(Vector3 v3)
        {
            x=v3.x; y=v3.y; z=v3.z;
        }       
    }
}