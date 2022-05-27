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
        /// 关卡类型矩形框
        /// </summary>
        private Image m_LevelTypeRectImg;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<ElasticBtn>();
            RegisterComponentsTypes<Image>();

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
            if (OtherConfigInfo.Exists)
            {
                OtherConfigInfo otherConfigInfo = OtherConfigInfo.GetSingleton();
                //加载关卡类型界面背景
                string[] data = ProjectTool.ParsingRESPath(otherConfigInfo.GetLevelTypeInterfaceBGSpritePath());
                m_Bg.sprite = AssetBundleLoad.LoadAsset<Sprite>(data[0], data[1]);
                //m_Bg.sprite = ResourcesLoad.Load<Sprite>(otherConfigInfo.GetLevelTypeInterfaceBGSpritePath(), true);
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
            if (OtherConfigInfo.Exists)
            {
                OtherConfigInfo otherConfigInfo = OtherConfigInfo.GetSingleton();
                //更新背景音乐
                GameAudio.PlayBackGroundAudio(otherConfigInfo.GetLevelTypePanelAudioPath());
            }

        }
        #endregion

        #region Button
       
        #endregion
    }
}
