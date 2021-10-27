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
        
        
        public override bool InitResources()
        {
            if(base.InitResources())
            {
                m_IsInit = true;

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
                m_IsInit = true;

                m_Common = "BirdCommon".ToLower();
                m_DestroyAudioPaths = new string[] { "BDe1" };
                return true;
            }
            return false;
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
                m_IsInit = true;

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
