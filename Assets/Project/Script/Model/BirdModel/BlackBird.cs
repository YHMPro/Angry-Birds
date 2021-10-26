using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class BlackBird : SkillBird
    {
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
        protected override void Awake()
        {
            m_Config = NotMonoSingletonFactory<BlackBirdConfig>.GetSingleton();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            //m_IsAbleBindBirdNets = true;
        }
     

        protected override void OnBirdFlyUpdate()
        {
            m_Anim.SetTrigger("IsSkill");//播放技能动画
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(m_Anim.AnimatorClipTimeLength("BirdBlackSkill"), ()=> 
            {
                DestroyCheckGo();
                OnSkillUpdate();
            });
        }

        protected override void OnSkillUpdate()
        {
            PlaySkillAudio();//播放技能音效
            GoReusePool.Put(GetType().Name, gameObject);//回收小鸟
            GameLogic.NowComeBird = null;//断开引用
        }          
        /// <summary>
        /// 销毁检测到的Go
        /// </summary>
        public void DestroyCheckGo()
        {
            while (m_DestroyGoLi.Count > 0)
            {
                GameObject go = m_DestroyGoLi[m_DestroyGoLi.Count - 1];
                m_DestroyGoLi.RemoveAt(m_DestroyGoLi.Count - 1);
                Destroy(go);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
            {
                if (!m_DestroyGoLi.Contains(collision.gameObject))
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
