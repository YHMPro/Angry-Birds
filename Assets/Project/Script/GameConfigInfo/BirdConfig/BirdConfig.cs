using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 小鸟配置
    /// </summary>
    public abstract class BirdConfig :AbleCollision
    {
        protected BirdConfig() { }
        
        /// <summary>
        /// 飞行音效路径数组
        /// </summary>
        protected string[] m_FlyAudioPaths = null;
        /// <summary>
        /// 选中音效路径数组
        /// </summary>
        protected string[] m_SelectAudioPaths = null;
        public override bool InitResources()
        {
            if(base.InitResources())
            {             
                m_Common = "BirdCommon";             
                m_DestroyAudioPaths = new string[] { m_Common+ "/BDe1" };
                return true;
            }
            return false;
        }

        public override bool InitAB()
        {
            if(base.InitAB())
            {
                m_Common = "BirdCommon".ToLower();
                m_DestroyAudioPaths = new string[] { "BDe1" };
                return true;
            }
            return false;
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
    }
    /// <summary>
    /// 红色小鸟配置
    /// </summary>
    public class RedBirdConfig : BirdConfig
    {
        public override bool InitResources()
        {
            if(base.InitResources())
            {
                m_SelfResPath = m_Tag + "/RedBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BRFly1"
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BRSelect1"
                };
            }
            return false;
        }

    }
}
