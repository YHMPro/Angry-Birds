using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class PigConfigInfo
    {    
        /// <summary>
        /// 小鸟配置信息容器
        /// </summary>
        public static Dictionary<string, PigConfigInfo> PigConfigInfoDic = new Dictionary<string, PigConfigInfo>();       
        /// <summary>
        /// 共同的
        /// </summary>
        protected string m_CommonPath;
        /// <summary>
        /// 猪死亡音效路径
        /// </summary>
        protected string m_PigDiedAudioPath;
        /// <summary>
        /// 猪受伤音效路径数组
        /// </summary>
        protected string[] m_PigHurtAudioPath;
        /// <summary>
        /// 猪预制路径
        /// </summary>
        protected string m_PigPrefabPath;

        protected virtual void InitConfigInfo()
        {
            m_PigDiedAudioPath = "Pig/PigCommon/De1";
            m_CommonPath = "Pig/" + GetType().Name + "/";
            m_PigPrefabPath = "Pig";
        }
        public string GetPigPrefabPath()
        {
            return m_CommonPath + m_PigPrefabPath;
        }
        public string GetDiedAudioPath()
        {
            return m_PigDiedAudioPath;
        }
        public string GetHurtAudioPath()
        {
            if (m_PigHurtAudioPath == null)
            {
                return null;
            }
            return m_CommonPath + m_PigHurtAudioPath[GetRandonNumber(0, m_PigHurtAudioPath.Length - 1)];
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
        /// <summary>
        /// 获取自定义范围内的随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GetRandonNumber(int min, int max)
        {
            System.Random random = new System.Random();
            return random.Next(min, max);
        }
    }

    public class YoungPigConfigInfo:PigConfigInfo
    {
        private static bool m_IsInit = false;

        public YoungPigConfigInfo()
        {
            if(!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
        protected override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_PigHurtAudioPath = CreateGroup("Hurt", 2);
        }
    }

    public class OldPigConfigInfo :PigConfigInfo
    {
        private static bool m_IsInit = false;

        public OldPigConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
        protected override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_PigHurtAudioPath = CreateGroup("Hurt", 2);
        }
    }

    public class RockPigConfigInfo :PigConfigInfo
    {
        private static bool m_IsInit = false;

        public RockPigConfigInfo()
        {
            if (!m_IsInit)
            {
                InitConfigInfo();
                m_IsInit = true;
            }
        }
        protected override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_PigHurtAudioPath = CreateGroup("Hurt", 2);
        }
    }
}
