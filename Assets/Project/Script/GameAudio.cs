using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏音效
    /// </summary>
    public class GameAudio 
    {
        /// <summary>
        /// 按钮音效
        /// </summary>
        private static Audio m_Button = null;
        /// <summary>
        /// 背景音效
        /// </summary>
        private static Audio m_BackGround = null;
        /// <summary>
        /// 播放按钮音效
        /// </summary>
        public static void PlayButtonAudio()
        {
            if (m_Button == null)
            {
                m_Button = AudioManager.ApplyForAudio();
                m_Button.SpatialBlend = 0;//设置为2D
                m_Button.AbleRecycle = false;//不可自动回收
                m_Button.Group = AudioMixerManager.GetAudioMixerGroup("Button");
            }
            if (!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
            {
                return;
            }
            OtherConfigInfo otherConfigInfo = NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton();
            if (AudioClipManager.GetAudioClip(otherConfigInfo.GetButtonAudioPath(), out AudioClip clip))
            {
                m_Button.Clip = clip;
                m_Button.Play();
            }
        }
        /// <summary>
        /// 播放背景音效
        /// </summary>
        public static void PlayBackGroundAudio(string audioPath)
        {          
            if (m_BackGround == null)
            {
                m_BackGround = AudioManager.ApplyForAudio();
                m_BackGround.Loop = true;//循环
                m_BackGround.SpatialBlend = 0;//设置为2D
                m_BackGround.AbleRecycle = false;//不可自动回收
                m_BackGround.Group = AudioMixerManager.GetAudioMixerGroup("BackGround");
            }            
            if (AudioClipManager.GetAudioClip(audioPath, out AudioClip clip))
            {
                m_BackGround.Clip = clip;
                m_BackGround.Play();
            }
        }
        
        
    }
}
