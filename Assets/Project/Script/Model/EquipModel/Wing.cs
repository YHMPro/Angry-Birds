using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 翅膀
    /// </summary>
    public class Wing : BaseMono
    {
        private Transform LeftWingTran;
        private Transform RightWingTran;
        private Animator m_Anim = null;
        [SerializeField]
        /// <summary>
        /// 绑定的目标
        /// </summary>
        private Transform m_BindTarget = null;
        protected override void Awake()
        {
            base.Awake();
            m_Anim = GetComponent<Animator>();
            LeftWingTran = transform.GetChild(0);
            RightWingTran = transform.GetChild(1);
        }

        protected override void Start()
        {
            base.Start();
           
        }

        private void Update()
        {
            SportUpdate();
        }
        /// <summary>
        /// 运动更新
        /// </summary>
        private void SportUpdate()
        {
            if(m_BindTarget == null)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.SportUpdate);
            }
            else
            {
                WingTrack();
                m_BindTarget.position = (LeftWingTran.position + RightWingTran.position) / 2f;
            }
        }
        /// <summary>
        /// 翅膀轨迹
        /// </summary>
        private void WingTrack()
        {

        }
        /// <summary>
        /// 设置绑定目标
        /// </summary>
        /// <param name="target">目标</param>
        public void SetBindTarget(Transform target)
        {           
            if(Equals(m_BindTarget,target))
            {
                return;
            }           
            m_BindTarget = target;
            if (target == null)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.SportUpdate);
            }
        }


    }
}
