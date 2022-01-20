using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.UI;
using Bird_VS_Boar.LevelConfig;
using Farme.Tool;
using Farme.UI;
using UnityEngine.SceneManagement;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 关卡按钮
    /// </summary>
    public class Level : BaseMono
    {
        /// <summary>
        /// 关卡索引
        /// </summary>
        private static int LevelIndex = 0;
        /// <summary>
        /// 关卡评星
        /// </summary>
        private int m_LevelRating = 0;
        [SerializeField]
        /// <summary>
        /// 默认星星
        /// </summary>
        private Sprite m_Stars_Default;
        [SerializeField]
        /// <summary>
        /// 填充星星
        /// </summary>
        private Sprite m_Stars_Fill;
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
            RegisterComponentsTypes<Image>();
            RegisterComponentsTypes<Text>();
            m_Btn = GetComponent<UIBtn>();
            m_TextIndex=GetComponent<Text>("LevelIndex");
        }

        protected override void Start()
        {
            base.Start();
            m_Btn.OnPointerClickEvent.AddListener(OnClick);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StarsFill(true);
             ++LevelIndex;
            RefreshUI();
        }

     
        protected override void OnDisable()
        {
            base.OnDisable();
            --LevelIndex;
        }
        #region RefreshUI
        private void RefreshUI()
        {          
            //获取关卡配置信息
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + LevelIndex);
            if(levelConfig == null)
            {
                Debuger.LogError("不存在此场景的配置");
                return;
            }
            m_TextIndex.text = LevelIndex.ToString();
            m_LevelRating = Mathf.Clamp(levelConfig.LevelRating,0,3);
            StarsFill();
        }
        #endregion

        #region Star
        /// <summary>
        /// 星星填充
        /// </summary>
        /// <param name="isReset">是否重置</param>
        private void StarsFill(bool isReset = false)
        {
            if (isReset)
            {
                for (int index = 1; index <= 3; index++)
                {
                    GetComponent<Image>("Star" + index).sprite = m_Stars_Default;//填充                                                                                    
                }
            }
            else
            {
                for (int index = 1; index <= m_LevelRating; index++)
                {
                    GetComponent<Image>("Star" + index).sprite = m_Stars_Fill;//填充                                                                                    
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
                    GameManager.NowLevelIndex = int.Parse(m_TextIndex.text);
                    GameManager.Init();
                    GameManager.GameStart();
                });          
            });
           
        }
        #endregion
    }
}
