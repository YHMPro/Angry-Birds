using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;

namespace Angry_Birds
{
    public class PinkBird : Bird
    {
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<PinkBirdConfigInfo>.GetSingleton();
            base.Awake();
        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(SkillUpdate);//持续监听技能释放指令
        }
        public override void BirdFlyUpdate()
        {
            //小鸟面朝飞行方向
            transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(m_Rig2D.velocity.normalized, Vector2.right));
            if (m_Rig2D.velocity.magnitude > 0.5f)
            {
                if (Physics2D.OverlapCircle(transform.position, m_CC2D.radius + 0.15f, LayerMask.GetMask(rayCastGroup)))
                {
                    m_IsHurt = true;
                    ProductBlister();
                    //m_Anim.SetTrigger("IsSkill");//播放技能动画
                    m_TRenderer.enabled = false;//关闭拖尾            
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
                }
            }
            else
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }

        private void ProductBlister()
        {

        }

        protected override void SkillUpdate()
        {
            base.SkillUpdate();
            if (IsReleaseSkill)
            {
                ProductBlister();
                //m_Anim.SetTrigger("IsSkill");
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }
    }
}
