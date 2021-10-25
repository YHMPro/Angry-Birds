using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class WriteBird : Bird
    {
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<WriteBirdConfigInfo>.GetSingleton();
            base.Awake();
        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(SkillUpdate);//持续监听技能释放指令
        }
        
        protected override void SkillUpdate()
        {
            base.SkillUpdate();
            if (IsReleaseSkill)
            {
                m_Rig2D.isKinematic = true;
                m_Rig2D.velocity = Vector2.zero;
                MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(ProductEggUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }
        private float m_Interval = 10;
        private float m_Time = 0;
        private void ProductEggUpdate()
        {
            if(Time.time>= m_Time)
            {
                if(GoLoad.Take(GameConfigInfo.EggPath,out GameObject go))
                {
                    m_Anim.SetTrigger("IsSkill");
                    PlaySkillAudio();
                    go.transform.position = transform.position;
                }
                m_Time = Time.time + m_Interval;
            }
        }

        protected override void PlaySkillAudio()
        {
            GameAudio.PlayEffectAudio(m_ConfigInfo.GetSkillAudioPath(true));
        }
    }
}
