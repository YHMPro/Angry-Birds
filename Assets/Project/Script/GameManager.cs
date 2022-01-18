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
    /// 游戏管理器
    /// </summary>
    public class GameManager 
    {       
        /// <summary>
        /// 当前场景中猪的数量
        /// </summary>
        public static int NowScenePigNum
        {
            get
            {
                return m_Pigs.Count;
            }
        }
        /// <summary>
        /// 当前场景中鸟的数量
        /// </summary>
        public static int NowSceneBirdNum
        {
            get
            {
                return m_Birds.Count;
            }
        }
        /// <summary>
        /// 猪列表
        /// </summary>
        private static List<Pig> m_Pigs=new List<Pig>();
        /// <summary>
        /// 鸟列表
        /// </summary>
        private static List<Bird>m_Birds=new List<Bird>();       
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            LevelConfigManager.ReadConfigTableData();//读取配置表数据
            LevelConfig.LevelConfig levelConfig =  LevelConfigManager.GetLevelConfig("第一关");//读取关卡配置数据

            foreach(var info in levelConfig.PigConfigs)
            {
                //info.PigType
            }

        }
        /// <summary>
        /// 清除
        /// </summary>
        public static void Clear()
        {
            m_Pigs = new List<Pig>();  
            m_Birds = new List<Bird>();
        }
        

        #region GameTarget
        /// <summary>
        /// 添加猪
        /// </summary>
        /// <param name="pig">猪实例</param>
        public static void AddPig(Pig pig)
        {
            if(m_Pigs.Contains(pig))
            {
                return;
            }
            m_Pigs.Add(pig);
        }
        /// <summary>
        /// 移除猪
        /// </summary>
        /// <param name="pig">猪实例</param>
        public static void RemovePig(Pig pig)
        {
            if (m_Pigs.Contains(pig))
            {
                m_Pigs.Remove(pig);
                Debuger.Log("当前场景内猪的数量:" + NowScenePigNum);
                MesgManager.MesgTirgger(ProjectEvents.LogicUpdateEvent);
            }
        }
        /// <summary>
        /// 添加鸟
        /// </summary>
        /// <param name="bird">鸟实例</param>
        public static void AddBird(Bird bird)
        {
            if(m_Birds.Contains(bird))
            {
                return;
            }
            m_Birds.Add(bird);
        }
        /// <summary>
        /// 移除鸟
        /// </summary>
        /// <param name="bird">鸟实例</param>
        public static void RemoveBird(Bird bird)
        {
            if(m_Birds.Contains(bird))
            {
                m_Birds.Remove(bird);
                Debuger.Log("当前场景内鸟的数量:" + NowSceneBirdNum);
                MesgManager.MesgTirgger(ProjectEvents.LogicUpdateEvent);
            }
        }
        #endregion
   
        #region GameOver
        /// <summary>
        /// 游戏结束
        /// </summary>
        /// <param name="isWin">是否胜利</param>
        public static void GameOver(bool isWin)
        {
            StandardWindow window = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameSceneWindow");
            if(window==null|| !window.GetPanel<GameOverPanel>("GameOverPanel", out var panel))
            {
                Debuger.LogError("窗口GameSceneWindow不存在或面板GameOverPanel不存在!!!");
                return;
            }
            panel.SetState(EnumPanelState.Show, () =>
            {
                if(isWin)
                {
                    panel.Win();
                }
                else
                {
                    panel.Lose();
                }              
            });
            GameStop();//游戏暂停
            Debuger.Log("游戏结束");
        }
        #endregion

        #region GameStart
        /// <summary>
        /// 游戏开始
        /// </summary>
        public static void GameStart()
        {
            Debuger.Log("游戏开始");
        }
        #endregion

        #region GameStop
        /// <summary>
        /// 游戏暂停
        /// </summary>
        public static void GameStop()
        {
            Debuger.Log("游戏暂停");
        }
        #endregion

        #region ReplayLevel
        /// <summary>
        /// 重玩本关
        /// </summary>
        public static void ReplayLevel()
        {
            Debuger.Log("重玩本关");
        }
        #endregion

        #region LastLevel
        /// <summary>
        /// 上一关
        /// </summary>
        public static void LastLevel()
        {
            Debuger.Log("上一关");
        }
        #endregion

        #region NextLevel
        /// <summary>
        /// 下一关
        /// </summary>
        public static void NextLevel()
        {
            Debuger.Log("下一关");
        }
        #endregion

        #region ReturnLevel
        /// <summary>
        /// 返回关卡
        /// </summary>
        public static void ReturnLevel()
        {
            Debuger.Log("返回关卡");
        }
        #endregion
    }
}
