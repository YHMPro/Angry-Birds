﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 游戏管理器
    /// </summary>
    public class GameManager 
    {       
        /// <summary>
        /// 当前场景中猪的数量
        /// </summary>
        public static int NowScenePigNum
        {
            get
            {
                return m_Pigs.Count;
            }
        }
        /// <summary>
        /// 当前场景中鸟的数量
        /// </summary>
        public static int NowSceneBirdNum
        {
            get
            {
                return m_Birds.Count;
            }
        }
        /// <summary>
        /// 猪列表
        /// </summary>
        private static List<Pig> m_Pigs=new List<Pig>();
        /// <summary>
        /// 鸟列表
        /// </summary>
        private static List<Bird>m_Birds=new List<Bird>();       
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            
        }
        /// <summary>
        /// 清除
        /// </summary>
        public static void Clear()
        {
            m_Pigs = new List<Pig>();  
            m_Birds = new List<Bird>();
        }
        

        #region GameTarget
        /// <summary>
        /// 添加猪
        /// </summary>
        /// <param name="pig">猪实例</param>
        public static void AddPig(Pig pig)
        {
            if(m_Pigs.Contains(pig))
            {
                return;
            }
            m_Pigs.Add(pig);
        }
        /// <summary>
        /// 移除猪
        /// </summary>
        /// <param name="pig">猪实例</param>
        public static void RemovePig(Pig pig)
        {
            if (m_Pigs.Contains(pig))
            {
                m_Pigs.Remove(pig);
                Debuger.Log("当前场景内猪的数量:" + NowScenePigNum);
                MesgManager.MesgTirgger(ProjectEvents.LogicUpdateEvent);
            }
        }
        /// <summary>
        /// 添加鸟
        /// </summary>
        /// <param name="bird">鸟实例</param>
        public static void AddBird(Bird bird)
        {
            if(m_Birds.Contains(bird))
            {
                return;
            }
            m_Birds.Add(bird);
        }
        /// <summary>
        /// 移除鸟
        /// </summary>
        /// <param name="bird">鸟实例</param>
        public static void RemoveBird(Bird bird)
        {
            if(m_Birds.Contains(bird))
            {
                m_Birds.Remove(bird);
                Debuger.Log("当前场景内鸟的数量:" + NowSceneBirdNum);
                MesgManager.MesgTirgger(ProjectEvents.LogicUpdateEvent);
            }
        }
        #endregion
   
        #region GameOver
        /// <summary>
        /// 游戏结束
        /// </summary>
        /// <param name="isWin">是否胜利</param>
        public static void GameOver(bool isWin)
        {
            
        }
        #endregion

        #region GameStart
        /// <summary>
        /// 游戏开始
        /// </summary>
        public static void GameStart()
        {
            Debuger.Log("游戏开始");
        }
        #endregion

        #region GameStop
        /// <summary>
        /// 游戏暂停
        /// </summary>
        public static void GameStop()
        {
            Debuger.Log("游戏暂停");
        }
        #endregion

        #region GameRestart
        /// <summary>
        /// 重新开始
        /// </summary>
        public static void GameRestart()
        {
            Debuger.Log("游戏重新开始");
        }
        #endregion
    }
}
