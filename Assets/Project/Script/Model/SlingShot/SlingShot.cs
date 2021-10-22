using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    /// <summary>
    /// 弹弓
    /// </summary>
    public class SlingShot : BaseMono
    {
        private Bird nowBindBird = null;
        private SpringJoint2D m_SJ2D;

        private float m_ApplyingMaxSpeed = 15;
        public float StretchDis
        {
            get { return 1; }
        }
        public bool SJ2DEnable
        {
            set
            {
                m_SJ2D.enabled = value;
            }
            get
            {
                return m_SJ2D.enabled;
            }
        }

        public Vector2 ApplyingVelocity
        {
            get
            {
                if(nowBindBird!=null)
                {
                    Vector2 birdToSelfDir = GetOriginGlobalPos(nowBindBird.transform.position.z) - nowBindBird.transform.position;
                    return birdToSelfDir.normalized * birdToSelfDir.magnitude / StretchDis * m_ApplyingMaxSpeed;
                }
                return Vector2.zero;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<LineRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            if (GetComponent("LeftRendererLine", out LineRenderer leftLR))
            {
                leftLR.positionCount = 0;
                leftLR.startWidth = 0.2f;
                leftLR.endWidth = 0.1f;
            }
            if (GetComponent("RightRendererLine", out LineRenderer rightLR))
            {
                rightLR.positionCount = 0;
                rightLR.startWidth = 0.2f;
                rightLR.endWidth = 0.1f;
            }
            m_SJ2D = GetComponent<SpringJoint2D>();
        }

        public void RendererLine(Vector3 aimPoint,float radius)
        {
            aimPoint = aimPoint - GetOriginGlobalPos(aimPoint.z);
            aimPoint = GetOriginGlobalPos(aimPoint.z) + aimPoint.normalized * (aimPoint.magnitude + radius / 2.0f);
            if (GetComponent("LeftRendererLine",out LineRenderer leftLR))
            {
                if(leftLR.positionCount!=2)
                {
                    leftLR.positionCount = 2;
                }
                leftLR.SetPosition(0, leftLR.transform.position);
                leftLR.SetPosition(1, aimPoint);
            }
            if(GetComponent("RightRendererLine", out LineRenderer rightLR))
            {
                if (rightLR.positionCount != 2)
                {
                    rightLR.positionCount = 2;
                }
                rightLR.SetPosition(0, rightLR.transform.position);
                rightLR.SetPosition(1, aimPoint);
            }
        }
        public void ClearLine()
        {
            if (GetComponent("LeftRendererLine", out LineRenderer leftLR))
            {
                leftLR.positionCount = 0;
            }
            if (GetComponent("RightRendererLine", out LineRenderer rightLR))
            {
                rightLR.positionCount = 0;
            }
        }
        public void BindBird(Bird bird)
        {
            nowBindBird = bird;
            if (MonoSingletonFactory<FlyPath>.SingletonExist)
            {
                MonoSingletonFactory<FlyPath>.GetSingleton().ActiveFlyPath(true);
            }
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(nowBindBird.BirdControlUpdate);

        }

        public void BreakBird()
        {
            if (nowBindBird == null)
                return;          
            nowBindBird.SetBirdRig2DVelocity(ApplyingVelocity);
            //计算预瞄准点位置
            if (MonoSingletonFactory<FlyPath>.SingletonExist)
            {
                FlyPath flyPath = MonoSingletonFactory<FlyPath>.GetSingleton();
                flyPath.SetFlyPath(0.2f,0.2f);
                MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(2, () =>
                 {
                     if(flyPath!=null)
                     {
                         flyPath.BreakBird();
                         flyPath.ActiveFlyPath(false);
                     }
                 });
            }       
        }

        public Vector3 GetOriginGlobalPos(float z)
        {
            Vector3 origin = transform.position + (Vector3)m_SJ2D.anchor;
            origin.z = z;
            return origin;
        }

        public Vector2 CountPathPoint(float time)
        {
            Vector2 v = ApplyingVelocity;
            Vector2 pos = Vector2.zero;
            pos.x = v.x * time;
            pos.y = v.y * time + 0.5f * Physics2D.gravity.y * time * time;
            return pos;
        }
        
    }
}
