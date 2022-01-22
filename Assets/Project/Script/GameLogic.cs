using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏逻辑处理
    /// </summary>
    public class GameLogic 
    {
        
        /// <summary>
        /// 游戏是否结束
        /// </summary>
        private static bool m_IsGameOver = false;
        /// <summary>
        /// 当前评星
        /// </summary>
        private static int m_NowRating = 0;
        /// <summary>
        /// 当前分数
        /// </summary>
        private static int m_NowScore = 0;
        /// <summary>
        /// 当前关卡分数
        /// </summary>
        public static int NowScore
        {
            get
            {
                return m_NowScore;
            }
        }
        /// <summary>
        /// 历史最佳分数
        /// </summary>
        private static int m_HistoryScore = 0;
        /// <summary>
        /// 历史最佳分数
        /// </summary>
        public static int HistoryScore
        {
            get { return m_HistoryScore; }
        }
        /// <summary>
        /// 当前登场的小鸟
        /// </summary>
        private static Bird m_NowComeBird = null;
        /// <summary>
        /// 当前登场的小鸟
        /// </summary>
        public static Bird NowComeBird
        {
            get
            {
                return m_NowComeBird;
            }
            set//测试代码
            {
                m_NowComeBird = value;
                if (m_NowComeBird==null)
                {
                    return;
                }
                GameManager.AddBird(value);
                GameManager.AddDiedTarget(value);
            }
        }
        /// <summary>
        /// 小鸟是否是当前登场的小鸟
        /// </summary>
        /// <param name="bird"></param>
        /// <returns></returns>
        public static bool BirdIsNowComeBirdLogic(Bird bird)
        {
            return NowComeBird == bird;
        }
        /// <summary>
        /// 钱币数量
        /// </summary>
        private static int m_CoinNum=1000;
        /// <summary>
        /// 钱币是否能够购买小鸟
        /// </summary>
        public static bool IsBuy
        {
            get
            {
                return m_CoinNum > 0;//待改
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            m_IsGameOver = false;
            MesgManager.MesgListen<int>(ProjectEvents.ScoreUpdateEvent, ScoreUpdate);
            MesgManager.MesgListen<EnumBirdType>(ProjectEvents.CoinUpdateEvent, CoinUpdate);
            MesgManager.MesgListen(ProjectEvents.LogicUpdateEvent, LogicUpdate);
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        public static void Clear()
        {
            m_NowScore = 0;
            MesgManager.MesgBreakListen<int>(ProjectEvents.ScoreUpdateEvent, ScoreUpdate);
            MesgManager.MesgBreakListen<EnumBirdType>(ProjectEvents.CoinUpdateEvent, CoinUpdate);
            MesgManager.MesgBreakListen(ProjectEvents.LogicUpdateEvent, LogicUpdate);
        }
        #region ScoreUpdate
        /// <summary>
        /// 记录分数
        /// </summary>
        /// <param name="score">分数</param>
        private static void ScoreUpdate(int score)
        {
            m_NowScore += score;
            Debuger.Log("总分变化:" + m_NowScore);
        }
        #endregion
        #region LogicUpdate
        /// <summary>
        /// 逻辑判定
        /// </summary>
        private static void LogicUpdate()
        {
            if(m_IsGameOver)
            {
                return;
            }
            if(GameManager.NowScenePigNum==0)//当前场景的猪为零
            {
                Debuger.Log("胜利");
                m_IsGameOver = true;
                GameManager.GameOver(true);
            }
            else if (!IsBuy)//钱币不能满足购买一只小鸟(最低价)
            {
                Debuger.Log("失败");
                m_IsGameOver = true;
                GameManager.GameOver(false);
            }        
        }
        #endregion
        #region CoinUpdate
        /// <summary>
        /// 硬币更新
        /// </summary>
        private static void CoinUpdate(EnumBirdType birdType)
        {
            if (BirdConfigInfo.BirdConfigInfoDic.TryGetValue(birdType, out var config))
            {
                m_CoinNum -= config.Coin;
                Debuger.Log("当前硬币数量:" + m_CoinNum);
            }
            else
            {
                Debuger.LogWarning("鸟配置信息读取失败。");
            }
        }
        #endregion
    }
}
