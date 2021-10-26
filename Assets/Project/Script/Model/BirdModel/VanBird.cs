using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class VanBird : SkillBird
    {
        private Transform m_AimTran;
        protected override void Awake()
        {
            m_Config = NotMonoSingletonFactory<VanBirdConfig>.GetSingleton();
            base.Awake();
            m_AimTran = transform.Find("Aim");
        }
        protected override void Start()
        {
            base.Start();
            m_AimTran.gameObject.SetActive(false);
        }
        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(OnSkillUpdate_Common);//持续监听技能释放指令
        }

        protected override void OnBirdFlyUpdate()
        {
            base.OnBirdFlyUpdate();
            m_Rig2D.isKinematic = false;
        }

        protected override void OnSkillUpdate()
        {           
            m_AimTran.gameObject.SetActive(true);
            m_Rig2D.isKinematic = true;
            m_Rig2D.velocity = Vector2.zero;
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(AimControlUpdate);
        }
        /// <summary>
        /// 目标控制更新
        /// </summary>
        private void AimControlUpdate()
        {
            if (MonoSingletonFactory<Camera2D>.SingletonExist)
            {
                Vector3 movePos = MonoSingletonFactory<Camera2D>.GetSingleton().ScreenToWorldPoint(Input.mousePosition, transform.position.z);
                m_AimTran.position = movePos;
                if (Input.GetMouseButtonDown(0))
                {
                    PlaySkillAudio();
                    Vector3 dir = (movePos - transform.position).normalized;                   
                    m_Rig2D.velocity = dir * 15.0f;
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(AimControlUpdate);
                    m_AimTran.gameObject.SetActive(false);
                }
            }

        }
    }
}
