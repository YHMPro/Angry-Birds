using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 猪配置
    /// </summary>
    public abstract class PigConfig : AbleCollision
    {
        protected PigConfig() { }
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_Common = "PigCommon";                
                m_DestroyAudioPaths = new string[] { m_Common + "/PDe1" };
                return true;
            }
            return false;
        }
    }
    public class Pig3Config : PigConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_SelfResPath = m_Tag + "/Pig3";
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/P3Hurt1",
                    m_Tag+"/P3Hurt2"
                };
                return true;
            }
            return false;
        }
    }
    public class Pig2Config : PigConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_SelfResPath = m_Tag + "/Pig2";
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/P2Hurt1",
                    m_Tag+"/P2Hurt2"
                };
                return true;
            }
            return false;
        }
    }
    public class Pig1Config: PigConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_SelfResPath = m_Tag + "/Pig1";
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/P1Hurt1",
                    m_Tag+"/P1Hurt2"
                };
                return true;
            }
            return false;
        }
    }

}
