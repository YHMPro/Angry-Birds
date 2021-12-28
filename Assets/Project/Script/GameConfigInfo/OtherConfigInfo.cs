using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class OtherConfigInfo
    {
        private string m_CommonPath;
        /// <summary>
        /// 小鸟气球预制路径
        /// </summary>
        protected string m_BlisterPrefabPath;
        /// <summary>
        /// Boom预制路径
        /// </summary>
        protected string m_BoomPrefabPath;
        /// <summary>
        /// 小鸟蛋预制路径
        /// </summary>
        protected string m_EggPrefabPath;
        /// <summary>
        /// 点预制路径
        /// </summary>
        protected string m_PointPrefabPath;
        /// <summary>
        /// 分数预制路径
        /// </summary>
        protected string m_ScorePrefabPath;
        public void InitConfigInfo()
        {
            m_CommonPath = GetType().Name + "/";
            m_BlisterPrefabPath = "Blister";
            m_ScorePrefabPath = "Score";
            m_BoomPrefabPath = "Boom";
            m_EggPrefabPath = "Egg";
            m_PointPrefabPath = "Point";

            Debug.Log(GetPointPrefabPath());
        }
        public string GetBlisterPrefabPath()
        {
            return m_CommonPath + m_BlisterPrefabPath;
        }
        public string GetBoomPrefabPath()
        {
            return m_CommonPath+m_BoomPrefabPath;
        }
        public string GetEggPrefabPath()
        {
            return m_CommonPath + m_EggPrefabPath;
        }

        public string GetScorePrefabPath()
        {
            return m_CommonPath + m_ScorePrefabPath;
        }
        public string GetPointPrefabPath()
        {
            return m_CommonPath + m_PointPrefabPath;
        }
    }
}
