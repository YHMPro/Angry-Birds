using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class BarrierConfigInfo
    {
        public static Dictionary<EnumBarrierType, BarrierConfigInfo> BarrierConfigInfoDic=new Dictionary<EnumBarrierType, BarrierConfigInfo>();
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
            m_CommonPath = "Barrier/" + GetType().Name + "/";
            m_BarrierPrefabPath = "Barrier";
            m_BarrierDestroyAudioPath = "De1";
            m_OrderInLayer = 11;
        }
        public string GetBarrierDestroyAudioPath()
        {
            return m_CommonPath + m_BarrierDestroyAudioPath;
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
            m_BarrierBrokenAudioPaths = CreateGroup("Broken", 2);
        }
    }
}
