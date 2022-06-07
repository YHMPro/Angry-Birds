using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using UnityEngine.UI;
using Bird_VS_Boar.LevelConfig;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏关卡面板
    /// </summary>
    public class GameLevelPanel : BasePanel
    {       
        /// <summary>
        /// 关卡矩形框
        /// </summary>
        private RectTransform m_LevelRect;
        /// <summary>
        /// 背景
        /// </summary>
        private Image m_Bg;
        /// <summary>
        /// 返回按钮
        /// </summary>
        private ElasticBtn m_ReturnBtn;
        /// <summary>
        /// 下一个季节
        /// </summary>
        private ElasticBtn m_NextSeasonBtn;
        /// <summary>
        /// 上一个季节
        /// </summary>
        private ElasticBtn m_LastSeasonBtn;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Image>();
            RegisterComponentsTypes<ElasticBtn>();
            RegisterComponentsTypes<RectTransform>();
            m_Bg=GetComponent<Image>("Bg");
            m_ReturnBtn = GetComponent<ElasticBtn>("ReturnBtn");
            m_NextSeasonBtn = GetComponent<ElasticBtn>("NextSeason");
            m_LastSeasonBtn = GetComponent<ElasticBtn>("LastSeason");
            m_LevelRect = GetComponent<RectTransform>("LevelRect");
        }

        protected override void Start()
        {
            base.Start();
            m_ReturnBtn.onClick.AddListener(OnReturn);
            m_NextSeasonBtn.onClick.AddListener(OnNextSeason);
            m_LastSeasonBtn.onClick.AddListener(OnLastSeason);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            RefreshPanel();
        }
        #region ElasticBtn
        /// <summary>
        /// 监听返回
        /// </summary>
        private void OnReturn()
        {
            GameManager.NowLevelIndex = -1;
            GameManager.NowLevelType = EnumGameLevelType.None;
            if (!WindowRoot.Exists)
            {
                return;
            }
            StandardWindow gameLoginWindow = WindowRoot.GetSingleton().GetWindow("GameLoginWindow");
            if (gameLoginWindow == null)
            {
                return;
            }          
            if (gameLoginWindow.GetPanel("GameLevelPanel", out GameLevelPanel gameLevelPanel))
            {
                gameLevelPanel.SetState(EnumPanelState.Hide, () =>//隐藏游戏关卡类型面板
                {
                    if (gameLoginWindow.GetPanel("GameLevelTypePanel", out GameLevelTypePanel gameLevelTypePanel))
                    {
                        gameLevelTypePanel.SetState(EnumPanelState.Show);
                    }
                    else
                    {
                        gameLoginWindow.CreatePanel<GameLevelTypePanel>("UI/GameLoginWindow/GameLevelTypePanel", "GameLevelTypePanel", EnumPanelLayer.BOTTOM, (panel) =>
                        {
                            Debuger.Log(panel);
                        });
                    }

                });
            }
        }
        /// <summary>
        /// 监听下一个季节
        /// </summary>
        private void OnNextSeason()
        {
            int season = (int)GameManager.NowLevelType+1;
            if(season==5)
            {
                season = 1;
            }
            GameManager.NowLevelType = (EnumGameLevelType)season;
            RefreshPanel();
        }
        /// <summary>
        /// 监听上一个季节
        /// </summary>
        private void OnLastSeason()
        {
            int season = (int)GameManager.NowLevelType - 1;
            if (season == 0)
            {
                season = 4;
            }
            GameManager.NowLevelType = (EnumGameLevelType)season;
            RefreshPanel();
        }
        #endregion

        #region RefreshPanel
        /// <summary>
        /// 刷新面板
        /// </summary>
        private void RefreshPanel()
        {          
            if (!OtherConfigInfo.Exists)
            {
                Debuger.LogError("配置信息未实例化");
                return;
            }
            OtherConfigInfo otherConfigInfo = OtherConfigInfo.GetSingleton();
            //更新关卡界面背景
            //Debug.Log("更改标记");
            string[] data = ProjectTool.ParsingRESPath(GameManager.NowSeasonConfigInfo.GetLevelInterfaceBGSpritePath());
            m_Bg.sprite = AssetBundleLoad.LoadAsset<Sprite>(data[0], data[1]);
            //m_Bg.sprite = ResLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetLevelInterfaceBGSpritePath());
            //更新背景音乐
            GameAudio.PlayBackGroundAudio(GameManager.NowSeasonConfigInfo.GetSeasonAudioPath());           
            //回收关卡
            for (int index=0;index<m_LevelRect.childCount;index++)
            {
                GoReusePool.Put("Level", m_LevelRect.GetChild(index).gameObject);
            }
            //更新界面UI   1.获取该季节所包含的关卡数量
            int levelNum = LevelConfigManager.GetLevelNum(GameManager.NowLevelType);
            while(levelNum > 0)
            {
                if (!GoReusePool.Take("Level", out GameObject levelGo))
                {
                    if (!GoLoad.Take(otherConfigInfo.GetLevelPrefabPath(), out levelGo, m_LevelRect))
                    {
                        Debuger.LogError("关卡预制不存在");
                        return;
                    }
                }              
                --levelNum;
            }          
        }        
        #endregion 

    }
}
