using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class FlyPath : MonoBehaviour
    {

        private Bird nowBindBird = null;
        private int m_PathPointCount = 5;
        private Transform[] m_PointTrans;
        private bool m_Active = false;

        private Transform[] PointTrans
        {
            get
            {
                if(m_PointTrans==null)
                {
                    m_PointTrans = new Transform[m_PathPointCount];
                    for (int i = 0; i < m_PathPointCount; i++)
                    {
                        if (GoLoad.Take("Prefabs/Point", out GameObject go, transform))
                        {
                            m_PointTrans[i] = go.transform;
                            go.SetActive(m_Active);
                        }
                    }
                }
                return m_PointTrans;
            }
        }
        private  void Start()
        {       
            
        }
        public void BindBird(Bird bird)
        {
            nowBindBird = bird;
        }

        public void BreakBird()
        {
            nowBindBird = null;
        }

        public void ActiveFlyPath(bool active)
        {
            if(m_Active!=active)
            {
                m_Active = active;
                foreach(var pointTran in PointTrans)
                {
                    pointTran.gameObject.SetActive(m_Active);
                }
            }
           

        }
        public void SetFlyPath(float startTime,float timeInterval)
        {
            if (nowBindBird == null)
                return;
            if (!m_Active)
                return;
            if(MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                for(int i=0;i< PointTrans.Length; i++)
                {
                    PointTrans[i].position = MonoSingletonFactory<SlingShot>.GetSingleton().CountPathPoint(startTime+timeInterval * i) + (Vector2)nowBindBird.transform.position;           
                }
            }
        }

    }
}
