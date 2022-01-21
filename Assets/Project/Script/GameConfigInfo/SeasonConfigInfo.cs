using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 季节配置信息
    /// </summary>
    public class SeasonConfigInfo 
    {
        /// <summary>
        /// 共同路径
        /// </summary>
        private string m_CommonPath;
        /// <summary>
        /// 季节配置信息容器
        /// </summary>
        public static Dictionary<EnumGameLevelType, SeasonConfigInfo> SeasonConfigInfoDic=new Dictionary<EnumGameLevelType, SeasonConfigInfo>();
        /// <summary>
        /// 季节音效路径
        /// </summary>
        private string m_SeasonAudioPath;
        /// <summary>
        /// 关卡类型背景精灵路径
        /// </summary>
        private string m_LevelTypeBGSpritePath;

        public string GetLevelTypeBGSpritePath()
        {
            return m_LevelTypeBGSpritePath;
        }

        public string GetSeasonAudioPath()
        {
            return m_SeasonAudioPath;
        }
        public virtual void InitConfigInfo()
        {
            m_CommonPath = "Season/"+GetType().Name + "/";
            m_SeasonAudioPath = m_CommonPath + "BGM";
            m_LevelTypeBGSpritePath = m_CommonPath + "LevelTyoeBG";
        }
    }
    /// <summary>
    /// 春季配置信息
    /// </summary>
    public class SpringConfigInfo: SeasonConfigInfo
    {
        private static bool m_IsInit = false;
        public SpringConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
    }
    /// <summary>
    /// 夏季配置信息
    /// </summary>
    public class SummerConfigInfo: SeasonConfigInfo
    {
        private static bool m_IsInit = false;
        public SummerConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
    }
    /// <summary>
    /// 秋季配置信息
    /// </summary>
    public class AutumnConfigInfo: SeasonConfigInfo
    {
        private static bool m_IsInit = false;
        public AutumnConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
    }
    /// <summary>
    /// 冬季配置信息
    /// </summary>
    public class WinterConfigInfo: SeasonConfigInfo
    {
        private static bool m_IsInit = false;
        public WinterConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
    }
}
