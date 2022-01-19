using System.Collections.Generic;
using System;
namespace Bird_VS_Boar.LevelConfig
{ 
    /// <summary>
    /// 关卡配置
    /// </summary>
    [Serializable]
    public class LevelConfig 
    {
        /// <summary>
        /// 关卡索引
        /// </summary>
        public int LevelIndex =-1;
        /// <summary>
        /// 关卡类型
        /// </summary>
        public EnumGameLevelType LevelType = EnumGameLevelType.None;
        /// <summary>
        /// 猪配置列表
        /// </summary>
        public List<PigConfig> PigConfigs=new List<PigConfig>();
        /// <summary>
        /// 障碍物配置列表
        /// </summary>
        public List<BarrierConfig> BarrierConfigs=new List<BarrierConfig>();
    }
    /// <summary>
    /// 猪配置
    /// </summary>
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
    /// <summary>
    /// 障碍物配置
    /// </summary>
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
