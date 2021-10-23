using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Angry_Birds
{
    public abstract class BirdConfigInfo 
    {
        #region Resources配置信息
        #region String     
        /// <summary>
        /// 飞行音效路径数组
        /// </summary>
        protected virtual string[] FlyAudioPaths
        {
            get { return new string[] { "" }; }
        }
        /// <summary>
        /// 碰撞音效路径数组
        /// </summary>
        protected virtual string[] CrashAudioPaths
        {
            get { return new string[] { "" }; }
        }
        /// <summary>
        /// 选中音效路径数组
        /// </summary>
        protected virtual string[] SelectAudioPaths
        {
            get { return new string[] { "" }; }
        }
        /// <summary>
        /// 技能音效路径数组
        /// </summary>
        protected virtual string[] SkillAudioPaths
        {
            get { return new string[] { "" }; }
        }
        /// <summary>
        /// 死亡音效路径数组
        /// </summary>
        protected virtual string[] DiedAudioPaths
        {
            get { return new string[] { "Prefabs/Bird/BDe1" }; }
        }
        #endregion
        #region Float
        /// <summary>
        /// 质量
        /// </summary>
        public virtual float Mass
        {
            get
            {
                return 0;
            }
        }
        #endregion
        #endregion

        #region AB包配置信息

        #endregion

        /// <summary>
        /// 获取选择音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns></returns>
        public virtual string GetSelectAudioPath(bool isRandom = true,int index=0)
        {
            index = Mathf.Clamp(index, 0, SelectAudioPaths.Length-1);
            if(isRandom)
            {
                System.Random random = new System.Random();
                return SelectAudioPaths[random.Next(0, SelectAudioPaths.Length - 1)];
            }
            return SelectAudioPaths[index];
        }
        /// <summary>
        /// 获取飞行音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns></returns>
        public virtual string GetFlyAudioPath(bool isRandom = true,int index = 0)
        {
            index = Mathf.Clamp(index, 0, FlyAudioPaths.Length - 1);
            if (isRandom)
            {
                System.Random random = new System.Random();
                return FlyAudioPaths[random.Next(0, FlyAudioPaths.Length - 1)];
            }
            return FlyAudioPaths[index];
        }
        /// <summary>
        /// 获取死亡音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns></returns>
        public virtual string GetDiedAudioPath(bool isRandom = true,int index = 0)
        {
            index = Mathf.Clamp(index, 0, DiedAudioPaths.Length - 1);
            if (isRandom)
            {
                System.Random random = new System.Random();
                return DiedAudioPaths[random.Next(0, DiedAudioPaths.Length - 1)];
            }
            return DiedAudioPaths[index];
        }
        /// <summary>
        /// 获取碰撞音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns></returns>
        public virtual string GetCrashAudioPath(bool isRandom = true, int index = 0)
        {
            index = Mathf.Clamp(index, 0, CrashAudioPaths.Length - 1);
            if (isRandom)
            {
                System.Random random = new System.Random();
                return CrashAudioPaths[random.Next(0, CrashAudioPaths.Length - 1)];
            }
            return CrashAudioPaths[index];
        }
        /// <summary>
        /// 获取碰撞音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns></returns>
        public virtual string GetSkillAudioPath(bool isRandom = true, int index = 0)
        {
            index = Mathf.Clamp(index, 0, SkillAudioPaths.Length - 1);
            if (isRandom)
            {
                System.Random random = new System.Random();
                return SkillAudioPaths[random.Next(0, SkillAudioPaths.Length - 1)];
            }
            return SkillAudioPaths[index];
        }
    }
    public class RedBirdConfigInfo : BirdConfigInfo
    {
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
        protected override string[] SelectAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/RedBird/Audio/BRSelect1" };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/RedBird/Audio/BRFly1" };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/RedBird/Audio/BRCo1" };
            }
        }
    }
    public class YellowBirdConfigInfo : BirdConfigInfo
    {
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
        protected override string[] SelectAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYSelect1"
                };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYFly1"
                };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYCo1",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo2",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo3",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo4",
                    "Prefabs/Bird/BirdYellow/Audio/BYCo5",
                };
            }
        }

        protected override string[] SkillAudioPaths
        {
            get
            {
                return new string[] {
                    "Prefabs/Bird/BirdYellow/Audio/BYSkill1"
                };
            }
        }
    }

    public class GreenBirdConfigInfo :BirdConfigInfo
    {
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
        protected override string[] SelectAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdGreen/Audio/BGSelect1" };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdGreen/Audio/BGFly1" };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdGreen/Audio/BGCo1",
                    "Prefabs/Bird/BirdGreen/Audio/BGCo2",
                    "Prefabs/Bird/BirdGreen/Audio/BGCo3",
                    "Prefabs/Bird/BirdGreen/Audio/BGCo4"
                };
            }
        }
    }
    public class BlackBirdConfigInfo : BirdConfigInfo
    {
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
        protected override string[] SkillAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdBlack/Audio/BBBlast1" };
            }
        }
        protected override string[] SelectAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdBlack/Audio/BBSelect1" };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdBlack/Audio/BBFly1" };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdBlack/Audio/BBCo1",
                    "Prefabs/Bird/BirdBlack/Audio/BBCo2",
                    "Prefabs/Bird/BirdBlack/Audio/BBCo3",
                    "Prefabs/Bird/BirdBlack/Audio/BBCo4"
                };
            }
        }
    }
    public class PinkBirdConfigInfo : BirdConfigInfo
    {
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
        protected override string[] SkillAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdPink/Audio/BPSkill1",
                    "Prefabs/Bird/BirdPink/Audio/BPSkill2",
                    "Prefabs/Bird/BirdPink/Audio/BPSkill3"
                };
            }
        }
        protected override string[] SelectAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdPink/Audio/BPSelect1",
                    "Prefabs/Bird/BirdPink/Audio/BPSelect2",
                    "Prefabs/Bird/BirdPink/Audio/BPSelect3"
                };
            }
        }
        protected override string[] FlyAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdPink/Audio/BPFly1",
                    "Prefabs/Bird/BirdPink/Audio/BPFly2",
                    "Prefabs/Bird/BirdPink/Audio/BPFly3"
                };
            }
        }
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Bird/BirdPink/Audio/BPCo1",
                    "Prefabs/Bird/BirdPink/Audio/BPCo2",
                    "Prefabs/Bird/BirdPink/Audio/BPCo3",
                    "Prefabs/Bird/BirdPink/Audio/BPCo4",
                    "Prefabs/Bird/BirdPink/Audio/BPCo5"
                };
            }
        }
    }
}
