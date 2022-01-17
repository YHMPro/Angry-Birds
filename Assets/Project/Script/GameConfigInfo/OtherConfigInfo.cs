using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class OtherConfigInfo
    {
        private string m_CommonPath;
        #region 渲染层级
        /// <summary>
        /// 气球层级
        /// </summary>
        protected int m_BlisterOrderInLayer = 0;
        /// <summary>
        /// 气球层级
        /// </summary>
        public int BlisterOrderInLayer
        {
            get
            {
                return m_BlisterOrderInLayer;
            }
        }
        /// <summary>
        /// Boom层级(最低)
        /// </summary>
        protected int m_BoomOrderInLayer = 0;
        /// <summary>
        /// Boom层级(最低)
        /// </summary>
        public int BoomOrderInLayer
        {
            get { return m_BoomOrderInLayer; }
        }
        /// <summary>
        /// 蛋层级
        /// </summary>
        protected int m_EggOrderInLayer = 0;
        /// <summary>
        /// 蛋层级
        /// </summary>
        public int EggOrderInLayer
        {
            get { return m_EggOrderInLayer; }
        }
        /// <summary>
        /// 点层级
        /// </summary>
        protected int m_PointOrderInLayer = 0;
        /// <summary>
        /// 点层级
        /// </summary>
        public int PointOrderInLayer
        {
            get { return m_PointOrderInLayer; }
        }
        /// <summary>
        /// 分数层级(最低)
        /// </summary>
        protected int m_ScoreOrderInLayer = 0;
        /// <summary>
        /// 分数层级(最低)
        /// </summary>
        public int ScoreOrderInLayer
        {
            get
            {
                return m_ScoreOrderInLayer;
            }
        }
        #endregion
        #region Audio
        /// <summary>
        /// 星星音效路径数组
        /// </summary>
        protected string[] m_StarAudioPaths;
        #endregion
        #region Prefab
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
        #endregion
        public void InitConfigInfo()
        {
            m_CommonPath = GetType().Name + "/";
            m_BlisterPrefabPath = "Blister";
            m_ScorePrefabPath = "Score";
            m_BoomPrefabPath = "Boom";
            m_EggPrefabPath = "Egg";
            m_PointPrefabPath = "Point";
            m_StarAudioPaths = new string[] { "Star1", "Star2", "Star3" };
            //层级顺序  分数>Boom>蛋>点
            m_PointOrderInLayer = -1;
            m_EggOrderInLayer = 1;
            m_BoomOrderInLayer = 20;
            m_ScoreOrderInLayer = 40;
        }

        public string GetStarAudioPath(int index)
        {
            return m_CommonPath + m_StarAudioPaths[Mathf.Clamp(index, 0, m_StarAudioPaths.Length - 1)];
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
