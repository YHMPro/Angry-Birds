using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{

    /// <summary>
    /// 小鸟配置信息
    /// </summary>
    public abstract class BirdConfigInfo
    {     
        /// <summary>
        /// 小鸟配置信息容器
        /// </summary>
        private static Dictionary<EnumBirdType,BirdConfigInfo> m_BirdConfigInfoDic=new Dictionary<EnumBirdType, BirdConfigInfo>();
        private static T GetBirdConfigInfo<T>(EnumBirdType birdType) where T : BirdConfigInfo,new()
        {
            if(!m_BirdConfigInfoDic.TryGetValue(birdType,out BirdConfigInfo info))
            {
                info = new T();
                info.InitConfigInfo();
                m_BirdConfigInfoDic.Add(birdType, info);
            }                        
            return (T)info;          
        }
        public static BirdConfigInfo GetBirdConfigInfo(EnumBirdType birdType)
        {
            BirdConfigInfo birdConfigInfo = null;
            switch(birdType)
            {
                case EnumBirdType.RedBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<RedBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.GreenBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<GreenBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.BlackBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<BlackBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.BlueBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<BlueBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.PinkBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<PinkBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.VanBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<VanBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.WhiteBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<WhiteBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.YellowBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<YellowBirdConfigInfo>(birdType);
                        break;
                    }
                case EnumBirdType.LittleBlueBird:
                    {
                        birdConfigInfo = GetBirdConfigInfo<BlueBirdConfigInfo>(birdType);
                        break;
                    }
            }
            return birdConfigInfo;
        }
        /// <summary>
        /// 所需硬币
        /// </summary>
        protected int m_Coin = 0;
        /// <summary>
        /// 所需硬币
        /// </summary>
        public int Coin
        {
            get { return m_Coin; }
        }
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
        /// 共同的
        /// </summary>
        protected string m_CommonPath;
        #region 预制路径      
        /// <summary>
        /// 小鸟预制路径
        /// </summary>
        protected string m_BirdPrefabPath;
        #endregion
        #region 音效路径
        /// <summary>
        /// 小鸟死亡音效路径
        /// </summary>
        protected string m_BirdDiedAudioPath;
        /// <summary>
        /// 小鸟被选中音效路径数组
        /// </summary>
        protected string[] m_BirdSelectAudioPaths;
        /// <summary>
        /// 小鸟碰撞音效路径数组
        /// </summary>
        protected string[] m_BirdCollisionAudioPaths;
        /// <summary>
        /// 小鸟飞行音效路径数组
        /// </summary>
        protected string[] m_BirdFlyAudioPaths;
        /// <summary>
        /// 小鸟技能音效路径数组
        /// </summary>
        protected string[] m_BirdSkillAudioPaths;
        #endregion        
        public virtual void InitConfigInfo()
        {        
            m_OrderInLayer = 2;//暂时所有小鸟都为2
            m_BirdDiedAudioPath = "BirdConfigInfo/De1";
            m_CommonPath= "BirdConfigInfo/" + GetType().Name+"/";
            m_BirdPrefabPath = m_CommonPath+"Bird";
        }
                 
        public string GetBirdPrefabPath()
        {
            return m_BirdPrefabPath;
        }
        public string GetDiedAudioPath()
        {
            return m_BirdDiedAudioPath;
        }

        public string GetSelectAudioPaths()
        {
            if (m_BirdSelectAudioPaths == null)
            {
                return null;
            }
            return m_CommonPath + m_BirdSelectAudioPaths[GetRandonNumber(0, m_BirdSelectAudioPaths.Length - 1)];
        }

        public string GetCollisionAudioPaths()
        {
            if (m_BirdCollisionAudioPaths == null)
            {
                return null;
            }
            return m_CommonPath + m_BirdCollisionAudioPaths[GetRandonNumber(0, m_BirdCollisionAudioPaths.Length - 1)];
        }

        public string GetFlyAudioPaths()
        {
            if (m_BirdFlyAudioPaths == null)
            {
                return null;
            }
            return m_CommonPath + m_BirdFlyAudioPaths[GetRandonNumber(0, m_BirdFlyAudioPaths.Length - 1)];
        }

        public string GetSkillAudioPaths()
        {
            if(m_BirdSkillAudioPaths == null)
            {
                return null;
            }
            return m_CommonPath + m_BirdSkillAudioPaths[GetRandonNumber(0, m_BirdSkillAudioPaths.Length - 1)];
        }
        /// <summary>
        /// 创建组
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="length">长度</param>
        protected string[] CreateGroup(string prefix, int length)
        {
            string[] group = new string[length];
            for(int i=0;i< group.Length;i++)
            {
                group[i] = prefix + (i+1);
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
    /// <summary>
    /// 红色小鸟配置信息
    /// </summary>
    public class RedBirdConfigInfo : BirdConfigInfo
    {      
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 1;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
        }
    }

    public class BlackBirdConfigInfo: BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 3;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
            m_BirdSkillAudioPaths = CreateGroup("Skill", 1);
            m_BirdCollisionAudioPaths = CreateGroup("Co", 4);        
        }
    }

    public class BlueBirdConfigInfo:BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 2;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
        }
    }

    public class GreenBirdConfigInfo:BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 2;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
            m_BirdCollisionAudioPaths = CreateGroup("Co", 4);
        }
    }

    public class PinkBirdConfigInfo :BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 2;
            m_BirdSelectAudioPaths = CreateGroup("Select", 3);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 3);
            m_BirdCollisionAudioPaths = CreateGroup("Co", 5);
            m_BirdSkillAudioPaths = CreateGroup("Skill", 3);
        }
    }

    public class VanBirdConfigInfo:BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 3;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
            m_BirdCollisionAudioPaths = CreateGroup("Co", 4);
            m_BirdSkillAudioPaths = CreateGroup("Skill", 1);
        }
    }

    public class WhiteBirdConfigInfo:BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 5;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
            m_BirdCollisionAudioPaths = CreateGroup("Co", 5);
            m_BirdSkillAudioPaths = CreateGroup("Skill", 1);
        }
    }

    public class YellowBirdConfigInfo :BirdConfigInfo
    {
        public override void InitConfigInfo()
        {
            base.InitConfigInfo();
            m_Coin = 2;
            m_BirdSelectAudioPaths = CreateGroup("Select", 1);
            m_BirdFlyAudioPaths = CreateGroup("Fly", 1);
            m_BirdCollisionAudioPaths = CreateGroup("Co", 5);
            m_BirdSkillAudioPaths = CreateGroup("Skill", 1);
        }
    }
}
