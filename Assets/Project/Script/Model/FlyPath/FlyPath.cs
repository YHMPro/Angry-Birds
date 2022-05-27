using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    public class FlyPath : MonoSingletonBase<FlyPath>
    {
        private Coroutine m_Cor;
        private int m_PathPointCount = 3;
        private Transform[] m_PointTrans;
        private bool m_Active = false;


        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (m_Cor != null)
            {
                if (ShareMono.Exists)
                {
                    ShareMono.GetSingleton().StopCoroutine(m_Cor);
                }
                m_Cor = null;
            }
        }

        private Transform[] PointTrans
        {
            get
            {
                if(m_PointTrans==null)
                {
                    if (!OtherConfigInfo.Exists)
                    {
                        Debuger.Log("OtherConfigInfo未实例化");
                        return null;
                    }              
                    m_PointTrans = new Transform[m_PathPointCount];                 
                    for (int i = 0; i < m_PathPointCount; i++)
                    {
                        if (GoLoad.Take(OtherConfigInfo.GetSingleton().GetPointPrefabPath(), out GameObject go, transform))
                        {
                            m_PointTrans[i] = go.transform;
                            go.SetActive(false);
                        }
                    }
                }
                return m_PointTrans;
            }
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
                if (m_Active)
                {
                    if(m_Cor != null)
                    {
                        ShareMono.GetSingleton().StopCoroutine(m_Cor);
                        m_Cor = null;
                    }
                    foreach (var pointTran in PointTrans)
                    {
                        pointTran.gameObject.SetActive(m_Active);
                    }
                }
                else
                {
                    m_Cor = ShareMono.GetSingleton().DelayAction(2, () =>
                    {
                        foreach (var pointTran in PointTrans)
                        {
                            pointTran.gameObject.SetActive(m_Active);
                        }
                    });
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
            if(SlingShot.Exists)
            {
                for(int i=0;i< PointTrans.Length; i++)
                {
                    PointTrans[i].position = SlingShot.GetSingleton().CountPathPoint(startTime+timeInterval * i) + (Vector2)GameLogic.NowComeBird.transform.position;           
                }
            }
        }

    }
}
