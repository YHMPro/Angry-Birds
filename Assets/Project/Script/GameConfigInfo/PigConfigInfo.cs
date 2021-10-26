using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class PigConfigInfo
    {
        #region Resources配置信息
        #region String         
        /// <summary>
        /// 碰撞音效路径数组
        /// </summary>
        protected virtual string[] CrashAudioPaths
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
            get { return new string[] { "Prefabs/Pig/PDe1" }; }
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
        /// 获取死亡音效路径
        /// </summary>
        /// <param name="isRandom">是否随机索引</param>
        /// <param name="index">自定义索引</param>
        /// <returns></returns>
        public virtual string GetDiedAudioPath(bool isRandom = true, int index = 0)
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

    public class Pig1ConfigInfo:PigConfigInfo
    {
        protected override string[] CrashAudioPaths
        {
            get
            {
                return new string[] { "Prefabs/Pig/Pig1/Audio/P1Hurt1",
                    "Prefabs/Pig/Pig1/Audio/P1Hurt2"
                };
            }
        }
        public override float Mass
        {
            get
            {
                return 1;
            }
        }
    }

}
