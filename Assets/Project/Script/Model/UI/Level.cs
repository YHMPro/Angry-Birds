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
    /// <summary>
    /// 关卡按钮
    /// </summary>
    public class Level : MonoBase
    {
        /// <summary>
        /// 背景
        /// </summary>
        private Image m_Img;
        /// <summary>
        /// 关卡锁
        /// </summary>
        private Image m_LevelLock;
        /// <summary>
        /// 星星矩形框
        /// </summary>
        private RectTransform m_StarRect;
        /// <summary>
        /// 关卡索引
        /// </summary>
        private int m_LevelIndex = 0;
        /// <summary>
        /// 关卡评星
        /// </summary>
        private int m_LevelRating = 0;
        /// <summary>
        /// 默认星星
        /// </summary>
        private static Sprite m_StarsDefault = null;
        /// <summary>
        /// 默认星星
        /// </summary>
        private static Sprite StarsDefault
        {
            get
            {
                if(m_StarsDefault == null)
                {
                    //Debug.Log("更改标记");
                    string[] data = ProjectTool.ParsingRESPath(GameManager.NowSeasonConfigInfo.GetStarDefaultSpritePath());
                    m_StarsDefault=AssetBundleLoad.LoadAsset<Sprite>(data[0], data[1]);
                    //m_StarsDefault = ResLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetStarDefaultSpritePath());              
                }
                return m_StarsDefault;
            }
        }
        /// <summary>
        /// 填充星星
        /// </summary>
        private static Sprite m_StarsFill = null;
        /// <summary>
        /// 填充星星
        /// </summary>
        private static Sprite StarsFill
        {
            get
            {
                if (m_StarsFill == null)
                {
                    //Debug.Log("更改标记");
                    string[] data = ProjectTool.ParsingRESPath(GameManager.NowSeasonConfigInfo.GetStarFillSpritePath());
                    m_StarsFill = AssetBundleLoad.LoadAsset<Sprite>(data[0], data[1]);
                    //m_StarsFill = ResLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetStarFillSpritePath());               
                }
                return m_StarsFill;
            }
        }
        /// <summary>
        /// 关卡索引文本
        /// </summary>
        private Text m_TextIndex;
        /// <summary>
        /// 按钮
        /// </summary>
        private ElasticBtn m_Btn;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<RectTransform>();
            RegisterComponentsTypes<Image>();
            RegisterComponentsTypes<Text>();
            m_Btn = GetComponent<ElasticBtn>();
            m_TextIndex=GetComponent<Text>("LevelIndex");
            m_Img = GetComponent<Image>();
            m_LevelLock = GetComponent<Image>("LevelLock");
            m_StarRect=GetComponent<RectTransform>("StarRect");
        }

        protected override void Start()
        {
            base.Start();
            m_Btn.onClick.AddListener(OnClick);                  
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StarsFillFun(true);
            
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            RefreshUI();
        }

        protected override void OnDisable()
        {         
            base.OnDisable();
          
        }
        #region RefreshUI
        private void RefreshUI()
        {
            #region 更新背景
            //Debug.Log("更改标记");
            string[] data = ProjectTool.ParsingRESPath(GameManager.NowSeasonConfigInfo.GetLevelBGSpritePath());
            m_Img.sprite = AssetBundleLoad.LoadAsset<Sprite>(data[0], data[1]);
            //m_Img.sprite = ResLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetLevelBGSpritePath());         
            #endregion
            #region 更新关卡所欲
            m_LevelIndex = 1;
            for (int index=0;index< transform.GetSiblingIndex();index++)
            {
                m_LevelIndex += transform.parent.GetChild(index).gameObject.activeInHierarchy ? 1 : 0;
            }         
            //获取关卡配置信息
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + m_LevelIndex);
            if(levelConfig == null)
            {
                Debuger.LogError("不存在此场景的配置:\n关卡类型:"+ GameManager.NowLevelType+"\n关卡索引:"+ m_LevelIndex);
                return;
            }
            //Debug.Log("更改标记");
            data = ProjectTool.ParsingRESPath(GameManager.NowSeasonConfigInfo.GetLevelLockSpritePath());
            m_LevelLock.sprite = AssetBundleLoad.LoadAsset<Sprite>(data[0], data[1]);
            //m_LevelLock.sprite = ResLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetLevelLockSpritePath());
            m_LevelLock.gameObject.SetActive(!levelConfig.IsThrough);
            m_Btn.interactable = levelConfig.IsThrough;
            m_StarRect.gameObject.SetActive(levelConfig.IsThrough);
            m_TextIndex.text = m_LevelIndex.ToString();
            m_LevelRating = Mathf.Clamp(levelConfig.LevelHistoryRating, 0,3);
            StarsFillFun();
            #endregion
        }
        #endregion

        #region Star
        /// <summary>
        /// 星星填充
        /// </summary>
        /// <param name="isReset">是否重置</param>
        private void StarsFillFun(bool isReset = false)
        {
            if (isReset)
            {
                for (int index = 1; index <= 3; index++)
                {
                    GetComponent<Image>("Star" + index).sprite = StarsDefault;//填充                                                                                    
                }
            }
            else
            {
                for (int index = 1; index <= m_LevelRating; index++)
                {
                    GetComponent<Image>("Star" + index).sprite = StarsFill;//填充                                                                                    
                }
            }
        }
        #endregion

        #region Button
        private void OnClick()
        {                 
            //关闭游戏登入窗口
            if(!WindowRoot.Exists)
            {
                Debuger.LogError("窗口根节点丢失");
                return;
            }
            StandardWindow window = WindowRoot.GetSingleton().GetWindow("GameLoginWindow");
            if(window==null)
            {
                Debuger.LogError("登入窗口实例不存在");
                return;
            }
            window.SetState(EnumWindowState.Destroy, () =>//销毁游戏登入窗口
            {
                GameManager.SceneLoad(EnumSceneType.GameScene, () =>
                {
                    GameManager.NowLevelIndex = m_LevelIndex;
                    GameManager.Init();
                    GameManager.GameStart();
                });          
            });
            StandardWindow gameGlobalWindow = WindowRoot.GetSingleton().GetWindow("GameGlobalWindow");
            if (gameGlobalWindow == null)
            {
                Debuger.LogError("全局窗口实例不存在");
                return;
            }
            if (gameGlobalWindow.GetPanel("GameCheatPanel", out GameCheatPanel gameCheatPanel))
            {
                gameCheatPanel.SetState(EnumPanelState.Show);
            }
        }
        #endregion
    }
}
