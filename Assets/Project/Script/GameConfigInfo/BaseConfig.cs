﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 配置基类
    /// </summary>
    public class BaseConfig
    {
        protected BaseConfig() { }
        /// <summary>
        /// 是否初始化
        /// </summary>
        protected bool m_IsInit = false;
        /// <summary>
        /// 是否初始化
        /// </summary>
        public bool IsInit
        {
            get { return m_IsInit; }
        }
        /// <summary>
        /// 飞行音效路径数组
        /// </summary>
        protected string[] m_FlyAudioPaths = null;
        /// <summary>
        /// 选中音效路径数组
        /// </summary>
        protected string[] m_SelectAudioPaths = null;
        /// <summary>
        /// 蛋路径
        /// </summary>
        protected string m_EggPath = "";
        /// <summary>
        /// 蛋路径
        /// </summary>
        public string EggPath
        {
            get
            {
                return m_EggPath;
            }
        }
        /// <summary>
        /// 弹弓音效
        /// </summary>
        protected string m_SlingShotAudioPath = null;
        /// <summary>
        /// 弹弓音效
        /// </summary>
        public string SlingShotAudioPath
        {
            get
            {
                return m_SlingShotAudioPath;
            }
        }
        /// <summary>
        /// 点路径
        /// </summary>
        protected string m_PointPath = "";
        /// <summary>
        /// 点路径
        /// </summary>
        public string PointPath
        {
            get
            {
                return m_PointPath;
            }
        }
        /// <summary>
        /// 气泡路径
        /// </summary>
        protected string m_BlisterPath = null;
        /// <summary>
        /// 气泡路径
        /// </summary>
        public string BlisterPath
        {
            get
            {
                return m_BlisterPath;
            }
        }
        /// <summary>
        /// Boom路径
        /// </summary>
        protected static string m_BoomPath = null;
        /// <summary>
        /// Boom路径
        /// </summary>
        public string BoomPath
        {
            get
            {
                return m_BoomPath;
            }
        }
        protected string[] m_SkillAudioPaths = null;
        /// <summary>
        /// 销毁音效路径数组
        /// </summary>
        protected string[] m_DestroyAudioPaths = null;
        /// <summary>
        /// 碰撞音效数组
        /// </summary>
        protected string[] m_CollisionAudioPaths = null;
        /// <summary>
        /// 自身Res路径信息
        /// </summary>
        protected string m_SelfResPath = "";
        /// <summary>
        /// 自身Res路径信息
        /// </summary>
        public string SelfResPath
        {
            get
            {
                return m_SelfResPath;
            }
        }
        /// <summary>
        /// 公共    公共在Resources加载中作为为文件夹名   在AB包加载中作为包
        /// </summary>
        protected string m_Common = null;
        /// <summary>
        /// 标签   标签在Resources加载中作为为文件夹名   在AB包加载中作为包
        /// </summary>
        protected string m_Tag = null;      
        /// <summary>
        /// 初始化Resources资源  用于资源预热
        /// </summary>
        /// <returns>是否成功</returns>
        public virtual bool InitResources()
        {
            if (m_IsInit)
                return false;
            m_Tag = GetType().Name;            
            return true;
        }
        /// <summary>
        /// 初始化AB包           用于资源预热
        /// </summary>
        /// <returns>是否成功</returns>
        public virtual bool InitAB()
        {
            if (m_IsInit)
                return false;
            m_Tag = GetType().Name.ToLower();
            return true;
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="audioPaths">音效路径数组</param>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns>路径</returns>
        protected virtual string GetPath(string [] audioPaths, bool isRandom = true, int index = 0)
        {
            if(audioPaths==null)
            {
                return "";
            }
            if (audioPaths.Length == 1)
            {
                return audioPaths[0];
            }
            index = Mathf.Clamp(index, 0, audioPaths.Length - 1);
            if (isRandom)
            {
                return audioPaths[GetRandonNumber(0, audioPaths.Length - 1)];
            }
            return audioPaths[index];
        }
        /// <summary>
        /// 获取自定义范围内的随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int GetRandonNumber(int min,int max)
        {
            System.Random random = new System.Random();
            return random.Next(min, max);
        }
        /// <summary>
        /// 获取技能音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns>路径</returns>
        public virtual string GetSkillAudioPath(bool isRandom = true, int index = 0)
        {
            return GetPath(m_SkillAudioPaths, isRandom, index);
        }
        /// <summary>
        /// 获取飞行音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns>路径</returns>
        public virtual string GetFlyAudioPath(bool isRandom = true, int index = 0)
        {
            return GetPath(m_FlyAudioPaths, isRandom, index);
        }
        /// <summary>
        /// 获取飞行音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns>路径</returns>
        public virtual string GetSelectAudioPath(bool isRandom = true, int index = 0)
        {
            return GetPath(m_SelectAudioPaths, isRandom, index);
        }
        /// <summary>
        /// 获取碰撞音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns>路径</returns>
        public virtual string GetCollisionAudioPath(bool isRandom = true, int index = 0)
        {
            return GetPath(m_CollisionAudioPaths, isRandom, index);
        }
        /// <summary>
        /// 获取销毁音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns>路径</returns>
        public virtual string GetDestroyedAudioPath(bool isRandom = true, int index = 0)
        {
            return GetPath(m_DestroyAudioPaths, isRandom, index);
        }
    }
}
