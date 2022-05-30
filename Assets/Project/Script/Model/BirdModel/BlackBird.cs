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
        private List<Rigidbody2D> m_BlownTarget = new List<Rigidbody2D>();
        /// <summary>
        /// 冲击波大小
        /// </summary>
        private float m_ShockwareSize = 5f;
        private float m_ExpandSize = 15f;
        private float m_RadiusMax = 1.5f;
        private float m_RadiusMin = 0.3f;
        [SerializeField]
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
            m_CFilter.SetLayerMask(LayerMask.GetMask(new string[] { "Barrier", "Pig" }));
            m_CPoints = new ContactPoint2D[60];
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (ShareMono.Exists)
            {
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ReleaseShockware);
            }
            m_Sr.enabled = true;//显示
            m_CC2D.radius = m_RadiusMin;
        }
        protected override void OnBirdFlyBreak()
        {
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.OnSkillUpdate_Common);
            m_Anim.SetTrigger("IsSkill");//播放技能动画
            m_Cor = ShareMono.GetSingleton().DelayAction(m_Anim.AnimatorClipTimeLength("BlackBirdSkill"), ()=> 
            {               
                OnSkillUpdate();                      
            });
        }
        public override void OnSkillUpdate_Common()
        {
            base.OnSkillUpdate_Common();
        }
        protected override void OnSkillUpdate()
        {
            m_IsCollision = true;//防止扩张过程中产生的碰撞导致碰撞相关的逻辑执行
            m_Sr.enabled = false;//隐藏
            PlaySkillAudio();//播放技能音效
            OpenBoom();//播放爆炸特效
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.ReleaseShockware);            
        }  
        /// <summary>
        /// 释放冲击波
        /// </summary>
        private void ReleaseShockware()
        {
            int length = m_Rig2D.GetContacts(m_CFilter,m_CPoints);
            //Debug.Log("冲击波对象数量:" + length);
            Vector2 dir;
            Rigidbody2D rig2D;
            for (int i=0;i<length;i++)
            {
                rig2D = m_CPoints[i].rigidbody;
                if(!m_BlownTarget.Contains(rig2D))
                {
                    m_BlownTarget.Add(rig2D);
                    dir = (m_CPoints[i].point - (Vector2)transform.position).normalized;
                    rig2D.AddForceAtPosition(m_ShockwareSize * dir, m_CPoints[i].point, ForceMode2D.Impulse);
                }               
            }
            float radius = m_CC2D.radius;
            radius += Time.deltaTime * m_ExpandSize;
            m_CC2D.radius = Mathf.Clamp(radius, m_RadiusMin, m_RadiusMax);
            if (m_CC2D.radius == m_RadiusMax)
            {
                m_BlownTarget.Clear();
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.ReleaseShockware);
                Died();//小鸟死亡          
            }

        }

        #region Boom
        public override void OpenBoom()
        {
            Boom.OpenBoom(EnumBoomType.BirdExplodeBoom, transform.position);
        }
        #endregion
        /// <summary>
        /// 销毁检测到的Go
        /// </summary>
        //public void DestroyCheckGo()
        //{
        //    while (DestroyGoLi.Count > 0)
        //    {
        //        GameObject go = DestroyGoLi[DestroyGoLi.Count - 1];   
        //        //IBoom boom = go.GetComponent<IBoom>();
        //        //if(boom!=null)
        //        //{
        //        //    boom.OpenBoom();
        //        //}
        //        //IScore score = go.GetComponent<IScore>();
        //        //if(score!=null)
        //        //{
        //        //    score.OpenScore();
        //        //}
        //        //DestroyGoLi.RemoveAt(DestroyGoLi.Count - 1);
        //        //IDiedAudio diedAudio = go.GetComponent<IDiedAudio>();
        //        //if (diedAudio != null)
        //        //{
        //        //    diedAudio.DiedAudio();
        //        //}
        //        //IDied died=go.GetComponent<IDied>();
        //        //if(died!=null)
        //        //{
        //        //    died.Died();
        //        //}        
        //    }
        //}

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        //    {
        //        if (!DestroyGoLi.Contains(collision.gameObject))
        //        {
        //            DestroyGoLi.Add(collision.gameObject);
        //        }
        //    }
        //}
        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        //    {
        //        if (DestroyGoLi.Contains(collision.gameObject))
        //        {
        //            DestroyGoLi.Remove(collision.gameObject);
        //        }
        //    }
        //}
    }
}
