using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
using System;
namespace Bird_VS_Boar
{
    public class BlackBird : SkillBird
    {
        /// <summary>
        /// 冲击波大小
        /// </summary>
        private float m_ShockwareSize = 3;
        [SerializeField]
        private List<GameObject> m_DestroyGoLi = null;
        private List<GameObject>  DestroyGoLi
        {
            get
            {
                if(m_DestroyGoLi==null)
                {
                    m_DestroyGoLi = new List<GameObject>();
                }
                return m_DestroyGoLi;
            }
        }
        private ContactFilter2D m_CFilter;
        private ContactPoint2D[] m_CPoints;
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.BlackBird;
            base.Awake();
            m_CFilter = new ContactFilter2D();
        }

        protected override void Start()
        {
            base.Start();
            m_CFilter.SetLayerMask(LayerMask.NameToLayer("Barrier"));
            m_CPoints = new ContactPoint2D[10];
        }
     
        protected override void OnBirdFlyBreak()
        {
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.OnSkillUpdate_Common);
            m_Anim.SetTrigger("IsSkill");//播放技能动画
            m_Cor = ShareMono.GetSingleton().DelayAction(m_Anim.AnimatorClipTimeLength("BlackBirdSkill"), ()=> 
            {
                ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.ReleaseShockware);
                OnSkillUpdate();                      
            });
        }
        public override void OnSkillUpdate_Common()
        {
            base.OnSkillUpdate_Common();
        }
        protected override void OnSkillUpdate()
        {
            PlaySkillAudio();//播放技能音效
            DestroyCheckGo();
            m_Cor = ShareMono.GetSingleton().DelayAction(m_Anim.AnimatorClipTimeLength("Shockware"), () =>
            {
                //Died();//小鸟死亡
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ReleaseShockware);
            });
        }  
        /// <summary>
        /// 释放冲击波
        /// </summary>
        private void ReleaseShockware()
        {
            int length = m_Rig2D.GetContacts(m_CFilter,m_CPoints);             
            Vector2 dir;
            for(int i=0;i<length;i++)
            {
                Rigidbody2D rig2D = m_CPoints[i].rigidbody;
                dir=(m_CPoints[i].point-(Vector2)transform.position).normalized;
                rig2D.AddForceAtPosition(m_ShockwareSize * dir, m_CPoints[i].point, ForceMode2D.Impulse);
            }

        }
        /// <summary>
        /// 销毁检测到的Go
        /// </summary>
        public void DestroyCheckGo()
        {
            while (DestroyGoLi.Count > 0)
            {
                GameObject go = DestroyGoLi[DestroyGoLi.Count - 1];   
                //IBoom boom = go.GetComponent<IBoom>();
                //if(boom!=null)
                //{
                //    boom.OpenBoom();
                //}
                //IScore score = go.GetComponent<IScore>();
                //if(score!=null)
                //{
                //    score.OpenScore();
                //}
                //DestroyGoLi.RemoveAt(DestroyGoLi.Count - 1);
                //IDiedAudio diedAudio = go.GetComponent<IDiedAudio>();
                //if (diedAudio != null)
                //{
                //    diedAudio.DiedAudio();
                //}
                //IDied died=go.GetComponent<IDied>();
                //if(died!=null)
                //{
                //    died.Died();
                //}        
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
            {
                if (!DestroyGoLi.Contains(collision.gameObject))
                {
                    DestroyGoLi.Add(collision.gameObject);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
            {
                if (DestroyGoLi.Contains(collision.gameObject))
                {
                    DestroyGoLi.Remove(collision.gameObject);
                }
            }
        }
    }
}
