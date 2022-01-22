using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Angry_Birds;
using Farme;
using Farme.UI;

namespace Bird_VS_Boar
{
    public abstract class SkillBird : Bird
    {
        protected override void OnMouseUp()
        {
            if (!m_IsCheck)
            {
                return;
            }
            base.OnMouseUp();         
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,this.OnSkillUpdate_Common);//持续监听技能释放指令
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.OnSkillUpdate_Common);
        }
        public override void OnSkillUpdate_Common()
        {
            if (!m_IsReleaseSkill)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    m_IsReleaseSkill = true;
                    OnSkillUpdate();
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,this.OnSkillUpdate_Common);
                }
            }
        }

        protected override void OnSkillUpdate()
        {         
            m_Anim.SetTrigger("IsSkill");
        }

        protected override void OnBirdFlyBreak()
        {         
            base.OnBirdFlyBreak();
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,this. OnSkillUpdate_Common);
        }
        protected override void PlaySkillAudio()
        {
            if (!BirdConfigInfo.BirdConfigInfoDic.TryGetValue(m_BirdType, out var config))
            {
                return;
            }
            PlayAudio(config.GetSkillAudioPaths());
        }
    }
}
