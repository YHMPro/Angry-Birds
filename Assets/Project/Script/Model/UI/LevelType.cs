using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.UI;
using Bird_VS_Boar.LevelConfig;
using Farme.Tool;
using Farme.UI;
namespace Bird_VS_Boar
{
    public class LevelType : BaseMono
    {
        /// <summary>
        /// 按钮
        /// </summary>
        private UIBtn m_Btn;
        /// <summary>
        /// 背景
        /// </summary>
        private Image m_Bg;
        /// <summary>
        /// 星星数量文本
        /// </summary>
        private Text m_StarText;
        [SerializeField]
        /// <summary>
        /// 关卡类型
        /// </summary>
        private EnumGameLevelType m_GameLevelType=EnumGameLevelType.None;
        /// <summary>
        /// 关卡类型
        /// </summary>
        public EnumGameLevelType GameLevelType
        {
            get { return m_GameLevelType; }
            set 
            {   if(m_GameLevelType!= EnumGameLevelType.None)//限制每次生成只能设置一次
                {
                    return;
                }
                m_GameLevelType = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Text>();
            m_Bg=GetComponent<Image>();
            m_Btn=GetComponent<UIBtn>();
            m_StarText=GetComponent<Text>("StarText");
        }

        protected override void Start()
        {
            base.Start();
            m_Btn.OnPointerClickEvent.AddListener(OnClick);
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            RefreshUI();
        }
        #region RefreshUI
        /// <summary>
        /// 刷新UI
        /// </summary>
        private void RefreshUI()
        {
            if(m_GameLevelType==EnumGameLevelType.None)
            {
                return;
            }
            int levelNum = LevelConfigManager.GetLevelNum(m_GameLevelType);
            int starTotal = 0;
            for (int index=1; index <= levelNum; index++)
            {
                starTotal += LevelConfigManager.GetLevelConfig(m_GameLevelType + "_" + index).LevelRating;
            }
            Debuger.Log("关卡:" + m_GameLevelType.ToString() + "\n星星总获得数:" + starTotal);
            m_StarText.text = starTotal.ToString();
            if(SeasonConfigInfo.SeasonConfigInfoDic.TryGetValue(m_GameLevelType,out SeasonConfigInfo info))
            {
                if(m_Bg.sprite==null)
                {
                    m_Bg.sprite = ResourcesLoad.Load<Sprite>(info.GetLevelTypeBGSpritePath(),true);
                }
            }
        }
        #endregion
        #region Button
        private void OnClick()
        {
            if(!MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                return;
            }
            StandardWindow gameLoginWindow = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameLoginWindow");
            if(gameLoginWindow==null)
            {
                return;
            }
            GameManager.NowLevelType = m_GameLevelType;
            if(gameLoginWindow.GetPanel("GameLevelTypePanel",out GameLevelTypePanel gameLevelTypePanel))
            {
                gameLevelTypePanel.SetState(EnumPanelState.Hide, () =>//隐藏游戏关卡类型面板
                {
                    if(gameLoginWindow.GetPanel("GameLevelPanel",out GameLevelPanel gameLevelPanel))
                    {
                        gameLevelPanel.SetState(EnumPanelState.Show);
                    }
                    else
                    {
                        gameLoginWindow.CreatePanel<GameLevelPanel>("UI/GameLoginWindow/GameLevelPanel", "GameLevelPanel", EnumPanelLayer.MIDDLE, (panel) =>
                        {
                            Debuger.Log(panel);
                        });
                    }
                   
                });
            }
        }
        #endregion
    }
}
