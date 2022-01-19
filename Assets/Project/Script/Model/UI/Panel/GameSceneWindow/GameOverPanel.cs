using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using UnityEngine.UI;
using Farme.Audio;
using Farme.Tool;
using Farme;

namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏结束面板
    /// </summary>
    public class GameOverPanel : BasePanel
    {
        /// <summary>
        /// 音效
        /// </summary>
        private Audio m_Effect = null;
        [SerializeField]
        /// <summary>
        /// 星星显示的时间间隔
        /// </summary>
        private float m_StarShowTimeInterval = 0.5f;
        [SerializeField]
        /// <summary>
        /// 默认星星
        /// </summary>
        private Sprite[] m_Stars_Default = new Sprite[3];
        [SerializeField]
        /// <summary>
        /// 填充星星
        /// </summary>
        private Sprite[] m_Stars_Fill = new Sprite[3];
        /// <summary>
        /// 赢
        /// </summary>
        private GameObject m_WinGo;
        /// <summary>
        /// 输
        /// </summary>
        private GameObject m_LoseGo;
        /// <summary>
        /// 当前分数文本
        /// </summary>
        private Text m_NowScoreText;
        /// <summary>
        /// 历史最佳分数
        /// </summary>
        private Text m_HistoryScoreText;
        /// <summary>
        /// 重玩本关
        /// </summary>
        private UIBtn m_ReplayLevelBtn;
        /// <summary>
        /// 上一关
        /// </summary>
        private UIBtn m_LastLevelBtn;
        /// <summary>
        /// 下一关
        /// </summary>
        private UIBtn m_NextLevelBtn;
        /// <summary>
        /// 返回关卡
        /// </summary>
        private UIBtn m_ReturnLevelBtn;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Text>(true);
            RegisterComponentsTypes<Image>(true);
            RegisterComponentsTypes<UIBtn>(true);
            m_WinGo = transform.Find("GameWinRect").gameObject;
            m_LoseGo= transform.Find("GameLoseRect").gameObject;
            m_NowScoreText = GetComponent<Text>("NowScoreText");
            m_HistoryScoreText = GetComponent<Text>("HistoryScoreText");
            m_ReplayLevelBtn = GetComponent<UIBtn>("ReplayLevelBtn");
            m_NextLevelBtn = GetComponent<UIBtn>("NextLevelBtn");
            m_LastLevelBtn = GetComponent<UIBtn>("LastLevelBtn");
            m_ReturnLevelBtn = GetComponent<UIBtn>("ReturnLevelBtn");
        }

        protected override void Start()
        {
            base.Start();
            m_ReplayLevelBtn.OnPointerClickEvent.AddListener(OnReplayLevel);
            m_NextLevelBtn.OnPointerClickEvent.AddListener(OnNextLevel);
            m_LastLevelBtn.OnPointerClickEvent.AddListener(OnLastLevel);
            m_ReturnLevelBtn.OnPointerClickEvent.AddListener(OnReturnLevel);
            StarsFill(true);
        }


        public void Update()
        {
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_WinGo.gameObject.SetActive(false);
            m_LoseGo.gameObject.SetActive(false);
            m_NextLevelBtn.gameObject.SetActive(true);
            StarsFill(true);
            StopCoroutine(IEStarsFill());
            RecyclyAudio();
        }

        protected override void OnDestroy()
        {         
            base.OnDestroy();
        }

        #region Win
        public void Win()
        {          
            m_WinGo.gameObject.SetActive(true);
            m_NowScoreText.text = GameLogic.NowScore.ToString();
            m_HistoryScoreText.text = GameLogic.HistoryScore.ToString();
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayRealtimeAction(0.5f,false,StarsFill);
        }
        #endregion

        #region Lose
        public void Lose()
        {
            m_NextLevelBtn.gameObject.SetActive(false);
            m_LoseGo.gameObject.SetActive(true);
            m_NowScoreText.text = GameLogic.NowScore.ToString();
            m_HistoryScoreText.text=GameLogic.HistoryScore.ToString();
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
                for(int index=1;index<=3;index++)
                {
                    GetComponent<Image>("Star" + index).sprite = m_Stars_Default[index - 1];//填充                                                                                    
                }
            }
            else
            {              
                StartCoroutine(IEStarsFill());           
            }
        }
        /// <summary>
        /// 星星填充协同程序
        /// </summary>
        private IEnumerator IEStarsFill()
        {
           
            if (!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                yield break;
            }
            OtherConfigInfo otherConfigInfo = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton();
            WaitForSecondsRealtime waitFor = new WaitForSecondsRealtime(m_StarShowTimeInterval);//使其不受Timescale影响
            for (int index=1;index<=3;index++)
            {
                GetComponent<Image>("Star" + index).sprite = m_Stars_Fill[index - 1];//填充星星               
                PlayAudio(otherConfigInfo.GetStarAudioPath(index - 1));//播放声音
                yield return waitFor;
            }
            //回收声音
            RecyclyAudio();
        }
        #endregion

        #region Audio
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioPath">音效路径</param>
        protected void PlayAudio(string audioPath)
        {
            if (audioPath == null)
            {
                Debuger.LogWarning(GetType().Name + ": 存在无效的音效路径，请查看对应配置表");
                return;
            }
            ApplyAudio();
            if (AudioClipManager.GetAudioClip(audioPath, out AudioClip clip))
            {
                m_Effect.Clip = clip;
                m_Effect.Play();
            }
        }
        /// <summary>
        /// 申请音效
        /// </summary>
        private void ApplyAudio()
        {
            if (m_Effect == null)
            {
                m_Effect = AudioManager.ApplyForAudio();
                m_Effect.SpatialBlend = 0;//设置为2D
                m_Effect.AbleRecycle = false;//不可自动回收
                m_Effect.Group = AudioMixerManager.GetAudioMixerGroup("Effect");
            }
        }
        /// <summary>
        /// 回收音效
        /// </summary>
        private void RecyclyAudio()
        {
            if (m_Effect != null)
            {
                m_Effect.AbleRecycle = true;
                m_Effect = null;
            }
        }
        #endregion

        #region Button
        /// <summary>
        /// 监听重玩本关
        /// </summary>
        private void OnReplayLevel()
        {
            
            SetState(EnumPanelState.Hide, () =>
            {            
                GameManager.ReplayLevel();
                GameManager.GameControl(EnumGameControlType.Continue);
            });
        }
        /// <summary>
        /// 监听上一关
        /// </summary>
        private void OnLastLevel()
        {
            SetState(EnumPanelState.Hide, () =>
            {              
                GameManager.LastLevel();
                GameManager.GameControl(EnumGameControlType.Continue);
            });
        }
        /// <summary>
        /// 监听下一关
        /// </summary>
        private void OnNextLevel()
        {
            SetState(EnumPanelState.Hide, () =>
            {               
                GameManager.NextLevel();
                GameManager.GameControl(EnumGameControlType.Continue);
            });
           
        }
        /// <summary>
        /// 监听返回关卡
        /// </summary>
        private void OnReturnLevel()
        {
            SetState(EnumPanelState.Destroy, () =>
            {               
                GameManager.ReturnLevel();
                GameManager.GameControl(EnumGameControlType.Continue);
            });     
        }
        #endregion
    }
}
