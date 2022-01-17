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
        /// 分数文本
        /// </summary>
        private Text m_ScoreText;

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Text>();
            RegisterComponentsTypes<Image>();
            m_WinGo = transform.Find("GameWinRect").gameObject;
            m_LoseGo= transform.Find("GameLoseRect").gameObject;
            m_ScoreText=GetComponent<Text>("ScoreText");
        }

        protected override void Start()
        {
            base.Start();

        }
        #region Star
        /// <summary>
        /// 星星填充
        /// </summary>
        private void StarsFill(bool isReset)
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
            if (NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                yield break;
            }
            WaitForSeconds waitFor = new WaitForSeconds(m_StarShowTimeInterval);
            for(int index=1;index<=3;index++)
            {
                GetComponent<Image>("Star" + index).sprite = m_Stars_Fill[index - 1];//填充               
                PlayAudio(NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().GetStarAudioPath(index - 1));//播放声音
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
    }
}
