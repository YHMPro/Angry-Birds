using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class Camera2D : MonoBehaviour
    {
        private Camera m_Camera2D;
        private float m_XMin = 3;
        private float m_YMin = 4;
        private float m_XMax = 5;
        private float m_YMax = 5;
        private void Awake()
        {
            m_Camera2D = GetComponent<Camera>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }
        public void BindBird()
        {        
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyFixUpdateAction(Follow);
        }

        public void Follow()
        {
            if (GameLogic.NowComeBird == null)
                return;
            Vector3 aimPos = GameLogic.NowComeBird.transform.position;
            aimPos = Limit(aimPos);
            transform.position = Vector3.Lerp(transform.position, Limit(aimPos), 0.5f*Time.fixedDeltaTime);
        }
        public void SetLimit(float xMin,float xMax,float yMin,float yMax)
        {
            m_XMin = xMin;
            m_YMax = yMin;
            m_XMax = xMax;
            m_YMax = yMax;
        }
        private Vector3 Limit(Vector3 v)
        {
            Vector3 result = Vector3.zero;
            result.x = Mathf.Clamp(v.x, m_XMin, m_XMax);
            result.y = Mathf.Clamp(v.y, m_YMin, m_YMax);
            result.z = transform.position.z;
            return result;
        }
        public void BreakBird()
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveFixUpdateAction(Follow);
        }
        public Vector3 ScreenToWorldPoint(Vector3 vector3,float z=0)
        {
            Vector3 result= m_Camera2D.ScreenToWorldPoint(vector3);
            result.z = z;
            return result;
        }
    }
}
