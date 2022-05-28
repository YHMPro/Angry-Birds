using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DG.Tweening;
using Farme.Extend;
namespace Bird_VS_Boar
{
    public class Staff : MonoBase,IEquipBinding
    {
        private Coroutine m_Co;
        private CircleCollider2D m_CCo2D;
        private int FilckerFrequency = 2;
        private Animator m_StaffRippleAnim;
        private SpriteRenderer m_TipSpr;
        private float m_FillTime = 8;
        private float m_FadeTime = 3f;
        private bool m_FilckerState = true;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Animator>();
            RegisterComponentsTypes<SpriteRenderer>();
            m_CCo2D=GetComponent<CircleCollider2D>();
            m_TipSpr =GetComponent<SpriteRenderer>("Tip");
            m_StaffRippleAnim =GetComponent<Animator>("StaffRipple");
            m_TipSpr.color = Color.white;
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            FilckerTip();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_TipSpr.color = Color.white;
            m_TipSpr.DOPause();
            m_StaffRippleAnim.gameObject.SetActive(false);
            if(m_Co!=null)
            {
                if(ShareMono.Exists)
                {
                    ShareMono.GetSingleton().StopCoroutine(m_Co);
                }
            }
        }

        protected override void Start()
        {
            base.Start();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IWeaken weaken = collision.GetComponent<IWeaken>();
            if(weaken!=null)
            {
                weaken.Velocity *= 0.35f;
            }
            if (m_FilckerState)
            {
                m_FilckerState = false;
                m_StaffRippleAnim.gameObject.SetActive(true);
                ToColor_1_1_1_0();
            }
        }
        /// <summary>
        /// 填充
        /// </summary>
        private void ToColor_1_1_1_1()
        {
            m_StaffRippleAnim.gameObject.SetActive(false);
            m_CCo2D.enabled = false;
            //m_TipSpr.color = new Color(1, 1, 1, 0);
            //m_TipSpr.DOColor(new Color(1, 1, 1, 1), m_FillTime).OnComplete(FilckerTip);
            m_Co = null;
            m_Co = ShareMono.GetSingleton().DelayAction(m_FadeTime, FilckerTip);
        }
        /// <summary>
        /// 衰变
        /// </summary>
        private void ToColor_1_1_1_0()
        {
            //m_TipSpr.color = Color.white;
            //m_TipSpr.DOColor(new Color(1, 1, 1, 0), m_FadeTime).OnComplete(ToColor_1_1_1_1);
            m_Co = null;
            m_Co = ShareMono.GetSingleton().DelayAction(m_FadeTime, ToColor_1_1_1_1);
        }
        /// <summary>
        /// 闪烁
        /// </summary>
        private void FilckerTip()
        {
            m_FilckerState = true;
            m_CCo2D.enabled = true;
            m_TipSpr.color = Color.white;
            m_TipSpr.DOColor(new Color(1, 1, 1, 0),1.0f/FilckerFrequency).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        public void Binding(Transform bindingTarget)
        {      
            if (bindingTarget==null|| (!bindingTarget.gameObject.activeInHierarchy))
            {
                this.gameObject.Recycle(gameObject.name);
                return;
            }            
            transform.position = bindingTarget.position;
        }
    }
}
