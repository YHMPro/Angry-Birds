using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Extend;
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
            m_BirdType = EnumBirdType.BlackBird;
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            //m_IsAbleBindBirdNets = true;
        }
     

        protected override void OnBirdFlyBreak()
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, OnSkillUpdate_Common);
            m_Anim.SetTrigger("IsSkill");//播放技能动画
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(m_Anim.AnimatorClipTimeLength("BlackBirdSkill"), ()=> 
            {
                OnSkillUpdate();                      
            });
        }

        protected override void OnSkillUpdate()
        {
          
            PlaySkillAudio();//播放技能音效
            DestroyCheckGo();
            GoReusePool.Put(GetType().Name, gameObject);//回收小鸟
        }          
        /// <summary>
        /// 销毁检测到的Go
        /// </summary>
        public void DestroyCheckGo()
        {
            while (DestroyGoLi.Count > 0)
            {
                GameObject go = DestroyGoLi[DestroyGoLi.Count - 1];   
                IBoom boom = go.GetComponent<IBoom>();
                if(boom!=null)
                {
                    boom.OpenBoom();
                }
                IScore score = go.GetComponent<IScore>();
                if(score!=null)
                {
                    score.OpenScore();
                }
                DestroyGoLi.RemoveAt(DestroyGoLi.Count - 1);
                Destroy(go);
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
