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
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,this.OnSkillUpdate_Common);//持续监听技能释放指令
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.OnSkillUpdate_Common);
        }
        public override void OnSkillUpdate_Common()
        {
            if (!m_IsReleaseSkill)
            {                
                if (Input.GetMouseButtonDown(0))
                {
                    if (!WindowRoot.Exists)
                    {
                        return;
                    }
                    WindowRoot windowRoot = WindowRoot.GetSingleton();
                    if (windowRoot.ES.IsPointerOverGameObject())//当操作对象是UI时则屏蔽此次事件响应
                    {
                        return;
                    }
                    m_IsReleaseSkill = true;
                    OnSkillUpdate();
                    ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,this.OnSkillUpdate_Common);
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
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,this. OnSkillUpdate_Common);
        }
        protected override void PlaySkillAudio()
        {        
            PlayAudio(m_ConfigInfo.GetSkillAudioPaths());
        }
    }
}
