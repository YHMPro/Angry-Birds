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
    public class Level : BaseMono
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
                    m_StarsDefault = ResourcesLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetStarDefaultSpritePath(), true);              
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
                    m_StarsFill = ResourcesLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetStarFillSpritePath(), true);               
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
        private UIBtn m_Btn;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<RectTransform>();
            RegisterComponentsTypes<Image>();
            RegisterComponentsTypes<Text>();
            m_Btn = GetComponent<UIBtn>();
            m_TextIndex=GetComponent<Text>("LevelIndex");
            m_Img = GetComponent<Image>();
            m_LevelLock = GetComponent<Image>("LevelLock");
            m_StarRect=GetComponent<RectTransform>("StarRect");
        }

        protected override void Start()
        {
            base.Start();
            m_Btn.OnPointerClickEvent.AddListener(OnClick);                  
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
            m_Img.sprite = ResourcesLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetLevelBGSpritePath(), true);         
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
            m_LevelLock.sprite = ResourcesLoad.Load<Sprite>(GameManager.NowSeasonConfigInfo.GetLevelLockSpritePath(),true);
            m_LevelLock.gameObject.SetActive(!levelConfig.IsThrough);
            m_Btn.Interactable = levelConfig.IsThrough;
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
            if(!MonoSingletonFactory<WindowRoot>.SingletonExist)
            {
                Debuger.LogError("窗口根节点丢失");
                return;
            }
            StandardWindow window = MonoSingletonFactory<WindowRoot>.GetSingleton().GetWindow("GameLoginWindow");
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
           
        }
        #endregion
    }
}
