using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;

namespace Angry_Birds
{
    public class PinkBird : Bird
    {
        /// <summary>
        /// 气泡数量
        /// </summary>
        private int m_BlisterCount = 3;
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
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
                    m_IsHurt = true;
                    m_TRenderer.enabled = false;//关闭拖尾  
                    m_Anim.SetTrigger("IsSkill");//播放技能动画                                                             
                }
            }
            else
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }

        public void ProductBlister()
        {
            for(int index=0;index< m_BlisterCount;index++)
            {
                if(GoLoad.Take(GameConfigInfo.BlisterPath,out GameObject go))
                {
                    if(index==0)
                    {
                        go.transform.position = transform.position;
                    }
                    else
                    {
                        Vector3 pos = transform.position;
                        if (index % 2 == 0)
                        {
                            pos.x -= 0.5f;
                        }
                        else
                        {
                            pos.x += 0.5f;
                        }
                        go.transform.position = pos;
                    }                       
                }
            }
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(0.1f, () =>
            {
                Destroy(gameObject);
            });
        }
        protected override void SkillUpdate()
        {
            base.SkillUpdate();
            if (IsReleaseSkill)
            {
                m_TRenderer.enabled = false;//关闭拖尾            
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
            }
        }
    }
}
