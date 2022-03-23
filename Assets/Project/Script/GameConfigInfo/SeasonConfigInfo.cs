using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Tool;
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
        /// 季节配置信息容器
        /// </summary>
        private static Dictionary<EnumGameLevelType, SeasonConfigInfo> m_SeasonConfigInfoDic = new Dictionary<EnumGameLevelType, SeasonConfigInfo>();
        /// <summary>
        /// 获取季节配置信息
        /// </summary>
        private static T GetSeasonConfigInfo<T>(EnumGameLevelType gameLevelType) where T : SeasonConfigInfo,new()
        {
            if(!m_SeasonConfigInfoDic.TryGetValue(gameLevelType,out SeasonConfigInfo info))
            {
                info = new T();
                info.InitConfigInfo();
                m_SeasonConfigInfoDic.Add(gameLevelType , info);
            }
            return (T)info;
        }
        /// <summary>
        /// 获取季节配置信息
        /// </summary>
        public static SeasonConfigInfo GetSeasonConfigInfo(EnumGameLevelType gameLevelType)
        {
            SeasonConfigInfo seasonConfigInfo = null;
            switch(gameLevelType)
            {
                case EnumGameLevelType.Spring:
                    {
                        seasonConfigInfo = GetSeasonConfigInfo<SpringConfigInfo>(gameLevelType);
                        break;
                    }
                case EnumGameLevelType.Summer:
                    {
                        seasonConfigInfo = GetSeasonConfigInfo<SummerConfigInfo>(gameLevelType);
                        break;
                    }
                case EnumGameLevelType.Autumn:
                    {
                        seasonConfigInfo = GetSeasonConfigInfo<AutumnConfigInfo>(gameLevelType);
                        break;
                    }
                case EnumGameLevelType.Winter:
                    {
                        seasonConfigInfo = GetSeasonConfigInfo<WinterConfigInfo>(gameLevelType);
                        break;
                    }
            }
            return seasonConfigInfo;
        }
        #region 音效路径
        /// <summary>
        /// 季节音效路径
        /// </summary>
        private string m_SeasonAudioPath;
        /// <summary>
        /// 关卡背景音乐
        /// </summary>
        private string m_LevelAudioPath;
        #endregion
        #region 精灵路径
        /// <summary>
        /// 关卡界面背景精灵路径
        /// </summary>
        private string m_LevelInterfaceBGSpritePath;
        /// <summary>
        /// 关卡类型背景精灵路径
        /// </summary>
        private string m_LevelTypeBGSpritePath;
        /// <summary>
        /// 关卡背景精灵路径
        /// </summary>
        private string m_LevelBGSpritePath;
        /// <summary>
        /// 默认星星路径
        /// </summary>
        private string m_StarDefaultSpritePath;
        /// <summary>
        /// 填充星星路径
        /// </summary>
        private string m_StarFillSpritePath;
        /// <summary>
        /// 关卡锁精灵路径
        /// </summary>
        private string m_LevelLockSpritePath;
        #endregion

        #region 预制路径
       
        #endregion

        public string GetLevelMapPrefabPath()
        {
            return m_CommonPath + "Map";
        }

        public string GetLevelInterfaceBGSpritePath()
        {
            return m_LevelInterfaceBGSpritePath;
        }
     
        public string GetLevelLockSpritePath()
        {
            return m_LevelLockSpritePath;
        }

        public string GetStarDefaultSpritePath()
        {
            return m_StarDefaultSpritePath;
        }

        public string GetStarFillSpritePath()
        {
            return m_StarFillSpritePath;
        }
        public string GetLevelBGSpritePath()
        {
            return m_LevelBGSpritePath;
        }
        public string GetLevelTypeBGSpritePath()
        {
            return m_LevelTypeBGSpritePath;
        }
        public string GetSeasonAudioPath()
        {
            return m_SeasonAudioPath;
        }
        public string GetLevelAudioPath()
        {
            return m_LevelAudioPath;
        }
        public virtual void InitConfigInfo()
        {
            m_CommonPath = "SeasonConfigInfo/" + GetType().Name + "/";
            m_SeasonAudioPath = m_CommonPath + "SeasonBGM";
            m_LevelAudioPath = m_CommonPath + "LevelBGM";
            m_LevelTypeBGSpritePath = m_CommonPath + "LevelTyoeBG";
            m_LevelBGSpritePath = m_CommonPath + "LevelBg";
            m_StarDefaultSpritePath = "SeasonConfigInfo/StarDefault";
            m_StarFillSpritePath = "SeasonConfigInfo/StarFill";
            m_LevelLockSpritePath = "SeasonConfigInfo/LevelLock";
            m_LevelInterfaceBGSpritePath = m_CommonPath+"BG";
            #region 待


            #endregion
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
