using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.Audio;

namespace Bird_VS_Boar
{
    public class GameAudio 
    {
        private static IAudioControl slingShot;
        private static IAudioControl birdSelect;
        private static IAudioControl buttonSelect;
        private static IAudioControl backGround;


        public static void PlaySlingAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (slingShot == null)
                {
                    slingShot = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (slingShot.SetAudioMixerGroup(group))
                        {
                            slingShot.SetLoop(true);
                            (slingShot as Audio2D).IsAutoRecycle = false;
                            Debug.Log("弹弓拉伸音效配置成功");
                        }
                    }
                }
                if (slingShot.SetAudioClip(audioClip))
                {
                    slingShot.Play();
                }
            }
        }

        public static void PauseSlingAudio()
        {
            if (slingShot == null)
            {
                return;
            }
            slingShot.Pause();
        }
        /// <summary>
        /// 播放小鸟选择音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayBirdAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (birdSelect == null)
                {
                    birdSelect = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (birdSelect.SetAudioMixerGroup(group))
                        {
                            (birdSelect as Audio2D).IsAutoRecycle = false;
                            Debug.Log("小鸟选择音效配置成功");
                        }
                    }
                }
                if (birdSelect.SetAudioClip(audioClip))
                {
                    birdSelect.Play();
                }
            }

        }
        /// <summary>
        /// 播放小按钮选择音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayButtonSelectAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (buttonSelect == null)
                {
                    buttonSelect = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (buttonSelect.SetAudioMixerGroup(group))
                        {
                            (buttonSelect as Audio2D).IsAutoRecycle = false;
                            Debug.Log("小鸟选择音效配置成功");
                        }
                    }
                }
                if (buttonSelect.SetAudioClip(audioClip))
                {
                    buttonSelect.Play();
                }
            }
        }
        /// <summary>
        /// 播放背景音效
        /// </summary>
        /// <param name="audioPath"></param>
        public static void PlayBackGroundAudio(string audioPath)
        {
            if (AudioClipMgr.GetAudioClip(audioPath, out AudioClip audioClip))
            {
                if (backGround == null)
                {
                    backGround = MonoSingletonFactory<Audio2DMgr>.GetSingleton().ApplyForAudioControl();
                    if (AudioMixerMgr.GetAudioMixerGroup("Effect", out AudioMixerGroup group))
                    {
                        if (backGround.SetAudioMixerGroup(group))
                        {
                            (backGround as Audio2D).IsAutoRecycle = false;
                            backGround.SetLoop(true);
                            Debug.Log("小鸟选择音效配置成功");
                        }
                    }
                }
                if (backGround.SetAudioClip(audioClip))
                {
                    backGround.Play();
                }
            }
        }
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
