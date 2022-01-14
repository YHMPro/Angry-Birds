using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Bird_VS_Boar.LevelConfig
{ 
    [Serializable]
    public class LevelConfig 
    {
        private static Dictionary<string, LevelConfig> m_LevelConfigDic = new Dictionary<string, LevelConfig>();
        public static void AddLevelConfig(LevelConfig levelConfig)
        {

        }
        public static void RemoveLevelConfig(LevelConfig levelConfig)
        {

        }
        public static LevelConfig GetLevelConfig(string key)
        {
            return null;
        }
        private List<PigConfig> m_PigConfigs=new List<PigConfig> ();
        private List<BarrierConfig> m_BarrierConfigs=new List<BarrierConfig> ();

        public void AddPigConfig(PigConfig pigConfig)
        {
            if(m_PigConfigs.Contains(pigConfig))
            {
                return;
            }
            m_PigConfigs.Add(pigConfig);
        }
        public void RemovePigConfig(PigConfig pigConfig)
        {
            if (m_PigConfigs.Contains(pigConfig))
            {
                m_PigConfigs.Remove(pigConfig);
            }
        }

        public void AddBarrierConfig(BarrierConfig barrierConfig)
        {
            if (m_BarrierConfigs.Contains(barrierConfig))
            {
                return;
            }
            m_BarrierConfigs.Add(barrierConfig);
        }
        public void RemoveBarrierConfig(BarrierConfig barrierConfig)
        {
            if (m_BarrierConfigs.Contains(barrierConfig))
            {
                m_BarrierConfigs.Remove(barrierConfig);
            }
        }       
    }

    [Serializable]
    public class PigConfig
    {
        /// <summary>
        /// 类型
        /// </summary>
        public EnumPigType PigType;
        /// <summary>
        /// 坐标
        /// </summary>
        public CustomVector3 Position;
        /// <summary>
        /// 旋转
        /// </summary>
        public CustomVector3 Euler;
        /// <summary>
        /// 缩放
        /// </summary>
        public CustomVector3 Scale;
    }
    [Serializable]
    public class BarrierConfig
    {
        /// <summary>
        /// 类型
        /// </summary>
        public EnumBarrierType BarrierType;
        /// <summary>
        /// 形状
        /// </summary>
        public EnumBarrierShapeType BarrierShapeType;
        /// <summary>
        /// 位置
        /// </summary>
        public CustomVector3 Position;
        /// <summary>
        /// 旋转
        /// </summary>
        public CustomVector3 Euler;
        /// <summary>
        /// 缩放
        /// </summary>
        public CustomVector3 Scale;
    }


}
