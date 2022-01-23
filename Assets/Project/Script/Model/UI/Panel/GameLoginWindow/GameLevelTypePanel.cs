using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using Farme;
using System;
using UnityEngine.UI;
using Farme.Tool;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏关卡类型面板
    /// </summary>
    public class GameLevelTypePanel : BasePanel
    {
        /// <summary>
        /// 界面背景
        /// </summary>
        private Image m_Bg;
        /// <summary>
        /// 返回按钮
        /// </summary>
        private UIBtn m_ReturnBtn;
        /// <summary>
        /// 关卡类型矩形框
        /// </summary>
        private Image m_LevelTypeRectImg;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<UIBtn>();
            RegisterComponentsTypes<Image>();

            m_ReturnBtn=GetComponent<UIBtn>("ReturnBtn");
            m_LevelTypeRectImg = GetComponent<Image>("LevelTypeRect");
            m_Bg = GetComponent<Image>("Bg");
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            Debuger.Log("播放关卡类型面板的音乐");
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            RefreshPanel();
        }

        protected override void Start()
        {
            base.Start();
            m_ReturnBtn.OnPointerClickEvent.AddListener(OnReturn);
            if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                OtherConfigInfo otherConfigInfo = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton();
                //加载关卡类型界面背景
                m_Bg.sprite = ResourcesLoad.Load<Sprite>(otherConfigInfo.GetLevelTypeInterfaceBGSpritePath(), true);
                foreach (var levelType in Enum.GetValues(typeof(EnumGameLevelType)))
                {
                    if ((EnumGameLevelType)levelType != EnumGameLevelType.None)
                    {
                        if (GoLoad.Take(otherConfigInfo.GetLevelTypePrefabPath(), out GameObject levelTypeGo, m_LevelTypeRectImg.rectTransform))
                        {
                            levelTypeGo.GetComponent<LevelType>().GameLevelType = (EnumGameLevelType)levelType;
                        }
                    }
                }
            }
        }


        #region RefreshPanel
        /// <summary>
        /// 刷新面板
        /// </summary>
        private void RefreshPanel()
        {
            if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                OtherConfigInfo otherConfigInfo = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton();
                //更新背景音乐
                GameAudio.PlayBackGroundAudio(otherConfigInfo.GetLevelTypePanelAudioPath());
            }

        }
        #endregion

        #region Button
        private void OnReturn()
        {

        }
        #endregion
    }
}
