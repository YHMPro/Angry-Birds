using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class BlackBird : Bird
    {
        [SerializeField]
        private List<GameObject> m_DestroyGoLi = null;
        protected override void Awake()
        {
            m_DestroyGoLi = new List<GameObject>();
            m_ConfigInfo = NotMonoSingletonFactory<BlackBirdConfigInfo>.GetSingleton();
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
                m_Anim.SetTrigger("IsSkill");
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
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
                    PlayCrashAudio();//播放碰撞音效
                    m_Anim.SetTrigger("IsSkill");//播放技能动画
                    m_TRenderer.enabled = false;//关闭拖尾            
                   
                }
            }
            else
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }
        /// <summary>
        /// 销毁检测到的Go
        /// </summary>
        public void DestroyCheckGo()
        {
           PlaySkillAudio();
           while(m_DestroyGoLi.Count>0)
            {
                GameObject go = m_DestroyGoLi[m_DestroyGoLi.Count - 1];
                m_DestroyGoLi.RemoveAt(m_DestroyGoLi.Count-1);
                Destroy(go);
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer==9|| collision.gameObject.layer==10)
            {
                if(!m_DestroyGoLi.Contains(collision.gameObject))
                {
                    m_DestroyGoLi.Add(collision.gameObject);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
            {
                if (m_DestroyGoLi.Contains(collision.gameObject))
                {
                    m_DestroyGoLi.Remove(collision.gameObject);
                }
            }
        }
    }
}
