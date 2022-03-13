using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
using Bird_VS_Boar.LevelConfig;
using Farme.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Farme.Audio;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 场景类型
    /// </summary>
    public enum EnumSceneType
    {
        /// <summary>
        /// 登入场景
        /// </summary>
        LoginScene,
        /// <summary>
        /// 游戏场景
        /// </summary>
        GameScene
    }
    /// <summary>
    /// 游戏关卡类型
    /// </summary>
    public enum EnumGameLevelType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 春季
        /// </summary>
        Spring,
        /// <summary>
        /// 夏季
        /// </summary>
        Summer,
        /// <summary>
        /// 秋季
        /// </summary>
        Autumn,
        /// <summary>
        /// 冬季
        /// </summary>
        Winter

    }
    /// <summary>
    /// 游戏控制
    /// </summary>
    public enum EnumGameControlType
    {
        /// <summary>
        /// 继续
        /// </summary>
        Continue,
        /// <summary>
        /// 停止
        /// </summary>
        Stop     
    }
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public class GameManager
    {     
        /// <summary>
        /// 当前关卡类型
        /// </summary>
        private static EnumGameLevelType m_NowLevelType = EnumGameLevelType.None;
        /// <summary>
        /// 当前关卡类型
        /// </summary>
        public static EnumGameLevelType NowLevelType
        {
            get
            {
                return m_NowLevelType;
            }
            set
            {
                m_NowLevelType = value;
            }
        }
        /// <summary>
        /// 当前关卡索引
        /// </summary>
        private static int m_NowLevelIndex = -1;
        /// <summary>
        /// 当前关卡索引
        /// </summary>
        public static int NowLevelIndex
        {
            get
            {
                return m_NowLevelIndex;
            }
            set
            {
                m_NowLevelIndex = value;
            }
        }
        /// <summary>
        /// 是否屏蔽游戏结束事件
        /// </summary>
        private static bool m_IsShieldGameOverEvent = false;
        /// <summary>
        /// 当前相机跟随的小鸟
        /// </summary>
        public static Bird NowCameraFollowTarget
        {
            get
            {
                if (m_Birds.Count == 0)
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
        /// 当前场景中可销毁的对象数量
        /// </summary>
        public static int NowSceneDiedTargetNum
        {
            get
            {
                return m_DiedTargets.Count;
            }
        }
        /// <summary>
        /// 猪列表
        /// </summary>
        private static List<Pig> m_Pigs = new List<Pig>();
        /// <summary>
        /// 鸟列表
        /// </summary>
        private static List<Bird> m_Birds = new List<Bird>();
        /// <summary>
        /// 可销毁对象列表
        /// </summary>
        private static List<IDied> m_DiedTargets = new List<IDied>();
        /// <summary>
        /// 协程
        /// </summary>
        private static Coroutine m_Cor = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        { 
            
            if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                OtherConfigInfo otherConfigInfo = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton();
                MonoSingletonFactory<SlingShot>.GetSingleton(GoLoad.Take(otherConfigInfo.GetSlingShotPrefabPath()));
                MonoSingletonFactory<FlyPath>.GetSingleton(GoLoad.Take(otherConfigInfo.GetFlyPathPrefabPath()));
                MonoSingletonFactory<Camera2D>.GetSingleton(GoLoad.Take(otherConfigInfo.GetCamera2DPrefabPath()));
            }
            //创建GameSceneWindow
            MonoSingletonFactory<WindowRoot>.GetSingleton().CreateWindow("GameSceneWindow", RenderMode.ScreenSpaceOverlay, (window) =>
            {
                window.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                window.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                window.CreatePanel<GoodsPanel>("UI/GameSceneWindow/GoodsPanel", "GoodsPanel", EnumPanelLayer.MIDDLE, (panel) =>//加载面板
                {

                });
                window.CreatePanel<GameInterfacePanel>("UI/GameSceneWindow/GameInterfacePanel", "GameInterfacePanel", EnumPanelLayer.MIDDLE, (panel) =>
                {

                });
                window.CreatePanel<GameOverPanel>("UI/GameSceneWindow/GameOverPanel", "GameOverPanel", EnumPanelLayer.TOP, (panel) =>
                {
                    panel.SetState(EnumPanelState.Hide);
                });
            });
        } 
        /// <summary>
        /// 清除
        /// </summary>
        public static void Clear()
        {
            m_Pigs.Clear();
            m_Birds.Clear();
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
                RemoveCor();
                m_Cor =MonoSingletonFactory<ShareMono>.GetSingleton().DelayRealtimeAction(3f, () =>
                {
                    MesgManager.MesgTirgger(ProjectEvents.LogicUpdateEvent);
                });
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
                RemoveCor();
                m_Cor =MonoSingletonFactory<ShareMono>.GetSingleton().DelayRealtimeAction(3f, () =>
                {
                    MesgManager.MesgTirgger(ProjectEvents.LogicUpdateEvent);
                });
                
            }
        }
        /// <summary>
        /// 添加可销毁对象
        /// </summary>
        /// <param name="died"></param>
        public static void AddDiedTarget(IDied died)
        {
            if (m_DiedTargets.Contains(died))
            {
                return;
            }
            m_DiedTargets.Add(died);
            //Debuger.Log("当前场景可销毁对象的数量:"+ died.Name + NowSceneDiedTargetNum);
        }
        /// <summary>
        /// 移除可销毁对象
        /// </summary>
        /// <param name="died"></param>
        public static void RemoveDiedTarget(IDied died)
        {
            if (m_DiedTargets.Contains(died))
            {
                m_DiedTargets.Remove(died);
                foreach(var d in m_DiedTargets)
                {
                    Debuger.Log("当前场景可销毁对象名称:" + d.Name + "总数:"+ NowSceneDiedTargetNum);
                }
                
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
            if(m_IsShieldGameOverEvent)
            {
                return;
            }
            //断开当前挂载小鸟的更新
            if (GameLogic.NowComeBird != null)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, GameLogic.NowComeBird.BirdControlUpdate);//移除小鸟控制更新
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
            Debuger.Log("游戏结束");
            GameControl(EnumGameControlType.Stop);//游戏暂停         
        }
        #endregion

        #region GameStart
        /// <summary>
        /// 游戏开始
        /// </summary>
        public static void GameStart()
        {       
            Debuger.Log("【游戏开始】\n【关卡类型】:"+m_NowLevelType.ToString()+"【关卡索引】:"+m_NowLevelIndex);           
            #region 依照数据表来加载场景内容              
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(m_NowLevelType.ToString()+"_"+m_NowLevelIndex);//读取关卡配置数据     
            if(levelConfig==null)
            {
                Debuger.LogError("不存在此场景的配置");
                return;
            }
            m_IsShieldGameOverEvent = false;
            GameLogic.NowComeBird = null;
            if (MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                Debuger.Log("清除弹弓线");
                MonoSingletonFactory<SlingShot>.GetSingleton().ClearLine();
            }     
            GameLogic.Init();//初始化逻辑管理器                
            if (MapManager.LoadMap())
            {
                foreach (var barrierConfig in levelConfig.BarrierConfigs)
                {
                    //获取障碍物预制路径
                    if (BarrierConfigInfo.BarrierConfigInfoDic.TryGetValue(barrierConfig.BarrierType, out var info))
                    {
                        if (!GoReusePool.Take(barrierConfig.BarrierType.ToString() + barrierConfig.BarrierShapeType.ToString(), out GameObject barrierGo))
                        {
                            if (!GoLoad.Take(info.GetBarrierPrefabPath(barrierConfig.BarrierShapeType), out barrierGo))
                            {
                                Debuger.LogError("障碍物配置信息错误");
                                continue;
                            }
                        }
                        barrierGo.transform.position = barrierConfig.Position.ToVector3();
                        barrierGo.transform.eulerAngles = barrierConfig.Euler.ToVector3();
                        barrierGo.transform.localScale = barrierConfig.Scale.ToVector3();
                    }
                }
                foreach (var piConfig in levelConfig.PigConfigs)
                {
                    //获取猪预制路径
                    if (PigConfigInfo.PigConfigInfoDic.TryGetValue(piConfig.PigType, out var info))
                    {
                        if (!GoReusePool.Take(piConfig.PigType.ToString(), out GameObject pigGo))
                        {
                            if (!GoLoad.Take(info.GetPigPrefabPath(), out pigGo))
                            {
                                Debuger.LogError("猪配置信息错误");
                                continue;
                            }
                        }
                        pigGo.transform.position = piConfig.Position.ToVector3();
                        pigGo.transform.eulerAngles = piConfig.Euler.ToVector3();
                        pigGo.transform.localScale = piConfig.Scale.ToVector3();
                    }
                }
            }
            #endregion
        }
        #endregion

        #region GameStop
        /// <summary>
        /// 游戏控制
        /// </summary>
        public static void GameControl(EnumGameControlType controlType)
        {       
            switch (controlType)
            {
                case EnumGameControlType.Continue:
                    {
                        Debuger.Log("游戏继续");
                        Time.timeScale = 1;                       
                        break;
                    }
                case EnumGameControlType.Stop:
                    {
                        Debuger.Log("游戏暂停");
                        Time.timeScale = 0;                        
                        break;
                    }
            }                    
        }
        #endregion

        #region ReplayLevel
        /// <summary>
        /// 重玩本关
        /// </summary>
        public static void ReplayLevel()
        {
            RecycleSceneAllGameTagret(() => 
            {
                Debuger.Log("重玩本关");
                //重新加载本关
                GameStart();
            });
            
        }
        #endregion

        #region LastLevel
        /// <summary>
        /// 上一关
        /// </summary>
        public static void LastLevel()
        {
            RecycleSceneAllGameTagret(() => 
            {
                --m_NowLevelIndex;
                GameStart();
                Debuger.Log("上一关");
            });
            
        }
        #endregion

        #region NextLevel
        /// <summary>
        /// 下一关
        /// </summary>
        public static void NextLevel()
        {
            RecycleSceneAllGameTagret(() => 
            {
                ++m_NowLevelIndex;
                GameStart();
                Debuger.Log("下一关");
            });
            
        }
        #endregion

        #region ReturnLevel
        /// <summary>
        /// 返回关卡
        /// </summary>
        public static void ReturnLevel()
        {
            //销毁弹弓
            MonoSingletonFactory<SlingShot>.ClearSingleton();
            //销毁飞行路径
            MonoSingletonFactory<FlyPath>.ClearSingleton();
            //销毁2D相机
            MonoSingletonFactory<Camera2D>.ClearSingleton();
            RecycleSceneAllGameTagret(() => 
            {
                //关闭游戏场景窗口
                if (!MonoSingletonFactory<WindowRoot>.SingletonExist)
                {
                    Debuger.LogError("窗口根节点丢失");
                    return;
                }
                StandardWindow gameSceneWindow = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameSceneWindow");
                if (gameSceneWindow == null)
                {
                    Debuger.LogError("登入窗口实例不存在");
                    return;
                }
                gameSceneWindow.SetState(EnumWindowState.Destroy, () =>//销毁游戏场景窗口
                {
                    Debuger.Log("返回关卡");
                    SceneLoad(EnumSceneType.LoginScene, () =>
                    {
                        MonoSingletonFactory<WindowRoot>.GetSingleton().CreateWindow("GameLoginWindow", RenderMode.ScreenSpaceOverlay, (gameLoginWindow) =>//加载游戏登入窗口
                        {
                            gameLoginWindow.CanvasScaler.referenceResolution = new Vector2(1920, 1080);//设置画布尺寸
                            gameLoginWindow.CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;//设置适配的方式
                            gameLoginWindow.CreatePanel<GameLevelPanel>("UI/GameLoginWindow/GameLevelPanel", "GameLevelPanel", EnumPanelLayer.MIDDLE, (panel) =>//加载面板
                            {

                            });                           
                        });
                    });
                });                
            },true);
            
        }
        #endregion

        #region RecycleGameTarget
        /// <summary>
        /// 回收场景内所有游戏对象(猪、鸟、障碍物、Boom、Score)
        /// </summary>
        private static void RecycleSceneAllGameTagret(UnityAction callback = null, bool isDestroy = false)
        {
            m_IsShieldGameOverEvent = true;
            GameLogic.Clear();
            //回收场景内的所有物体(猪、障碍物、小鸟)        
            for (int index = m_DiedTargets.Count - 1; index >= 0; index--)
            {            
                m_DiedTargets[index].Died(isDestroy);
            }
            callback?.Invoke();
        }
        #endregion

        #region Scene

        /// <summary>
        /// 场景加载
        /// </summary>
        /// <param name="sceneType"></param>
        public static void SceneLoad(EnumSceneType sceneType,UnityAction callback)
        {
            Farme.SceneLoad.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Single, () =>
            {
                #region 清除Update行为
                MonoSingletonFactory<ShareMono>.GetSingleton().ClearUpdate();
                MonoSingletonFactory<ShareMono>.GetSingleton().ClearFixedUpdate();
                MonoSingletonFactory<ShareMono>.GetSingleton().ClearLateUpdate();
                #endregion
                #region 清除通过Resources加载的资源缓存
                ResourcesLoad.ClearAllCache();
                #endregion
                #region 清除通过AudioClipManager加载的音效缓存
                if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    AudioClipManager.UnLoadAllAudioClip(new string[] //忽略组
                    {
                        NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().GetButtonAudioPath()
                    });
                }               
                #endregion                
                GC.Collect();
            }, (result) => 
            {
                if(result)
                {
                    Debuger.Log("场景加载成功");
                    callback?.Invoke();
                }
            }, (pro) => 
            {
                Debuger.Log("场景加载进度:" + pro);
            });
        }
        /// <summary>
        /// 场景卸载
        /// </summary>
        /// <param name="sceneType"></param>
        /// <param name="callback"></param>
        public static void UnSceneLoad(EnumSceneType sceneType, UnityAction callback)
        {
            //Farme.SceneLoad.UnLoadSceneAsync()
        }
        #endregion

        #region Quit
        /// <summary>
        /// 程序退出
        /// </summary>
        public static void ProgramExit()
        {
            Application.Quit();
        }
        #endregion

        #region DataSave      
        /// <summary>
        /// 保存关卡数据到本地(程序退出时调用)
        /// </summary>
        public static void SaveLevelDataToThisLocality()
        {
            LevelConfigManager.SaveLevelConfig();
        }
        #endregion
        /// <summary>
        /// 移除协程
        /// </summary>
        private static void RemoveCor()
        {
            if(m_Cor!=null)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_Cor);
                m_Cor = null;
            }
        }
    }
}
