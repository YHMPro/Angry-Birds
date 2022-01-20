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
        /// 用于缓存关卡的矩形框
        /// </summary>
        private RectTransform m_LevelRectCache;
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
        private UIBtn m_ReturnBtn;
        /// <summary>
        /// 下一个季节
        /// </summary>
        private UIBtn m_NextSeasonBtn;
        /// <summary>
        /// 上一个季节
        /// </summary>
        private UIBtn m_LastSeasonBtn;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Image>(true);
            RegisterComponentsTypes<UIBtn>(true);
            RegisterComponentsTypes<RectTransform>(true);
            m_Bg=GetComponent<Image>("Bg");
            m_ReturnBtn = GetComponent<UIBtn>("ReturnBtn");
            m_NextSeasonBtn = GetComponent<UIBtn>("NextSeason");
            m_LastSeasonBtn = GetComponent<UIBtn>("LastSeason");
            m_LevelRect = GetComponent<RectTransform>("LevelRect");
            m_LevelRectCache = GetComponent<RectTransform>("LevelRectCache");
        }

        protected override void Start()
        {
            base.Start();
            m_ReturnBtn.OnPointerClickEvent.AddListener(OnReturn);
            m_NextSeasonBtn.OnPointerClickEvent.AddListener(OnNextSeason);
            m_LastSeasonBtn.OnPointerClickEvent.AddListener(OnLastSeason);
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            RefreshPanel();
        }
        #region UIBtn
        /// <summary>
        /// 监听返回
        /// </summary>
        private void OnReturn()
        {
            GameManager.NowLevelIndex = -1;
            GameManager.NowLevelType = EnumGameLevelType.None;

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
            if(!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                Debuger.LogError("配置信息未实例化");
                return;
            }
            OtherConfigInfo otherConfigInfo = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton();
            if (!SeasonConfigInfo.SeasonConfigInfoDic.TryGetValue(GameManager.NowLevelType,out SeasonConfigInfo seasonConfigInfo))
            {
                Debuger.LogError("没有找到季节配置信息");
                return;
            }
            //更新背景音乐
            GameAudio.PlayBackGroundAudio(seasonConfigInfo.GetSeasonAudioPath());   
            //回收关卡
            for(int index=0;index<m_LevelRect.childCount;index++)
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
