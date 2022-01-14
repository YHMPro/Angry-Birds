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
    }
}
