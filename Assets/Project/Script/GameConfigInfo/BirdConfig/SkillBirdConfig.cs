using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 技能小鸟配置
    /// </summary>
    public abstract class SkillBirdConfig : BirdConfig
    {
        protected SkillBirdConfig() { }
        /// <summary>
        /// 技能音效路径数组
        /// </summary>
        protected string[] m_SkillAudioPaths = null;
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

    }

    public class VanBirdConfig : SkillBirdConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_IsInit = true;

                m_SelfResPath = m_Tag + "/VanBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BVFly1",
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BVSelect1",
                };
                m_SkillAudioPaths = new string[] {
                    m_Tag+"/BVSkill1"
                };
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/BVCo1",
                    m_Tag+"/BVCo2",
                    m_Tag+"/BVCo3",
                    m_Tag+"/BVCo4"
                };
                return true;
            }
            return false;
        }
    }
    public class WhiteBirdConfig : SkillBirdConfig
    {
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
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_IsInit = true;
                m_EggPath = m_Tag + "/Egg";
                m_SelfResPath = m_Tag + "/WriteBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BWFly1",
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BWSelect1",
                };
                m_SkillAudioPaths = new string[] {
                    m_Tag+"/BWSkill1"
                };
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/BWCo1",
                    m_Tag+"/BWCo2",
                    m_Tag+"/BWCo3",
                    m_Tag+"/BWCo4",
                    m_Tag+"/BWCo5"
                };
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 蓝色小鸟配置
    /// </summary>
    public class BlueBirdConfig : SkillBirdConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_IsInit = true;

                m_SelfResPath = m_Tag + "/BlueBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BBFly1",
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BBSelect1",
                };               
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 粉色小鸟配置
    /// </summary>
    public class PinkBirdConfig : SkillBirdConfig
    {
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
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_IsInit = true;

                m_SelfResPath = m_Tag + "/PinkBird";
                m_BlisterPath = m_Tag + "/Blister";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BPFly1",
                    m_Tag+"/BPFly2",
                    m_Tag+"/BPFly3"
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BPSelect1",
                    m_Tag+"/BPSelect2",
                    m_Tag+"/BPSelect3"
                };
                m_SkillAudioPaths = new string[] {
                    m_Tag+"/BPSkill1",
                    m_Tag+"/BPSkill2",
                    m_Tag+"/BPSkill3"
                };
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/BPCo1",
                    m_Tag+"/BPCo2",
                    m_Tag+"/BPCo3",
                    m_Tag+"/BPCo4",
                    m_Tag+"/BPCo5"
                };
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 黑色小鸟配置
    /// </summary>
    public class BlackBirdConfig :SkillBirdConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_IsInit = true;

                m_SelfResPath = m_Tag + "/BlackBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BBFly1"
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BBSelect1"
                };
                m_SkillAudioPaths = new string[] {
                    m_Tag+"/BBSkill1"
                };
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/BBCo1",
                    m_Tag+"/BBCo2",
                    m_Tag+"/BBCo3",
                    m_Tag+"/BBCo4"
                };
                return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 绿色小鸟配置
    /// </summary>
    public class GreenBirdConfig:SkillBirdConfig
    {
        public override bool InitResourcesPath()
        {
            if (base.InitResourcesPath())
            {
                m_IsInit = true;

                m_SelfResPath = m_Tag + "/GreenBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BGFly1"
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BGSelect1"
                };
                m_SkillAudioPaths = new string[] {
                    m_Tag+"/BGSkill1"
                };
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/BGCo1",
                    m_Tag+"/BGCo2",
                    m_Tag+"/BGCo3",
                    m_Tag+"/BGCo4",
                    m_Tag+"/BGCo5"
                };
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 黄色小鸟配置
    /// </summary>
    public class YellowBirdConfig: SkillBirdConfig
    {
        public override bool InitResourcesPath()
        {
            if(base.InitResourcesPath())
            {
                m_IsInit = true;

                m_SelfResPath = m_Tag + "/YellowBird";
                m_FlyAudioPaths = new string[] {
                    m_Tag+"/BYFly1"
                };
                m_SelectAudioPaths = new string[] {
                    m_Tag+"/BYSelect1"
                };
                m_SkillAudioPaths = new string[] {
                    m_Tag+"/BYSkill1"
                };
                m_CollisionAudioPaths = new string[] {
                    m_Tag+"/BYCo1",
                    m_Tag+"/BYCo2",
                    m_Tag+"/BYCo3",
                    m_Tag+"/BYCo4",
                    m_Tag+"/BYCo5"
                };
                return true;
            }
            return false;
        }
    }


}
