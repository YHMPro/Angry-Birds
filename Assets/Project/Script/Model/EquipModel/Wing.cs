using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DG.Tweening;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 翅膀
    /// </summary>
    public class Wing : MonoBase
    {
        /// <summary>
        /// 移动距离
        /// </summary>
        [SerializeField]
        private float m_MoveDistance = 10;
        /// <summary>
        /// 缓动类型
        /// </summary>
        [SerializeField]
        private Ease m_Ease;
        /// <summary>
        /// 左翅膀
        /// </summary>
        private Transform LeftWingTran;
        /// <summary>
        /// 右翅膀
        /// </summary>
        private Transform RightWingTran;
        /// <summary>
        /// 动画状态机
        /// </summary>
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
            transform.DOMoveY(m_MoveDistance, 5).SetLoops(-1, LoopType.Yoyo).SetEase(m_Ease);
        }      
        /// <summary>
        /// 运动更新
        /// </summary>
        private void SportUpdate()
        {
            if(m_BindTarget == null)
            {
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.SportUpdate);
            }
            else
            {
                m_BindTarget.position = (LeftWingTran.position + RightWingTran.position) / 2f;
            }
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
                ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.SportUpdate);
            }
        }
        [SerializeField]
        /// <summary>
        /// 设置翅膀轨迹更新
        /// </summary>
        /// <param name="isUpdate">是否更新(0:停止  1:开始)</param>
        private void SetWingTrackUpdate(int isUpdate)
        {
            switch(isUpdate)
            {
                case 0:
                    {
                        transform.DOPause();
                        break;
                    }
                case 1:
                    {
                        transform.DOPlay();                    
                        break;
                    }
            } 
        }
    }
}
