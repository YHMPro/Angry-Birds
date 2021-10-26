﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 可销毁
    /// </summary>
    public abstract class AbleDestroyed : BaseConfig
    {
        protected AbleDestroyed() { }

        /// <summary>
        /// Boom路径
        /// </summary>
        private static string m_BoomPath = "Common/Boom";
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
        /// <summary>
        /// 销毁音效路径数组
        /// </summary>
        protected string[] m_DestroyAudioPaths = null;          
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
