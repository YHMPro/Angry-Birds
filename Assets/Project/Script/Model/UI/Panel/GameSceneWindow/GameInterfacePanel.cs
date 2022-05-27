using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using UnityEngine.UI;
using Farme.Extend;
using UnityEngine.EventSystems;
using Farme.Tool;
using DG.Tweening;
using Bird_VS_Boar.LevelConfig;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏界面面板
    /// </summary>
    public class GameInterfacePanel : BasePanel
    {
        [SerializeField]
        private Ease m_Ease=Ease.InSine;
        private Image m_ButtonListImg;
        private ElasticBtn m_SetBtn;
        private ElasticBtn m_LastLevelBtn;    
        private ElasticBtn m_ReturnLevelBtn;
        private ElasticBtn m_ReplayLevelBtn;
        private ElasticBtn m_NextLevelBtn;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Image>();
            RegisterComponentsTypes<ElasticBtn>();
            m_SetBtn=GetComponent<ElasticBtn>("SetBtn");
            m_LastLevelBtn = GetComponent<ElasticBtn>("LastLevelBtn");
            m_ReturnLevelBtn = GetComponent<ElasticBtn>("ReturnLevelBtn");
            m_ReplayLevelBtn = GetComponent<ElasticBtn>("ReplayLevelBtn");
            m_NextLevelBtn = GetComponent<ElasticBtn>("NextLevelBtn");
            m_ButtonListImg =GetComponent<Image>("ButtonList");
        }

        protected override void Start()
        {
            base.Start();
            m_SetBtn.onClick.AddListener(OnSet);
            m_LastLevelBtn.onClick.AddListener(OnLastLevel);
            m_ReturnLevelBtn.onClick.AddListener(OnReturnLevel);
            m_ReplayLevelBtn.onClick.AddListener(OnReplayLevel);
            m_NextLevelBtn.onClick.AddListener(OnNextLevel);
            m_ButtonListImg.UIEventRegistered(EventTriggerType.PointerEnter, OnButtonListImgEnter);
            m_ButtonListImg.UIEventRegistered(EventTriggerType.PointerExit, OnButtonListImgExit);
            RefreshPanel();
        }
        protected override void OnDestroy()
        {            
            m_ButtonListImg.UIEventRemove(EventTriggerType.PointerEnter, OnButtonListImgEnter);
            m_ButtonListImg.UIEventRemove(EventTriggerType.PointerExit, OnButtonListImgExit);
            base.OnDestroy();
        }

        #region Button
        private void OnSet()
        {
            GameManager.GameControl(EnumGameControlType.Stop);
            //m_SetBtn.gameObject.SetActive(false);
            //打开设置面板
            StandardWindow window = WindowRoot.GetSingleton().GetWindow("GameGlobalWindow");
            if (window == null || !window.GetPanel<GameSetPanel>("GameSetPanel", out var panel))
            {
                Debuger.LogError("窗口GameSceneWindow不存在或面板GameOverPanel不存在!!!");
                return;
            }
            panel.SetState(EnumPanelState.Show, () =>
            {
                panel.ActiveDataControl(false);
            });
        }
        private void OnLastLevel()
        {            
            GameManager.LastLevel();      
        }

        private void OnReturnLevel()
        {
            GameManager.GameControl(EnumGameControlType.Continue);
            GameManager.ReturnLevel();
        }

        private void OnReplayLevel()
        {
            GameManager.ReplayLevel();
        }

        private void OnNextLevel()
        {        
            GameManager.NextLevel();     
        }
        #endregion


        #region 刷新面板
        public void RefreshPanel()
        {
            int levelNum = LevelConfigManager.GetLevelNum(GameManager.NowLevelType);            
            #region 按钮更新
            m_LastLevelBtn.interactable = GameManager.NowLevelIndex > 1;
            m_NextLevelBtn.interactable = GameManager.NowLevelIndex < levelNum ? LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + (GameManager.NowLevelIndex + 1)).IsThrough : false;
            #endregion
        }
        #endregion
        #region 指针事件监听
        private void OnButtonListImgEnter(BaseEventData bEData)//进入
        {
            GameManager.GameControl(EnumGameControlType.Stop);
            m_ButtonListImg.transform.DOLocalMoveX(960f, 0.5f).SetEase(m_Ease).SetUpdate(true);
            
        }
        private void OnButtonListImgExit(BaseEventData bEData)//离开
        {        
            if (RelyWindow!=null)
            {
                if(RelyWindow.Raycast(out List<RaycastResult> resultLi))
                {
                    foreach(var result in resultLi)
                    {                        
                        if(Equals(result.gameObject, m_ButtonListImg.gameObject))
                        {
                            return;
                        }
                    }
                }               
            }
            GameManager.GameControl(EnumGameControlType.Continue);
            m_ButtonListImg.transform.DOLocalMoveX(1160f, 0.5f).SetEase(m_Ease).SetUpdate(true);
        }
        #endregion
    }
}
