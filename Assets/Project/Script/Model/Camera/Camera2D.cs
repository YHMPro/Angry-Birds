using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Angry_Birds
{
    public class Camera2D : MonoBehaviour
    {
        private Camera m_Camera2D;
        private void Awake()
        {
            m_Camera2D = GetComponent<Camera>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector3 ScreenToWorldPoint(Vector3 vector3,float z=0)
        {
            Vector3 result= m_Camera2D.ScreenToWorldPoint(vector3);
            result.z = z;
            return result;
        }
    }
}
