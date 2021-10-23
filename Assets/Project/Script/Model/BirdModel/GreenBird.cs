using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class GreenBird : Bird
    {
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<GreenBirdConfigInfo>.GetSingleton();
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
                m_Rig2D.velocity = new Vector2(-m_Rig2D.velocity.x, m_Rig2D.velocity.y);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }
    }
}
