using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.UI;
using UnityEngine.UI;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏设置面板
    /// </summary>
    public class GameSetPanel : BasePanel
    {
        private Slider m_GlobalVolume;
        private Slider m_ButtonVolume;
        private Slider m_BgVolume;
        private Slider m_EffectVolume;
        private ElasticBtn m_DataControlBtn;
        private ElasticBtn m_CloseBtn;
        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<Slider>();
            RegisterComponentsTypes<ElasticBtn>();

            m_GlobalVolume=GetComponent<Slider>("GlobalVolume");
            m_ButtonVolume =GetComponent<Slider>("ButtonVolume");
            m_BgVolume=GetComponent<Slider>("BGVolume");
            m_EffectVolume=GetComponent<Slider>("EffectVolume");
            m_DataControlBtn = GetComponent<ElasticBtn>("DataControlBtn");
            m_CloseBtn = GetComponent<ElasticBtn>("CloseBtn");

            
        }

        protected override void Start()
        {
            base.Start();
            m_CloseBtn.onClick.AddListener(OnClose);
            m_DataControlBtn.onClick.AddListener(OnDataControl);
            m_GlobalVolume.onValueChanged.AddListener(OnGlobalVolume);
            m_ButtonVolume.onValueChanged.AddListener(OnButtonVolume);
            m_BgVolume.onValueChanged.AddListener(OnBgVolume);
            m_EffectVolume.onValueChanged.AddListener(OnEffectVolume);
        }

        protected override void OnDestroy()
        {
            m_GlobalVolume.onValueChanged.RemoveListener(OnGlobalVolume);
            m_ButtonVolume.onValueChanged.RemoveListener(OnButtonVolume);
            m_BgVolume.onValueChanged.RemoveListener(OnBgVolume);
            m_EffectVolume.onValueChanged.RemoveListener(OnEffectVolume);
            base.OnDestroy();
        }
        #region 刷新面板
        public void RefreshPanel()
        {
            m_GlobalVolume.value = PlayerPrefs.GetFloat("GlobalVolume", 1);
            m_ButtonVolume.value = PlayerPrefs.GetFloat("ButtonVolume", 1);
            m_BgVolume.value = PlayerPrefs.GetFloat("BgVolume", 1);
            m_EffectVolume.value = PlayerPrefs.GetFloat("EffectVolume", 1);
            OnGlobalVolume(m_GlobalVolume.value);
            OnButtonVolume(m_ButtonVolume.value);
            OnBgVolume(m_BgVolume.value);
            OnEffectVolume(m_EffectVolume.value);
        }
        #endregion
        /// <summary>
        /// 数据控制按钮激活控制
        /// </summary>
        /// <param name="active"></param>
        public void ActiveDataControl(bool active)
        {
            m_DataControlBtn.interactable = active;
        }

        #region ButtonEvent
        private void OnClose()
        {
            SetState(EnumPanelState.Hide, () => 
            {
                GameManager.GameControl(EnumGameControlType.Continue);
            });
        }
        private void OnDataControl()
        {
            GameManager.ResetLevelData();
        }
        #endregion

        #region SliderEvent
        private void OnGlobalVolume(float volume)
        {
            GameAudio.VolumeControl(EnumAudioType.Master, volume);
            PlayerPrefs.SetFloat("GlobalVolume", volume);          
        }
        private void OnButtonVolume(float volume)
        {
            GameAudio.VolumeControl(EnumAudioType.Button, volume);
            PlayerPrefs.SetFloat("ButtonVolume", volume);
        }
        private void OnBgVolume(float volume)
        {
            GameAudio.VolumeControl(EnumAudioType.BackGround, volume);
            PlayerPrefs.SetFloat("BgVolume", volume);
        }
        private void OnEffectVolume(float volume)
        {
            GameAudio.VolumeControl(EnumAudioType.Effect, volume);
            PlayerPrefs.SetFloat("EffectVolume", volume);
        }
        #endregion
    }
}
