using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
using Bird_VS_Boar.LevelConfig;

namespace Bird_VS_Boar
{
    public class Camera2D : MonoSingletonBase<Camera2D>
    {
        private Camera m_Camera2D;
        private float m_XMin = 3;
        private float m_YMin = 4;
        private float m_XMax = 5;
        private float m_YMax = 4;
        protected override void Awake()
        {
            base.Awake();
            m_Camera2D = GetComponent<Camera>();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            //读取关卡配置信息来设置位置
            Debuger.Log("读取关卡配置信息来设置位置");
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + GameManager.NowLevelIndex);
            if (levelConfig == null)
            {
                Debuger.LogError("不存在此场景的配置");
                return;
            }
            transform.position = levelConfig.Camera2DPosition.ToVector3();
        }
        // Start is called before the first frame update
        protected override  void Start()
        {
            base.Start();
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Late, Follow);//暂时放在这里
            SetLimit(6f, 15, 4.5f, 6.5f);
        }

        protected override void OnDestroy()
        {
            if (ShareMono.Exists)
            {
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Late, Follow);//暂时放在这里
            }
        }
        public void BindBird()
        {        
            //MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Fixed,Follow);//待优化
        }

        public void Follow()
        {
            Vector3 aimPos;
            if (GameManager.NowCameraFollowTarget == null)
            {
                //return;
                aimPos = SlingShot.GetSingleton().transform.position;
            }
            else
            {
                aimPos = GameManager.NowCameraFollowTarget.transform.position;
            }
           
            aimPos = Limit(aimPos);
            transform.position = Vector3.Lerp(transform.position, Limit(aimPos),1.5f*Time.fixedDeltaTime);
        }
        public void SetLimit(float xMin,float xMax,float yMin,float yMax)
        {
            m_XMin = xMin;
            m_XMax= xMax;
            m_YMin = yMin;
            m_YMax = yMax;         
        }
        private Vector3 Limit(Vector3 v)
        {
            Vector3 result = Vector3.zero;
            result.x = Mathf.Clamp(v.x, m_XMin, m_XMax);
            result.y = Mathf.Clamp(v.y, m_YMin, m_YMax);
            result.z = transform.position.z;
            return result;
        }
        public void BreakBird()
        {
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Late, Follow);
        }
        public Vector3 ScreenToWorldPoint(Vector3 vector3,float z=0)
        {
            Vector3 result= m_Camera2D.ScreenToWorldPoint(vector3);
            result.z = z;
            return result;
        }
    }
}
