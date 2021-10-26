using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 可碰撞
    /// </summary>
    public abstract class AbleCollision : AbleDestroyed
    {
        protected AbleCollision() { }
        /// <summary>
        /// 碰撞音效数组
        /// </summary>
        protected string[] m_CollisionAudioPaths = null;
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
    }
}

