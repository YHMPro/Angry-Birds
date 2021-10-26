using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;

namespace Bird_VS_Boar
{
    public class PinkBird : SkillBird
    {
        /// <summary>
        /// 气泡数量
        /// </summary>
        private int m_BlisterCount = 3;
        /// <summary>
        /// 气泡间距
        /// </summary>
        private float m_IntervalDistance = 1.25f;
        protected override void Awake()
        {
            m_Config = NotMonoSingletonFactory<PinkBirdConfig>.GetSingleton();
            base.Awake();
        }

        protected override void OnBirdFlyUpdate()
        {
            OnSkillUpdate();
            PlaySkillAudio();
        }
       
        /// <summary>
        /// 生成气球
        /// </summary>
        public void ProductBlister()
        {
            for (int index = 0; index < m_BlisterCount; index++)
            {
                GameObject go;
                if(!GoReusePool.Take(typeof(Blister).Name,out go))
                {
                    if (!GoLoad.Take((m_Config as PinkBirdConfig).BlisterPath, out go))
                    {
                        return;
                    }
                }
                if (index == 0)
                {
                    go.transform.position = transform.position;
                }
                else
                {
                    Vector3 pos = transform.position;
                    if (index % 2 == 0)
                    {
                        pos.x -= m_IntervalDistance;
                    }
                    else
                    {
                        pos.x += m_IntervalDistance;
                    }
                    go.transform.position = pos;
                }
               
            }
            PlaySkillAudio();//播放技能音效
            GoReusePool.Put(GetType().Name, gameObject);//回收小鸟
            GameLogic.NowComeBird = null;//断开引用
        }

        protected override void OnSkillUpdate()
        {         
            ProductBlister();
        }
        
    }
}
