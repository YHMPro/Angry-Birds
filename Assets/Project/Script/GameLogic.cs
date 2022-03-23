using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
using Bird_VS_Boar.LevelConfig;
using Farme.UI;

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
        /// 当前评星(0~3)
        /// </summary>
        private static int m_NowRating = 0;
        /// <summary>
        /// 当前评星
        /// </summary>
        public static int NowRating => m_NowRating;
        /// <summary>
        /// 历史最佳评星
        /// </summary>
        private static int m_HistoryRating = 0;
        /// <summary>
        /// 历史最佳评星
        /// </summary>
        public static int HistoryRating => m_HistoryRating;
        /// <summary>
        /// 当前分数
        /// </summary>
        private static int m_NowScore = 0;
        /// <summary>
        /// 当前关卡分数
        /// </summary>
        public static int NowScore => m_NowScore;     
        /// <summary>
        /// 历史最佳分数
        /// </summary>
        private static int m_HistoryScore = 0;
        /// <summary>
        /// 历史最佳分数
        /// </summary>
        public static int HistoryScore => m_HistoryScore;
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
        /// 钱币数量
        /// </summary>
        public static int CoinNum => m_CoinNum;
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
            m_NowComeBird = null;
            MesgManager.MesgListen<int>(ProjectEvents.ScoreUpdateEvent, ScoreUpdate);
            MesgManager.MesgListen<BirdConfigInfo>(ProjectEvents.CoinUpdateEvent, CoinUpdate);
            MesgManager.MesgListen(ProjectEvents.LogicUpdateEvent, LogicUpdate);
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + GameManager.NowLevelIndex);
            m_HistoryScore = levelConfig.LevelHistoryScore;
            m_HistoryRating = levelConfig.LevelHistoryRating;
            m_CoinNum = levelConfig.CoinNum;
            CoinUpdate(null);
        }
        /// <summary>
        /// 清除数据
        /// </summary>
        public static void Clear()
        {
            m_NowScore = 0;
            m_NowRating = 0;
            MesgManager.MesgBreakListen<int>(ProjectEvents.ScoreUpdateEvent, ScoreUpdate);
            MesgManager.MesgBreakListen<BirdConfigInfo>(ProjectEvents.CoinUpdateEvent, CoinUpdate);
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
                #region 设置下一关开发
                int levelNum = LevelConfigManager.GetLevelNum(GameManager.NowLevelType);
                if(GameManager.NowLevelIndex < levelNum)
                {
                    LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + (GameManager.NowLevelIndex + 1)).IsThrough = true;
                }
                RatingLogic();//评星判断        
                GameManager.GameOver(true);
                #endregion
                //设置当前关卡的最佳历史分数
                if (m_NowScore> m_HistoryScore)
                {
                    GameManager.NowLevelConfig.LevelHistoryScore = m_NowScore;
                }
                //设置当前关卡的星级
                if(m_NowRating>m_HistoryRating)
                {
                   GameManager.NowLevelConfig.LevelHistoryRating = m_NowRating;
                }
            }
            else if ((m_NowComeBird==null) && (!IsBuy))//钱币不能满足购买一只小鸟(最低价)并且当前无登场小鸟
            {
                Debuger.Log("失败");
                m_IsGameOver = true;
                GameManager.GameOver(false);
            }        
        }
        #endregion
        #region CoinUpdate
        public static void CoinAdd()
        {
            m_CoinNum++;
            StandardWindow window = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameSceneWindow");
            if (window == null || !window.GetPanel<GoodsPanel>("GoodsPanel", out var panel))
            {
                Debuger.LogError("窗口GameSceneWindow不存在或面板GoodsPanel不存在!!!");
                return;
            }
            panel.RefreshPanel();
        }
        private static void CoinUpdate(BirdConfigInfo configInfo)
        {
            if (configInfo != null)
            {
                m_CoinNum -= configInfo.Coin;
            }
            StandardWindow window = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameSceneWindow");
            if (window == null || !window.GetPanel<GoodsPanel>("GoodsPanel", out var panel))
            {
                Debuger.LogError("窗口GameSceneWindow不存在或面板GoodsPanel不存在!!!");
                return;
            }
            panel.RefreshPanel();
        }     
        #endregion

        #region RatingLogic
        /// <summary>
        /// 评星判断
        /// </summary>
        private static void RatingLogic()
        {
            m_NowRating = Mathf.Clamp(m_CoinNum + ((m_NowComeBird == null) ? 0 : 1), 0, 3);
        }
        #endregion
    }
}
