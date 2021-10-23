using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class YellowBird : Bird
    {
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<YellowBirdConfigInfo>.GetSingleton();
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
            if(IsReleaseSkill)
            {
                m_Rig2D.velocity *= 2;//2倍速度
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }
    }
}
