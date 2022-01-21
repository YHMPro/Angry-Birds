using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using UnityEngine.UI;
using Farme.Extend;
using UnityEngine.EventSystems;
using Farme.Tool;
using DG.Tweening;
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
        private UIBtn m_SetBtn;
        private UIBtn m_LastLevelBtn;    
        private UIBtn m_ReturnLevelBtn;
        private UIBtn m_ReplayLevelBtn;
        private UIBtn m_NextLevelBtn;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Image>();
            RegisterComponentsTypes<UIBtn>();
            m_SetBtn=GetComponent<UIBtn>("SetBtn");
            m_LastLevelBtn = GetComponent<UIBtn>("LastLevelBtn");
            m_ReturnLevelBtn = GetComponent<UIBtn>("ReturnLevelBtn");
            m_ReplayLevelBtn = GetComponent<UIBtn>("ReplayLevelBtn");
            m_NextLevelBtn = GetComponent<UIBtn>("NextLevelBtn");
            m_ButtonListImg =GetComponent<Image>("ButtonList");
        }

        protected override void Start()
        {
            base.Start();
            m_SetBtn.OnPointerClickEvent.AddListener(OnSet);
            m_LastLevelBtn.OnPointerClickEvent.AddListener(OnLastLevel);
            m_ReturnLevelBtn.OnPointerClickEvent.AddListener(OnReturnLevel);
            m_ReplayLevelBtn.OnPointerClickEvent.AddListener(OnReplayLevel);
            m_NextLevelBtn.OnPointerClickEvent.AddListener(OnNextLevel);
            m_ButtonListImg.UIEventRegistered(EventTriggerType.PointerEnter, OnButtonListImgEnter);
            m_ButtonListImg.UIEventRegistered(EventTriggerType.PointerExit, OnButtonListImgExit);              
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


        #region 指针事件监听
        private void OnButtonListImgEnter(BaseEventData bEData)//进入
        {
            GameManager.GameControl(EnumGameControlType.Stop);
            Debuger.Log("测试:进入");
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
            Debuger.Log("测试:离开");
        }
        #endregion
    }
}
