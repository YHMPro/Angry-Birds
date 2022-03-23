using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.UI;
using Farme.Tool;
using UnityEngine.UI;

namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏登入面板
    /// </summary>
    public class GameLoginPanel : BasePanel
    {
        private UIBtn m_StartBtn;
        private UIBtn m_SetBtn;


        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<UIBtn>();
            m_StartBtn = GetComponent<UIBtn>("StartBtn");
            m_SetBtn = GetComponent<UIBtn>("SetBtn");
        }

        protected override void Start()
        {
            base.Start();
            m_StartBtn.OnPointerClickEvent.AddListener(OnStart);
            m_SetBtn.OnPointerClickEvent.AddListener(OnSet);
        }

        #region ButtonEvent
        private void OnStart()
        {
            StandardWindow window = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameLoginWindow");
            if (window == null)
            {
                Debuger.LogError("窗口GameLoginWindow不存在");
                return;
            }
            SetState(EnumPanelState.Destroy, () =>
            {
                window.CreatePanel<GameLevelTypePanel>("UI/GameLoginWindow/GameLevelTypePanel", "GameLevelTypePanel", EnumPanelLayer.MIDDLE, (panel) =>//加载面板
                {

                });
            });    
        }
        private void OnSet()
        {
            StandardWindow window = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameGlobalWindow");
            if (window == null || !window.GetPanel<GameSetPanel>("GameSetPanel", out var panel))
            {
                Debuger.LogError("窗口GameSceneWindow不存在或面板GameOverPanel不存在!!!");
                return;
            }
            panel.SetState(EnumPanelState.Show, () =>
            {
                panel.ActiveDataControl(true);
            });
        }
        #endregion
    }
}
