using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class ProjectEvents 
    {
        /// <summary>
        /// 分数更新事件
        /// </summary>
        public readonly static string ScoreUpdateEvent = "ScoreUpdateEvent";
        /// <summary>
        /// 硬币更新事件
        /// </summary>
        public readonly static string CoinUpdateEvent = "CoinUpdateEvent";
        /// <summary>
        /// 逻辑更新事件
        /// </summary>
        public readonly static string LogicUpdateEvent = "LogicUpdateEvent";
        /// <summary>
        /// 保存关卡数据事件
        /// </summary>
        public readonly static string SaveLevelDataEvent = "SaveLevelDataEvent";
        /// <summary>
        /// 保存关卡数据到本地
        /// </summary>
        public readonly static string SaveLevelDataToThisLocalityEvent = "SaveLevelDataToThisLocalityEvent";
    }
}
