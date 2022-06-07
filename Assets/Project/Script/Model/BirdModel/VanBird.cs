using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class VanBird : SkillBird
    {
        private Transform m_AimTran;
        /// <summary>
        /// 重力缩放值
        /// </summary>
        private float m_GravityScale = 0;
        protected override void Awake()
        {
            m_BirdType = EnumBirdType.VanBird;
            //m_ConfigInfo = BirdConfigInfo.GetBirdConfigInfo<VanBirdConfigInfo>(m_BirdType);
            base.Awake();
            m_AimTran = transform.Find("Aim");
        }
        protected override void Start()
        {
            base.Start();
            m_AimTran.gameObject.SetActive(false);
            m_GravityScale = m_Rig2D.gravityScale;
        }      
        protected override void OnBirdFlyBreak()
        {
            base.OnBirdFlyBreak();
            m_Rig2D.gravityScale = m_GravityScale;
        }

        protected override void OnSkillUpdate()
        {           
            m_AimTran.gameObject.SetActive(true);
            m_Rig2D.gravityScale = 0;
            m_Rig2D.velocity = Vector2.zero;
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,AimControlUpdate);
        }
        /// <summary>
        /// 目标控制更新
        /// </summary>
        private void AimControlUpdate()
        {
            if (Camera2D.Exists)
            {
                Vector3 movePos = Camera2D.GetSingleton().ScreenToWorldPoint(Input.mousePosition, transform.position.z);
                m_AimTran.position = movePos;
                if (Input.GetMouseButtonDown(0))
                {                  
                    PlaySkillAudio();
                    Vector3 dir = (movePos - transform.position).normalized;                   
                    m_Rig2D.velocity = dir * 20f;
                    ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,AimControlUpdate);
                    m_AimTran.gameObject.SetActive(false);
                }
            }

        }
    }
}
