using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class Bird : BaseMono
    {
        private bool m_IsCheck = false;
        protected float m_StretchDis =1f;
        protected CircleCollider2D m_CC2D = null;
        protected Rigidbody2D m_Rig2D = null;
        protected string[] rayCastGroup = new string[] { "Pig", "Barrier" };
        private bool m_IsHurt = false;
        protected override void Awake()
        {
            base.Awake();
            m_CC2D = GetComponent<CircleCollider2D>();
            m_Rig2D = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnMouseEnter()
        {
            
        }

        protected virtual void OnMouseDown()
        {
            m_IsCheck = true;
            if (MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                SlingShot slingShot = MonoSingletonFactory<SlingShot>.GetSingleton();
                slingShot.SJ2DEnable = false;
            }

        }

        protected virtual void OnMouseUp()
        {
            m_IsCheck = false;
            if (MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                SlingShot slingShot = MonoSingletonFactory<SlingShot>.GetSingleton();
                slingShot.BreakBird();//断开关联
                slingShot.ClearLine();//清除线
            }
        }

        protected virtual void OnMouseExit()
        {
            
        }

        
        public virtual void BirdControlUpdate()
        {
            if (m_IsCheck)//是否选中
            {
                if (MonoSingletonFactory<Camera2D>.SingletonExist)
                {
                    Vector3 movePos = MonoSingletonFactory<Camera2D>.GetSingleton().ScreenToWorldPoint(Input.mousePosition, transform.position.z);
                    if (MonoSingletonFactory<SlingShot>.SingletonExist)
                    {
                        SlingShot slingShot = MonoSingletonFactory<SlingShot>.GetSingleton();
                        Vector3 dir = movePos - slingShot.GetOriginGlobalPos(transform.position.z);                    
                        //限制拉伸的范围
                        transform.position = dir.magnitude <= slingShot.StretchDis ? movePos : dir.normalized * slingShot.StretchDis + slingShot.GetOriginGlobalPos(transform.position.z);
                        //渲染线
                        slingShot.RendererLine(transform.position, m_CC2D.radius);
                        //计算预瞄准点位置
                        if(MonoSingletonFactory<FlyPath>.SingletonExist)
                        {
                            MonoSingletonFactory<FlyPath>.GetSingleton().SetFlyPath(0.2f,0.2f);
                        }

                    }             
                }
            }
        }

        public virtual void BirdFlyUpdate()//用于处理鸟的飞行后的首次碰撞  
        {
            if (m_Rig2D.velocity.magnitude > 0.5f)
            {
                if (Physics2D.Raycast(transform.position, m_Rig2D.velocity.normalized, m_CC2D.radius + 0.1f, LayerMask.GetMask(rayCastGroup)))
                {

                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                }
            }
            else
            {               
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
            }
        }

        public void SetBirdRig2DVelocity(Vector2 velocity)
        {
            m_Rig2D.velocity = velocity;
        }
    }
}
