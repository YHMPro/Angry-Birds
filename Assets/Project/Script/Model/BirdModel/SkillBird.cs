using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Angry_Birds;
using Farme;

namespace Bird_VS_Boar
{
    public abstract class SkillBird : Bird
    {


        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(OnSkillUpdate_Common);//持续监听技能释放指令
        }

        public override void OnSkillUpdate_Common()
        {
            if (!m_IsReleaseSkill)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    m_IsReleaseSkill = true;
                    OnSkillUpdate();                  
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(OnSkillUpdate_Common);
                }
            }
        }

        protected override void OnSkillUpdate()
        {         
            m_Anim.SetTrigger("IsSkill");
        }

        
        protected override void PlaySkillAudio()
        {
            PlayBirdAudio(m_ACs[0], (m_Config as SkillBirdConfig).GetSkillAudioPath());
        }

    }
}
