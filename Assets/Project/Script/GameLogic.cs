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
            
        }
        /// <summary>
        /// 逻辑判定
        /// </summary>
        public static void Logic()
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
        /// <summary>
        /// 硬币更新
        /// </summary>
        public static void CoinUpdate(EnumBirdType birdType)
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

    }
}
