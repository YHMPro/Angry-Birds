using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.Audio;

namespace Bird_VS_Boar
{
    public class GameAudio 
    {
        private static IAudioControl m_SlingShot;
        private static IAudioControl m_Button;
        private static IAudioControl m_BackGround;

        #region SlingShot
        public static void PlaySlingShotAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (m_SlingShot == null)
                {
                    m_SlingShot = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (m_SlingShot.SetAudioMixerGroup(group))
                        {
                            m_SlingShot.SetLoop(true);
                            (m_SlingShot as Audio2D).IsAutoRecycle = false;
                            Debug.Log("弹弓拉伸音效配置成功");
                        }
                    }
                }
                if (m_SlingShot.SetAudioClip(audioClip))
                {
                    m_SlingShot.Play();
                }
            }
        }

        public static void PauseSlingShotAudio()
        {
            if (m_SlingShot == null)
            {
                return;
            }
            m_SlingShot.Pause();
        }
        #endregion
        #region Bird
        /// <summary>
        /// 播放小鸟选择音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayBirdAudio(IAudioControl birdAC , string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {               
                if (birdAC.SetAudioClip(audioClip))
                {
                    birdAC.Play();
                }
            }

        }
        #endregion
        #region Button
        /// <summary>
        /// 播放小按钮选择音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayButtonAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (m_Button == null)
                {
                    m_Button = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (m_Button.SetAudioMixerGroup(group))
                        {
                            (m_Button as Audio2D).IsAutoRecycle = false;
                            Debug.Log("小鸟选择音效配置成功");
                        }
                    }
                }
                if (m_Button.SetAudioClip(audioClip))
                {
                    m_Button.Play();
                }
            }
        }
        #endregion
        #region BG
        /// <summary>
        /// 播放背景音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayBackGroundAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (m_BackGround == null)
                {
                    m_BackGround = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (m_BackGround.SetAudioMixerGroup(group))
                        {
                            (m_BackGround as Audio2D).IsAutoRecycle = false;
                            m_BackGround.SetLoop(true);
                            Debug.Log("小鸟选择音效配置成功");
                        }
                    }
                }
                if (m_BackGround.SetAudioClip(audioClip))
                {
                    m_BackGround.Play();
                }
            }
        }
        public static void PauseBackGroundAudio()
        {
            if (m_BackGround == null)
            {
                return;
            }
            m_BackGround.Pause();
        }
        #endregion

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayEffectAudio(string audioPath)
        {     
            if(AudioClipMgr.GetAudioClip(audioPath,out AudioClip audioClip ))
            {
                IAudioControl audioControl = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();             
                if(audioControl.SetAudioClip(audioClip))
                {
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (audioControl.SetAudioMixerGroup(group))
                        {
                            audioControl.Play();
                        }
                    }
                }
            }         
        }
    }
}
