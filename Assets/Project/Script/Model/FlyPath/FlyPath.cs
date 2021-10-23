using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class FlyPath : MonoBehaviour
    {
        private int m_PathPointCount = 3;
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
                            go.SetActive(false);                          
                        }
                    }
                }
                return m_PointTrans;
            }
        }
        private  void Start()
        {       
            
        }
      
        /// <summary>
        /// 飞行路径活动状态
        /// </summary>
        /// <param name="active"></param>
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
        /// <summary>
        /// 设置飞行路径
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="timeInterval"></param>
        public void SetFlyPath(float startTime,float timeInterval)
        {
            if (GameLogic.NowComeBird == null)
                return;
            if (!m_Active)
                return;
            if(MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                for(int i=0;i< PointTrans.Length; i++)
                {
                    PointTrans[i].position = MonoSingletonFactory<SlingShot>.GetSingleton().CountPathPoint(startTime+timeInterval * i) + (Vector2)GameLogic.NowComeBird.transform.position;           
                }
            }
        }

    }
}
