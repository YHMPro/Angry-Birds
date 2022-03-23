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
        /// 关卡类型
        /// </summary>
        public EnumGameLevelType LevelType = EnumGameLevelType.None;
        /// <summary>
        /// 关卡历史最佳评星
        /// </summary>
        public int LevelHistoryRating = 0;
        /// <summary>
        /// 关卡历史最佳分数
        /// </summary>
        public int LevelHistoryScore = 0;
        /// <summary>
        /// 硬币数量
        /// </summary>
        public int CoinNum = 0;
        /// <summary>
        /// 是否通过(满足关卡开启的基本条件即可开锁)
        /// </summary>
        public bool IsThrough = false;
        /// <summary>
        /// 弹弓坐标
        /// </summary>
        public CustomVector3 SlingShotPosition;
        /// <summary>
        /// 相机坐标
        /// </summary>
        public CustomVector3 Camera2DPosition;
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
