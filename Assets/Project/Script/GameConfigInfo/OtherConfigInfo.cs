using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class OtherConfigInfo:SingletonBase<OtherConfigInfo>
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
        /// 关卡胜利音效路径
        /// </summary>
        protected string m_LevelWinAudioPath;
        /// <summary>
        /// 关卡失败音效路径
        /// </summary>
        protected string m_LevelLoseAudioPath;
        /// <summary>
        /// 关卡开始鸟的音效路径
        /// </summary>
        protected string m_LevelStartBirdAudioPath;
        /// <summary>
        /// 关卡开始猪的音效路径
        /// </summary>
        protected string m_LevelStartPigAudioPath;
        /// <summary>
        /// 关卡类型面板音效路径
        /// </summary>
        protected string m_LevelTypePanelAudioPath;
        /// <summary>
        /// 弹弓音效路径
        /// </summary>
        protected string m_SlingShotAudioPath;
        /// <summary>
        /// 按钮音效路径
        /// </summary>
        protected string m_ButtonAudioPath;
        /// <summary>
        /// 星星音效路径数组
        /// </summary>
        protected string[] m_StarAudioPaths;
        #endregion
        #region Prefab

        #region 暂时先放这里获取
        /// <summary>
        /// 法杖预制路径
        /// </summary>
        protected string m_StaffPrefabPath;
        #endregion

        /// <summary>
        /// 关卡类型预制路径
        /// </summary>
        protected string m_LevelTypePrefabPath;
        /// <summary>
        /// 2D相机预制路径
        /// </summary>
        protected string m_Camera2DPrefabPath;
        /// <summary>
        /// 飞行路径预制路径
        /// </summary>
        protected string m_FlyPathPrefabPath;
        /// <summary>
        /// 弹弓预制路径
        /// </summary>
        protected string m_SlingShotPrefabPath;
        /// <summary>
        /// 关卡预制路径
        /// </summary>
        protected string m_LevelPrefabPath;
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
        #region 精灵
        /// <summary>
        /// 关卡类型界面背景精灵路径
        /// </summary>
        protected string m_LevelTypeInterfaceBGSpritePath;
        #endregion
        public void InitConfigInfo()
        {
            m_CommonPath = GetType().Name + "/";
            m_StaffPrefabPath = m_CommonPath + "Staff";
            m_LevelTypePrefabPath = m_CommonPath+ "LevelType";
            m_Camera2DPrefabPath = m_CommonPath+ "Camera2D";
            m_FlyPathPrefabPath = m_CommonPath+"FlyPath";
            m_SlingShotPrefabPath = m_CommonPath+"SlingShot";
            m_SlingShotAudioPath = m_CommonPath+ "SlingShotAudio";
            m_LevelPrefabPath = m_CommonPath+ "Level";
            m_ButtonAudioPath = m_CommonPath+"Button";
            m_BlisterPrefabPath = m_CommonPath+ "Blister";
            m_ScorePrefabPath = m_CommonPath+ "Score";
            m_BoomPrefabPath = m_CommonPath+ "Boom";
            m_EggPrefabPath = m_CommonPath+ "Egg";
            m_PointPrefabPath = m_CommonPath+ "Point";
            m_LevelTypePanelAudioPath = m_CommonPath+"BGM";
            m_LevelTypeInterfaceBGSpritePath = m_CommonPath+ "BG";
            m_LevelWinAudioPath = m_CommonPath+"LevelWin";
            m_LevelLoseAudioPath = m_CommonPath+ "LevelLose";
            m_LevelStartBirdAudioPath = m_CommonPath+ "LevelStartBird";
            m_LevelStartPigAudioPath = m_CommonPath+ "LevelStartPig";
            m_StarAudioPaths = new string[] { "Star1", "Star2", "Star3" };
            //层级顺序  分数>Boom>蛋>点
            m_PointOrderInLayer = -1;
            m_EggOrderInLayer = 1;
            m_BoomOrderInLayer = 20;
            m_ScoreOrderInLayer = 40;

            #region 待
            
            
            #endregion
        }

        public string GetStaffPrefabPath()
        {
            return m_StaffPrefabPath;
        }

        public string GetLevelStartPigAudioPath()
        {
            return m_LevelStartPigAudioPath;
        }
        
        //public string GetLevelStartBirdAudioPath()
        //{
        //    return m_CommonPath+m_LevelStartBirdAudioPath;
        //}

        public string GetLevelLoseAudioPath()
        {
            return m_LevelLoseAudioPath;
        }

        public string GetLevelWinAudioPath()
        {
            return  m_LevelWinAudioPath;
        }

        public string GetLevelTypePanelAudioPath()
        {
            return m_LevelTypePanelAudioPath;
        }

        public string GetLevelTypeInterfaceBGSpritePath()
        {
            return m_LevelTypeInterfaceBGSpritePath;
        }
        public string GetLevelTypePrefabPath()
        {
            return  m_LevelTypePrefabPath;
        }

        public string GetCamera2DPrefabPath()
        {
            return m_Camera2DPrefabPath;
        }
        public string GetFlyPathPrefabPath()
        {
            return  m_FlyPathPrefabPath;
        }
        public string GetSlingShotPrefabPath()
        {
            return  m_SlingShotPrefabPath;
        }
        public string GetSlingShotAudioPath()
        {
            return   m_SlingShotAudioPath;
        }
        public string GetLevelPrefabPath()
        {
            return   m_LevelPrefabPath;
        }
        public string GetButtonAudioPath()
        {
            return  m_ButtonAudioPath;
        }
        public string GetStarAudioPath(int index)
        {
            return m_CommonPath + m_StarAudioPaths[Mathf.Clamp(index, 0, m_StarAudioPaths.Length - 1)];
        }
        public string GetBlisterPrefabPath()
        {
            return  m_BlisterPrefabPath;
        }
        public string GetBoomPrefabPath()
        {
            return m_BoomPrefabPath;
        }
        public string GetEggPrefabPath()
        {
            return  m_EggPrefabPath;
        }
        public string GetScorePrefabPath()
        {
            return  m_ScorePrefabPath;
        }
        public string GetPointPrefabPath()
        {
            return  m_PointPrefabPath;
        }
    }
}
