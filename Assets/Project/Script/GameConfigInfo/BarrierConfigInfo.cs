using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class BarrierConfigInfo
    {
        /// <summary>
        /// 障碍物配置信息
        /// </summary>
        private static Dictionary<EnumBarrierType, BarrierConfigInfo> m_BarrierConfigInfoDic = new Dictionary<EnumBarrierType, BarrierConfigInfo>();
        /// <summary>
        /// 障碍物配置信息
        /// </summary>
        private static T GetBarrierConfigInfo<T>(EnumBarrierType barrierType) where T : BarrierConfigInfo,new()
        {
            if(!m_BarrierConfigInfoDic.TryGetValue(barrierType,out BarrierConfigInfo info))
            {
                info=new T();
                info.InitConfigInfo();
                m_BarrierConfigInfoDic.Add(barrierType, info);
            }
            return (T)info;
        }

        public static BarrierConfigInfo GetBarrierConfigInfo(EnumBarrierType barrierType)
        {
            BarrierConfigInfo barrierConfigInfo = null;
            switch(barrierType)
            {
                case EnumBarrierType.Ice:
                    {
                        barrierConfigInfo = GetBarrierConfigInfo<IceConfigInfo>(barrierType);
                        break;
                    }
                case EnumBarrierType.Wood:
                    {
                        barrierConfigInfo = GetBarrierConfigInfo<WoodConfigInfo>(barrierType);
                        break;
                    }
                case EnumBarrierType.Rock:
                    {
                        barrierConfigInfo = GetBarrierConfigInfo<RockConfigInfo>(barrierType);
                        break;
                    }
            }
            return barrierConfigInfo;
        }
        /// <summary>
        /// 质量
        /// </summary>
        protected float m_Mass = 0;
        /// <summary>
        /// 质量
        /// </summary>
        public float Mass => m_Mass;
        /// <summary>
        /// 排序层级
        /// </summary>
        protected int m_OrderInLayer = 0;
        /// <summary>
        /// 排序层级
        /// </summary>
        public int OrderInLayer
        {
            get { return m_OrderInLayer; }
        }
        /// <summary>
        /// 共同
        /// </summary>
        protected string m_CommonPath;
        /// <summary>
        /// 障碍物破碎音效路径数组
        /// </summary>
        protected string[] m_BarrierBrokenAudioPaths;
        /// <summary>
        /// 障碍物预制路径
        /// </summary>
        protected string m_BarrierPrefabPath;
        /// <summary>
        /// 障碍物销毁音效路径
        /// </summary>
        protected string m_BarrierDestroyAudioPath;
        public virtual void InitConfigInfo()
        {    
            m_CommonPath = "BarrierConfigInfo/" + GetType().Name + "/";
            m_BarrierPrefabPath = "Barrier";
            m_BarrierDestroyAudioPath = m_CommonPath+ "De1";
            m_OrderInLayer = 11;
        }
        public string GetBarrierDestroyAudioPath()
        {
            return m_BarrierDestroyAudioPath;
        }
        public string GetBarrierPrefabPath(EnumBarrierShapeType barrierShape)
        {

            return m_CommonPath + barrierShape + m_BarrierPrefabPath;
        }
        public string GetBarrierBrokenAudioPath(int index)
        {
            if(m_BarrierBrokenAudioPaths==null)
            {
                return null;
            }
            return m_CommonPath + m_BarrierBrokenAudioPaths[Mathf.Clamp(index,0, m_BarrierBrokenAudioPaths.Length-1)];
        }

        /// <summary>
        /// 创建组
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="length">长度</param>
        protected string[] CreateGroup(string prefix, int length)
        {
            string[] group = new string[length];
            for (int i = 0; i < group.Length; i++)
            {
                group[i] = prefix + (i + 1);
            }
            return group;
        }
    }

    public class IceConfigInfo : BarrierConfigInfo
    {
        private static bool m_IsInit = false;

        public IceConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }

        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Mass = 1f;
            m_BarrierBrokenAudioPaths = CreateGroup("Broken", 2);
        }
    }

    public class WoodConfigInfo : BarrierConfigInfo
    {
        private static bool m_IsInit = false;

        public WoodConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }

        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Mass = 2f;
            m_BarrierBrokenAudioPaths = CreateGroup("Broken", 2);
        }
    }

    public class RockConfigInfo:BarrierConfigInfo
    {
        private static bool m_IsInit = false;

        public RockConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }

        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Mass = 3f;
            m_BarrierBrokenAudioPaths = CreateGroup("Broken", 2);
        }
    }
}
