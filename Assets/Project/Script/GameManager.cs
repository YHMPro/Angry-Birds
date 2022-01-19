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
        /// 是否重玩关卡
        /// </summary>
        private static bool m_IsReplayLevel = false;
        /// <summary>
        /// 当前相机跟随的小鸟
        /// </summary>
        public static Bird NowCameraFollowTarget
        {
            get
            {
                if(m_Birds.Count == 0)
                {
                    return null;
                }
                return m_Birds[m_Birds.Count - 1];
            }
        }
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
        /// 可销毁对象列表
        /// </summary>
        private static List<IDied> m_DiedTargets=new List<IDied>();
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            GameStart("第一关");
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
        /// <summary>
        /// 添加障碍物
        /// </summary>
        /// <param name="died"></param>
        public static void AddDiedTarget(IDied died)
        {
            if (m_DiedTargets.Contains(died))
            {
                return;
            }
            m_DiedTargets.Add(died);
        }
        /// <summary>
        /// 移除障碍物
        /// </summary>
        /// <param name="died"></param>
        public static void RemoveDiedTarget(IDied died)
        {
            if (m_DiedTargets.Contains(died))
            {
                m_DiedTargets.Remove(died);
                Debuger.Log("当前场景可销毁对象的数量:" + NowSceneBirdNum);
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
            if(m_IsReplayLevel)
            {
                return;
            }
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
        /// <param name="key">关卡key</param>
        public static void GameStart(string key)
        {
            m_IsReplayLevel = false;
            Debuger.Log("游戏开始");
            GameLogic.Init();
            #region 依照数据表来加载场景内容   这里是测试代码
            LevelConfigManager.ReadConfigTableData();//读取配置表数据
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(key);//读取关卡配置数据     
            foreach (var config in levelConfig.BarrierConfigs)
            {
                //获取障碍物预制路径
                if (BarrierConfigInfo.BarrierConfigInfoDic.TryGetValue(config.BarrierType, out var info))
                {
                    if (!GoReusePool.Take(config.BarrierType.ToString() + config.BarrierShapeType.ToString(), out GameObject go))
                    {
                        if (!GoLoad.Take(info.GetBarrierPrefabPath(config.BarrierShapeType), out go))
                        {
                            Debuger.LogError("障碍物配置信息错误");
                            return;
                        }
                    }
                    go.transform.position = config.Position.ToVector3();
                    go.transform.eulerAngles = config.Euler.ToVector3();
                    go.transform.localScale = config.Scale.ToVector3();
                }
            }
            foreach (var config in levelConfig.PigConfigs)
            {
                //获取障碍物预制路径
                if (PigConfigInfo.PigConfigInfoDic.TryGetValue(config.PigType, out var info))
                {
                    if (GoLoad.Take(info.GetPigPrefabPath(), out GameObject go))
                    {
                        go.transform.position = config.Position.ToVector3();
                        go.transform.eulerAngles = config.Euler.ToVector3();
                        go.transform.localScale = config.Scale.ToVector3();
                    }
                }
            }
            #endregion
        }
        #endregion

        #region GameStop
        /// <summary>
        /// 游戏暂停
        /// </summary>
        public static void GameStop()
        {
            Debuger.Log("游戏暂停");
            //Time.timeScale = 0;
        }
        #endregion

        #region ReplayLevel
        /// <summary>
        /// 重玩本关
        /// </summary>
        public static void ReplayLevel()
        {
            m_IsReplayLevel = true;
            Debuger.Log("重玩本关");
            GameLogic.Clear();
            //回收场景内的所有物体(猪、障碍物、小鸟)        
            for (int index = m_DiedTargets.Count-1; index>=0; index--)
            {
                m_DiedTargets[index].Died();
            }
            //重新加载本关
            GameStart("第一关");
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

        #region RecycleGameTarget
        /// <summary>
        /// 回收场景内所有游戏对象(猪、鸟、障碍物)
        /// </summary>
        private static void RecycleSceneAllGameTagret()
        {

        }
        #endregion
    }
}
