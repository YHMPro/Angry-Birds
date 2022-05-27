using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 音效类型
    /// </summary>
    public enum EnumAudioType
    {
        /// <summary>
        /// 全局
        /// </summary>
        Master,
        /// <summary>
        /// 按钮
        /// </summary>
        Button,
        /// <summary>
        /// 背景
        /// </summary>
        BackGround,
        /// <summary>
        /// 特效
        /// </summary>
        Effect
    }
    /// <summary>
    /// 游戏音效
    /// </summary>
    public class GameAudio 
    {
        private static Audio m_BackGroundOnce = null;
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
                m_Button = AudioManager.ApplyForAudio(false);
                m_Button.SpatialBlend = 0;//设置为2D
                m_Button.AbleRecycle = false;//不可自动回收
                m_Button.Group = AudioMixerManager.GetAudioMixerGroup("Button");
            }
            if (!OtherConfigInfo.Exists)
            {
                return;
            }
            OtherConfigInfo otherConfigInfo = OtherConfigInfo.GetSingleton();
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
                m_BackGround = AudioManager.ApplyForAudio(false);
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
        /// <summary>
        /// 播放背景音效(只播放一次后自动回收)
        /// </summary>
        /// <param name="audioPath">音效路径</param>
        public static void PlayBackGroundAudioOnce(string audioPath)
        {
            if (m_BackGroundOnce == null)
            {
                m_BackGroundOnce = AudioManager.ApplyForAudio(false);
                m_BackGroundOnce.Loop = false;//循环
                m_BackGroundOnce.SpatialBlend = 0;//设置为2D
                m_BackGroundOnce.AbleRecycle = false;//不可自动回收
                m_BackGroundOnce.Group = AudioMixerManager.GetAudioMixerGroup("BackGround");
            }
            if (AudioClipManager.GetAudioClip(audioPath, out AudioClip clip))
            {
                m_BackGroundOnce.Clip = clip;
                m_BackGroundOnce.Play();
            }
        }
        /// <summary>
        /// 音量控制
        /// </summary>
        /// <param name="audioType">音效类型</param>
        /// <param name="volume">音量</param>
        public static void VolumeControl(EnumAudioType audioType,float volume)
        {
            AudioManager.SetVolume(audioType.ToString(), audioType.ToString() + "Volume", volume);
        }
    }
}
